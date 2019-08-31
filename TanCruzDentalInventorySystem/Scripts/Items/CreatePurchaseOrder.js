﻿$(document).ready(function () {

    $("#MainForm").submit(function (e) {
        $("#MainForm").validate();
    });

    var itemsTable = $('#purchaseOrderDetailTable').DataTable({
        data: {},
        "searching": false,
        "columns": [
            {
                "data": null,
                "title": "#",
                "width": "30px",
                "defaultContent": '',
                "className": "dt-center select-checkbox"
            },
            {
                "data": "ItemName",
                "title": "Item Name",
                "width": "200px",
                "className": "dt-left"
            },
            {
                "data": "ItemId",
                "title": "Item Id",
                "width": "150px",
                "className": "dt-left"
            },
            {
                "data": "ItemPriceAmount",
                "title": "Item Price",
                "width": "90px",
                "className": "dt-center"
            },
            {
                "data": null,
                "title": "Quantity On-hand",
                "width": "150px",
                "defaultContent": '',
                "className": "dt-center"
            },
            {
                "data": "Quantity",
                "title": "Quantity",
                "width": "120px",
                "defaultContent": "<input type='text' />",
                "className": "dt-center"
            },
            {
                "data": "Total",
                "title": "Total",
                "width": "110px",
                "defaultContent": '',
                "className": "dt-center"
            }
        ],
        "language": {
            "processing": "loading....",
            "emptyTable": "No data found"
        },
        select: {
            style: 'os',
            selector: 'td'
        },
        createdRow: function (row, data, dataIndex, cells) {
            var td = $(row).find("[class^=truncate]");
            if (td) {
                td.attr("title", td.html());
            }

            $(row).attr("id", "row_" + dataIndex);

            $(cells[1]).attr("id", "td_Item_ItemName_" + String(dataIndex));
            $(cells[1]).append($("<input type='hidden' id='PurchaseOrder_PurchaseOrderDetails_" + String(dataIndex) +
                "__Item_ItemName' name='PurchaseOrder.PurchaseOrderDetails[" + String(dataIndex) + "].Item.ItemName' value='" + $(cells[1]).html() + "'/>"));

            $(cells[2]).attr("id", "td_Item_ItemId_" + String(dataIndex));
            $(cells[2]).append($("<input type='hidden' id='PurchaseOrder_PurchaseOrderDetails_" + String(dataIndex) +
                "__Item_ItemId' name='PurchaseOrder.PurchaseOrderDetails[" + String(dataIndex) + "].Item.ItemId' value='" + $(cells[2]).html() + "'/>"));

            $(cells[3]).attr("id", "td_ItemPriceAmount_" + String(dataIndex));
            $(cells[3]).append($("<input type='hidden' id='PurchaseOrder_PurchaseOrderDetails_" + String(dataIndex) +
                "__ItemPriceAmount' name='PurchaseOrder.PurchaseOrderDetails[" + String(dataIndex) + "].ItemPriceAmount' value='" + $(cells[3]).html() + "'/>"));

            $(cells[4]).attr("id", "td_QuantityOnHand_" + String(dataIndex));
            $(cells[4]).append($("<input type='hidden' id='PurchaseOrder_PurchaseOrderDetails_" + String(dataIndex) +
                "__QuantityOnHand' name='PurchaseOrder.PurchaseOrderDetails[" + String(dataIndex) + "].QuantityOnHand' value='" + $(cells[4]).html() + "'/>"));

            $(cells[5]).attr("id", "td_Quantity_" + String(dataIndex));
            $(row).find("td input[type*='text']").each(function () {
                $(this).attr("id", "PurchaseOrder_PurchaseOrderDetails_" + String(dataIndex) + "__Quantity");
                $(this).attr("name", "PurchaseOrder.PurchaseOrderDetails[" + String(dataIndex) + "].Quantity");

                $(this).on("focusout", function () {

                    var quantity = parseFloat(!$(this).parent().parent().find("td input[type*='text']").val() ? 0 : $(this).parent().parent().find("td input[type*='text']").val());
                    var itemPrice = parseFloat($(this).parent().parent().find("td[id^='td_ItemPriceAmount']").text());

                    var total = quantity * itemPrice;
                    var DocumentTotal = 0.0;

                    $(this).parent().parent().find("td[id^='td_PurchaseOrderDetailTotal']").text(total.toFixed(2));

                    //Document Total Computation
                    $.each($('#PurchaseOrderDetailTable').find("td[id^='td_PurchaseOrderDetailTotal']"), function (index, value) {
                        DocumentTotal = parseFloat(DocumentTotal) + parseFloat(!$(this).text() ? 0 : $(this).text());
                    });

                    $("#PurchaseOrder_PurchaseOrderTax").val(parseFloat(DocumentTotal / 9.33).toFixed(2));
                    $("#PurchaseOrder_PurchaseOrderTotal").val(DocumentTotal.toFixed(2));
                });
            });

            $(cells[6]).attr("id", "td_PurchaseOrderDetailTotal_" + String(dataIndex));
            $(cells[6]).append($("<input type='hidden' id='PurchaseOrder_PurchaseOrderDetails_" + String(dataIndex) +
                "__PurchaseOrderDetailTotal' name='PurchaseOrder.PurchaseOrderDetails[" + String(dataIndex) + "].PurchaseOrderDetailTotal' value='" + !$(cells[6]).html() ? "0" : $(cells[5]).html() + "'/>"));


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

    var itemSearchTable = $('#ItemSearchTable').DataTable({
        data: {},
        "columns": [
            {
                "data": null,
                "title": "#",
                "width": "30px",
                "defaultContent": '',
                "className": "dt-center"
            },
            {
                "data": "ItemName",
                "title": "Item Name",
                "width": "200px",
                "className": "dt-left"
            },
            {
                "data": "ItemId",
                "title": "Item Id",
                "width": "150px",
                "className": "dt-left"
            },
            {
                "data": "ItemPriceAmount",
                "title": "Item Price",
                "width": "90px",
                "className": "dt-center"
            },
            {
                "data": null,
                "title": "Quantity On-hand",
                "width": "150px",
                "defaultContent": '',
                "className": "dt-center"
            },
            {
                "data": null,
                "title": "Quantity",
                "width": "120px",
                "defaultContent": '',
                "className": "dt-center"
            }
        ],
        columnDefs: [{
            orderable: false,
            className: 'select-checkbox',
            targets: 0
        },
        {
            targets: 1,
            'createdCell': function (td, cellData, rowData, row, col) {
                $(td).attr('id', 'ItemName');
            }
        },
        {
            targets: 2,
            'createdCell': function (td, cellData, rowData, row, col) {
                $(td).attr('id', 'ItemId');
            }
        }],
        select: {
            style: 'os',
            selector: 'td'
        },
        order: [[1, 'asc']],
        "language": {
            "processing": "loading....",
            "emptyTable": "No data found"
        },
        createdRow: function (row, data, dataIndex) {
            var td = $(row).find("[class^=truncate]");
            if (td) {
                td.attr("title", td.html());
            };
            $(row).attr('id', 'someID');
        },
        "paging": true,
        "autoWidth": false,
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

    var businessPartnerSearchTable = $('#BusinessPartnerSearchTable').DataTable({
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

    businessPartnerSearchTable.on('select', function (e, dt, type, indexes) {
        console.log('asdf');
        var rowData = businessPartnerSearchTable.rows(indexes).data().toArray();
        var BusinessPartnerId = $(rowData[0][0])[0].defaultValue;

        var BusinessPartnerName = $('<div>' + rowData[0][0] + '</div>').text();

        $('#PurchaseOrder_BusinessPartner_BusinessPartnerName').val(BusinessPartnerName);
        $('#PurchaseOrder_BusinessPartner_BusinessPartnerId').val(BusinessPartnerId);
    });

    
    $('#searchItemButton').on('click', function () {
        $('#ItemSearchModal').modal("show");

        var itemList;
        var obj;

        $.get('/Item/GetItemList', function (data) { //Replace with global URL not hardcoded
            obj = data;

            itemSearchTable.clear();
            itemSearchTable.rows.add(obj);
            itemSearchTable.draw();

        });
    });

    $('#doneButton').on('click', function () {

        $("#ItemSearchTable tbody tr.selected td#ItemId").each(function (index, value) {

            $.get('/Item/GetSingleItem/', { itemId: $(this).text() }, function (data) { //Replace with global URL not hardcoded
                obj = [
                    data
                ];

                itemsTable.rows.add(obj);
                itemsTable.draw();
            });
        });

        $('#ItemSearchModal').modal("hide");
    });

    $('#PurchaseOrder_BusinessPartner_BusinessPartnerName').on('click', function () {
        $('#BusinessPartnerSearchModal').modal("show");
    });

    $('#PurchaseOrder_BusinessPartner_BusinessPartnerName').on('change', function () {
        if ($('#PurchaseOrder_BusinessPartner_BusinessPartnerName').val() === "") {
            $('#PurchaseOrder_BusinessPartner_BusinessPartnerId').val("");
        }
    });

    //Date pickers
    $("#PurchaseOrder_DeliveryDate").datepicker();
    $("#PurchaseOrder_DeliveryDate").on("change", function () {
        $("#MainForm").validate().element("#PurchaseOrder_DeliveryDate");
    });

    $("#PurchaseOrder_PostingDate").datepicker();
    $("#PurchaseOrder_PostingDate").on("change", function () {
        $("#MainForm").validate().element("#PurchaseOrder_PostingDate");
    });

    $("#PurchaseOrder_DocumentDate").datepicker();
    $("#PurchaseOrder_DocumentDate").on("change", function () {
        $("#MainForm").validate().element("#PurchaseOrder_DocumentDate");
    });

    $("#deleteItembutton").on("click", function () {
        var rows = table
            .rows('.selected')
            .remove()
            .draw();
    });

});