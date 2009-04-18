using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using System.Collections.Generic;
using System.Data;

public static class Posts
{
    public static Post GetById(int id)
    {
        return GetPostFromRow(Database.PostGetById(id));
    }

    public static void Delete(int id)
    {
        Database.PostDel(id);
    }

    /// <summary>
    /// Забираем посты постранично, с учетом даты и аттачей
    /// </summary>
    /// <param name="page">Страница которая нам нужна</param>
    /// <param name="count">Кол-во постов на страницу</param>
    public static List<Post> GetPosts(int page, int count)
    {
        return GetPostsFromTable(Database.PostGet(page, count));
    }
    /// <summary>
    /// Забираем посты постранично, с учетом даты, аттачей и категории
    /// </summary>
    /// <param name="page">Страница которая нам нужна</param>
    /// <param name="count">Кол-во постов на страницу</param>
    /// <param name="count">id категории</param>
    public static List<Post> GetPostsByCat(int page, int count, int cat_id)
    {
        return GetPostsFromTable(Database.PostGetByCat(page, count, cat_id));
    }



    /// <summary>
    /// Популярные посты
    /// </summary>
    /// <param name="period">Период, в днях. Например популярные посты за последние N дней.</param>
    /// <param name="count">Кол-во нужных постов</param>
    public static List<Post> GetTop(int period, int count)
    {
        return GetPostsFromTable(Database.PostGetTop(period, count));
    }

    /// <summary>
    /// Добавление нового поста
    /// </summary>
    /// <param name="post">Сам пост, CreateDate будет изменен на дату добавления новости в базу.</param>
    public static Post Add(Post post)
    {
        DataRow dr = Database.PostAdd(post.Title, 
                                      post.Description, 
                                      post.Text, 
                                      post.Category.Id, 
                                      Convert.ToByte(post.Attached), 
                                      post.Source, 
                                      post.Author.Id);
        return GetPostFromRow(dr);
    }

    private static List<Post> GetPostsFromTable(DataTable dt)
    {
        List<Post> posts = new List<Post>();
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            posts.Add(GetPostFromRow(dt.Rows[i]));
        }
        return posts;
    }

    private static Post GetPostFromRow(DataRow dr)
    {
        Post post;
        if (dr == null)
        {
            post = new Post();
        }
        else
        {
            post = new Post(Convert.ToInt32(dr["id"]),
                         Convert.ToString(dr["title"]),
                         Convert.ToString(dr["description"]),
                         Convert.ToString(dr["text"]),
                         Convert.ToDateTime(dr["cdate"]),
                         Convert.ToInt32(dr["user_id"]),
                         Convert.ToInt32(dr["cat_id"]),
                         Convert.ToBoolean(dr["attached"]),
                         Convert.ToInt32(dr["views"]),
                         Convert.ToString(dr["source"]),
                         Convert.ToInt32(dr["comments_count"]));
        }
        return post;
    }
}
