$(document).ready(function () {

});
/* State Master */
function ReloadStateCityddl() {
    $("#ddlStateList").dxSelectBox("getDataSource").reload();
    $("#ddlCityList").dxSelectBox("getDataSource").reload();

    $("#ddlStateList").dxSelectBox('instance').option('value', "0");
    $("#ddlCityList").dxSelectBox('instance').option('value', "0");
}
function ReloadCityddl() {
    $("#ddlCityList").dxSelectBox("getDataSource").reload();
    $("#ddlCityList").dxSelectBox('instance').option('value', "0");
}

function dxSelectBox_OnOpenedstateDDl(ev) {
    var list = ev.component._list;
    list.option('useNativeScrolling', true);
    list._scrollView.option('useNative', true);
    list.reload();
}
function editdataEvent(e) {

    $("#ddlCountryList").dxSelectBox("getDataSource").reload();
    $("#ddlStateList").dxSelectBox("getDataSource").reload();
    $("#ddlCityList").dxSelectBox("getDataSource").reload();
    setTimeout(function () {
        
        var ddlCountryList = $("#ddlCountryList").dxSelectBox('instance');
        ddlCountryList.option('value', parseInt(e.row.data.ref_CountryId));


        var ddlStateList = $("#ddlStateList").dxSelectBox('instance');
        ddlStateList.option('value', parseInt(e.row.data.ref_StateId));


        var ddlCityList = $("#ddlCityList").dxSelectBox('instance');
        ddlCityList.option('value', parseInt(e.row.data.ref_CityId));


    }, 100);

    $("#ref_CityId").val(e.row.data.ref_CityId);
    $("#ref_StateId").val(e.row.data.ref_StateId);
    $("#ref_CountryId").val(e.row.data.ref_CountryId);

    $("#intGlCode").val(e.row.data.intGlCode);
    $("#ref_AddressId").val(e.row.data.ref_AddressID);
    $("#ref_EventintGlCode").val(e.row.data.intGlCode);
    $("#ref_EventTypeID").val(e.row.data.ref_EventTypeID);

    $("#txtEventName").val(e.row.data.varEventName);
    $("#txtEventDescription").val(e.row.data.varEventDescription);
    $("#txtEventContent").val(e.row.data.varEventContent);

    $("#txtAddress").val(e.row.data.varAddressLine1);
    $("#txtPinCode").val(e.row.data.varPostalCode);
    $("#txtMobileNo").val(e.row.data.varContactMobileNo);
    $("#txtEmail").val(e.row.data.varEmailAddress);

    $("#txtGMapLocation").val(e.row.data.varGMapLocation);
    $("#txtContactPerson").val(e.row.data.varContactPerson);

    $("#txtPayment").val(e.row.data.varPaymentType);
    $("#txtRegistration").val(e.row.data.varRegistrationLink);

    $("#chkStatus").prop('checked', e.row.data.chrActive == 'Y' ? true : false);
    $("#txtPublishDate").val(e.row.data.dtEventPublishDate);
    $("#txtStartDate").val(e.row.data.dtStartDate);
    $("#txtEndDate").val(e.row.data.dtEndDate);

}
function resetValidationEvent() {

    $('.input-validation-error').addClass('input-validation-valid');
    $('.input-validation-error').removeClass('input-validation-error');
    //Removes validation message after input-fields
    $('.field-validation-error').addClass('field-validation-valid');
    $('.field-validation-error').removeClass('field-validation-error');
    $('.field-validation-valid span').html('')

    $("textarea").val('');
    $('input:text').val('');
    $("input[type='email']").val('');
    $("input[type='datetime-local']").val('');
    $('#txtAddress').val('');

    $("#grdEventList").dxDataGrid('instance').refresh();
    $("#grdEventList").dxDataGrid('instance').clearFilter();
    $("#ddlCountryList").dxSelectBox('instance').option('value', "0");
    $("#ddlStateList").dxSelectBox('instance').option('value', "0");
    $("#ddlCityList").dxSelectBox('instance').option('value', "0");


    $("#ddlCountryList").focus();


    $("#ref_EventintGlCode").val('0');
    //$("#Action").val('Insert');

    $("#chkStatus").prop('checked', false);

    $("#chrActive").val('false');

}
function ExportExcelEvent() {
    $("#grdEventList").dxDataGrid("instance").exportToExcel(false);
}

function ValidateDataEvent() {

    var chrActive = $("#chkStatus").prop('checked');
    $("#chrActive").val(chrActive == false ? 'InActive' : 'Active');

    var ddlCountry = $("#ddlCountryList").dxSelectBox('instance').option('value');
    if (ddlCountry == undefined || ddlCountry == null || ddlCountry == '' || ddlCountry == '0') {
        PopUpMessage('Please Select Country.', "fa fa-exclamation-circle popup_icon");
        $("#ddlCountry").focus();
        return false;
    }
    $("#ref_CountryID").val(ddlCountry);

    var ddlState = $("#ddlStateList").dxSelectBox('instance').option('value');
    if (ddlState == undefined || ddlState == null || ddlState == '' || ddlState == '0') {
        PopUpMessage('Please Select State.', "fa fa-exclamation-circle popup_icon");
        $("#ddlStateList").focus();
        return false;
    }
    $("#ref_StateID").val(ddlState);

    var ddlCity = $("#ddlCityList").dxSelectBox('instance').option('value');
    if (ddlCity == undefined || ddlCity == null || ddlCity == '' || ddlCity == '0') {
        PopUpMessage('Please Select State.', "fa fa-exclamation-circle popup_icon");
        $("#ddlCityList").focus();
        return false;
    }
    $("#ref_CityId").val(ddlCity);

    setTimeout(function () {
        $.ajax({
            type: "POST",
            data: $('#frmEvent').serialize(),
            timeout: 15000, // adjust the limit. currently its 15 seconds
            url: configuration.onLoad() + "Event/Save_Events",
            success: function (response) {
                if (response.Unauthorized == "401") {
                    window.location.href = configuration.onLoad() + 'Home';
                }
                else if (response.Table[0].intStatus == 0) {
                    PopUpMessage(response.Table[0].varMessage, "fa fa-exclamation-circle popup_icon_failure");
                }
                else {
                    PopUpWithClose(response.Table[0].varMessage, "fa fa-check-circle popup_icon_success");
                    resetValidationEvent();
                    $("#grdEventList").dxDataGrid('instance').refresh();
                }
            },
            error: function (error) {
                PopUpMessage("Something Went Wrong,Please Try Again Later.", "fa fa-exclamation-circle popup_failure");
            }
        });
        //$('#loading').fadeOut();
    }, 1000);
}

