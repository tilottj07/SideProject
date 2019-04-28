
$(document).ready(function () {

    $('#TeamIdParam').select2();
    $('#TeamIdParam').change(function(e) { reloadGridData(); });

 

    
    setupGrid();

    var placeholderElement = $('#modal-placeholder');

    $('button[data-toggle="ajax-modal"]').click(function (event) {
        var url = $(this).data('url');
        $.get(url).done(function (data) {
            placeholderElement.html(data);
            placeholderElement.find('.modal').modal('show');
            instantiateModal();
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
            else {
                instantiateModal();
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
        "createdRow": function( row, data, dataIndex){
                if( data.isSelected ==  `true`){
                    $(row).addClass('bg-warning');
                }
            },
        "ajax": getUrlPrefix() + "Schedule/_getSchedulesGridData/?startDate=" + $('#StartDate').val() + '&endDate=' + $('#EndDate').val() + '&teamId=' + $('#TeamIdParam').val(),
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
    //alert('woot');
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
            instantiateModal();
        });
    }
}

function instantiateModal() {
    $('#TeamId').select2();
    $('#UserId').select2();
    $('#SupportLevel').select2();

    populateUserSelectList();

    $("TeamId").change(function(e) { populateUserSelectList(); });
}

function populateUserSelectList() {
    $.getJSON(getUrlPrefix() + "Schedule/GetTeamUsersSelectList/?teamId=" + $('#TeamId').val() + "&userId=" + $('#UserId').val(), function(data){
        $('#UserId').empty();
        $.each(data, function(index, value) {
                if (value.selected == 'true') {
                    $('#UserId').append('<option value="' + value.value + '" selected="selected">' + value.text + '</option>');
                }
                else {
                    $('#UserId').append('<option value="' + value.value + '">' + value.text + '</option>');
                }
            });
      });
}


