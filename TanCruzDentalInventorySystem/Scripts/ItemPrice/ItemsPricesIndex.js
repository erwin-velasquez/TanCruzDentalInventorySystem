$(document).ready(function () {


    $('#ItemsPricesListTable').DataTable({
        "searching": false,
        "language": {
            "processing": "loading....",
            "emptyTable": "No data found"
        },
        //"columnDefs": [{
        //    "orderable": false,
        //    "targets": 1,
        //    "data": null,
        //    "className": "center",
        //    "defaultContent": '<a href="" class="editor_view">View</a>',
        //    "width": "55px"
        //}],
        "paging": true,
        "autoWidth": false,
        "bLengthChange": false,
        "bPaginate": false
    });


    $('#ItemsPricesListTable').on('click', 'a.editor_view', function (e) {
        e.preventDefault();
        var ItemId = $(this).parents('tr').find('input[name*="ItemId"]')[0].value;
        location.href = ViewItemPriceRecordUrl + '' + ItemId;
    });
});