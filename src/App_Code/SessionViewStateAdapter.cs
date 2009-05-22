using System.Web.UI;
using ITCommunity;

namespace ITCommunity
{
    /// <summary>
    /// �������, ������ viewstate � ������, � �� �� �������� � �������.
    /// </summary>
    public class SessionViewStateAdapter : System.Web.UI.Adapters.PageAdapter
    {
        public override PageStatePersister GetStatePersister()
        {
            return new SessionPageStatePersister(Page);
        }
    }
}