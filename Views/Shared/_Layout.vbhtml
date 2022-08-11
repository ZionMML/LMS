<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>@ViewBag.Title</title>
    @Styles.Render("~/Content/css")
    @Scripts.Render("~/bundles/modernizr")
</head>
<body>
    <div class="navbar navbar-inverse navbar-fixed-top">
        <div class="container">
            <div class="navbar-header">
                <button type="button" class="navbar-toggle" data-toggle="collapse" data-target=".navbar-collapse">
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                </button>
                @Html.ActionLink("Leave Management System (Logout)", "Index", "Home", New With {.area = ""}, New With {.class = "navbar-brand"})
            </div>
            <div class="navbar-collapse collapse">
                <ul class="nav navbar-nav">
                    <li>@Html.ActionLink("Admin Profile", "Index", "Admin")</li>
                    <li>@Html.ActionLink("User Profile", "Index", "UserInfo")</li>
                    <li class="nav-item dropdown">
                        <a class="navbar-link dropdown-toggle" href="#" id="navbarDropdownMenuLink" role="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false" >
                            Leaves
                        </a>
                        <div class="dropdown-menu" aria-labelledby="navbarDropdownMenuLink">
                            <a class="dropdown-item">@Html.ActionLink("Leaves History", "Index", "History")</a><br />
                            <a class="dropdown-item">@Html.ActionLink("Leaves Summary", "Index", "Summary")</a><br />
                            <a class="dropdown-item">@Html.ActionLink("Leaves Summary Calendar", "Index", "LoadHtml")</a>
                        </div>
                    </li>
                    <li nav-item>@Html.ActionLink("Change Password", "UpdatePassword", "UserInfo")</li>
                </ul>
                <h5 style="color:white;padding-top:8px">@Html.Raw(ViewBag.LoginUserId)</h5>
            </div>
        </div>
    </div>
    <div class="container body-content">
        @RenderBody()
        <hr />
        <footer>
            <p>&copy; @DateTime.Now.Year - Leave Management System</p>
        </footer>
    </div>

    @*@Scripts.Render("~/bundles/jquery")
    @Scripts.Render("~/bundles/bootstrap")*@
    @Scripts.Render("~/bundles/lib")
    @RenderSection("scripts", required:=False)
</body>
</html>
