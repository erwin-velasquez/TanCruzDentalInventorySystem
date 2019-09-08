$(document).ready(function () {

    var table2 = $('#salesOrderListTable').DataTable({
        "searching": false,
        "language": {
            "processing": "loading....",
            "emptyTable": "No data found"
        },
        "columnDefs": [{
            "orderable": false,
            "targets": 1,
            "className": "center",
            "data": null,
            "defaultContent": '<a href="" class="editor_payment">Add Payment</a>',
            "width": "125px"
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

    $('#salesOrderListTable').on('click', 'a.editor_payment', function (e) {
        e.preventDefault();
        var SalesOrderId = $(this).parents('tr').find('input[name*="SalesOrderId"]')[0].value;
        location.href = SalesOrderPaymentURL + '?salesOrderId=' + SalesOrderId;
    });
});