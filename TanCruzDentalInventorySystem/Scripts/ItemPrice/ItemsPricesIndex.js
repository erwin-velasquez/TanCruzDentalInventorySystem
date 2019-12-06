$(document).ready(function () {


    $('#ItemsPricesListTable').DataTable({
        "searching": false,
        "language": {
            "processing": "loading....",
            "emptyTable": "No data found"
        },
        "columnDefs": [
            {
                "data": "ItemId",
                "width": "70px",
                "className": "dt-center",
                "targets": 1
            }],
        "paging": true,
        "autoWidth": false,
        "bLengthChange": false,
        "bPaginate": false
    });
});