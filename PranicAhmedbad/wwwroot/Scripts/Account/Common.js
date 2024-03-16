

//function PopUpWithClose(message, classname) {
//    var popup = $("#msg-popup-close").dxPopup("instance");
//    popup.option("contentTemplate", $("#popup-template-close"));
//    popup.show();
//    $("#hdnPopUpMessageTypeclose").attr('class', classname);
//    $("#spanPopupMessageclose").html(message);
//}


//function PopUpMessage(message, classname) {
//    var popup = $("#msg-popup").dxPopup("instance");
//    popup.option("contentTemplate", $("#popup-template"));
//    popup.show();
//    $("#hdnPopUpMessageType").attr('class', classname);
//    $("#spanPopupMessage").html(message);
//}

function PopUpWithClose(message, classname) {
    Swal.fire({
        title: "Good job!",
        text: message,
        type: "success",
        showCancelButton: false,
        confirmButtonColor: "#556ee6",
        cancelButtonColor: "#f46a6a",
        icon: classname
    });
}

function PopUpMessage(message, classname) {

    if (classname.indexOf("popup_icon_success") > 0) {
        Swal.fire({
            title: "Good job!",
            text: message,
            type: "success",
            showCancelButton: false,
            confirmButtonColor: "#556ee6",
            cancelButtonColor: "#f46a6a",
            icon: classname
        });
    } else if (classname.indexOf("popup_icon_failure") > 0) {
        Swal.fire({
            title: "Oops...",
            text: message,
            type: "error",
            showCancelButton: false,
            confirmButtonColor: "#556ee6",
            cancelButtonColor: "#f46a6a",
            icon: classname
        });
    } else if (classname.indexOf("popup_failure") > 0) {
        Swal.fire({
            title: "Oops...",
            html: message,
            type: "error",
            showCancelButton: false,
            confirmButtonColor: "#556ee6",
            cancelButtonColor: "#f46a6a",
            icon: classname
        });
    } else if (classname.indexOf("popup_icon") > 0) {
        Swal.fire({
            title: "Oops...",
            text: message,
            type: "info",
            showCancelButton: false,
            confirmButtonColor: "#556ee6",
            cancelButtonColor: "#f46a6a",
            icon: classname
        });
    } else  {
        Swal.fire({ 
            text: message,
            //type: "info",
            showCancelButton: !0,
            confirmButtonColor: "#556ee6",
            cancelButtonColor: "#f46a6a",
            icon: classname
        });
    }

}

function isNumber(evt) {
    evt = (evt) ? evt : window.event;
    var charCode = (evt.which) ? evt.which : evt.keyCode;
    if (charCode > 31 && (charCode < 48 || charCode > 57)) {
        return false;
    }
    return true;
}

function closepopup() {
    if (document.URL.toString().indexOf("CreateShipmentLCL") > -1) {
        window.location.href = config.projectURL + "Transaction/CreateShipmentLCL";
    }
    else if (document.URL.toString().indexOf("AccidentIncident") > -1) {
        window.location.href = config.projectURL + 'Home';
    }
    else if (document.URL.toString().indexOf("DODetails") > -1) {
        window.location.href = config.projectURL + 'Transaction/DODetails';
        window.location.reload(true);
    }
    else if (document.URL.toString().indexOf("ShipmentAllocation") > -1) {
        window.location.href = config.projectURL + 'Transaction/ShipmentAllocation';
    }
}

function ConfirmPopUp(headerTitle) {
    var confirmResult = DevExpress.ui.dialog.confirm(headerTitle, "Confirm changes");
    return confirmResult;
}

function pageScroll() {
    window.scroll({
        top: document.body.scrollHeight,
        left: 0,
        behavior: 'smooth'
    });
}
function pageScrollBottom() {
    window.scroll({
        top: 0,
        left: 0,
        behavior: 'smooth'
    });
}

function dxSelectBox_OnOpened(ev) {
    var list = ev.component._list;
    if (!list.option('useNativeScrolling')) {
        list.option('useNativeScrolling', true);
        list._scrollView.option('useNative', true);
        list.reload();
    }
}


function dxSelectBox_OnInitialized(ev) {
    ev.component.option('dataSource', data);
}

function customizeExcelCell(e) {
    if (e.gridCell.rowType == "header") {
        e.backgroundColor = "#21a0d2";
        e.font.color = '#ffffff';
    }
}


$(".only-numeric").bind("keypress", function (e) {
    var keyCode = e.which ? e.which : e.keyCode

    if (!(keyCode >= 48 && keyCode <= 57)) {
        $(".error").css("display", "inline");
        return false;
    } else {
        $(".error").css("display", "none");
    }
});


$(".only-decimal").bind("keypress", function (e) {

    if ((event.which != 46 || $(this).val().indexOf('.') != -1) && (event.which < 48 || event.which > 57)) {
        event.preventDefault();
    }
});