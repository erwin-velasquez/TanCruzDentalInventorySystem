$(document).ready(function () {

    var table = $('#itemHomeTable').DataTable({
        "language": {
            "processing": "loading....",
            "emptyTable": "No data found"
        },
        select: {
            style: 'os',
            selector: 'td'
        },
        "paging": true,
        "autoWidth": true,
        "bLengthChange": false,
        "bPaginate": false
    });

});