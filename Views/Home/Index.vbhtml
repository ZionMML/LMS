@ModelType LMS.ViewModels.LoginViewModel
@Code
    ViewData("Title") = "Home Page"
End Code

<style>
    .login{
        margin: 100px auto;
        width:350px;
        padding: 30px 25px;
        background: white;
        border-radius: 10px;
        -moz-border-radius:10px;
        -webkit-border-radius:10px;
    }

    h1.login-title {
        margin: -28px -25px 25px;
        padding: 15px 25px;
        line-height: 30px;
        font-size: 25px;
        font-weight: 300;
        color: #ADADAD;
        text-align: center;
        background: #f7f7f7;
    }

    .login-message{
        color:#f90808;
    }

    input[type="text"],input[type="password"],.login-input{
        width:235px;
        height:50px;
        margin-bottom:25px;
        padding-left:10px;
        font-size:15px;
        background:#fff;
        border:1px solid #ccc;
        border-radius:4px;
    }

    .login-input:focus{
        border-color:#6e8095;
        outline: none;
    }

    .login-button {
        width: 100%;
        height: 50px;
        padding: 0;
        font-size: 20px;
        color:#fff;        
        text-align: center;
        background:#3071a9;
        border:0;
        border-radius:5px;
        cursor:pointer;
        outline:0;
        margin-top:25px;
    }

    .login-lost{
        text-align:center;
        margin-bottom:0px;
    }

    .login-lost a{
        color:#666;
        text-decoration:none;
        font-size:13px;
    }
</style>

@Using (Html.BeginForm("Edit", "Home"))
    @Html.AntiForgeryToken()

    @<div class="login">

        <div class="form-group">
            <img src="~/Content/images/leave-management.png" alt="Login Logo" class="center" />
            <br/>
            <h1 class="login-title">Leave Management System Login</h1>
            <div class="form-group">
                <div class="form-group">
                    @Html.LabelFor(Function(model) model.USER_ID, htmlAttributes:=New With {.class = "control-label col-md"})
                    <br />
                    <div class="col-md-10">
                        @Html.EditorFor(Function(model) model.USER_ID, New With {.htmlAttributes = New With {.class = "form-control login-input", .required = "required"}})
                        @Html.ValidationMessageFor(Function(model) model.USER_ID, "", New With {.class = "text-danger"})
                    </div>
                </div>
                <div class="form-group">
                    @Html.LabelFor(Function(model) model.PASSWORD, htmlAttributes:=New With {.class = "control-label col-md"})
                    <br />
                    <div class="col-md-10">
                        @Html.PasswordFor(Function(model) model.PASSWORD, New With {.htmlAttributes = New With {.class = "form-control login-input", .required = "required"}})
                        @Html.ValidationMessageFor(Function(model) model.PASSWORD, "", New With {.class = "text-danger"})
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-md-offset-2 col-md">
                        <input type="submit" id="btnSubmit" value="Login" class="btn btn-default"/>
                    </div>
                </div>

                @Html.ValidationSummary(False, "", New With {.class = "text-danger"})

            </div>
        </div>
    </div>
End Using

