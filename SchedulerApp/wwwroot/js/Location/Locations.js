
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
    var table = $('#locationsTable').DataTable({
        "processing": false,
        "serverSide": false,
        "ajax": getUrlPrefix() + "Location/_getLocationsGridData",
        "columns": [
            { "data": "locationName" },
            { "data": "description" },
            { "data": "address" },
            { "data": "city" },
            { "data": "stateRegion" },
            { "data": "country" },
            { "data": "zipCode" },
            { "defaultContent": "<button>Edit</button>" }
        ]
    });

    $('#locationsTable tbody').on( 'click', 'button', function () {
        var data = table.row( $(this).parents('tr') ).data();
        editLocation(data.locationId);
    });
}

function reloadGridData() {
    $('#locationsTable').DataTable().ajax.reload();
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

