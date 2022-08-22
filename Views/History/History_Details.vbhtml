@ModelType LMS.ViewModels.HistoryViewModel
@code
    ViewData("Title") = "History_Details"
    Layout = "~/Views/Shared/_Layout.vbhtml"
End Code

<h2>Leave Application</h2>

@Using (Html.BeginForm("Edit", "History", FormMethod.Post, New With {.enctype = "multipart/form-data"}))
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
        @Html.LabelFor(Function(model) model.LEAVE_TYPE, htmlAttributes:=New With {.class = "control-label col-md-2"})
        <div class="col-md-10">
            @Html.DropDownListFor(Function(model) model.LEAVE_TYPE, ViewData("LeaveTypesList"))
            @Html.ValidationMessageFor(Function(model) model.LEAVE_TYPE, "", New With {.class = "text-danger"})
        </div>
    </div>

    <div class="form-group">
        @Html.LabelFor(Function(model) model.LEAVE_FROM, htmlAttributes:=New With {.class = "control-label col-md-2"})
        <div class="col-md-10">
            @Html.EditorFor(Function(model) model.LEAVE_FROM, New With {.htmlAttributes = New With {.class = "form-control"}})
            @Html.DropDownListFor(Function(model) model.LEAVE_FROM_AMPM, ViewData("AMPMList"))
            @Html.ValidationMessageFor(Function(model) model.LEAVE_FROM, "", New With {.class = "text-danger"})
        </div>
    </div>

    <div class="form-group">
        @Html.LabelFor(Function(model) model.LEAVE_TO, htmlAttributes:=New With {.class = "control-label col-md-2"})
        <div class="col-md-10">
            @Html.EditorFor(Function(model) model.LEAVE_TO, New With {.htmlAttributes = New With {.class = "form-control"}})
            @Html.DropDownListFor(Function(model) model.LEAVE_TO_AMPM, ViewData("AMPMList"))
            @Html.ValidationMessageFor(Function(model) model.LEAVE_TO, "", New With {.class = "text-danger"})
        </div>
    </div>

    <div class="form-group">
        @Html.LabelFor(Function(model) model.TOTAL_TAKEN_LEAVE, htmlAttributes:=New With {.class = "control-label col-md-2"})
        <div class="col-md-10">
            @Html.EditorFor(Function(model) model.TOTAL_TAKEN_LEAVE, New With {.htmlAttributes = New With {.class = "form-control"}})
            @Html.ValidationMessageFor(Function(model) model.TOTAL_TAKEN_LEAVE, "", New With {.class = "text-danger"})
        </div>
    </div>

    <div class="form-group">
        @Html.LabelFor(Function(model) model.REMARKS, htmlAttributes:=New With {.class = "control-label col-md-2"})
        <div class="col-md-10">
            @Html.EditorFor(Function(model) model.REMARKS, New With {.htmlAttributes = New With {.class = "form-control"}})
            @Html.ValidationMessageFor(Function(model) model.REMARKS, "", New With {.class = "text-danger"})
        </div>
    </div>

    <div class="form-group">
        @Html.LabelFor(Function(model) model.STATUS, htmlAttributes:=New With {.class = "control-label col-md-2"})
        <div class="col-md-10">
            @Html.EditorFor(Function(model) model.STATUS, New With {.htmlAttributes = New With {.class = "form-control"}})
            @Html.ValidationMessageFor(Function(model) model.STATUS, "", New With {.class = "text-danger"})
        </div>
    </div>

    <div class="form-group">
        @Html.LabelFor(Function(model) model.DOCUMENTS, htmlAttributes:=New With {.class = "control-label col-md-2"})
        <div class="col-md-10">
            @Html.TextBoxFor(Function(model) model.PostedFile, New With {.type = "file"})
            <input id="btnUpload" name="submitButton" type="submit" value="Upload" class="btn btn-default" />
            @Html.ValidationMessageFor(Function(model) model.PostedFile, "", New With {.class = "text-danger"})
            <button id="btnRemove" type="submit" name="submitButton" value="Remove" class="btn btn-default">
                <span class="glyphicon glyphicon-trash" aria-hidden="true"></span>
            </button>
            <span style="color:dodgerblue">@Html.Raw(ViewBag.Message)</span>
        </div>
    </div>

    @Html.HiddenFor(Function(model) model.HIDDEN_ID)
    @Html.HiddenFor(Function(model) model.CURRENT_USER_ID)
    @Html.HiddenFor(Function(model) model.HIDDEN_TOTAL_LEAVE_TAKEN)
    @Html.HiddenFor(Function(model) model.HIDDEN_STATUS)
    @Html.HiddenFor(Function(model) model.HIDDEN_DOCUMENT)
    @Html.HiddenFor(Function(model) model.ISADMIN)
    @Html.HiddenFor(Function(model) model.HIDDEN_USER_ID)
    @Html.HiddenFor(Function(model) model.HIDDEN_APPROVED_BY)

    <div class="form-group">
        <div class="col-md-offset-2 col-md-10">
            <input id="btnSubmit" name="submitButton" type="submit" value="Submit" class="btn btn-default" />
            <input id="btnCancel" name="submitButton" type="submit" value="Cancel Leave" class="btn btn-default" />
            <input id="btnResubmit" name="submitButton" type="submit" value="Resubmit Leave" class="btn btn-default" />
            <input id="btnApprove" name="submitButton" type="submit" value="Approve" class="btn btn-default" />
        </div>
    </div>
</div>
End Using

<div>
    @Html.ActionLink("Back to List", "Index")
</div>

<style>
    #LEAVE_FROM_AMPM, #LEAVE_TO_AMPM{
        display: block;
        height: 34px;
        padding: 6px 12px;
        font-size: 14px;
        line-height: 1.42;
        color: #555555;
        vertical-align: middle;
        background-color: #ffffff;
        border: 1px solid #cccccc;
        border-radius: 4px;
        -webkit-box-shadow: inset 0 1px 1px rgb(0 0 0 / 8%);
        box-shadow: inset 0 1px 1px rgb(0 0 0 / 8%);
        -webkit-transition: border-color ease-in-out 0.15s, box-shadow ease-in-out 0.15s;
        transition: border-color ease-in-out 0.15s, box-shadow ease-in-out 0.15s;
    }
</style>

@Section Scripts 
    @Scripts.Render("~/bundles/jqueryval")
    <link href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.5.0/css/bootstrap-datepicker.css"
          rel="stylesheet" />
    <script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.5.0/js/bootstrap-datepicker.js">
    </script>
    <script>
        $(document).ready(function () {
            var uploadedFile = $("#afilename").attr("href");

            if (uploadedFile) {
                uploadedFile = uploadedFile.split("/").pop();
            }

            $("#LEAVE_TYPE").addClass("form-control");
            $("#LEAVE_FROM_AMPM").addClass("ampm");

            $("#LEAVE_FROM").datepicker({ format: "dd-mm/yyyy", daysOfWeekDisabled: [0, 6] });
            $("#LEAVE_TO").datepicker({ format: "dd-mm/yyyy", daysOfWeekDisabled: [0, 6] });

            $("#USER_ID").attr('disabled', true);
            $("#TOTAL_TAKEN_LEAVE").attr('disabled', true);
            $("#STATUS").attr('disabled', true);

            if ($("#ISADMIN").val() == 'True') {
                $("#btnSubmit").hide();
                $("#btnCancel").hide();
                $("#btnResubmit").hide();
                $("#btnUpload").hide();
                $("#btnRemove").hide();

                if ($("#STATUS").val().includes('Pending')) {
                    $("#btnApprove").show();
                } else {
                    $("#btnApprove").hide();
                }
            } else {
                $("#btnApprove").hide();

                if ($("#STATUS").val() == 'Pending Approve') {
                    $("#btnSubmit").show();
                    $("#btnCancel").show();
                    $("#btnResubmit").hide();
                    $("#btnUpload").show();
                    if (uploadedFile) {
                        $("#btnRemove").show();
                    } else {
                        $("#btnRemove").hide();
                    }

                } else if ($("#STATUS").val() == 'Pending Cancel') {
                    $("#btnSubmit").hide();
                    $("#btnCancel").hide();
                    $("#btnResubmit").hide();
                    $("#btnUpload").hide();
                    $("#btnRemove").hide();
                } else if ($("#STATUS").val() == 'Canceled') {
                    $("#btnSubmit").hide();
                    $("#btnCancel").hide();
                    $("#btnResubmit").show();
                    $("#btnUpload").hide();
                    $("#btnRemove").hide();
                } else if ($("#STATUS").val() == 'Approved') {
                    $("#btnSubmit").hide();
                    $("#btnCancel").show();
                    $("#btnResubmit").hide();
                    $("#btnUpload").hide();
                    $("#btnRemove").hide();
                } else {
                    $("#btnSubmit").show();
                    $("#btnCancel").show();
                    $("#btnResubmit").hide();
                    $("#btnUpload").show();
                    if (uploadedFile) {
                        $("#btnRemove").show();
                    } else {
                        $("#btnRemove").hide();
                    }
                }
            }

            if ($('#HIDDEN_ID').val() == 0) {
                $('#LEAVE_TO_AMPM').val('PM'); //New Record, set default as 'PM'
            }

            if ($('#afilename').text() != '') {
                var filename = $('#afilename').text();
                $('#HIDDEN_DOCUMENT').val(filename);
            }

            $('#LEAVE_TYPE').select().change(function () {
                if ($("#LEAVE_TYPE").val() == 'Medical Leave') {
                    $('#LEAVE_FROM_AMPM').val('AM');
                    $('#LEAVE_TO_AMPM').val('PM');
                    if ($('#LEAVE_FROM').val() != '' && $('#LEAVE_TO').val() != '') {
                        calculate_leaves_taken();
                    }
                }
            });

            $('#LEAVE_FROM').datepicker().on('changeDate', function (e) {
                if ($('#LEAVE_TO').val() != '') {
                    calculate_leaves_taken();
                }
            });

            $('#LEAVE_FROM_AMPM').select().change(function () {
                if ($('#LEAVE_TYPE').val() == 'Medical Leave' && $('LEAVE_FROM_AMPM').val() == 'PM') {
                    $('#LEAVE_FROM_AMPM').val('AM');
                    alert("(Error) Medical Leave should be starting from AM to PM.");
                    return;
                }

                if ($('#LEAVE_TO').val() != '') {
                    calculate_leaves_taken();
                }
            });

            $('#LEAVE_TO').datepicker().on('changeDate', function (e) {
                if ($('#LEAVE_FROM').val() == '') {
                    alert('(Error) Leave From cannot be blank.');
                } else {
                    calculate_leaves_taken();
                }
            });

            $('#LEAVE_TO_AMPM').select().change(function () {
                if ($('#LEAVE_TYPE').val() == 'Medical Leave' && $('LEAVE_TO_AMPM').val() == 'AM') {
                    $('#LEAVE_FROM_AMPM').val('PM');
                    alert("(Error) Medical Leave should be starting from AM to PM.");
                    return;
                }

                if ($('#LEAVE_FROM').val() == '') {
                    alert('(Error) Leave From cannot be blank.');
                } else {
                    calculate_leaves_taken();
                }
            });

            function formatDate(tmpDate) {
                return ((tmpDate.getDate() > 9) ? tmpDate.getDate() : ('0' + tmpdate.getDate())) + '/' + ((tmpDate.getMonth() > 8) ? (tmpDate.getMonth() + 1) : ('0' + (tmpDate.getMonth() + 1))) + '/' + tmpDate.getFullYear();
            };

            function calculate_leaves_taken() {
                var start = $('#LEAVE_FROM').datepicker("getDate");
                var end = $('#LEAVE_TO').datepicker("getDate");

                var startAMPM = $('#LEAVE_FROM_AMPM').val();
                var endAMPM = $('#LEAVE_TO_AMPM').val();

                var startDate = new Date(start);
                var endDate = new Date(end);

                if (startDate > endDate) {
                    alert("(Error) Leave From date cannot be greater than Leave To date.");
                } else {
                    var leaveData = {};
                    leaveData.startDt = formatDate(startDate);
                    leaveData.endDt = formatDate(endDate);
                    leaveData.startAMPM = startAMPM;
                    leaveData.endAMPM = endAMPM;

                    $.ajax({
                        type: 'CAL', //using customised HTTP versb to call multiple action methods
                        async: true,
                        dataType: "json",
                        url: "/api/History",
                        data: leaveData
                    })

                        .done(function (data) {
                            if (data.ErrMsg) {
                                alert(data.ErrMsg);
                            } else {                                
                                $('#TOTAL_TAKEN_LEAVE').val(data.ttlLeaves);
                                $('#HIDDEN_TOTAL_LEAVE_TAKEN').val(data.ttlLeaves);
                                
                            }
                        })
                        .fail(function (err) {
                            alert("Error:" + err);
                        })
                }

            }

        });
    </script>
End Section
