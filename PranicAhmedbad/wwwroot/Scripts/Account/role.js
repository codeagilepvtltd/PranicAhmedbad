$(document).ready(function () {
   
});

function ValidateData() {
    var chrActive = $("#chkchrActive").prop('checked');
    $("#chrActive").val(chrActive == false ? 'InActive' : 'Active');

    if ($("#txtRoleName").val() == "") {
        PopUpMessage("Please Enter Role Name.", "fa fa-exclamation-circle popup_icon");
        $("#txtRoleName").focus();
        return false;
    }
   
    setTimeout(function () {
        $.ajax({
            type: "POST",
            data: $('#frmRoleDetail').serialize(),
            timeout: 15000, // adjust the limit. currently its 15 seconds
            url: configuration.onLoad() + "Account/Save_Roles",
            success: function (response) {
                if (response.Unauthorized == "401") {
                    window.location.href = configuration.onLoad() + 'Home';
                }
                else if (response.Table[0].intStatus == 0) {
                    PopUpMessage(response.Table[0].varMessage, "fa fa-exclamation-circle popup_icon_failure");
                }
                else {
                    PopUpWithClose(response.Table[0].varMessage, "fa fa-check-circle popup_icon_success");
                    resetValidation();
                    $("#grdRolesDetials").dxDataGrid('instance').refresh();
                }
            },
            error: function (error) {
                PopUpMessage("Something Went Wrong,Please Try Again Later.", "fa fa-exclamation-circle popup_failure");
            }
        });
        //$('#loading').fadeOut();
    }, 1000);
}
function ExportExcel() {
    $("#grdRolesDetials").dxDataGrid("instance").exportToExcel(false);
}

function resetValidation() {

    //Removes validation from input-fields
    $('.input-validation-error').addClass('input-validation-valid');
    $('.input-validation-error').removeClass('input-validation-error');
    //Removes validation message after input-fields
    $('.field-validation-error').addClass('field-validation-valid');
    $('.field-validation-error').removeClass('field-validation-error');
    $('.field-validation-valid span').html('')

    $('input:text').val('');
    $("#intGlCode").val('0');
    $("#Action").val('Insert');

    $("#chkchrActive").prop('checked', true);

    $("#chrActive").val(true);
    $("#grdRolesDetials").dxDataGrid('instance').refresh();
    $("#grdRolesDetials").dxDataGrid('instance').clearFilter();

}


function editdata(e) {
    $("#intGlCode").val(e.row.data.intGlCode);
    $("#Action").val('Update');

    $("#txtRoleName").val(e.row.data.varRoleName);
    $("#chkchrActive").prop('checked', e.row.data.chrActive == 'Active' ? true : false);

}