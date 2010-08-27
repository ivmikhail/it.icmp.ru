using System.Web.Mvc;
using System.ServiceModel.Syndication;
using System.Xml;
using System;
using System.Collections.Generic;

using ITCommunity.Core;
using ITCommunity.DB;
using ITCommunity.DB.Tables;
using ITCommunity.Models;


namespace ITCommunity.Controllers {

    public class BrowseController : BaseController
    {
        private List<BrowseItem> getPathItems(string dir)
        {
            string p = (dir=="") ? "/" : dir; 
            string[] pathes = p.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
            List<BrowseItem> result = new List<BrowseItem>(pathes.Length);
            string path = "";
            for (int i = 0; i < pathes.Length; i++)
            {
                path += pathes[i] + "/";
                BrowseItem bi = BrowseItem.Get(BrowseItem.GetRealPathOfLink(path));                
                result.Add(bi);
            }
            return result;
        }

        private string unescapeLink(string link)
        {
            string result;
            try
            {
                result = Uri.UnescapeDataString(link);
            }
            catch (UriFormatException e)
            {
                Logger.Log.Error("Кажется нас пытаются хакнуть", e);
                result = "/";
            }
            return result;
        }


        public ActionResult View(string directory)
        {
            string dir = unescapeLink(directory ?? "");            

            BrowseModel model = new BrowseModel();
            model.Pathes = getPathItems(dir);

            bool isRootDir = dir == "";
            dir = BrowseItem.GetRealPathOfLink(dir);
            if (dir != null)
            {                
                if (!isRootDir)
                {
                    BrowseItem rootDirInfo = BrowseItem.Get(BrowseItem.GetRealPathOfLink(""));
                    rootDirInfo.LinkDir = "";
                    model.RootDir = rootDirInfo;
                }                
                model.Files = BrowseItem.GetList(dir, isRootDir);
                return View(model);
            } else {                
                return NotFound();
            }
        }





    }
}
