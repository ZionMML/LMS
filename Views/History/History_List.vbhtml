@ModelType  LMS.ViewModels.HistoryViewModel
@code
    ViewData("Title") = "History_List"
    Layout = "~/Views/Shared/_Layout.vbhtml"
End Code

<h2>Leaves History List</h2>

@Using (Html.BeginForm())
    @Html.AntiForgeryToken()
    @<div>
        @Html.ValidationSummary(False, "", New With {.class = "text-danger"})

        <div class="col-md-2">
            @Html.ActionLink("Apply Leave", "Create", "History", Nothing, New With {.class = "btn btn-primary btnCreate"})
        </div>
        @Html.LabelFor(Function(model) model.START_DATE, htmlAttributes:=New With {.class = "control-label col-md-2"})
        <div class="col-md-2">
            @Html.EditorFor(Function(model) model.START_DATE, New With {.htmlAttributes = New With {.class = "form-control"}})
            @Html.ValidationMessageFor(Function(model) model.START_DATE, "", New With {.class = "text-danger"})
        </div>
        @Html.LabelFor(Function(model) model.END_DATE, htmlAttributes:=New With {.class = "control-label col-md-2"})
        <div class="col-md-2">
            @Html.EditorFor(Function(model) model.END_DATE, New With {.htmlAttributes = New With {.class = "form-control"}})
            @Html.ValidationMessageFor(Function(model) model.END_DATE, "", New With {.class = "text-danger"})
        </div>
        <div class="col-md-2">
            <button type="button" id="btnFind" value="Find" class="btn btn-primary">Find</button>
        </div>
        <div class="form-group">
            <table id="history" class="table table-bordered table-hover">
                <thead>
                    <tr>
                        <th>Id</th>
                        <th>User Id</th>
                        <th>User Name</th>
                        <th>Leave Type</th>
                        <th>Leave From</th>
                        <th>Session</th>
                        <th>Leave To</th>
                        <th>Session</th>
                        <th>Total Leaves Taken</th>
                        <th>Status</th>
                        <th>Updated By</th>
                        <th>Updated Date</th>
                        <th>Approved By</th>
                        <th>Approved Date</th>
                    </tr>
                </thead>
                <tbody></tbody>
            </table>
            @Html.HiddenFor(Function(model) model.ISADMIN)
        </div>
    </div>
End Using

@section scripts
    @Scripts.Render("~/bundles/jqueryval")
    <link href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.5.0/css/bootstrap-datepicker.css"
          rel="stylesheet" />
    <script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.5.0/js/bootstrap-datepicker.js">

    </script>
    <script>
        $(document).ready(function () {

            $("#START_DATE").datepicker({ format: "dd-mm-yyyy", daysOfWeekDisabled: [0, 6] });
            $("#END_DATE").datepicker({ format: "dd-mm-yyyy", daysOfWeekDisabled: [0, 6] });

            var tmpStartDt = sessionStorage.getItem("sStartDt");
            var tmpEndDt = sessionStorage.getItem("sEndDt");

            if (tmpStartDt != null || tmpEndDt != null) {
                $("#START_DATE").datepicker("setDate", tmpStartDt);
                $("#END_DATE").datepicker("setDate", tmpEndDt);
            } else {
                setDefaultDates();
            }

            if ($('#ISADMIN').val() == 'True') {
                $(".btnCreate").hide();
            } else {
                $(".btnCreate").show();
            }

            InitDataTable();
            generateLeaves();

            $('#btnFind').click(function () {
                if ($('#START_DATE').val() != '' && $('#END_DATE').val() == '') {
                    alert('(Error) End Date cannot be empty!');
                    return;
                } else if ($('#START_DATE').vale() == '' && $('#END_DATE').val() != '') {
                    alert('(Error) Start Date cannot be empty!');
                    return;
                }

                ClearDataAll();
                generateLeaves();
            });

            function formatDate(tmpDate) {
                return ((tmpDate.getDate() > 9) ? tmpDate.getDate() : ('0' + tmpdate.getDate())) + '/' + ((tmpDate.getMonth() > 8) ? (tmpDate.getMonth() + 1) : ('0' + (tmpDate.getMonth() + 1))) + '/' + tmpDate.getFullYear();
            };

            function setDefaultDates() {
                var dStartDt = new Date();
                var dEndDt = new Date();

                dStartDt.setDate(dStartDt.getDate() - 30);
                dEndDt.setDate(dEndDt.getDate() + 30);

                $("#START_DATE").datepicker("setDate", dStartDt);
                $("#END_DATE").datepicker("setDate", dEndDt);
            }

            function generateLeaves() {

                var startDate = $('#START_DATE').val();
                var endDate = $('#END_DATE').val();

                if (startDate == '' && endDate == '') {
                    $.ajax({
                        url: "/api/History",
                        dataSrc: "",
                        success: ListSuccess
                    });
                } else {
                    var searchInfo = {};
                    searchInfo.startDate = $('#START_DATE').val();
                    searchInfo.endDate = $('#END_DATE').val();

                    sessionStorage.setItem("sStartDt", $('#START_DATE').val());
                    sessionStorage.setItem("sEndDt", $('#END_DATE').val());

                    $.ajax({
                        type: 'POST',
                        async: true,
                        dataType: "json",
                        url: "/api/History/GetLeaves",
                        data: searchInfo,
                        success: ListSuccess
                    });
                }
            };

            function ListSuccess(data) {
                DrawGridDetails(data);
            };

            function InitDataTable() {
                table = $("#history").DataTable({
                    columns: [
                        {
                            data: "ID"
                        },
                        {
                            data: "USER_ID"
                        },
                        {
                            data: "USER_NAME"
                        },
                        {
                            data: "LEAVE_TYPE"
                        },
                        {
                            data: "LEAVE_FROM"
                        },
                        {
                            data: "LEAVE_FROM_AMPM"
                        },
                        {
                            data: "LEAVE_TO"
                        },
                        {
                            data: "LEAVE_TO_AMPM"
                        },
                        {
                            data: "TOTAL_TAKEN_LEAVE"
                        },
                        {
                            data: "STATUS"
                        },
                        {
                            data: "UPDATED_BY"
                        },
                        {
                            data: "UPDATED_DATE"
                        },
                        {
                            data: "APPROVED_BY"
                        },
                        {
                            data: "APPROVED_DATE"
                        },
                        {
                            data: "ID",
                            render: function (data) {
                                var detailsURL = '@Url.Action("Edit", "History")/' + data;
                                var cancelURL = '@Url.Action("Cancel", "History")/' + data;
                                var resubmitURL = '@Url.Action("Resubmit")/' + data;
                                return '<a href=\"' + detailsURL + '\" class=\"btn-link\">Details</a>';
                                return '<a href=\"' + cancelURL + '\" class=\"btn-link\">Cancel</a>';
                                return '<a href=\"' + resubmitURL + '\" class=\"btn-link\">Re-submit</a>';
                            }
                        }
                    ]
                });
            };

            function ClearDataAll() {
                table.clear();
            };

            function DrawGridDetails(obj) {
                table.clear().draw();
                table.rows.add(obj).draw();
            }
        });
    </script>

End Section


