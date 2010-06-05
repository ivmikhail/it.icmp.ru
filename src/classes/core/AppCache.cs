using System;
using System.Web;
using System.Web.Caching;

namespace ITCommunity.Core {
    /// <summary>
    /// Кеш приложения 
    /// </summary>
    public static class AppCache {

        private static object @lock = new object();

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
        /// <param name="dataLoader">Делегат на функцию загрузки данных из БД для хранения в кеше</param>
        /// <param name="time">Время жизни кеша в часах</param>
        /// <returns>Обьект из кеша</returns>
        public static T Get<T>(string key, Func<T> dataLoader, double time) where T :class {
            object obj;
            lock (@lock) {
                if ((obj = Get(key)) == null) {
                    obj = dataLoader.DynamicInvoke();
                    Insert(key, obj, time);
                }
            }
            return (T)obj;
        }

        public static T Get<T>(string key, Func<T> dataLoader) where T : class {
            return Get<T>(key, dataLoader, GetTime(key));
        }

        /// <summary>
        /// Простое получение обьекта из кеша
        /// </summary>
        /// <param name="key">Ключ</param>
        /// <returns>Обьект из кеша</returns>
        public static object Get(string key) {
            return HttpRuntime.Cache.Get(GetName(key));
        }

        /// <summary>
        /// Добавление объекта в кеш
        /// </summary>
        /// <param name="key">Ключ</param>
        /// <param name="obj">Обьект</param>
        /// <param name="time">time</param>
        /// <param name="cacheTime">Время жизни кеша в часах</param>
        private static void Insert(string key, object obj, double time) {
            var expiration = DateTime.Now.AddHours(time);
            HttpRuntime.Cache.Insert(GetName(key), obj, null, expiration, Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
        }

        private static void Insert(string key, object obj) {
            Insert(key, obj, GetTime(key));
        }

        /// <summary>
        /// Удаляем из кеша обьект
        /// </summary>
        /// <param name="key">Идентификатор обьекта</param>
        public static void Remove(string key) {
            HttpRuntime.Cache.Remove(GetName(key));
        }


        /// <summary>
        /// Берем имя кэша из конфига
        /// </summary>
        /// <param name="key">Идентификатор обьекта</param>
        /// <returns>Если не находит в конфиге, то возвращает параметр key</returns>
        private static string GetName(string key) {
            var name = Config.Get(key + "CacheName");
            return (name == null) ? key : name;
        }

        /// <summary>
        /// Берем время кэша из конфига
        /// </summary>
        /// <param name="key">Идентификатор обьекта</param>
        /// <returns>Если не находит в конфиге, то возвращает значение по-умолчанию</returns>
        private static double GetTime(string key) {
            var strTime = Config.Get(key + "CachePer");
            return (strTime == null) ? Config.GetDouble("DefaultCachePer") : Convert.ToDouble(strTime);
        }
    }
}
