@ModelType LMS.ViewModels.AdminsViewModel
@Code
    ViewData("Title") = "Admin_Details"
    Layout = "~/Views/Shared/_Layout.vbhtml"
End Code

@Using (Html.BeginForm("Edit", "Admin"))
    @Html.AntiForgeryToken()

    @<div class="form-horizontal">
        <hr />
        @Html.ValidationSummary(False, "", New With {.class = "text-danger"})
        <div class="form-group">
            @Html.LabelFor(Function(model) model.USER_ID, htmlAttributes:=New With {.class = "control-label col-md-2"})
            <div class="col-md-10">
                @Html.EditorFor(Function(model) model.USER_ID, New With {.htmlAttributes = New With {.class = "form-control"}})
                @Html.ValidationMessageFor(Function(model) model.USER_ID, "", New With {.class = "text-danger"})
            </div>
        </div>

        <div class="form-group">
            @Html.LabelFor(Function(model) model.USER_NAME, htmlAttributes:=New With {.class = "control-label col-md-2"})
            <div class="col-md-10">
                @Html.EditorFor(Function(model) model.USER_NAME, New With {.htmlAttributes = New With {.class = "form-control"}})
                @Html.ValidationMessageFor(Function(model) model.USER_NAME, "", New With {.class = "text-danger"})
            </div>
        </div>

        <div class="form-group" id="divpwd">
            @Html.LabelFor(Function(model) model.PASSWORD, htmlAttributes:=New With {.class = "control-label col-md-2"})
            <div class="col-md-10">
                @Html.PasswordFor(Function(model) model.PASSWORD, New With {.htmlAttributes = New With {.class = "form-control"}})
                @Html.ValidationMessageFor(Function(model) model.PASSWORD, "", New With {.class = "text-danger"})
            </div>
        </div>

        <div class="form-group">
            @Html.LabelFor(Function(model) model.EMAIL_ADDR, htmlAttributes:=New With {.class = "control-label col-md-2"})
            <div class="col-md-10">
                @Html.EditorFor(Function(model) model.EMAIL_ADDR, New With {.htmlAttributes = New With {.class = "form-control"}})
                @Html.ValidationMessageFor(Function(model) model.EMAIL_ADDR, "", New With {.class = "text-danger"})
            </div>
        </div>

        @Html.HiddenFor(Function(model) model.EDIT_FLAG)
        @Html.HiddenFor(Function(model) model.EXISTING_USERID)

        <div class="form-group">
            <div class="col-md-offset-2 col-md-10">
                <input type="submit" value="Save" class="btn btn-default" />
            </div>
        </div>
    </div>
End Using

<div>
    @Html.ActionLink("Back to List", "")
</div>

@Section Scripts 
    @Scripts.Render("~/bundles/jqueryval")
<script>
    $(document).ready(function () {
        var userid = $("#USER_ID").val();

        $("#PASSWORD").addClass("form-control");

        if (userid != '') {
            $("#USER_ID").attr('disabled', true);
            $("#divpwd").hide();
        }
    });
    </script>
End Section
