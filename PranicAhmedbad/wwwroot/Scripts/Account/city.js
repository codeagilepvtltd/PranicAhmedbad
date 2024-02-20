$(document).ready(function () {

});
/* State Master */

function editdataCity(e) {
    debugger;


    setTimeout(function () {
        $("#ddlCountryList").dxSelectBox("getDataSource").reload();
        var ddlCountryList = $("#ddlCountryList").dxSelectBox('instance');
        ddlCountryList.option('value', parseInt(e.row.data.ref_CountryID));

        $("#ddlStateList").dxSelectBox("getDataSource").reload();
        var ddlStateList = $("#ddlStateList").dxSelectBox('instance');
        ddlStateList.option('value', parseInt(e.row.data.ref_StateID));
    }, 100);

    
    $("#intGlCode").val(e.row.data.intGlCode);
    $("#ref_CountryId").val(e.row.data.ref_CountryID);
    $("#ref_StateID").val(e.row.data.ref_StateID);

    $("#chrActive").val(e.row.data.chrActive);
    //$("#Action").val('Update');
    $("#txtCityCode").val(e.row.data.varCityCode);
    $("#txtCityName").val(e.row.data.varCityName);
    $("#chkStatus").prop('checked', e.row.data.chrActive == 'Active' ? true : false);
}
function resetValidationcity() {

    //Removes validation from input-fields
    $('.input-validation-error').addClass('input-validation-valid');
    $('.input-validation-error').removeClass('input-validation-error');
    //Removes validation message after input-fields
    $('.field-validation-error').addClass('field-validation-valid');
    $('.field-validation-error').removeClass('field-validation-error');
    $('.field-validation-valid span').html('')

    $('input:text').val('');
    $("#grdCityList").dxDataGrid('instance').refresh();
    $("#grdCityList").dxDataGrid('instance').clearFilter();
    $("#ddlCountryList").dxSelectBox('instance').option('value', "0");
    $("#ddlStateList").dxSelectBox('instance').option('value', "0");


    $("#ddlCountryList").focus();


    $("#intGlCode").val('0');
    //$("#Action").val('Insert');

    $("#chkStatus").prop('checked', false);

    $("#chrActive").val('false');

}
function ExportExcelCity() {
    $("#grdCityList").dxDataGrid("instance").exportToExcel(false);
}

function ValidateDataCity() {
    var chrActive = $("#chkStatus").prop('checked');
    var ddlCountry = $("#ddlCountryList").dxSelectBox('instance').option('value');

    if (ddlCountry == undefined || ddlCountry == null || ddlCountry == '' || ddlCountry == '0') {
        PopUpMessage('Please Select Country.', "fa fa-exclamation-circle popup_icon");
        $("#ddlCountry").focus();
        return false;
    }

    var ddlState = $("#ddlStateList").dxSelectBox('instance').option('value');

    if (ddlState == undefined || ddlState == null || ddlState == '' || ddlState == '0') {
        PopUpMessage('Please Select State.', "fa fa-exclamation-circle popup_icon");
        $("#ddlStateList").focus();
        return false;
    }
    if ($("#txtCityCode").val() == "") {
        PopUpMessage("Please Enter City Code.", "fa fa-exclamation-circle popup_icon");
        $("#txtCityCode").focus();
        return false;
    }
    if ($("#txtCityName").val() == "") {
        PopUpMessage("Please Enter City Name.", "fa fa-exclamation-circle popup_icon");
        $("#txtCityName").focus();
        return false;
    }
    $("#ref_CountryId").val(ddlCountry);
    $("#ref_StateID").val(ddlState);

    setTimeout(function () {
        $.ajax({
            type: "POST",
            data: $('#frmCity').serialize(),
            timeout: 15000, // adjust the limit. currently its 15 seconds
            url: configuration.onLoad() + "Account/Save_City",
            success: function (response) {
                if (response.Unauthorized == "401") {
                    window.location.href = configuration.onLoad() + 'Account/Save_City';
                }
                else if (response.Table[0].intStatus == 0) {
                    PopUpMessage(response.Table[0].varMessage, "fa fa-exclamation-circle popup_icon_failure");
                }
                else {
                    PopUpWithClose(response.Table[0].varMessage, "fa fa-check-circle popup_icon_success");
                    resetValidationcity();
                    $("#grdCityList").dxDataGrid('instance').refresh();
                }
            },
            error: function (error) {
                PopUpMessage("Something Went Wrong,Please Try Again Later.", "fa fa-exclamation-circle popup_failure");
            }
        });
        //$('#loading').fadeOut();
    }, 1000);
}

