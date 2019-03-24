
$(document).ready(function () {


    setupGrid();


    var placeholderElement = $('#modal-placeholder');

    $('button[data-toggle="ajax-modal"]').click(function (event) {
        var url = $(this).data('url');
        $.get(url).done(function (data) {
            placeholderElement.html(data);
            placeholderElement.find('.modal').modal('show');
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



});


function setupGrid() {
    var table = $('#usersTable').DataTable({
        "processing": false,
        "serverSide": false,
        "ajax": getUrlPrefix() + "User/_getUserGridData",
        "columns": [
            { "data": "userName" },
            { "data": "displayName" },
            { "data": "primaryPhone" },
            { "data": "email" },
            { "data": "backupPhone" },
            { "defaultContent": "<button>Edit</button>" }
        ]
    });

    $('#usersTable tbody').on( 'click', 'button', function () {
        var data = table.row( $(this).parents('tr') ).data();
        editUser(data.userId);
    });
}

function reloadGridData() {
    $('#usersTable').DataTable().ajax.reload();
}

function editUser(userId) {

    var placeholderElement = $('#modal-placeholder');

    var url = getUrlPrefix() + 'User/EditUserModal/' + userId;
    $.get(url).done(function (data) {
        placeholderElement.html(data);
        placeholderElement.find('.modal').modal('show');
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
}


