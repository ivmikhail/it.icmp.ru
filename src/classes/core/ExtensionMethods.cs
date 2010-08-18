using System;
using System.Collections.Generic;
using System.Linq;


namespace ITCommunity.Core {

    public static class ExtensionMethods {

        private static Random _random = new Random();

        /// <summary>
        /// Расширенный метод для постраничного выбора
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="page"></param>
        /// <param name="count"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public static List<T> Paged<T>(this IQueryable<T> query, int page, int count, ref int totalCount) where T : class {
            totalCount = query.Count();

            var result = query.Skip((page - 1) * count).Take(count);

            return result.ToList();
        }

        /// <summary>
        /// Расширенный метод для выбора случайного элемента списка
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static T Random<T>(this IList<T> list) where T : class {
            return list[_random.Next(list.Count)];
        }
    }
}