$(document).ready(function () {

    $('#btn-add-new-sales-order').click(function (e) {
        location.href = CreateSalesOrderURL;
    });

    var table2 = $('#salesOrderListTable').DataTable({
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
            "width": "125px",
        },
        {
            "data": "SalesOrderId",
            "width": "140px",
            "className": "dt-center",
            "targets": 2
        }],
        "paging": true,
        "autoWidth": false,
        "bLengthChange": false,
        "bPaginate": false
    });

    $('#salesOrderListTable').on('click', 'a.editor_edit', function (e) {
        e.preventDefault();
        var SalesOrderId = $(this).parents('tr').find('input[name*="SalesOrderId"]')[0].value;
        location.href = EditSalesOrderRecordUrl + '' + SalesOrderId;
    });

    $('#salesOrderListTable').on('click', 'a.editor_view', function (e) {
        e.preventDefault();
        var SalesOrderId = $(this).parents('tr').find('input[name*="SalesOrderId"]')[0].value;
        location.href = ViewSalesOrderRecordUrl + '' + SalesOrderId;
    });
});