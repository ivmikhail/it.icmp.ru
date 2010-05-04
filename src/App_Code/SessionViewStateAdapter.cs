using System.Web.UI;
using System.Web.UI.Adapters;

namespace ITCommunity {
	/// <summary>
	/// Адаптер, храним viewstate в сессии, а не на странице у клиента.
	/// </summary>
	public class SessionViewStateAdapter : PageAdapter {

		public override PageStatePersister GetStatePersister() {
			return new SessionPageStatePersister(Page);
		}
	}
}