
$(document).ready(function () {
    var table = $('#salesOrderDetailTable').DataTable({
        "searching": false,
        "language": {
            "processing": "loading....",
            "emptyTable": "No data found"
        },
        "paging": true,
        "bLengthChange": false,
        "bPaginate": false,
        "createdRow": function (row) {
            var td = $(row).find("[class^=truncate]");
            if (td) {
                td.attr("title", td.html());
            }
        }
    });

    $("#printButton").on("click", function (e) {
        $.get('/SalesOrder/PrintSalesOrderReceipt/', { SalesOrderId: $("#SalesOrderId").val() }, function (data) { //Replace with global URL not hardcoded

            alert(data);
        });
    });
});