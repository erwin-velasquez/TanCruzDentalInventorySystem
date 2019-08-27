$(function () {
    $.validator.methods.date = function (value, element) {

        return this.optional(element) || moment(value, "MM/DD/YYYY", true).isValid();
    };

});