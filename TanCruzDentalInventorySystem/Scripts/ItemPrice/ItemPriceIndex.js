$(document).ready(function () {

    $('#btn-add-new-item-price').click(function (e) {
        location.href = CreateItemPriceURL;
    });

    $('#itemPriceListTable').DataTable({
        "searching": false,
        "language": {
            "processing": "loading....",
            "emptyTable": "No data found"
        },
        "columnDefs": [{
            "orderable": false,
            "targets": 1,
            "data": null,
            "className": "center",
            "defaultContent": '<a href="" class="editor_edit">Edit</a> / <a href="" class="editor_view">View</a> / <a href="" class="editor_remove">Delete</a>',
            "width": "125px"
        },
        {
            "data": "ItemGroupId",
            "width": "140px",
            "className": "dt-center",
            "targets": 2
        }],
        "paging": true,
        "autoWidth": false,
        "bLengthChange": false,
        "bPaginate": false
    });

    $('#itemPriceListTable').on('click', 'a.editor_edit', function (e) {
        e.preventDefault();
        var ItemPriceId = $(this).parents('tr').find('input[name*="ItemPriceId"]')[0].value;
        location.href = EditItemPriceRecordUrl + '' + ItemPriceId;
    });

    $('#itemPriceListTable').on('click', 'a.editor_view', function (e) {
        e.preventDefault();
        var ItemPriceId = $(this).parents('tr').find('input[name*="ItemPriceId"]')[0].value;
        location.href = ViewItemPriceRecordUrl + '' + ItemPriceId;
    });
});