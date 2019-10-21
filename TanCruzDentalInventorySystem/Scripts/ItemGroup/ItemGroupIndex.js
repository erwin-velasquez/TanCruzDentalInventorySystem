$(document).ready(function () {

    $('#btn-add-new-item-group').click(function (e) {
        location.href = CreateItemGroupURL;
    });

    var table2 = $('#itemGroupListTable').DataTable({
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

    $('#itemGroupListTable').on('click', 'a.editor_edit', function (e) {
        e.preventDefault();
        var ItemGroupId = $(this).parents('tr').find('input[name*="ItemGroupId"]')[0].value;
        location.href = EditItemGroupRecordUrl + '' + ItemGroupId;
    });

    $('#itemGroupListTable').on('click', 'a.editor_view', function (e) {
        e.preventDefault();
        var ItemGroupId = $(this).parents('tr').find('input[name*="ItemGroupId"]')[0].value;
        location.href = ViewItemGroupRecordUrl + '' + ItemGroupId;
    });
});