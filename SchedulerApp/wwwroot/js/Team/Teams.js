
$(document).ready(function () {

    setupGrid();
});

function setupGrid() {
    var table = $('#teamsTable').DataTable({
        "processing": false,
        "serverSide": false,
        "ajax": getUrlPrefix() + "Team/_getTeamsGridData",
        "columns": [
            { "data": "teamName" },
            { "data": "teamDescription" },
            { "data": "teamLocation" },
            { "data": "teamEmail" },
            { "data": "teamLeader" },
            { "defaultContent": "<button>Edit</button>" }
        ]
    });

    $('#teamsTable tbody').on( 'click', 'button', function () {
        var data = table.row( $(this).parents('tr') ).data();
       // editUser(data.userId);
        alert('TODO');
    });
}

function reloadGridData() {
    $('#teamsTable').DataTable().ajax.reload();
}
