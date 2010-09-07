using System;
using System.Web;
using System.Drawing;
using System.IO;
using System.Collections.Generic;
using System.Drawing.Imaging;
using ITCommunity.Core;
using System.Drawing.Drawing2D;

namespace ITCommunity {
    // страх божий
    public class Picture {

        public static int MaxThumbWidth {
            get { return Config.GetInt("MaxThumbWidth"); }
        }

        public static int MaxThumbHeight {
            get { return Config.GetInt("MaxThumbHeight"); }
        }

        public string Name { get; set; }
        public string BasePath { get; set; }

        public string ThumbUrl {
            get { return BasePath + "/thumb/" + Name; }
        }

        public string FullUrl {
            get { return BasePath + "/full/" + Name; }
        }

        public string ThumbPath {
            get { return ThumbDir(BasePath) + "/" + Name; }
        }

        public string FullPath {
            get { return FullDir(BasePath) + "/" + Name; }
        }

        public Picture(string basePath, string name) {
            BasePath = basePath;
            Name = name;
        }

        public void Delete() {
            File.Delete(FullPath);
            File.Delete(ThumbPath);
        }

        public static List<Picture> GetList(string basePath) {
            var pictures = new List<Picture>();
            var directory = FullDir(basePath);

            if (Directory.Exists(directory)) {
                var files = Directory.GetFiles(directory);
                foreach (string file in files) {
                    var info = new FileInfo(file);
                    pictures.Add(new Picture(info.Name, basePath));
                }
            }

            return pictures;
        }

        public static Picture Upload(HttpPostedFileBase image, string basePath) {
            var extension = Path.GetExtension(image.FileName).ToLower();
            var name = new Random().Next(0, 999999).ToString() + extension;

            var picture = new Picture(name, basePath);

            CreateDirectories(basePath);

            image.SaveAs(picture.FullPath);

            var fullBitmap = new Bitmap(picture.FullPath);
            int width = fullBitmap.Width;
            int height = fullBitmap.Height;

            if (width > MaxThumbWidth) {
                height = height * MaxThumbWidth / width;
            }

            if (height > MaxThumbHeight) {
                width = width * MaxThumbHeight / height;
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

        public static void Move(string srcPath, string dstPath) {
            var src = HttpContext.Current.Request.MapPath(srcPath);
            var dst = HttpContext.Current.Request.MapPath(dstPath);

            if (Directory.Exists(src)) {
                Directory.Move(src, dst);
            }
        }

        public static string ReplaceUrls(string srcPath, string dstPath, string data) {
            var pictures = GetList(srcPath);

            foreach (Picture picture in pictures) {
                if (data.Contains(picture.FullUrl) || data.Contains(picture.ThumbUrl)) {
                    var newPicture = new Picture(picture.Name, dstPath);

                    data = data.Replace(picture.FullUrl, newPicture.FullUrl);
                    data = data.Replace(picture.ThumbUrl, newPicture.ThumbUrl);
                } else {
                    picture.Delete();
                }
            }

            return data;
        }

        private static void CreateDirectories(string basePath) {
            var dir = HttpContext.Current.Request.MapPath(basePath);

            CreateDirectory(dir);
            CreateDirectory(ThumbDir(basePath));
            CreateDirectory(FullDir(basePath));
        }

        private static void CreateDirectory(string path) {
            if (Directory.Exists(path) == false) {
                Directory.CreateDirectory(path);
            }
        }

        private static string ThumbDir(string basePath) {
            return HttpContext.Current.Request.MapPath(basePath + "/thumb"); 
        }

        private static string FullDir(string basePath) {
            return HttpContext.Current.Request.MapPath(basePath + "/full"); 
        }

    }
}
