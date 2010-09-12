using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Web;


namespace ITCommunity.Core {

    public class Picture {

        public static int MaxThumbWidth {
            get { return Config.GetInt("MaxThumbWidth"); }
        }

        public static int MaxThumbHeight {
            get { return Config.GetInt("MaxThumbHeight"); }
        }

        public static int MaxSize {
            get { return Config.GetInt("PictureMaxSize"); }
        }

        public static string AllowedTypes {
            get { return Config.Get("PictureContentTypes"); }
        }

        public string Name { get; set; }
        public string BasePath { get; set; }

        public string ThumbUrl {
            get { return Config.SiteAddress + BasePath + "/thumb/" + Name; }
        }

        public string FullUrl {
            get { return Config.SiteAddress + BasePath + "/full/" + Name; }
        }

        public string ThumbPath {
            get { return GetThumbDir(BasePath) + "/" + Name; }
        }

        public string FullPath {
            get { return GetFullDir(BasePath) + "/" + Name; }
        }

        private Picture(string basePath, string name) {
            BasePath = basePath;
            Name = name;
        }

        public static void Clear(string basePath) {
            var pictures = GetList(basePath);

            foreach (var picture in pictures) {
                picture.Delete();
            }

            Directory.Delete(GetThumbDir(basePath));
            Directory.Delete(GetFullDir(basePath));
        }

        public static void DeleteUnused(string basePath, string data) {
            var pictures = GetList(basePath);

            foreach (var picture in pictures) {
                if (data.Contains(picture.FullUrl) == false && data.Contains(picture.ThumbUrl) == false) {
                    picture.Delete();
                }
            }
        }

        public static List<Picture> GetList(string basePath) {
            var pictures = new List<Picture>();
            var directory = GetFullDir(basePath);

            if (Directory.Exists(directory)) {
                var files = Directory.GetFiles(directory);
                foreach (string file in files) {
                    var info = new FileInfo(file);
                    pictures.Add(new Picture(basePath, info.Name));
                }
            }

            return pictures;
        }

        public static Picture Upload(HttpPostedFileBase image, string basePath) {
            var extension = Path.GetExtension(image.FileName).ToLower();
            var name = new Random().Next(0, 999999).ToString() + extension;

            var picture = new Picture(basePath, name);

            CreateDirectories(basePath);

            image.SaveAs(picture.FullPath);

            var fullBitmap = new Bitmap(picture.FullPath);
            int width = fullBitmap.Width;
            int height = fullBitmap.Height;

            if (width > MaxThumbWidth) {
                height = height * MaxThumbWidth / width;
                width = MaxThumbWidth;
            }

            if (height > MaxThumbHeight) {
                width = width * MaxThumbHeight / height;
                height = MaxThumbHeight;
            }

            var thumbBitmap = new Bitmap(width, height);
            var graphics = Graphics.FromImage(thumbBitmap);
            graphics.FillRectangle(Brushes.White, 0, 0, width, height);
            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphics.DrawImage(fullBitmap, 0, 0, width, height);

            var Info = ImageCodecInfo.GetImageEncoders();
            var Params = new EncoderParameters(1);
            Params.Param[0] = new EncoderParameter(Encoder.Quality, 100L);

            thumbBitmap.Save(picture.ThumbPath, Info[1], Params);

            graphics.Dispose();
            thumbBitmap.Dispose();
            Params.Dispose();
            fullBitmap.Dispose();

            return picture;
        }

        public static string ReplaceUrls(string srcPath, string dstPath, string data) {
            var pictures = GetList(srcPath);

            foreach (Picture picture in pictures) {
                if (data.Contains(picture.FullUrl) || data.Contains(picture.ThumbUrl)) {
                    var newPicture = new Picture(dstPath, picture.Name);

                    data = data.Replace(picture.FullUrl, newPicture.FullUrl);
                    data = data.Replace(picture.ThumbUrl, newPicture.ThumbUrl);

                    picture.Move(newPicture.BasePath);
                }
            }

            return data;
        }

        private static void CreateDirectories(string basePath) {
            CreateDirectory(GetThumbDir(basePath));
            CreateDirectory(GetFullDir(basePath));
        }

        private static void CreateDirectory(string path) {
            if (Directory.Exists(path) == false) {
                Directory.CreateDirectory(path);
            }
        }

        private static string GetThumbDir(string basePath) {
            return HttpContext.Current.Request.MapPath(HttpContext.Current.Request.ApplicationPath + basePath + "/thumb");
        }

        private static string GetFullDir(string basePath) {
            return HttpContext.Current.Request.MapPath(HttpContext.Current.Request.ApplicationPath + basePath + "/full");
        }

        private void Delete() {
            if (File.Exists(FullPath)) {
                File.Delete(FullPath);
            }
            if (File.Exists(ThumbPath)) {
                File.Delete(ThumbPath);
            }
        }

        private void Move(string dstPath) {
            var dst = HttpContext.Current.Request.MapPath(HttpContext.Current.Request.ApplicationPath + dstPath);
            var newPicture = new Picture(dstPath, Name);

            if (Directory.Exists(dst) == false) {
                CreateDirectories(dstPath);
            }

            if (File.Exists(FullPath)) {
                File.Move(FullPath, newPicture.FullPath);
            }
            if (File.Exists(ThumbPath)) {
                File.Move(ThumbPath, newPicture.ThumbPath);
            }
        }
    }
}
