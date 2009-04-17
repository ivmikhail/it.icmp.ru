using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using System.Collections.Generic;
using System.Data;

public static class Comments
{
    /// <summary>
    /// ������� �������
    /// </summary>
    /// <param name="id">Id ��������</param>
    public static void Delete(int id)
    {
        Database.CommentDel(id);
    }

    /// <summary>
    /// �������� �������� �����
    /// </summary>
    /// <param name="postId">������������� �����</param>
    public static List<Comment> GetByPost(int postId)
    {
        return GetCommentFromTable(Database.CommentGetByPost(postId));
    }

    /// <summary>
    /// �������� ��������� �����������
    /// </summary>
    /// <param name="count">���-�� ������ ���������</param>
    public static List<Comment> GetLast(int count)
    {
        return GetCommentFromTable(Database.CommentGetLast(count));
    }

    /// <summary>
    /// ���������� ����������� � ����
    /// </summary>
    /// <param name="comment">��� �������</param>
    public static Comment Add(Comment comment)
    {
        Comment comm = GetCommentFromRow(Database.CommentAdd(comment.Post.Id, comment.Author.Id, comment.Ip, comment.Text));
        return comm;
    }

    private static List<Comment> GetCommentFromTable(DataTable dt)
    {
        List<Comment> comments = new List<Comment>();
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            comments.Add(GetCommentFromRow(dt.Rows[i]));
        }
        return comments;
    }

    private static Comment GetCommentFromRow(DataRow dr)
    {
        Comment comment;
        if (dr == null)
        {
            comment = new Comment();
        }
        else
        {
            comment = new Comment(Convert.ToInt32(dr["id"]),
                             Convert.ToInt32(dr["post_id"]),
                             Convert.ToInt32(dr["user_id"]),
                             Convert.ToDateTime(dr["cdate"]),
                             Convert.ToString(dr["ip"]), 
                             Convert.ToString(dr["text"]));
        }

        return comment;
    }
}
