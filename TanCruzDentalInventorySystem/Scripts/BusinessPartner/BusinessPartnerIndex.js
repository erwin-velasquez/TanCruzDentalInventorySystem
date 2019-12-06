$(document).ready(function () {

    $('#btn-add-new-business-partner').click(function (e) {
        location.href = CreateBusinessPartnerURL;
    });

    var table2 = $('#businessPartnerListTable').DataTable({
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