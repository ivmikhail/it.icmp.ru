using System;
using System.Data;
using System.Web;
using System.Web.Caching;

namespace ITCommunity
{
    /// <summary>
    /// Кеш приложения 
    /// </summary>
    public static class AppCache
    {
        /// <summary>
        /// Данный метод возвращает обьект из кеша по ключу. 
        /// 
        /// Причем если данного обьекта не существует, метод сам загрузит данные в кеш 
        /// по переданному делегату и параметрам. Загруженный обьект в кеш будет доступен по
        /// переданному ключу и умрет в соотв. время.
        /// 
        /// Метод пытается гарантировать(я не гарантирую) что только один поток отправится 
        /// в базу данных за данными.
        /// 
        /// Курево - http://habrahabr.ru/blogs/net/61617/
        /// </summary>
        /// <param name="key">Идентификатор</param>
        /// <param name="lock">Нужен для гарантии, передавайте к примеру "new object()"</param>
        /// <param name="data_loader">Делегат на функцию загрузки данных из БД для хранения в кеше</param>
        /// <param name="loader_params">Параметры передаваемые делегату(функции загрузки данных из БД). 
        /// Если параметров нет то рекомендуется передавать null</param>
        /// <param name="absoluteExpiration">Дата смерти обьекта</param>
        /// <returns>Обьект из кеша</returns>
        public static object Get(string key, object @lock, Delegate data_loader, object[] loader_params, DateTime absoluteExpiration)
        {
            object value;
            if ((value = HttpRuntime.Cache.Get(key)) == null)
            {
                lock (@lock)
                {
                    value = data_loader.DynamicInvoke(loader_params);
                    HttpRuntime.Cache.Insert(key, value, null, absoluteExpiration, Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
                }
            }
            return value;
        }

        /// <summary>
        /// Простое получение обьекта из кеша
        /// </summary>
        /// <param name="key">ключ</param>
        /// <returns>null or object</returns>
        public static object Get(string key)
        {
            return HttpRuntime.Cache.Get(key);
        }
        public static void Insert(string key, object obj, object @lock, DateTime absoluteExpiration)
        {
            lock (@lock)
            {
                HttpRuntime.Cache.Insert(key, obj, null, absoluteExpiration, Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
            }
        }

        /// <summary>
        /// Удаляем из кеша обьект
        /// </summary>
        /// <param name="key">Идентификатор обьекта</param>
        public static void Remove(string key)
        {
            HttpRuntime.Cache.Remove(key);
        }
    }
}
