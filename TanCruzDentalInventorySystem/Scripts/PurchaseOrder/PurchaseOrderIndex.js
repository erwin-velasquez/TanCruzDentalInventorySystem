$(document).ready(function () {

    $('#btn-add-new-sales-order').click(function (e) {
        location.href = CreatePurchaseOrderURL;
    });

    var table2 = $('#purchaseOrderListTable').DataTable({
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
            "data": "PurchaseOrderId",
            "width": "140px",
            "className": "dt-center",
            "targets": 2
        }],
        "paging": true,
        "autoWidth": false,
        "bLengthChange": false,
        "bPaginate": false
    });

    $('#purchaseOrderListTable').on('click', 'a.editor_edit', function (e) {
        e.preventDefault();
        var PurchaseOrderId = $(this).parents('tr').find('input[name*="PurchaseOrderId"]')[0].value;
        location.href = EditPurchaseOrderRecordUrl + '' + PurchaseOrderId;
    });

    $('#purchaseOrderListTable').on('click', 'a.editor_view', function (e) {
        e.preventDefault();
        var PurchaseOrderId = $(this).parents('tr').find('input[name*="PurchaseOrderId"]')[0].value;
        location.href = ViewPurchaseOrderRecordUrl + '' + PurchaseOrderId;
    });
});