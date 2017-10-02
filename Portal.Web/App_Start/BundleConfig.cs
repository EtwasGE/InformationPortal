using System.Web.Optimization;

namespace Portal.Web
{
    public static class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.IgnoreList.Clear();

            bundles.Add(
                new StyleBundle("~/Bundles/account-vendor/css")
                    .Include("~/Content/fonts/roboto/roboto.css", new CssRewriteUrlTransform())
                    .Include("~/Content/lib/bootstrap/dist/css/bootstrap.css", new CssRewriteUrlTransform())
                    .Include("~/Content/lib/toastr/toastr.css", new CssRewriteUrlTransform())
                    .Include("~/Content/lib/sweetalert/dist/sweetalert.css", new CssRewriteUrlTransform())
                    .Include("~/Content/lib/font-awesome/css/font-awesome.css", new CssRewriteUrlTransform())
                    
                    .Include("~/Content/css/icheck/square/_all.css", new CssRewriteUrlTransform())
                    .Include("~/Content/lib/loading/loading.css", new CssRewriteUrlTransform())
                    .Include("~/Content/lib/loading/loading-btn.css", new CssRewriteUrlTransform())
                    .Include("~/Content/css/Abp.css", new CssRewriteUrlTransform())
                    .Include("~/Content/css/AdminLTE.css", new CssRewriteUrlTransform())
            );

            bundles.Add(
                new ScriptBundle("~/Bundles/account-vendor/js/bottom")
                    .Include(
                        "~/Content/lib/json2/json2.js",
                        "~/Content/lib/jquery/dist/jquery.js",
                        "~/Content/lib/bootstrap/dist/js/bootstrap.js",
                        "~/Content/lib/moment/min/moment-with-locales.js",
                        "~/Content/lib/jquery-validation/dist/jquery.validate.js",
                        "~/Content/lib/blockUI/jquery.blockUI.js",
                        "~/Content/lib/toastr/toastr.js",
                        "~/Content/lib/sweetalert/dist/sweetalert-dev.js",
                        "~/Content/lib/spin.js/spin.js",
                        "~/Content/lib/spin.js/jquery.spin.js",
                        "~/Content/lib/abp-web-resources/Abp/Framework/scripts/abp.js",
                        "~/Content/lib/abp-web-resources/Abp/Framework/scripts/libs/abp.jquery.js",
                        "~/Content/lib/abp-web-resources/Abp/Framework/scripts/libs/abp.toastr.js",
                        "~/Content/lib/abp-web-resources/Abp/Framework/scripts/libs/abp.blockUI.js",
                        "~/Content/lib/abp-web-resources/Abp/Framework/scripts/libs/abp.spin.js",
                        "~/Content/lib/abp-web-resources/Abp/Framework/scripts/libs/abp.sweet-alert.js",
                        
                        "~/Content/lib/icheck/icheck.js",
                        "~/Content/js/screenWidth.js"
                    )
            );

            bundles.Add(
                new StyleBundle("~/Bundles/error-vendor/css")
                    .Include("~/Content/lib/bootstrap/dist/css/bootstrap.css", new CssRewriteUrlTransform())
                    .Include("~/Content/lib/font-awesome/css/font-awesome.css", new CssRewriteUrlTransform())
                    .Include("~/Content/css/skins/_all-skins.css", new CssRewriteUrlTransform())
                    .Include("~/Content/css/AdminLTE.css", new CssRewriteUrlTransform())
            );
            
            //VENDOR RESOURCES

            //~/Bundles/vendor/css
            bundles.Add(
                new StyleBundle("~/Bundles/vendor/css")
                    .Include("~/Content/fonts/roboto/roboto.css", new CssRewriteUrlTransform())
                    .Include("~/Content/lib/bootstrap/dist/css/bootstrap.css", new CssRewriteUrlTransform())
                    .Include("~/Content/lib/bootstrap-select/dist/css/bootstrap-select.css",
                        new CssRewriteUrlTransform())
                    .Include("~/Content/lib/toastr/toastr.css", new CssRewriteUrlTransform())
                    .Include("~/Content/lib/sweetalert/dist/sweetalert.css", new CssRewriteUrlTransform())
                    .Include("~/Content/lib/famfamfam-flags/dist/sprite/famfamfam-flags.css",
                        new CssRewriteUrlTransform())
                    .Include("~/Content/lib/font-awesome/css/font-awesome.css", new CssRewriteUrlTransform())
                    //.Include("~/Content/lib/Waves/dist/waves.css", new CssRewriteUrlTransform())
                    //.Include("~/Content/lib/animate.css/animate.css", new CssRewriteUrlTransform())
                    //.Include("~/Content/css/materialize.css", new CssRewriteUrlTransform())
                    
                    .Include("~/Content/css/skins/_all-skins.css", new CssRewriteUrlTransform())
                    .Include("~/Content/css/icheck/square/_all.css", new CssRewriteUrlTransform())
                    .Include("~/Content/css/Abp.css", new CssRewriteUrlTransform())
                    .Include("~/Content/css/AdminLTE.css", new CssRewriteUrlTransform())
                    .Include("~/Content/css/main.css", new CssRewriteUrlTransform())
                    .Include("~/Content/css/ribbon.css", new CssRewriteUrlTransform())
            );

            //~/Bundles/vendor/bottom (Included in the bottom for fast page load)
            bundles.Add(
                new ScriptBundle("~/Bundles/vendor/js/bottom")
                    .Include(
                        "~/Content/lib/json2/json2.js",
                        "~/Content/lib/jquery/dist/jquery.js",
                        "~/Content/lib/bootstrap/dist/js/bootstrap.js",
                        "~/Content/lib/moment/min/moment-with-locales.js",
                        "~/Content/lib/jquery-validation/dist/jquery.validate.js",
                        "~/Content/lib/blockUI/jquery.blockUI.js",
                        "~/Content/lib/toastr/toastr.js",
                        "~/Content/lib/sweetalert/dist/sweetalert-dev.js",
                        "~/Content/lib/spin.js/spin.js",
                        "~/Content/lib/spin.js/jquery.spin.js",
                        "~/Content/lib/bootstrap-select/dist/js/bootstrap-select.js",
                        "~/Content/lib/jquery-slimscroll/jquery.slimscroll.js",
                        //"~/Content/lib/Waves/dist/waves.js",
                        "~/Content/lib/push.js/push.js",
                        "~/Content/lib/abp-web-resources/Abp/Framework/scripts/abp.js",
                        "~/Content/lib/abp-web-resources/Abp/Framework/scripts/libs/abp.jquery.js",
                        "~/Content/lib/abp-web-resources/Abp/Framework/scripts/libs/abp.toastr.js",
                        "~/Content/lib/abp-web-resources/Abp/Framework/scripts/libs/abp.blockUI.js",
                        "~/Content/lib/abp-web-resources/Abp/Framework/scripts/libs/abp.spin.js",
                        "~/Content/lib/abp-web-resources/Abp/Framework/scripts/libs/abp.sweet-alert.js",
                        "~/Content/lib/signalr/jquery.signalR.js",
                        
                        "~/Content/lib/jquery-ajax/jquery.unobtrusive-ajax.js",
                        "~/Content/lib/icheck/icheck.js",
                        "~/Content/lib/clipboard/clipboard.js",
                        "~/Content/js/screenWidth.js",
                        "~/Content/js/AdminLTE.js",
                        "~/Content/js/Abp.js"
                    )
            );
        }
    }
}