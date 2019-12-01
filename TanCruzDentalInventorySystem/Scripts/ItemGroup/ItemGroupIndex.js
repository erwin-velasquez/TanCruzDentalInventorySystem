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
        "columnDefs": [
        {
            "data": "ItemGroupId",
            "width": "100px",
            "className": "dt-center",
            "targets": 1
        }],
        "paging": true,
        "autoWidth": false,
        "bLengthChange": false,
        "bPaginate": false
    });
});