//Phần kiểm tra email nhập vào có đúng định dạng hay không

function validateEmail(email) {
    var emailReg = /^([\w-\.]+@([\w-]+\.)+[\w-]{2,4})?$/;
    if (emailReg.test(email)) {
        return true;
    }
    else {
        return false;
    }
    //return emailReg.test($email);
}
