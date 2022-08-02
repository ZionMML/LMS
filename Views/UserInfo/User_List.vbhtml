@ModelType IEnumerable(Of LMS.Models.Users)
@Code
    ViewData("Title") = "User_List"
    Layout = "~/Views/Shared/_Layout.vbhtml"
End Code

<h2>User Info List</h2>
<p>
    @Html.ActionLink("New User", "Create", "UserInfo", Nothing, New With {.class = "btn btn-primary"})
</p>
@Using (Html.BeginForm())

    @Html.AntiForgeryToken()
    @<div class="form-horizontal">
        @Html.ValidationSummary(False, "", New With {.class = "text-danger"})
    </div>
End Using
<Table id="users" Class="table table-bordered table-hover">
    <thead>
        <tr>
            <th>ID</th>
            <th>User Id</th>
            <th>User Name</th>
            <th>Company</th>
            <th>Team Lead Name</th>
            <th>Entitled Annual Leaves</th>
            <th>Annual Leave Balance</th>
            <th>Entitled Medical Leaves</th>
            <th>Medical Leave Balance</th>
            <th>Entitled Other Leaves</th>
            <th>Other Leave Balance</th>
            <th>Created By</th>
            <th>Created Date</th>
            <th>Updated By</th>
            <th>Updated Date</th>
            <th>Edit</th>
            <th>Delete</th>
        </tr>
    </thead>
    <tbody></tbody>
</Table>
@Section scripts
    <script>
        $(document).ready(function () {
            $.fn.dataTable.ext.errMode = 'throw';

            var table = $("#users").DataTable({
                ajax: {
                    url: "/api/UserInfo",
                    dataSrc: ""
                },
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
                        data: "COMPANY"
                    },
                    {
                        data: "TEAM_LEAD_NAME"
                    },
                    {
                        data: "MAX_ANNUAL_LEAVE"
                    },
                    {
                        data: "ANNUAL_LEAVE_BALANCE"
                    },
                    {
                        data: "MAX_MEDICAL_LEAVE"
                    },
                    {
                        data: "MEDICAL_LEAVE_BALANCE"
                    },
                    {
                        data: "MAX_OTHER_LEAVE"
                    },
                    {
                        data: "OTHER_LEAVE_BALANCE"
                    },
                    {
                        data: "CREATED_BY"
                    },
                    {
                        data: "CREATED_DATE"
                    },
                    {
                        data: "UPDATED_BY"
                    },
                    {
                        data: "UPDATED_DATE"
                    },
                    {
                        data: "ID",
                        render: function (data) {
                            var myUrl = '@Url.Action("Edit", "UserInfo")/' + data;
                            return '<a href=\"' + myUrl + '\" class=\"btn-link\">Edit</a>';
                        }
                    },
                    {
                        data: "ID",
                        render: function (data) {
                            return "<button class='btn-link js-delete' data-param-id=" + data + ">Delete</button>";
                        }
                    }
                ],
                "columnDefs": [
                    {
                        "targets:": [0],
                        "visible": false,
                        "searchable": false
                    }
                ]
            });

            $("#users").on("click", ".js-delete", function () {
                var button = $(this);

                bootbox.confirm("Are you sure you want to delete this record?", function (result) {
                    if (result) {
                        var userinfoData = {};
                        userinfoData.ID = button.attr("data-param-id");

                        $.ajax({
                            type: 'POST',
                            async: true,
                            dataType: "json",
                            url: "/api/UserInfo/DeleteValue",
                            data: userinfoData
                        })
                            .done(function (data) {
                                if (data.ErrMsg) {
                                    toastr.error(data.ErrMsg);
                                }
                                table.rows(button.parents("tr")).remove().draw();
                            })
                            .fail(function (data) {
                                toastr.error(data.ErrMsg);
                            })
                    }
                });
            });
        });
    </script>
End Section
