﻿var dataTable;
$(document).ready(function () {
    dataTable = loadDataTable();
});

function loadDataTable() {
    
    return $('#tblData').DataTable({
        "ajax": { url:'/admin/user/getall' },
        "columns": [
            { data: 'name', "width": "15%" },
            { data: 'email', "width": "15%" },
            { data: 'phoneNumber', "width": "15%" },
            { data: 'company.name', "width": "15%" },
            { data: 'role', "width": "10%" },
            {
                data: { id: "id", lockoutEnd: "lockoutEnd" },
                "render": function (data) {
                    console.log(data.id);
                    var today = new Date().getTime();
                    var lockout = new Date(data.lockoutEnd).getTime();

                    if (lockout > today) {
                        return `
                        <div class="w-75 btn-group " role="group">
                             <a onclick=LockUnlock('${data.id}') class="btn btn-danger text-white" style="cursor:pointer; width:100px;">
                                <i class="bi bi-unlock-fill">
                                    Lock
                                </i>
                              </a>

                            <a class="btn btn-success text-white" href="/admin/user/RoleManagement/?userId=${data.id}" style="cursor:pointer; width:150px;">
                                <i class="bi bi-pencil-square">
                                    Permission
                                </i>
                              </a>
                        </div>`
                    }
                    else {
                        return `
                        <div class="w-75 btn-group" role="group">
                          <a onclick=LockUnlock('${data.id}') class="btn btn-success text-white" style="cursor:pointer; width:100px;">
                            <i class="bi bi-unlock-fill">
                                Unlock
                            </i>
                          </a>
                        <a href="/admin/user/RoleManagement/?userId=${data.id}" class="btn btn-success text-white" style="cursor:pointer; width:150px;">
                            <i class="bi bi-pencil-square">
                                Permission
                            </i>
                          </a>
                        </div>`
                    }
                },
                "width": "25%"
            }
        ]
    });
}

function LockUnlock(id) {
    $.ajax({
        type: "POST",
        url: "/Admin/User/LockUnlock",
        data: JSON.stringify(id),
        contentType: "application/json",
        success: function (data) {
            if (data.success) {
                toastr.success("ss")
                dataTable.ajax.reload();
            }
        }
    });
}

