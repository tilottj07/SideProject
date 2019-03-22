
$(document).ready(function () {


    $('#usersTable').DataTable({
        "processing": false,
        "serverSide": false,
        "ajax": getUrlPrefix() + "User/_getUserGridData",
        "columns": [
            { "data": "userName" },
            { "data": "displayName" },
            { "data": "firstName" },
            { "data": "middleInitial" },
            { "data": "lastName" }
        ]
    });


 



});

