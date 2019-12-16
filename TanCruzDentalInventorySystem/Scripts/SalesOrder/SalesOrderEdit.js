
$(document).ready(function () {
    var table = $('#salesOrderDetailTable').DataTable({
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
    $("#SalesOrder_DeliveryDate").datepicker();
    $("#SalesOrder_DeliveryDate").on("change", function () {
        $("#MainForm").validate().element("#SalesOrder_DeliveryDate");
    });

    $("#SalesOrder_PostingDate").datepicker();
    $("#SalesOrder_PostingDate").on("change", function () {
        $("#MainForm").validate().element("#SalesOrder_PostingDate");
    });

    $("#SalesOrder_DocumentDate").datepicker();
    $("#SalesOrder_DocumentDate").on("change", function () {
        $("#MainForm").validate().element("#SalesOrder_DocumentDate");
    });

    $("#printButton").on("click", function (e) {
        $.get('/SalesOrder/PrintSalesOrderReceipt/', { SalesOrderId: $("#SalesOrder_SalesOrderId").val() }, function (data) { //Replace with global URL not hardcoded

            alert(data);
        });
    });
});