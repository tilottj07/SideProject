
$(document).ready(function () {

    setupGrid();


    var placeholderElement = $('#modal-placeholder');

    $('button[data-toggle="ajax-modal"]').click(function (event) {
        var url = $(this).data('url');
        $.get(url).done(function (data) {
            placeholderElement.html(data);
            placeholderElement.find('.modal').modal('show');

            $('#TeamUserIds').multiselect();
        });
    });
    

    placeholderElement.on('click', '[data-save="modal"]', function (event) {
        event.preventDefault();
        
        var form = $(this).parents('.modal').find('form');
        var actionUrl = form.attr('action');
        var dataToSend = form.serialize();
        

        $.post(actionUrl, dataToSend).done(function (data) {

            var newBody = $('.modal-body', data);
            placeholderElement.find('.modal-body').replaceWith(newBody);

            var isValid = newBody.find('[name="IsValid"]').val() == 'True';
            if (isValid) {
                placeholderElement.find('.modal').modal('hide');
                reloadGridData();
            }
            
        });
    }); 

    placeholderElement.on('click', '[data-save="delete"]', function (event) {
        event.preventDefault();
        var form = $(this).parents('.modal').find('form');
        var actionUrl = getUrlPrefix() + 'Team/_deleteTeam/' + $('#TeamId').val();
        var dataToSend = form.serialize();

        $.post(actionUrl, dataToSend).done(function (data) {
            placeholderElement.find('.modal').modal('hide');
            reloadGridData();
        });
    });
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
        editTeam(data.teamId);
    });
}

function reloadGridData() {
    $('#teamsTable').DataTable().ajax.reload();
}


function editTeam(teamId) {

    var url = getUrlPrefix() + 'Team/EditTeamModal/' + teamId;
    editTeamModal(url); 
}


function editTeamModal(url) {
    var placeholderElement = $('#modal-placeholder');

    if (url != null) {
        $.get(url).done(function (data) {
            placeholderElement.html(data);
            placeholderElement.find('.modal').modal('show');
        });
    }
}
