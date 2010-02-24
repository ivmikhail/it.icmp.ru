using System;
using System.Web;
using System.Web.Caching;

namespace ITCommunity {
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
		/// <param name="data_loader">Делегат на функцию загрузки данных из БД для хранения в кеше</param>
		/// <param name="loader_params">Параметры передаваемые делегату(функции загрузки данных из БД). 
		/// Если параметров нет то рекомендуется передавать null</param>
		/// <param name="cacheTime">Время жизни кеша в часах</param>
		/// <returns>Обьект из кеша</returns>
		public static object Get(string key, Delegate data_loader, object[] loader_params, double cacheTime) {
			object obj;
			lock (@lock) {
				if ((obj = Get(key))== null) {
					DateTime absoluteExpiration = DateTime.Now.AddHours(cacheTime);
					obj = data_loader.DynamicInvoke(loader_params);
					HttpRuntime.Cache.Insert(key, obj, null, absoluteExpiration, Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
				}
			}
			return obj;
		}

		/// <summary>
		/// Простое получение обьекта из кеша
		/// </summary>
		/// <param name="key">Ключ</param>
		/// <returns>Обьект из кеша</returns>
		public static object Get(string key) {
			return HttpRuntime.Cache.Get(key);
		}

		/// <summary>
		/// Добавление объекта в кеш
		/// </summary>
		/// <param name="key">Ключ</param>
		/// <param name="obj">Обьект</param>
		/// <param name="cacheTime">Время жизни кеша в часах</param>
		public static void Insert(string key, object obj, double cacheTime) {
			lock (@lock) {
				DateTime absoluteExpiration = DateTime.Now.AddHours(cacheTime);
				HttpRuntime.Cache.Insert(key, obj, null, absoluteExpiration, Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
			}
		}

		/// <summary>
		/// Удаляем из кеша обьект
		/// </summary>
		/// <param name="key">Идентификатор обьекта</param>
		public static void Remove(string key) {
			HttpRuntime.Cache.Remove(key);
		}
	}
}
