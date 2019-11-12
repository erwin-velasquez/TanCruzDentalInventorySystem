$(document).ready(function () {
    var table = $('#salesOrderPaymentDetailTable').DataTable({
        "searching": false,
        "language": {
            "processing": "loading....",
            "emptyTable": "No data found"
        },
        "columns": [
            {
                "data": null,
                "title": "#",
                "defaultContent": '',
                "className": "dt-center"
            },
            {
                "data": "PaymentType",
                "title": "Payment Type",
                "className": "dt-left"
            },
            {
                "data": "CheckNumber",
                "title": "Check Number",
                "className": "dt-left"
            },
            {
                "data": "IssuingBank",
                "title": "Issuing Bank",
                "className": "dt-left"
            },
            {
                "data": "PaymentAmount",
                "title": "Payment Amount",
                "className": "PaymentAmountDetail dt-left"
            },
            {
                "data": "PaymentDate",
                "title": "Payment Date",
                "format": 'MM-DD-YYYY h:mm A',
                "type": 'datetime',
                "className": "dt-left"
            }
        ],
        "paging": true,
        "autoWidth": false,
        "bLengthChange": false,
        "bPaginate": false,
        createdRow: function (row, data, dataIndex, cells) {
            $(cells[1]).attr("id", "PaymentType_" + String(dataIndex));
            $(cells[1]).append($("<input type='hidden' name='SalesOrderPayment.SalesOrderPaymentDetails[xxx].PaymentType' class='PaymentType'" +
                " value='" + $(cells[1]).html() + "'/>"));

            $(cells[2]).attr("id", "CheckNumber_" + String(dataIndex));
            $(cells[2]).append($("<input type='hidden' name='SalesOrderPayment.SalesOrderPaymentDetails[xxx].CheckNumber' class='CheckNumber'" +
                " value='" + $(cells[2]).html() + "'/>"));

            $(cells[3]).attr("id", "IssuingBank_" + String(dataIndex));
            $(cells[3]).append($("<input type='hidden' name='SalesOrderPayment.SalesOrderPaymentDetails[xxx].BankName' class='BankName'" +
                " value='" + $(cells[3]).html() + "'/>"));

            $(cells[4]).attr("id", "PaymentAmount_" + String(dataIndex));
            $(cells[4]).append($("<input type='hidden' name='SalesOrderPayment.SalesOrderPaymentDetails[xxx].SalesOrderPaymentDetailTotal' class='PaymentAmount'" +
                " value='" + $(cells[4]).html() + "'/>"));

            $(cells[5]).attr("id", "PaymentDate_" + String(dataIndex));
            $(cells[5]).append($("<input type='hidden' name='SalesOrderPayment.SalesOrderPaymentDetails[xxx].PaymentDate' class='PaymentDate'" +
                " value='" + $(cells[5]).html() + "'/>"));
        }
    });

    $('#MainForm').on('submit', function (e) {
        var counter = 0;

        $('#salesOrderPaymentDetailTable tbody tr').each(function (unit) {
            $(this).find("td input[type='hidden']").each(function (unit) {
                $(this).prop('id', this.name.replace('[xxx]', '_' + counter + '_').replace('.', '_').replace('.', '_'));
                $(this).prop('name', this.name.replace('[xxx]', '[' + counter + ']'));
            });

            counter++;
        });
    });

    $('#btnAddPayment').on('click', function (e) {
        $.ajax({
            url: '/Payment/CreateSalesOrderPaymentDetailModal/',
            type: 'GET',
            cache: false,
            dataType: 'html',
            success: function (result) {

                $(result).appendTo("body");

                $('#addPayment').on('click', function () {

                    var totalPaymentAmount = 0;
                    var salesOrderTotal = parseFloat($('#SalesOrderPayment_SalesOrder_SalesOrderTotal').val());

                    $('tbody .PaymentAmountDetail').each(function () {
                        totalPaymentAmount = totalPaymentAmount + parseFloat($(this).text());
                    });

                    var obj = [
                        {
                            PaymentType: $("#PaymentType").val(),
                            CheckNumber: $("#CheckNumber").val(),
                            IssuingBank: $("#BankName").val(),
                            PaymentAmount: $("#SalesOrderPaymentDetailTotal").val(),
                            PaymentDate: $("#PaymentDate").val()
                        }
                    ];

                    if (!(salesOrderTotal - (parseFloat(totalPaymentAmount) + parseFloat(obj[0].PaymentAmount)) < 0)) {
                        $('#TotalAmounttoPay').val(salesOrderTotal - (parseFloat(totalPaymentAmount) + parseFloat(obj[0].PaymentAmount)));

                        $('#SalesOrderPayment_PaymentTotal').val((parseFloat(totalPaymentAmount) + parseFloat(obj[0].PaymentAmount)).toFixed(2));

                        table.rows.add(obj);
                        table.draw();

                        $('#SalesOrderPaymentDetailModal').modal("hide");
                    }
                    else
                    {
                        var newObject = $('<div class="alert alert-danger" role="alert"></div >');

                        newObject.html('Sales Order Amount (' + (salesOrderTotal - totalPaymentAmount).toFixed(2) + ') has been exceeded!');

                        $('#SalesOrderPaymentDetailModal .modal-body #validationMessage').append(newObject);
                    }

                    
                });

                $("#SalesOrderPaymentDetailModal #PaymentDate").datepicker();
                $("#SalesOrderPaymentDetailModal #PaymentDate").on("change", function () {
                    $("#MainForm").validate().element("#PaymentDate");
                });

                $("#SalesOrderPaymentDetailModal").modal("show");

                $('#SalesOrderPaymentDetailModal').on('hidden.bs.modal', function () {
                    $(this).remove("#SalesOrderPaymentDetailModal");
                });

            }, error: function (xhr, status, error) {
                alert(xhr.responseText);
            }
        });
    });

});
