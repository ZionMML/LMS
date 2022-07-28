@ModelType IEnumerable(Of LMS.Models.Admins)
@Code
    ViewData("Title") = "Admin_List"
    Layout = "~/Views/Shared/_Layout.vbhtml"
End Code

<h2>Admin Info List</h2>

<p>
    @Html.ActionLink("New Admin", "Create", "Admin", Nothing, New With {.class = "btn btn-primary"})
</p>

@Using (Html.BeginForm())

    @Html.AntiForgeryToken()
    @<div class="form-horizontal">
        @Html.ValidationSummary(False, "", New With {.class = "text-danger"})
    </div>
End Using
<Table id="admins" Class="table table-bordered table-hover">
    <thead>
        <tr>
            <th>ID</th>
            <th>User Id</th>
            <th>User Name</th>
            <th>Email Address</th>
            <th>Created By</th>
            <th>Created Date</th>
            <th>Updated By</th>
            <th>Updated Date</th>
            <th>Edit</th>
            <th>Delete</th>
        </tr>
    </thead>
    <tbody></tbody>
</table>

@Section scripts
    <script>
        $(document).ready(function () {
            var table = $("#admins").DataTable({
                ajax: {
                    url: "/api/Admin",
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
                        data: "EMAIL_ADDR"
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
                            var myUrl = '@Url.Action("Edit", "Admin")/' + data;
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
        });
    </script>
End Section 
