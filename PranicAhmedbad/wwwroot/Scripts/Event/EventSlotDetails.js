

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

    var ddlEventStatus = $("#ddlEventStatusList").dxSelectBox('instance').option('value');
    if (ddlEventStatus == undefined || ddlEventStatus == null || ddlEventStatus == '' || ddlEventStatus == '0') {
        PopUpMessage('Please Select Event Type.', "fa fa-exclamation-circle popup_icon");
        $("#ddlEventList").focus();
        return false;
    }
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
