using System;
using System.Data;
using System.Web;
using System.Web.Caching;

namespace ITCommunity
{
    /// <summary>
    /// ��� ���������� 
    /// </summary>
    public static class AppCache
    {
        /// <summary>
        /// ������ ����� ���������� ������ �� ���� �� �����. 
        /// 
        /// ������ ���� ������� ������� �� ����������, ����� ��� �������� ������ � ��� 
        /// �� ����������� �������� � ����������. ����������� ������ � ��� ����� �������� ��
        /// ����������� ����� � ����� � �����. �����.
        /// 
        /// ����� ������� �������������(� �� ����������) ��� ������ ���� ����� ���������� 
        /// � ���� ������ �� �������.
        /// 
        /// ������ - http://habrahabr.ru/blogs/net/61617/
        /// </summary>
        /// <param name="key">�������������</param>
        /// <param name="lock">����� ��� ��������, �����������r � ������� "new object()"</param>
        /// <param name="data_loader">������� �� ������� �������� ������ �� �� ��� �������� � ����</param>
        /// <param name="loader_params">��������� ������������ ��������(������� �������� ������ �� ��). 
        /// ���� ���������� ��� �� ������������� ���������� null</param>
        /// <param name="absoluteExpiration">���� ������ �������</param>
        /// <returns>������ �� ����</returns>
        public static object Get(string key, object @lock, Delegate data_loader, object[] loader_params, DateTime absoluteExpiration)
        {
            object value;
            if ((value = HttpRuntime.Cache.Get(key)) == null)
            {
                lock (@lock)
                {
                    if ((value = HttpRuntime.Cache.Get(key)) == null)
                    {
                        value = data_loader.DynamicInvoke(loader_params);
                        HttpRuntime.Cache.Insert(key, value, null, absoluteExpiration, Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
                    }
                }
            }
            return value;
        }

        /// <summary>
        /// ������� �� ���� ������
        /// </summary>
        /// <param name="key">������������� �������</param>
        public static void Remove(string key)
        {
            HttpRuntime.Cache.Remove(key);
        }
    }
}