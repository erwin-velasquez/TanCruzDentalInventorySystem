
$(document).ready(function () {
    var table = $('#purchaseOrderDetailTable').DataTable({
        "searching": false,
        "language": {
            "processing": "loading....",
            "emptyTable": "No data found"
        },
        "select": {
            style: 'os',
            selector: 'td'
        },
        "paging": true,
        "bLengthChange": false,
        "bPaginate": false,
        "createdRow": function (row) {
            var td = $(row).find("[class^=truncate]");
            if (td) {
                td.attr("title", td.html());
            }
        },
        "columnsDef": [
            {
                "target": 0,
                "data": null,
                "title": "#",
                "className": "dt-center select-checkbox"
            }
        ]
    });

    //Date pickers
    $("#PurchaseOrder_DeliveryDate").datepicker();
    $("#PurchaseOrder_DeliveryDate").on("change", function () {
        $("#MainForm").validate().element("#PurchaseOrder_DeliveryDate");
    });

    $("#PurchaseOrder_PostingDate").datepicker();
    $("#PurchaseOrder_PostingDate").on("change", function () {
        $("#MainForm").validate().element("#PurchaseOrder_PostingDate");
    });

    $("#PurchaseOrder_DocumentDate").datepicker();
    $("#PurchaseOrder_DocumentDate").on("change", function () {
        $("#MainForm").validate().element("#PurchaseOrder_DocumentDate");
    });
});