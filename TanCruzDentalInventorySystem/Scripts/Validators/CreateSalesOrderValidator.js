$.validator.addMethod('SalesOrderDetails_Quantity', function (value, element, isactive) {
    return false;
}, "Cannot be more than Quantity on Hand!");