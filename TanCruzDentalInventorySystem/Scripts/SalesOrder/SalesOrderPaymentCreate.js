$(document).ready(function () { 


    var table = $('#salesOrderPaymentDetailTable').DataTable({
        data: {},
        "searching": false,
        "columns": [
            {
                "data": null,
                "title": "#",
                //"width": "30px",
                "defaultContent": '',
                "className": "dt-center select-checkbox"
            },
            {
                "data": "ItemName",
                "title": "Item Name",
                //"width": "200px",
                "className": "dt-left"
            },
            {
                "data": "ItemId",
                "title": "Item Id",
                //"width": "150px",
                "className": "dt-left"
            },
            {
                "data": "ItemPriceAmount",
                "title": "Item Price",
                //"width": "90px",
                "className": "dt-center"
            },
            {
                "data": "QuantityOnHand",
                "title": "Quantity On-hand",
                //"width": "150px",
                "defaultContent": '',
                "className": "dt-center"
            },
            {
                "data": "Quantity",
                "title": "Quantity",
                //"width": "120px",
                "defaultContent": "<input type='text' />",
                "className": "dt-center"
            },
            {
                "data": "Total",
                "title": "Total",
                //"width": "110px",
                "defaultContent": '',
                "className": "dt-center"
            }
        ],
        createdRow: function (row, data, dataIndex, cells) {
            var td = $(row).find("[class^=truncate]");
            if (td) {
                td.attr("title", td.html());
            }

            $(row).attr("id", "row_" + dataIndex);

            $(cells[1]).attr("id", "td_Item_ItemName_" + String(dataIndex));
            $(cells[1]).append($("<input type='hidden' id='SalesOrder_SalesOrderDetails_" + String(dataIndex) +
                "__Item_ItemName' name='SalesOrder.SalesOrderDetails[" + String(dataIndex) + "].Item.ItemName' value='" + $(cells[1]).html() + "'/>"));

            $(cells[2]).attr("id", "td_Item_ItemId_" + String(dataIndex));
            $(cells[2]).append($("<input type='hidden' id='SalesOrder_SalesOrderDetails_" + String(dataIndex) +
                "__Item_ItemId' name='SalesOrder.SalesOrderDetails[" + String(dataIndex) + "].Item.ItemId' value='" + $(cells[2]).html() + "'/>"));

            $(cells[3]).attr("id", "td_ItemPriceAmount_" + String(dataIndex));
            $(cells[3]).append($("<input type='hidden' id='SalesOrder_SalesOrderDetails_" + String(dataIndex) +
                "__ItemPriceAmount' name='SalesOrder.SalesOrderDetails[" + String(dataIndex) + "].ItemPriceAmount' value='" + $(cells[3]).html() + "'/>"));

            $(cells[4]).attr("id", "td_QuantityOnHand_" + String(dataIndex));
            $(cells[4]).append($("<input type='hidden' id='SalesOrder_SalesOrderDetails_" + String(dataIndex) +
                "__QuantityOnHand' name='SalesOrder.SalesOrderDetails[" + String(dataIndex) + "].QuantityOnHand' value='" + $(cells[4]).html() + "'/>"));

            $(cells[5]).attr("id", "td_Quantity_" + String(dataIndex));
            $(row).find("td input[type*='text']").each(function () {
                $(this).attr("id", "SalesOrder_SalesOrderDetails_" + String(dataIndex) + "__Quantity");
                $(this).attr("name", "SalesOrder.SalesOrderDetails[" + String(dataIndex) + "].Quantity");

                $(this).on("focusout", function () {

                    var quantity = parseFloat(!$(this).parent().parent().find("td input[type*='text']").val() ? 0 : $(this).parent().parent().find("td input[type*='text']").val());
                    var itemPrice = parseFloat($(this).parent().parent().find("td[id^='td_ItemPriceAmount']").text());
                    var itemQOH = parseFloat($(this).parent().parent().find("td[id^='td_QuantityOnHand']").text());

                    var total = quantity * itemPrice;
                    var DocumentTotal = 0.0;

                    if (itemQOH < quantity) {
                        $(this).parent().parent().addClass("error");
                        $(this).focus();
                    }

                    $(this).parent().parent().find("td[id^='td_SalesOrderDetailTotal']").text(total.toFixed(2));

                    //Document Total Computation
                    $.each($('#salesOrderDetailTable').find("td[id^='td_SalesOrderDetailTotal']"), function (index, value) {
                        DocumentTotal = parseFloat(DocumentTotal) + parseFloat(!$(this).text() ? 0 : $(this).text());
                    });

                    $("#SalesOrder_SalesOrderTax").val(parseFloat(DocumentTotal / 9.33).toFixed(2));
                    $("#SalesOrder_SalesOrderTotal").val(DocumentTotal.toFixed(2));
                });
            });

            $(cells[6]).attr("id", "td_SalesOrderDetailTotal_" + String(dataIndex));
            $(cells[6]).append($("<input type='hidden' id='SalesOrder_SalesOrderDetails_" + String(dataIndex) +
                "__SalesOrderDetailTotal' name='SalesOrder.SalesOrderDetails[" + String(dataIndex) + "].SalesOrderDetailTotal' value='" + !$(cells[6]).html() ? "0" : $(cells[5]).html() + "'/>"));


        },
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
        "bPaginate": false,
        buttons: [
            {
                text: 'Reload',
                action: function (e, dt, node, config) {
                    dt.ajax.reload();
                }
            }
        ]
    });

    var table2 = $('#BusinessPartnerSearchTable').DataTable({
        "searching": false,
        "language": {
            "processing": "loading....",
            "emptyTable": "No data found"
        },
        "bLengthChange": false,
        "select": {
            style: 'single',
            selector: 'td'
        }
    });

    table2.on('select', function (e, dt, type, indexes) {
        console.log('asdf');
        var rowData = table2.rows(indexes).data().toArray();
        var BusinessPartnerId = $(rowData[0][0])[0].defaultValue;

        var BusinessPartnerName = $('<div>' + rowData[0][0] + '</div>').text();

        $('#SalesOrderPayment_BusinessPartner_BusinessPartnerName').val(BusinessPartnerName);
        $('#SalesOrderPayment_BusinessPartner_BusinessPartnerId').val(BusinessPartnerId);
    });

    $('#SalesOrderPayment_BusinessPartner_BusinessPartnerName').on('click', function () {
        $('#BusinessPartnerSearchModal').modal("show");
    });

    $('#SalesOrderPayment_BusinessPartner_BusinessPartnerName').on('change', function () {
        if ($('#SalesOrder_BusinessPartner_BusinessPartnerName').val() === "") {
            $('#SalesOrder_BusinessPartner_BusinessPartnerId').val("");
        }
    });
});