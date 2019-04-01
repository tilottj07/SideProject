
$(document).ready(function () {

    $('#TeamId').select2();
    $('#SearchButton').click(function () { refreshGridData(); });

    
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

    placeholderElement.on('click', '[data-save="delete"]', function (event) {
        event.preventDefault();
        var form = $(this).parents('.modal').find('form');
        var actionUrl = getUrlPrefix() + 'Location/_deleteLocation/' + $('#LocationId').val();
        var dataToSend = form.serialize();

        $.post(actionUrl, dataToSend).done(function (data) {
            placeholderElement.find('.modal').modal('hide');
            reloadGridData();
        });
    });
});

function setupGrid() {
    var table = $('#schedulesTable').DataTable({
        "processing": false,
        "serverSide": false,
        "ajax": getUrlPrefix() + "Schedule/_getSchedulesGridData/?startDate=" + $('#StartDate').val() + '&endDate=' + $('#EndDate').val() + '&teamId=' + $('#TeamId').val(),
        "columns": [
            { "data": "displayName" },
            { "data": "teamName" },
            { "data": "supportLevel" },
            { "data": "startDate" },
            { "data": "endDate" },
            { "defaultContent": "<button>Edit</button>" }
        ]
    });

    $('#schedulesTable tbody').on( 'click', 'button', function () {
        var data = table.row( $(this).parents('tr') ).data();
        editLocation(data.locationId);
    });
}

function reloadGridData() {
    $('#schedulesTable').DataTable().ajax.reload();
}


function editLocation(locationId) {

    var url = getUrlPrefix() + 'Location/EditLocationModal/' + locationId;
    editLocationModal(url); 
}


function editLocationModal(url) {
    var placeholderElement = $('#modal-placeholder');

    if (url != null) {
        $.get(url).done(function (data) {
            placeholderElement.html(data);
            placeholderElement.find('.modal').modal('show');
        });
    }
}


