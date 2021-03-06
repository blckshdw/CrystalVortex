﻿using System.Web;
using System.Web.Optimization;

namespace CrystalVortex
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/bootstrap-social.css",
                      "~/Content/bootstrap-custom.css",
                      "~/Scripts/dropzone/basic.css",
                      "~/Scripts/dropzone/dropzone.css",
                      "~/Content/fonts/tillium/stylesheet.css",
                      "~/Content/fonts/ptsans/stylesheet.css",
                      "~/Content/fonts/opensans_bold_macroman/stylesheet.css",
                      "~/Content/fonts/opensans_regular_macroman/stylesheet.css",
                      "~/Content/jplayer.css",
                       "~/Content/site.css"                     
                      ));

            bundles.Add(new ScriptBundle("~/bundles/facebook").Include(
                "~/Scripts/facebook.js"));

            bundles.Add(new ScriptBundle("~/bundles/dropzone").Include(
                "~/Scripts/dropzone/dropzone.js"));

            bundles.Add(new ScriptBundle("~/bundles/jplayer").Include(
                "~/Scripts/jquery-jplayer/jquery.jplayer.js",
                "~/Scripts/jquery-jplayer/ttw-music-player.js"));
            

        }
    }
}
