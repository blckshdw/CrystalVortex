using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CrystalVortex.Helpers
{
    public static class HelperExtensions
    {
        public static MvcHtmlString CurrencyFormat(this HtmlHelper helper, string value)
        {
            var result = string.Format("{0:C2}", value);
            return new MvcHtmlString(result);
        }

        public static MvcHtmlString DisplayReleaseImage(this HtmlHelper helper, string releaseCode)
        {
            DirectoryInfo di = new DirectoryInfo(HttpContext.Current.Server.MapPath("~/Images/AlbumArt"));
            var files = di.GetFiles(releaseCode + "-Artwork.*");
            if (files != null && files.Length > 0)
            {
                var fileUri = files[0].FullName.Replace(di.FullName, "~/Images/AlbumArt/");
                var absFile = VirtualPathUtility.ToAbsolute(fileUri);
                return new MvcHtmlString(String.Format("<img class=\"img-responsive thumbnail\"  src=\"{0}\"></img>", absFile));
            } 

            var placeHolder = VirtualPathUtility.ToAbsolute("~/Images/AlbumArt/Placeholder.png");
            return new MvcHtmlString("<img class=\"img-responsive thumbnail\" src=\""+ placeHolder + "\"></img>");
        }
        public static MvcHtmlString DisplayReleaseImagePath(this HtmlHelper helper, string releaseCode)
        {
            DirectoryInfo di = new DirectoryInfo(HttpContext.Current.Server.MapPath("~/Images/AlbumArt"));
            var files = di.GetFiles(releaseCode + "-Artwork.*");
            if (files != null && files.Length > 0)
            {
                var fileUri = files[0].FullName.Replace(di.FullName, "~/Images/AlbumArt/");
                var absFile = VirtualPathUtility.ToAbsolute(fileUri);
                return new MvcHtmlString(absFile);
            }

            var placeHolder = VirtualPathUtility.ToAbsolute("~/Images/AlbumArt/Placeholder.png");
            return new MvcHtmlString(placeHolder);
        }
    }

}
