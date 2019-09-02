
$(document).ready(function () {
    var table = $('#purchaseOrderDetailTable').DataTable({
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
});