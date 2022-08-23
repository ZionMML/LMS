@ModelType LMS.ViewModels.SummaryViewModel
@Code
    ViewData("Title") = "Summary_List"
    Layout = "~/Views/Shared/_Layout.vbhtml"
End Code

<h2>Leaves Summary List</h2>
@Using (Html.BeginForm())
    @<div>
        @Html.HiddenFor(Function(model) model.ISADMIN)
        @Html.HiddenFor(Function(model) model.USER_ID)
    </div>
End Using
<table id="summary" class="table table-bordered table-hover">
    <thead>
        <tr>
            <th>User Id</th>
            <th>User Name</th>
            <th>Leave Type</th>
            <th>Total Leaves Taken</th>
            <th>Total Entitled Leaves</th>
            <th>Leaves Balance</th>
        </tr>
    </thead>
</table>

@Section scripts
<script>
    $(document).ready(function () {
        var leaveData = {};
        leaveData.userId = $("#USER_ID").val();
        leaveData.isAdmin = $("#ISADMIN").val();

        var table = $("#summary").DataTable({
            ajax: {
                type: 'POST',
                async: true,
                dataType: "json",
                url: "/api/Summary",
                dataSrc: "",
                data: leaveData
            },
            columns: [
                {
                    data: "USER_ID"
                },
                {
                    data: "USER_NAME"
                },
                {
                    data: "LEAVE_TYPES"
                },
                {
                    data: "TOTAL_LEAVES_TAKEN"
                },
                {
                    data: "ENTITLED_LEAVES"
                },
                {
                    data: "LEAVE_BALANCE"
                }
            ]
        })
    });
</script>
End Section


