
$(document).ready(function () {

    $("#confirm").on('click', function (e) {
        if (e.preventDefault) { e.preventDefault(); }
        alertify.confirm("This is a confirm dialog", function (e) {
            if (e) {
                alertify.success("You've clicked OK");
            } else {
                alertify.error("You've clicked Cancel");
            }
        });
    });

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

        var categoryName = $('#CategoryName').val();


        alertify.confirm('Delete Category', 'Delete ' + categoryName + '?',
            function () { alertify.success('Ok'); deleteItem(); },
            function () { alertify.error('Cancel') });
                             
    });
});

function deleteItem () {
    var form = $('#EditCategoryModalForm');
    var actionUrl = getUrlPrefix() + 'Category/_deleteCategory/' + $('#CategoryId').val();
    var dataToSend = form.serialize();

    $.post(actionUrl, dataToSend).done(function (data) {
        placeholderElement.find('.modal').modal('hide');
        reloadGridData();
    });
}

function setupGrid() {
    var table = $('#categoriesTable').DataTable({
        "processing": false,
        "serverSide": false,
        "ajax": getUrlPrefix() + "Category/_getCategoriesGridData",
        "columns": [
            { "data": "categoryName" },
            { "data": "categoryDescription" },
            { "data": "categoryEmail" },
            { "defaultContent": "<button>Edit</button>" }
        ]
    });

    $('#categoriesTable tbody').on('click', 'button', function () {
        var data = table.row($(this).parents('tr')).data();
        editCategory(data.categoryId);
    });
}

function reloadGridData() {
    $('#categoriesTable').DataTable().ajax.reload();
}


function editCategory(categoryId) {

    var url = getUrlPrefix() + 'Category/EditCategoryModal/' + categoryId;
    editCategoryModal(url);
}


function editCategoryModal(url) {
    var placeholderElement = $('#modal-placeholder');

    if (url != null) {
        $.get(url).done(function (data) {
            placeholderElement.html(data);
            placeholderElement.find('.modal').modal('show');
        });
    }
}


function testAlert() {

    alertify.alert('woot');
}
