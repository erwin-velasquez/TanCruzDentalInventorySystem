

$(document).ready(function () {

    

    var table = $('#salesOrderDetailTable').DataTable({
        "scrollX": true,
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
            },
        ],
        "language": {
            "processing": "loading....",
            "emptyTable": "No data found"
        },
        createdRow: function (row, data, dataIndex, cells) {
            var td = $(row).find("[class^=truncate]");
            if (td) {
                td.attr("title", td.html());
            }

            $(cells[2]).attr("id", "SalesOrder_SalesOrderDetails_" + String(dataIndex) + "__SalesOrderDetailId");
            $(cells[2]).attr("name", "SalesOrder.SalesOrderDetails[" + String(dataIndex) + "].SalesOrderDetailId");
            $(cells[2]).attr("value", $(cells[2]).html());

            $(row).find("td input[type*='text']").each(function () {
                $(this).attr("id", "SalesOrder_SalesOrderDetails_" + String(dataIndex) + "__Quantity");
                $(this).attr("name", "SalesOrder.SalesOrderDetails[" + String(dataIndex) + "].Quantity");
            });
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

    var table1 = $('#ItemSearchTable').DataTable({
        "scrollX": true,
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
            selector: 'td:first-child'
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
                "SalesOrderDetailId": null,
                "SalesOrderId": null,
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
                "SalesOrderDetailDiscount": 10.5,
                "SalesOrderDetailDiscountAmount": 100.9,
                //"Tax": {
                //"TaxId": "TI00000001",
                //"TaxName": "TaxName_1",
                //"TaxDescription": "TaxDesc_1",
                //"TaxValue": 2400.0,
                //"IsDefault": false,
                //"TaxStatus": null,
                //"UserId": null
                //},
                "SalesOrderDetailTax": 200.0,
                "SalesOrderDetailTotal": 500.0,
                "Remarks": "First Sale",
                //"UserId": null,
                //"ChangedDate": "2019-08-18T08:10:37.6084235",
                //"VersionTimeStamp": 637017126376084235
            };
            itemList.push(k);
        });
        $("#SalesOrderDetailsJson").val(JSON.stringify(itemList));
    });

    $('#searchItemButton').on('click', function () {
        $('#ItemSearchModal').modal("show");

        var itemList;
        var obj;

        $.get('/Item/GetItemList', function (data) { //Replace with global URL not hardcoded
            obj = data;

            table1.clear();
            table1.rows.add(obj);
            table1.draw();
            
        });
    });

    $('#doneButton').on('click', function () {

        $("#ItemSearchTable tbody tr.selected td#ItemId").each(function (index, value) {

            $.get('/Item/GetSingleItem/', { itemId: $(this).text() }, function (data) { //Replace with global URL not hardcoded
                obj = [
                    data
                ];

                table.rows.add(obj);
                table.draw();
            });
        });

        $('#ItemSearchModal').modal("hide");
    });
});
