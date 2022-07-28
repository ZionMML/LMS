Imports System.Web.Optimization

Public Module BundleConfig
    ' For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
    Public Sub RegisterBundles(ByVal bundles As BundleCollection)

        bundles.Add(New ScriptBundle("~/bundles/lib").Include(
                    "~/Scripts/jquery-{version}.js",
                    "~/Scripts/bootstrap.js",
                    "~/Scripts/bootbox.js",
                    "~/Scripts/respond.js",
                    "~/Scripts/DataTables/jquery.dataTables.js",
                    "~/Scripts/DataTables/dataTables.bootstrap.js",
                    "~/Scripts/toastr.js",
                    "~/Scripts/common.js"
))

        bundles.Add(New ScriptBundle("~/bundles/jqueryval").Include(
                    "~/Scripts/jquery.validate*"))

        ' Use the development version of Modernizr to develop with and learn from. Then, when you're
        ' ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
        bundles.Add(New ScriptBundle("~/bundles/modernizr").Include(
                    "~/Scripts/modernizr-*"))

        'bundles.Add(New ScriptBundle("~/bundles/bootstrap").Include(
        '          "~/Scripts/bootstrap.js"))

        bundles.Add(New StyleBundle("~/Content/css").Include(
                  "~/Content/bootstrap.css",
                  "~/Content/site.css",
                  "~/Content/DataTables/css/dataTables.bootstrap4.css"
        ))
    End Sub
End Module

