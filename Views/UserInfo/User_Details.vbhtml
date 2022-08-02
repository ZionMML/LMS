@ModelType LMS.ViewModels.UsersViewModel
@Code
    ViewData("Title") = "User_Details"
    Layout = "~/Views/Shared/_Layout.vbhtml"
End Code

<h2>User Info Details</h2>

@Using (Html.BeginForm("Edit", "UserInfo"))
    @Html.AntiForgeryToken()

    @<div class="form-horizontal">

    <hr />
    @Html.ValidationSummary(False, "", New With {.class = "text-danger"})
     <div class="form-group" id="details">
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

         <div class="form-group">
             @Html.LabelFor(Function(model) model.COMPANY, htmlAttributes:=New With {.class = "control-label col-md-2"})
             <div class="col-md-10">
                 @Html.EditorFor(Function(model) model.COMPANY, New With {.htmlAttributes = New With {.class = "form-control"}})
                 @Html.ValidationMessageFor(Function(model) model.COMPANY, "", New With {.class = "text-danger"})
             </div>
         </div>

         <div class="form-group">
             @Html.LabelFor(Function(model) model.TEAM_LEAD_NAME, htmlAttributes:=New With {.class = "control-label col-md-2"})
             <div class="col-md-10">
                 @Html.DropDownListFor(Function(model) model.TEAM_LEAD_NAME, ViewData("TeamLeadList"))
                 @Html.ValidationMessageFor(Function(model) model.TEAM_LEAD_NAME, "", New With {.class = "text-danger"})
             </div>
         </div>

         <div class="form-group">
             @Html.LabelFor(Function(model) model.MAX_ANNUAL_LEAVE, htmlAttributes:=New With {.class = "control-label col-md-2"})
             <div class="col-md-10">
                 @Html.EditorFor(Function(model) model.MAX_ANNUAL_LEAVE, New With {.htmlAttributes = New With {.class = "form-control"}})
                 @Html.ValidationMessageFor(Function(model) model.MAX_ANNUAL_LEAVE, "", New With {.class = "text-danger"})
             </div>
         </div>

         <div class="form-group">
             @Html.LabelFor(Function(model) model.MAX_MEDICAL_LEAVE, htmlAttributes:=New With {.class = "control-label col-md-2"})
             <div class="col-md-10">
                 @Html.EditorFor(Function(model) model.MAX_MEDICAL_LEAVE, New With {.htmlAttributes = New With {.class = "form-control"}})
                 @Html.ValidationMessageFor(Function(model) model.MAX_MEDICAL_LEAVE, "", New With {.class = "text-danger"})
             </div>
         </div>

         <div class="form-group">
             @Html.LabelFor(Function(model) model.MAX_OTHER_LEAVE, htmlAttributes:=New With {.class = "control-label col-md-2"})
             <div class="col-md-10">
                 @Html.EditorFor(Function(model) model.MAX_OTHER_LEAVE, New With {.htmlAttributes = New With {.class = "form-control"}})
                 @Html.ValidationMessageFor(Function(model) model.MAX_OTHER_LEAVE, "", New With {.class = "text-danger"})
             </div>
         </div>
     </div>
        
     <div class="form-group" id="password">
         <div class="form-group">
             @Html.LabelFor(Function(model) model.PASSWORD, htmlAttributes:=New With {.class = "control-label col-md-2"})
             <div class="col-md-10">
                 @Html.PasswordFor(Function(model) model.PASSWORD, New With {.htmlAttributes = New With {.class = "form-control"}})
                 @Html.ValidationMessageFor(Function(model) model.PASSWORD, "", New With {.class = "text-danger"})
             </div>
         </div>
     </div>     

        @Html.HiddenFor(Function(model) model.ID)
        @Html.HiddenFor(Function(model) model.UPDATE_PWD_FLAG)
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
                var updatePwd = $("#UPDATE_PWD_FLAG").val();

                $("#TEAM_LEAD_NAME").addClass("form-control");
                $("#PASSWORD").addClass("form-control");

                if (userid != '') {
                    $("#USER_ID").attr('disabled', true);
                    $("#password").hide();
                }

                if (updatePwd == 'Y') {
                    $("#details").hide();
                }

            });
        </script>
    End Section
