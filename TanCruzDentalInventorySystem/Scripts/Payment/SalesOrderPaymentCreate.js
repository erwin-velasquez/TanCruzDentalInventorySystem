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
                "className": "dt-left"
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
        "bPaginate": false
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

                    var obj = [
                        {
                            PaymentType: $("#PaymentType").val(),
                            CheckNumber: $("#CheckNumber").val(),
                            IssuingBank: $("#BankName").val(),
                            PaymentAmount: $("#SalesOrderPaymentDetailTotal").val(),
                            PaymentDate: $("#PaymentDate").val()
                        }
                    ];

                    table.rows.add(obj);
                    table.draw();

                    $('#SalesOrderPaymentDetailModal').modal("hide");
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
