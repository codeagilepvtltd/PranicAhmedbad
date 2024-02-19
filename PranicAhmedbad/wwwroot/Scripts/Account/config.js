var configuration = {
    onLoad: function () {
        if (window.location.origin != '') {
            if (window.location.pathname.indexOf('PranicAhmedabad') != -1) {
                return window.location.origin + '/' + window.location.pathname.split('/')[1] + '/';
            }
            else {
                return window.location.origin + '/';
            }
        }
        else {
            return 'http://localhost:7284/';
        }
    }
}

//var config = {
//    projectURL: 'http://novapackfilms.in/'
//}

var config = {
    projectURL: 'http://localhost:7284/'
}