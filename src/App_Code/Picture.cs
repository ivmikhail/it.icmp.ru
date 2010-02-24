using System;
using System.Web;
using System.Drawing;
using System.IO;
using System.Collections.Generic;
using System.Drawing.Imaging;

namespace ITCommunity {
	// страх божий
	public class Picture {

		#region Properties

		private string _name;
		private string _fullurl;
		private string _thumburl;
		private Post _post;

		public string Name {
			get { return _name; }
			set { _name = value; }
		}

		public string ThumbUrl {
			get { return _thumburl; }
			set { _thumburl = value; }
		}

		public string FullUrl {
			get { return _fullurl; }
			set { _fullurl = value; }
		}

		public Post Post {
			get { return _post; }
			set { _post = value; }
		}

		#endregion

		public Picture() {
			_post = new Post();
			_name = "";
			_fullurl = "";
			_thumburl = "";
		}

		public Picture(Post post, string name, bool isTempFolder) {
			_post = post;
			_name = name;
			int postId = -1;
			if (!isTempFolder) {
				postId = post.Id;
			}
			_fullurl = Config.String("PostImagesFolder") + "/" + post.Author.Id + "/" + postId + "/full/" + name;
			_thumburl = Config.String("PostImagesFolder") + "/" + post.Author.Id + "/" + postId + "/thumb/" + name;
		}

		public static List<Picture> GetByPost(Post post) {
			List<Picture> pics = new List<Picture>();

			int userId = post.Author.Id;
			string path = HttpContext.Current.Request.MapPath(Config.String("PostImagesFolder") + "/" + userId + "/" + post.Id + "/thumb");
			string[] files = new string[0];
			if (Directory.Exists(path)) {
				files = Directory.GetFiles(path);
			}
			foreach (string file in files) {
				pics.Add(new Picture(post, Path.GetFileName(file), false));
			}

			return pics;
		}

		public static string CreateFolder(int userId, int postId, string folder) {
			string currentFolder = HttpContext.Current.Request.MapPath(Config.String("PostImagesFolder") + "/" + userId + "/" + postId + "/" + folder);
			if (!Directory.Exists(currentFolder)) {
				Directory.CreateDirectory(currentFolder);
			}
			return currentFolder;
		}

		public static Picture UploadImage(HttpPostedFile img, Post post) {
			Random rand = new Random();
			string filename = rand.Next(0, 999999) + Path.GetExtension(img.FileName);
			filename = filename.ToLower();

			string fullpath = CreateFolder(post.Author.Id, -1, "full") + "/" + filename;
			string thumbpath = CreateFolder(post.Author.Id, -1, "thumb") + "/" + filename;

			Stream stream = img.InputStream;
			byte[] buffer = new byte[stream.Length];
			stream.Read(buffer, 0, (int)stream.Length);
			int len = (int)stream.Length;
			stream.Dispose();
			stream.Close();

			FileStream fs = new FileStream(fullpath, FileMode.Create);
			fs.Write(buffer, 0, len);
			Picture pic = new Picture();

			try {
				Bitmap bmp = new Bitmap(fs);
				if (fs.Length > Config.Num("PostImgSize") || bmp.Width > Config.Num("PostImgWidth") || bmp.Height > Config.Num("PostImgHeight")) {
					Logger.Log.Info("Пользователь(login - " + CurrentUser.User.Login + ") пытается загрузить картинку не подходящую по размерам");
					throw new Exception("Размеры картинки недопустимы");
				}
				pic = MakeThumbnail(bmp, filename, thumbpath, post);
				bmp.Dispose();
				fs.Dispose();
				fs.Close();
			}
			catch (Exception ex) {
				fs.Dispose();
				fs.Close();
				File.Delete(fullpath);
				Logger.Log.Info("Изображение загруженное пользователем(login - " + CurrentUser.User.Login + ") не сохранилась", ex);
			}

			return pic;
		}

		public static void DeleteTempFolderFiles(Post post) {
			//temp folder = -1
			DirectoryInfo dir = new DirectoryInfo(HttpContext.Current.Request.MapPath(Config.String("PostImagesFolder") + "/" + post.Author.Id + "/" + -1 + "/"));
			if (dir.Exists) {
				DeleteFiles(dir);
			}
		}

		public void Delete() {
			string fullPath = HttpContext.Current.Request.MapPath(this.FullUrl);
			string thumbPath = HttpContext.Current.Request.MapPath(this.ThumbUrl);
			File.Delete(fullPath);
			File.Delete(thumbPath);
		}

		private static Picture MakeThumbnail(Bitmap sourceBmp, string filename, string folder, Post post) {
			// Стандартный метод GetThumbnail генерирует изображения хренового качества
			int max = Config.Num("MaxThumbWidth");
			int height = sourceBmp.Height;
			int width = sourceBmp.Width;
			if (width > max) { //TODO: проверить на правильность
				float x;
				float y;
				if (width > height) {
					x = max;
					y = (float)max * (float)height / (float)width;
				}
				else {
					y = max;
					x = (float)width / (float)height * (float)max;
				}

				Bitmap thumb = new Bitmap(Convert.ToInt32(x), Convert.ToInt32(y));
				Graphics g = Graphics.FromImage(thumb);
				g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
				g.FillRectangle(Brushes.White, 0, 0, x, y);
				g.DrawImage(sourceBmp, 0, 0, x, y);

				ImageCodecInfo[] Info = ImageCodecInfo.GetImageEncoders();
				EncoderParameters Params = new EncoderParameters(1);
				Params.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 100L);
				thumb.Save(folder, Info[1], Params);

				g.Dispose();
				thumb.Dispose();
				Params.Dispose();
			}
			else {
				sourceBmp.Save(folder);
			}
			return new Picture(post, filename, true);
		}

		/// <summary>
		/// Данный метод вызывается после добавления поста, чтобы прибить изображения:
		///  - пихаем изображения из временной папки в нужную, 
		///  - изменяем новость(папка то изменилась), 
		///  - удаляем ненужные картинки
		/// </summary>
		/// <param name="newpost_id">Идентификатор новости</param>
		/// <param name="user_id">Кюррент юзер</param>
		public static void FixImages(Post post) {
			MergeTempAndPostFolder(post);
			// Изменяем ссылки на картинки в новости, так как новость получила свой айдишник + удаляем ненужные картинки
			CheckImgInNews(post);
		}

		private static void CheckImgInNews(Post post) {
			List<Picture> pics = Picture.GetByPost(post);
			string desc = post.Description;
			string text = post.Text;

			string fullOldVal = "";
			string fullNewVal = "";
			string thumbOldVal = "";
			string thumbNewVal = "";

			foreach (Picture pic in pics) {
				if (text.Contains(pic.Name) || desc.Contains(pic.Name)) {
					fullOldVal = Config.String("PostImagesFolder") + "/" + post.Author.Id + "/" + -1 + "/full/" + pic.Name;
					fullNewVal = Config.String("PostImagesFolder") + "/" + post.Author.Id + "/" + post.Id + "/full/" + pic.Name;

					thumbOldVal = Config.String("PostImagesFolder") + "/" + post.Author.Id + "/" + -1 + "/thumb/" + pic.Name;
					thumbNewVal = Config.String("PostImagesFolder") + "/" + post.Author.Id + "/" + post.Id + "/thumb/" + pic.Name;

					desc = desc.Replace(fullOldVal, fullNewVal);
					desc = desc.Replace(thumbOldVal, thumbNewVal);

					text = text.Replace(fullOldVal, fullNewVal);
					text = text.Replace(thumbOldVal, thumbNewVal);
				}
				else {
					pic.Delete();
				}
			}
			post.Description = desc;
			post.Text = text;
			post.Update();
		}

		private static void MergeTempAndPostFolder(Post post) {
			//temp folder = -1
			string path = Config.String("PostImagesFolder") + "/" + post.Author.Id + "/";
			string truepath = HttpContext.Current.Request.MapPath(path);

			if (Directory.Exists(truepath + -1)) {
				MergeFolders(truepath + -1, truepath + post.Id);
				DeleteTempFolderFiles(post);
			}
		}

		/// <summary>
		/// Удаляет все файлы внутри директории(включая в субдерикториях)
		/// </summary>
		/// <param name="dir">директория</param>
		private static void DeleteFiles(DirectoryInfo dir) {
			DirectoryInfo[] subDirectories = dir.GetDirectories();
			if (subDirectories.Length > 0) {
				for (int i = 0; i < subDirectories.Length; i++) {
					FileInfo[] files = subDirectories[i].GetFiles();
					foreach (FileInfo file in files) {
						file.Delete();
					}
					DeleteFiles(subDirectories[i]);
				}
			}
		}

		private static void MergeFolders(string source, string destination) {
			if (!Directory.Exists(destination)) {
				Directory.CreateDirectory(destination);
			}
			string[] files = Directory.GetFiles(source);
			foreach (string file in files) {
				string name = Path.GetFileName(file);
				string dest = Path.Combine(destination, name);
				File.Copy(file, dest);
			}
			string[] folders = Directory.GetDirectories(source);
			foreach (string folder in folders) {
				string name = Path.GetFileName(folder);
				string dest = Path.Combine(destination, name);
				MergeFolders(folder, dest);
			}
		}
	}
}
