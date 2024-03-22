

function editEventSlot(e) {

    $("#ddlEventList").dxSelectBox("getDataSource").reload();
    $("#ddlTraineeList").dxSelectBox("getDataSource").reload();
    //$("#ddlEventStatusList").dxSelectBox("getDataSource").reload();
    setTimeout(function () {

        //var ddlCountryList = $("#ddlEventStatusList").dxSelectBox('instance');
        //ddlCountryList.option('value', parseInt(e.row.data.ref_StatusID));

        var ddlStateList = $("#ddlTraineeList").dxSelectBox('instance');
        ddlStateList.option('value', parseInt(e.row.data.ref_TrainerID));
        
        var ddlEventList = $("#ddlEventList").dxSelectBox('instance');
        ddlEventList.option('value', parseInt(e.row.data.ref_EventID));


    }, 100);
    
    $("#txtDate").val(e.row.data.dtDate);
    $("#txtFromtime").val(e.row.data.varTimeFrom);
    $("#txtTotime").val(e.row.data.varTimeTo);


    $("#TxtintNoofSeats").val(e.row.data.intNoofSeats);
    $("#ref_SlotintGlCode").val(e.row.data.intGlCode);
    $("#chkStatus").prop('checked', e.row.data.chrActive == 'Y' ? true : false);

    document.getElementById("frmEventSlot").scrollIntoView();

}
function ValidateDataEvent() {

    var chrActive = $("#chkStatus").prop('checked');
    $("#chrActive").val(chrActive == false ? 'InActive' : 'Active');

    var ddlEvent = $("#ddlEventList").dxSelectBox('instance').option('value');
    if (ddlEvent == undefined || ddlEvent == null || ddlEvent == '' || ddlEvent == '0') {
        PopUpMessage('Please Select State.', "fa fa-exclamation-circle popup_icon");
        $("#ddlEventList").focus();
        return false;
    }
    //$("#ref_StateID").val(ddlEvent);

    var ddlTraine = $("#ddlTraineeList").dxSelectBox('instance').option('value');
    if (ddlTraine == undefined || ddlTraine == null || ddlTraine == '' || ddlTraine == '0') {
        PopUpMessage('Please Select State.', "fa fa-exclamation-circle popup_icon");
        $("#ddlTraineeList").focus();
        return false;
    }
    /*$("#ref_CityId").val(ddlCity);*/

    //var ddlEventStatus = $("#ddlEventStatusList").dxSelectBox('instance').option('value');
    //if (ddlEventStatus == undefined || ddlEventStatus == null || ddlEventStatus == '' || ddlEventStatus == '0') {
    //    PopUpMessage('Please Select Event Type.', "fa fa-exclamation-circle popup_icon");
    //    $("#ddlEventList").focus();
    //    return false;
    //}
   /* $("#ref_EntityID").val(ddlEventStatus);*/

    setTimeout(function () {
        $.ajax({
            type: "POST",
            data: $('#frmEventSlot').serialize(),
            timeout: 15000, // adjust the limit. currently its 15 seconds
            url: configuration.onLoad() + "Event/Save_EventSlot",
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
                    $("#grdEventSlotList").dxDataGrid('instance').refresh();
                }
            },
            error: function (error) {
                PopUpMessage("Something Went Wrong,Please Try Again Later.", "fa fa-exclamation-circle popup_failure");
            }
        });
        //$('#loading').fadeOut();
    }, 1000);
}

function resetValidationEvent() {

    $('.input-validation-error').addClass('input-validation-valid');
    $('.input-validation-error').removeClass('input-validation-error');
    //Removes validation message after input-fields
    $('.field-validation-error').addClass('field-validation-valid');
    $('.field-validation-error').removeClass('field-validation-error');
    $('.field-validation-valid span').html('')
    
    $("input[type='time']").val('');
    $("input[type='date']").val('');
    $("input[type='datetime-local']").val('');
    $('#TxtintNoofSeats').val('0');

    $("#grdEventSlotList").dxDataGrid('instance').refresh();
    $("#grdEventSlotList").dxDataGrid('instance').clearFilter();
    $("#ddlEventList").dxSelectBox('instance').option('value', "0");
   // $("#ddlEventStatusList").dxSelectBox('instance').option('value', "0");
    $("#ddlTraineeList").dxSelectBox('instance').option('value', "0");


    $("#ref_SlotintGlCode").val('0');
    //$("#Action").val('Insert');

    $("#chkStatus").prop('checked', false);

    $("#chrActive").val('false');

}
function ExportExcelEvent() {
    $("#grdEventSlotList").dxDataGrid("instance").exportToExcel(false);
}
