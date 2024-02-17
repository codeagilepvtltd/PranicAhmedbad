$(document).ready(function () {
   
});


function ExportExcel() {
    $("#grdSupplierDetials").dxDataGrid("instance").exportToExcel(false);
}

$.ajaxSetup({
    data: {
        __RequestVerificationToken: document.getElementsByName("__RequestVerificationToken")[0].value
    }
});