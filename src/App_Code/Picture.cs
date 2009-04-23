using System;
using System.Data;
using System.Web;
using System.Drawing;
using System.IO;
using System.Collections.Generic;
using System.Drawing.Imaging;

public class Picture
{
    private string _name;
    private string _fullurl;
    private string _thumburl;
    private int _postId;

    public string Name
    {
        get
        {
            return _name;
        }
        set
        {
            _name = value;
        }
    }

    public string ThumbUrl
    {
        get
        {
            return _thumburl;
        }
        set
        {
            _thumburl = value;
        }
    }

    public string FullUrl
    {
        get
        {
            return _fullurl;
        }
        set
        {
            _fullurl = value;
        }
    }

    public int PostId
    {
        get
        {
            return _postId;
        }
        set
        {
            _postId = value;
        }
    }
    public Picture(int post_id, string name, int user_id)
    {
        _postId = post_id;
        _name = name;
        _fullurl = Global.PostImagesFolder + "/" + user_id + "/" + post_id + "/full/" + name;
        _thumburl = Global.PostImagesFolder + "/" + user_id + "/" + post_id + "/thumb/" + name;
    }
    public Picture()
    {
        _postId = -1;
        _name = "";
        _fullurl = "";
        _thumburl = "";
    }
    /// <summary>
    /// Удаляем незаюзанные фотки
    /// </summary>
    /// <param name="post_id">Пост</param>
    /// <param name="user_id">Юзер</param>
    private static void DeleteNotNecessaryImages(int post_id, int user_id)
    {
        throw new NotImplementedException();
    }
    public List<Picture> GetByPost(int post_id)
    {
        List<Picture> pics = new List<Picture>();
        Post post = Post.GetById(post_id);
        string path = "./" + Global.PostImagesFolder + "/" + post.Author.Id + "/" + post_id;
        DirectoryInfo dir = new DirectoryInfo(HttpContext.Current.Request.MapPath(path));
        return pics;
    }


    private static string CreateFolder(int user_id, int post_id, string folder)
    {
        string current_folder = Global.PostImagesFolder + "/" + user_id + "/" + post_id + "/" + folder + "/";
        DirectoryInfo dir = new DirectoryInfo(HttpContext.Current.Request.MapPath(current_folder));
        dir.Create();
        dir.Refresh();
        return current_folder;
    }

    public static Picture UploadImage(string value, int user_id, int post_id)
    {
        string filename = Guid.NewGuid().ToString("N") + Path.GetExtension(value);
        filename = filename.ToLower();
        string fullpath = HttpContext.Current.Server.MapPath(CreateFolder(CurrentUser.User.Id, 0, "full")) + filename;
        string thumbpath = HttpContext.Current.Server.MapPath(CreateFolder(CurrentUser.User.Id, 0, "thumb")) + filename;
        Stream stream = File.OpenRead(value);
        byte[] buffer = new byte[stream.Length];
        stream.Read(buffer, 0, (int)stream.Length);
        int len = (int)stream.Length;
        stream.Dispose();
        stream.Close();

        FileStream fs = new FileStream(fullpath, FileMode.Create);
        fs.Write(buffer, 0, len);
        Picture pic = new Picture();

        try
        {
            Bitmap bmp = new Bitmap(fs);
            if (fs.Length > Global.PostImageSize || bmp.Width > Global.PostImageWidth || bmp.Height > Global.PostImageHeight)
            {
                throw new Exception("Размеры картинки недопустимы");
            }
            pic = MakeThumbnail(bmp, filename, thumbpath, user_id, post_id);
            bmp.Dispose();
            fs.Dispose();
            fs.Close();
        } catch (Exception ex)
        {
            fs.Dispose();
            fs.Close();
            File.Delete(fullpath);

            // буагага, чтобы предупреждение об неиспользуемом экзепшне не вылезало
            pic.Name = ex.Message;
            pic.Name = "";
        } 

        return pic;
    }

    private static Picture MakeThumbnail(Bitmap source_bmp, string filename, string folder, int user_id, int post_id)
    {
        // Стандартный метод GetThumbnail генерирует изображения хренового качества
        int max = Global.MaxThumbWidth;
        int height = source_bmp.Height;
        int width = source_bmp.Width;
        if (width > max)
        {
            float x;
            float y;
            if (width > height)
            {
                x = max;
                y = (float)max * (float)height / (float)width;
            } else
            {
                y = max;
                x = (float)width / (float)height * (float)max;
            }

            Bitmap thumb = new Bitmap(Convert.ToInt32(x), Convert.ToInt32(y));
            Graphics g = Graphics.FromImage(thumb);
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            g.FillRectangle(Brushes.White, 0, 0, x, y);
            g.DrawImage(source_bmp, 0, 0, x, y);

            ImageCodecInfo[] Info = ImageCodecInfo.GetImageEncoders();
            EncoderParameters Params = new EncoderParameters(1);
            Params.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 100L);
            thumb.Save(folder, Info[1], Params);

            thumb.Dispose();
        } else
        {
            source_bmp.Save(folder);
        }
        return new Picture(post_id, filename, user_id);
   }
   /// <summary>
   /// Данный метод вызывается после добавления поста, чтобы прибить изображения:
   ///  - пихаем изображения из временной папки в нужную, 
   ///  - изменяем новость(папка то изменилась), 
   ///  - удаляем ненужные картинки
   /// </summary>
   /// <param name="newpost_id">Идентификатор новости</param>
   /// <param name="user_id">Кюррент юзер</param>
   public static void FixImages(Post post, int user_id)    
   {
       // Переименовываем
        string path = "./" + Global.PostImagesFolder + "/" + user_id + "/";
        DirectoryInfo source = new DirectoryInfo(HttpContext.Current.Request.MapPath(path + 0));
        source.MoveTo(HttpContext.Current.Request.MapPath(path + post.Id));

       //TODO Изменить саму новость

       // Удаляем ненужные картинки
       DeleteNotNecessaryImages(post.Id, user_id);
    }
}
