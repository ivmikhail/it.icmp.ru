using System.Web.UI;
using ITCommunity;

namespace ITCommunity
{
    /// <summary>
    /// Адаптер, храним viewstate в сессии, а не на странице у клиента.
    /// </summary>
    public class SessionViewStateAdapter : System.Web.UI.Adapters.PageAdapter
    {
        public override PageStatePersister GetStatePersister()
        {
            return new SessionPageStatePersister(Page);
        }
    }
}