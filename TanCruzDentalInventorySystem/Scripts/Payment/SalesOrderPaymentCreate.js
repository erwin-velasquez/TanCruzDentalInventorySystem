$(document).ready(function () {
    var table2 = $('#salesOrderPaymentDetailTable').DataTable({
        "searching": false,
        "language": {
            "processing": "loading....",
            "emptyTable": "No data found"
        },
        "paging": true,
        "autoWidth": false,
        "bLengthChange": false,
        "bPaginate": false
    });
});