$(document).ready(function () {
   
});
/* State Master */

function editdataState(e)
{
    var selectBox = $("#ddlCountryList").dxSelectBox("instance");
    setTimeout(function (event) {
        selectBox.option('value', e.row.data.ref_CountryId);
    }, 50);
    $("#intGlCode").val(e.row.data.intGlCode);
    $("#ref_CountryId").val(e.row.data.ref_CountryId); 
    $("#chrActive").val(e.row.data.chrActive); 
    //$("#Action").val('Update');
    $("#txtStateName").val(e.row.data.varStateName);
    $("#chkStatus").prop('checked', e.row.data.chrActive == 'Active' ? true : false);
}
function resetValidationstate() {

    //Removes validation from input-fields
    $('.input-validation-error').addClass('input-validation-valid');
    $('.input-validation-error').removeClass('input-validation-error');
    //Removes validation message after input-fields
    $('.field-validation-error').addClass('field-validation-valid');
    $('.field-validation-error').removeClass('field-validation-error');
    $('.field-validation-valid span').html('')

    $('input:text').val('');
    $("#grdStateList").dxDataGrid('instance').refresh();
    $("#ddlCountryList").dxSelectBox('instance').option('value', "0");
    $("#grdStateList").dxDataGrid('instance').clearFilter();

    $("#ddlCountryList").focus();


   $("#intGlCode").val('0');
    //$("#Action").val('Insert');

    $("#chkStatus").prop('checked', false);

    $("#chrActive").val('false');

}
function ExportExcelState()
{
    $("#grdStateList").dxDataGrid("instance").exportToExcel(false);
}

function ValidateDataState() {
    var chrActive = $("#chkStatus").prop('checked');
    var ddlCountry = $("#ddlCountryList").dxSelectBox('instance').option('value');
    
    if (ddlCountry == undefined || ddlCountry == null || ddlCountry == '' || ddlCountry == '0')
    {
        PopUpMessage('Please Select Country.', "fa fa-exclamation-circle popup_icon");
        $("#ddlCountry").focus();
        return false;
    }
    if ($("#txtStateName").val() == "") {
        PopUpMessage("Please Enter State Name.", "fa fa-exclamation-circle popup_icon");
        $("#txtStateName").focus();
        return false;
    }
    $("#ref_CountryId").val(ddlCountry);
    setTimeout(function () {
        $.ajax({
            type: "POST",
            data: $('#frmStates').serialize(),
            timeout: 15000, // adjust the limit. currently its 15 seconds
            url: configuration.onLoad() + "Account/Save_States",
            success: function (response)
            {
                if (response.Unauthorized == "401")
                {
                    window.location.href = configuration.onLoad() + 'Account/States';
                }
                else if (response.Table[0].intStatus == 0)
                {
                    PopUpMessage(response.Table[0].varMessage, "fa fa-exclamation-circle popup_icon_failure");
                }
                else
                {
                    PopUpWithClose(response.Table[0].varMessage, "fa fa-check-circle popup_icon_success");
                    resetValidationstate();
                    $("#grdStateList").dxDataGrid('instance').refresh();
                    $("#ddlCountryList").dxDataGrid('instance').refresh();
                }
            },
            error: function (error) {
                PopUpMessage("Something Went Wrong,Please Try Again Later.", "fa fa-exclamation-circle popup_failure");
            }
        });
        //$('#loading').fadeOut();
    }, 1000);
}

