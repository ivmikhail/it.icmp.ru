using System;
using System.Collections.Generic;

namespace ITCommunity {
	public class MenuItemComparator : IComparer<MenuItem> {

		public int Compare(MenuItem x, MenuItem y) {
			return x.Sort.CompareTo(y.Sort);
		}
	}
}
