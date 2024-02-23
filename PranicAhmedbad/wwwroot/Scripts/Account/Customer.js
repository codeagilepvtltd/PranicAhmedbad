$(document).ready(function () {

});
/* State Master */

function editdataCutomer(e) {

    $("#ddlCountryList").dxSelectBox("getDataSource").reload();
    $("#ddlStateList").dxSelectBox("getDataSource").reload();
    $("#ddlCityList").dxSelectBox("getDataSource").reload();
    $("#ddlGender").dxSelectBox("getDataSource").reload();
    setTimeout(function () {

        var ddlCountryList = $("#ddlCountryList").dxSelectBox('instance');
        ddlCountryList.option('value', parseInt(e.row.data.ref_CountryID));

        
        var ddlStateList = $("#ddlStateList").dxSelectBox('instance');
        ddlStateList.option('value', parseInt(e.row.data.ref_StateID));

        
        var ddlCityList = $("#ddlCityList").dxSelectBox('instance');
        ddlCityList.option('value', parseInt(e.row.data.ref_CityId));

        var ddlGender = $("#ddlGender").dxSelectBox('instance');
        ddlGender.option('value', parseInt(e.row.data.chrGender));


    }, 100);

    $("#ref_CityId").val(e.row.data.ref_CityId);
    $("#ref_StateID").val(e.row.data.ref_StateID);
    $("#ref_CountryID").val(e.row.data.ref_CountryID);
    $("#chrGender").val(e.row.data.chrGender);
    $("#intGlCode").val(e.row.data.intGlCode);
    $("#ref_AddressId").val(e.row.data.ref_AddressId);
    $("#ref_CustintGlCode").val(e.row.data.intGlCode);
    
    $("#txtFirstName").val(e.row.data.varFirstName);
    $("#txtMiddleName").val(e.row.data.varMiddleName);
    $("#txtLastName").val(e.row.data.varLasteName);
    $("#txtAddress").val(e.row.data.varAddressLine1);
    $("#txtPinCode").val(e.row.data.varPostalCode);
    $("#txtMobileNo").val(e.row.data.varContactNo);
    $("#txtEmail").val(e.row.data.varEmailAddress);
    $("#ref_LoginID").val(e.row.data.ref_LoginID);
    $("#ref_EntityTypeID").val(e.row.data.ref_EntityTypeID);

    
    $("#chkStatus").prop('checked', e.row.data.chrActive == 'Y' ? true : false);
    $("#txtDOB").val(e.row.data.dtDOB);
    
}
function resetValidationcustomer() {

    //Removes validation from input-fields
    $('.input-validation-error').addClass('input-validation-valid');
    $('.input-validation-error').removeClass('input-validation-error');
    //Removes validation message after input-fields
    $('.field-validation-error').addClass('field-validation-valid');
    $('.field-validation-error').removeClass('field-validation-error');
    $('.field-validation-valid span').html('')

    $('input:text').val('');
    $("input[type='email']").val('');
    $("input[type='datetime-local']").val('');
    $('#txtAddress').val('');

    $("#grdCustomerList").dxDataGrid('instance').refresh();
    $("#grdCustomerList").dxDataGrid('instance').clearFilter();
    $("#ddlCountryList").dxSelectBox('instance').option('value', "0");
    $("#ddlStateList").dxSelectBox('instance').option('value', "0");
    $("#ddlCityList").dxSelectBox('instance').option('value', "0");
    $("#ddlGender").dxSelectBox('instance').option('value', "0");


    $("#ddlCountryList").focus();


    $("#intGlCode").val('0');
    //$("#Action").val('Insert');

    $("#chkStatus").prop('checked', false);

    $("#chrActive").val('false');

}
function ExportExcelCustomer() {
    $("#grdCustomerList").dxDataGrid("instance").exportToExcel(false);
}

function ValidateDataCustomer() {

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

    var ddlGender = $("#ddlGender").dxSelectBox('instance').option('value');
    if (ddlGender == undefined || ddlCity == null || ddlCity == '' || ddlCity == '0') {
        PopUpMessage('Please Select Gender.', "fa fa-exclamation-circle popup_icon");
        $("#ddlGender").focus();
        return false;
    }
    $("#chrGender").val(ddlGender);
    
    setTimeout(function () {
        $.ajax({
            type: "POST",
            data: $('#frmCustomer').serialize(),
            timeout: 15000, // adjust the limit. currently its 15 seconds
            url: configuration.onLoad() + "Account/Save_Customer",
            success: function (response)
            {
                if (response.Unauthorized == "401") {
                    window.location.href = configuration.onLoad() + 'Home';
                }
                else if (response.Table[0].intStatus == 0)
                {
                    PopUpMessage(response.Table[0].varMessage, "fa fa-exclamation-circle popup_icon_failure");
                }
                else
                {
                    PopUpWithClose(response.Table[0].varMessage, "fa fa-check-circle popup_icon_success");
                    resetValidationcustomer();
                    $("#grdCustomerList").dxDataGrid('instance').refresh();
                }
            },
            error: function (error) {
                PopUpMessage("Something Went Wrong,Please Try Again Later.", "fa fa-exclamation-circle popup_failure");
            }
        });
        //$('#loading').fadeOut();
    }, 1000);
}

