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
        /// вызвав переданную функцию. Загруженный обьект в кеш будет доступен по
        /// переданному ключу и умрет в соотв. время указанное в конфиге.
        /// 
        /// Метод пытается гарантировать(я не гарантирую) что только один поток отправится 
        /// в базу данных за данными.
        /// 
        /// Курево - http://habrahabr.ru/blogs/net/61617/
        /// </summary>
        /// <param name="key">Идентификатор</param>
        /// <param name="dataLoader">Функция загрузки данных для хранения в кеше</param>
        /// <returns>Обьект из кеша</returns>
        public static T Get<T>(string key, Func<T> dataLoader) where T : class {
            object obj;
            lock (@lock) {
                if ((obj = Get(key)) == null) {
                    obj = dataLoader.DynamicInvoke();
                    Insert(key, obj);
                }
            }
            return (T)obj;
        }

        /// <summary>
        /// Простое получение обьекта из кеша
        /// </summary>
        /// <param name="key">Идентификатор обьекта</param>
        /// <returns>Обьект из кеша</returns>
        public static object Get(string key) {
            return HttpRuntime.Cache.Get(key);
        }

        /// <summary>
        /// Добавление объекта в кеш
        /// </summary>
        /// <param name="key">Идентификатор обьекта</param>
        /// <param name="obj">Обьект</param>
        private static void Insert(string key, object obj) {
            var expiration = DateTime.Now.AddHours(GetTime(key));
            HttpRuntime.Cache.Insert(key, obj, null, expiration, Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
        }

        /// <summary>
        /// Удаляем из кеша обьект
        /// </summary>
        /// <param name="key">Идентификатор обьекта</param>
        public static void Remove(string key) {
            HttpRuntime.Cache.Remove(key);
        }

        /// <summary>
        /// Берем время кэша из конфига
        /// </summary>
        /// <param name="key">Идентификатор обьекта</param>
        /// <returns>Если не находит в конфиге, то возвращает значение по-умолчанию DefaultCachePer</returns>
        private static double GetTime(string key) {
            var time = Config.GetDouble(key + "CachePer");
            return (time == 0) ? Config.GetDouble("DefaultCachePer") : time;
        }
    }
}
