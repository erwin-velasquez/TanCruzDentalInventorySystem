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
        "columnDefs": [{
            "orderable": false,
            "targets": 1,
            "data": null,
            "className": "center",
            "defaultContent": '<a href="" class="editor_edit">Edit</a> / <a href="" class="editor_view">View</a> / <a href="" class="editor_remove">Delete</a>',
            "width": "125px"
        },
        {
            "data": "BusinessPartnerId",
            "width": "140px",
            "className": "dt-center",
            "targets": 2
        }],
        "paging": true,
        "autoWidth": false,
        "bLengthChange": false,
        "bPaginate": false
    });

    $('#businessPartnerListTable').on('click', 'a.editor_edit', function (e) {
        e.preventDefault();
        var BusinessPartnerId = $(this).parents('tr').find('input[name*="BusinessPartnerId"]')[0].value;
        location.href = EditBusinessPartnerRecordUrl + '' + BusinessPartnerId;
    });

    $('#businessPartnerListTable').on('click', 'a.editor_view', function (e) {
        e.preventDefault();
        var BusinessPartnerId = $(this).parents('tr').find('input[name*="BusinessPartnerId"]')[0].value;
        location.href = ViewBusinessPartnerRecordUrl + '' + BusinessPartnerId;
    });
});