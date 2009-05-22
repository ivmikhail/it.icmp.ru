using System.Web.UI;
using ITCommunity;

namespace ITCommunity
{
    public class SessionViewStateAdapter : System.Web.UI.Adapters.PageAdapter
    {
        public override PageStatePersister GetStatePersister()
        {
            return new SessionPageStatePersister(Page);
        }
    }
}