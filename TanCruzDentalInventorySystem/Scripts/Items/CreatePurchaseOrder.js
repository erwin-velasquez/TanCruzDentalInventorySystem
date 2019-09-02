$(document).ready(function () {

    $("#MainForm").submit(function (e) {
        $("#MainForm").validate();
    });

    var table = $('#purchaseOrderDetailTable').DataTable({
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
                "data": null,
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
                "__QuantityOnHand' name='PurchaseOrder.PurchaseOrderDetails[" + String(dataIndex) + "].QuantityOnHand' value='" + /*$(cells[4]).html()*/ "50" + "'/>"));

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
                    $(this).parent().parent().find("td[id^='td_QuantityOnHand']").text(total.toFixed(2));

                    //Document Total Computation
                    $.each($('#purchaseOrderDetailTable').find("td[id^='td_PurchaseOrderDetailTotal']"), function (index, value) {
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

        $('#PurchaseOrder_BusinessPartner_BusinessPartnerName').val(BusinessPartnerName);
        $('#PurchaseOrder_BusinessPartner_BusinessPartnerId').val(BusinessPartnerId);
    });

    $('#saveChangesButton').on('click', function () {
        console.log(JSON.stringify(table.rows().data().toArray()));
        table.row.add({
            "Item.ItemName": $("#txt-item-name").val(),
            "Item.ItemId": $("#txt-item-id").val(),
            "Item.ItemPrice": $("#txt-item-price").val(),
            "Item.Quantity": $("#txt-item-quantity").val(),
            "Item.Total": $("#txt-item-total").val(),
        }).draw();

        var itemList = [];

        $.each(table.rows().data().toArray(), function (index, value) {
            var mm = value;
            var k = {
                "PurchaseOrderDetailId": null,
                "PurchaseOrderId": null,
                "Item": {
                    "ItemId": value["Item.ItemId"],
                    "ItemName": value["Item.ItemName"],
                    "ItemDescription": null,
                    "ItemGroup": null,
                    //"ItemPrice": "300.5m",//parseFloat(value["Item.ItemPrice"]),
                    //"ItemPrice": parseFloat(value["Item.ItemPrice"]),
                    //"ItemPriceAmount": parseFloat(value["Item.ItemPrice"]),
                    "Currency": null,
                    "UnitOfMeasure": null,
                    "BusinessPartner": null,
                    "PurchasingUnitOfMeasure": null,
                    "ItemsPerUnitOfMeasure": 5,
                    "PurchasingRemarks": "testing edit",
                    "InventoryUnitOfMeasure": null,
                    "MinimumInventoryRequired": 100,
                    "IsActive": false,
                    "UserId": null,
                    "ChangedDate": null,
                    "VersionTimeStamp": 0
                },
                //"ItemPrice": {
                //"ItemPriceId": "IP00000007",
                //"ItemPriceName": "ItemPriceTHREEName",
                //"ItemPriceDescription": "ItemPriceTHREE Description",
                //"Type": "AC",
                //"PriceAmount": 250.0,
                //"BaseCurrency": "3"
                //},
                "ItemPriceAmount": parseFloat(value["Item.ItemPrice"]),
                "Quantity": value["Item.Quantity"],
                "QuantityOnHand": 8.0,
                "PurchaseOrderDetailDiscount": 10.5,
                "PurchaseOrderDetailDiscountAmount": 100.9,
                //"Tax": {
                //"TaxId": "TI00000001",
                //"TaxName": "TaxName_1",
                //"TaxDescription": "TaxDesc_1",
                //"TaxValue": 2400.0,
                //"IsDefault": false,
                //"TaxStatus": null,
                //"UserId": null
                //},
                "PurchaseOrderDetailTax": 200.0,
                "PurchaseOrderDetailTotal": 500.0,
                "Remarks": "First Sale",
                //"UserId": null,
                //"ChangedDate": "2019-08-18T08:10:37.6084235",
                //"VersionTimeStamp": 637017126376084235
            };
            itemList.push(k);
        });
        $("#PurchaseOrderDetailsJson").val(JSON.stringify(itemList));
    });

    $('#searchItemButton').on('click', function () {

        $.ajax({
            url: '/Item/ItemSearchModal/',
            type: 'GET',
            cache: false,
            dataType: 'html',
            success: function (result) {

                $(result).appendTo("body");

                var table2 = $('#ItemSearchTable').DataTable({
                    "searching": false,
                    "language": {
                        "processing": "loading....",
                        "emptyTable": "No data found"
                    },
                    "select": {
                        style: 'os',
                        selector: 'td'
                    },
                    columnDefs: [{
                        orderable: false,
                        className: 'select-checkbox',
                        targets: 0
                    }],
                    "paging": true,
                    "autoWidth": false,
                    "bLengthChange": false,
                    "bPaginate": false
                });

                $('#ItemSearchDone').on('click', function () {
                    console.log("ItemSearchDone");
                    $("#ItemSearchTable tbody tr.selected input[name*='ItemId']").each(function (index, value) {

                        $.get('/api/ItemApi/ItemRecord/', { itemId: this.value }, function (data) { //Replace with global URL not hardcoded
                            obj = [
                                {
                                    ItemName: data.item.ItemName,
                                    ItemId: data.item.ItemId,
                                    ItemPriceAmount: data.item.ItemPriceAmount,
                                    Quantity: data.item.Quantity,
                                    Total: data.item.ItemTotal
                                }
                            ];

                            table.rows.add(obj);
                            table.draw();
                        });
                    });

                    $('#ItemSearchModal').modal("hide");
                });

                $("#ItemSearchModal").modal("show");

                $('#ItemSearchModal').on('hidden.bs.modal', function () {
                    $(this).remove("#ItemSearchModal");
                });

            }, error: function (xhr, status, error) {
                alert(xhr.responseText);
            }
        });



        /*************OBSOLETE
        
        $('#ItemSearchModal').modal("show");

        var itemList;
        var obj;

        $.get('/Item/GetItemList', function (data) { //Replace with global URL not hardcoded
            obj = data;

            table1.clear();
            table1.rows.add(obj);
            table1.draw();
            
        });*/
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
