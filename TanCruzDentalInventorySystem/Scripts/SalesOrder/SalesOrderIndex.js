$(document).ready(function () {

    $('#btn-add-new-sales-order').click(function (e) {
        location.href = '';
    });

    var table = $('#salesOrderListTable1').DataTable({
        "scrollX": true,
        "ajax": {
            "type": "GET",
            "url": GetSalesOrderListURL,
            "dataType": "JSON"
        },
        "columns": [
            {
                "data": null,
                "className": "center",
                "defaultContent": '<a href="" class="editor_edit">Edit</a> / <a href="" class="editor_view">View</a> / <a href="" class="editor_remove">Delete</a>',
                "width": "125px",
            },
            {
                "data": "SalesOrderId",
                "title": "Sales Order Id",
                "width": "140px",
                "className": "dt-center"
            },
            {
                "data": "SalesOrderControlNumber",
                "title": "PO Control Number",
                "width": "150px",
                "className": "dt-center"
            },
            {
                "data": "BusinessPartner.BusinessPartnerName",
                "title": "Business Partner",
                "width": "240px",
                "className": "dt-center"
            },
            {
                "data": "Currency.CurrencyName",
                "title": "Currency",
                "width": "120p",
                "className": "dt-center"
            },
            {
                "data": "SalesOrderStatus",
                "title": "Status",
                "className": "dt-center",
            },
            {
                "data": "DeliveryDate",
                "title": "Delivery Date",
                "width": "120px",
                "className": "dt-center",
                "render": function (data) {
                    return moment(data).format('MM/DD/YYYY');
                }
            },
            {
                "data": "PostingDate",
                "title": "Posting Date",
                "width": "120px",
                "className": "dt-center",
                "render": function (data) {
                    return moment(data).format('MM/DD/YYYY');
                }
            },
            {
                "data": "DocumentDate",
                "title": "Document Date",
                "width": "120px",
                "className": "dt-center",
                "render": function (data) {
                    return moment(data).format('MM/DD/YYYY');
                }
            },
            {
                "data": "Remarks",
                "title": "Remarks",
                "width": "120px",
                "className": "truncate-remarks"
            },
            {
                "data": "RefDocNumber",
                "title": "Ref Doc Num",
                "width": "140px",
                "className": "dt-left",
            },
            {
                "data": "SalesOrderDiscount",
                "title": "PO Discount Amount",
                "width": "190px",
                "className": "dt-right"
            },
            {
                "data": "SalesOrderDiscountAmount",
                "title": "SO Order Amount",
                "width": "140px",
                "className": "dt-right"
            },
            {
                "data": "SalesOrderTax",
                "title": "SO Tax",
                "width": "70px",
                "className": "dt-right"
            },
            {
                "data": "SalesOrderTotal",
                "title": "SO Total",
                "width": "70px",
                "className": "dt-right"
            },
            {
                "data": "UserId",
                "title": "User Id",
                "width": "130px",
                "className": "dt-left"
            }
        ],
        "language": {
            "processing": "loading....",
            "emptyTable": "No data found"
        },
        createdRow: function (row) {
            var td = $(row).find("[class^=truncate]");
            if (td) {
                td.attr("title", td.html());
            }
        },
        "paging": true,
        "autoWidth": true,
    });

    var table2 = $('#salesOrderListTable').DataTable({
        "searching": false,
        "language": {
            "processing": "loading....",
            "emptyTable": "No data found"
        },
        "select": {
            style: 'os',
            selector: 'td'
        },
        "columnDefs": [{
            "orderable": false,
            "targets": 1,
            "data": null,
            "className": "center",
            "defaultContent": '<a href="" class="editor_edit">Edit</a> / <a href="" class="editor_view">View</a> / <a href="" class="editor_remove">Delete</a>',
            "width": "125px",
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

    $('#salesOrderListTable').on('click', 'a.editor_edit', function (e) {
        e.preventDefault();
        var SalesOrderId = $(this).parents('tr').find('input[name*="SalesOrderId"]')[0].value;
        location.href = EditSalesOrderRecordUrl + '' + SalesOrderId;
    });

    $('#salesOrderListTable').on('click', 'a.editor_view', function (e) {
        e.preventDefault();
        var SalesOrderId = $(this).parents('tr').find('input[name*="SalesOrderId"]')[0].value;
        location.href = ViewSalesOrderRecordUrl + '' + SalesOrderId;
    });
});