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

function checkEmail(element) {
    if ($("#" + element).val() != "") {
        if (!validateEmail($("#" + element).val())) {
            var emails = document.getElementById("GENERAL_EMAIL");
            $("#GENERAL_EMAIL_ERROR").hide();
            $("#CHECK_GENERAL_EMAIL").show();
            emails.focus();
            return false;
        }
    }
    $("#CHECK_GENERAL_EMAIL").hide();
    return true;
}



function checkCUSTOMER_CODE() {
    if (document.getElementById('CUSTOMER_CODE').value == ""
        || document.getElementById('CUSTOMER_CODE').value == undefined) {
        $("#CUSTOMER_CODE_ERROR").show();
        return false;
    }
    else {
        return true;
    }
}

function checkCONTACT_NAME() {
    if (document.getElementById('CONTACT_NAME').value == ""
        || document.getElementById('CONTACT_NAME').value == undefined) {
        $("#CONTACT_NAME_ERROR").show();
        return false;
    }
    else {
        return true;
    }
}

function checkDATE_CREATE() {
    if (document.getElementById('DATE_CREATE').value == ""
        || document.getElementById('DATE_CREATE').value == undefined) {
        $("#DATE_CREATE_ERROR").show();
        return false;
    }
    else {
        return true;
    }
}

function checkDATE_END() {
    if (document.getElementById('DATE_END').value == ""
        || document.getElementById('DATE_END').value == undefined) {
        $("#DATE_END_ERROR").show();
        return false;
    }
    else {
        return true;
    }
}

function checkCONTACT_PHONE_WORK() {
    if (document.getElementById('CONTACT_PHONE_WORK').value == ""
        || document.getElementById('CONTACT_PHONE_WORK').value == undefined) {
        $("#CONTACT_PHONE_WORK_ERROR").show();
        return false;
    }
    else {
        return true;
    }
}

function checkBUSINESS_TAX() {
    if (document.getElementById('BUSINESS_TAX').value == ""
       || document.getElementById('BUSINESS_TAX').value == undefined) {
        $("#BUSINESS_TAX_ERROR").show();
        return false;
    }
    else {
        return true;
    }
}

function checkGENERAL_EMAIL() {
    if (document.getElementById('GENERAL_EMAIL').value == ""
        || document.getElementById('GENERAL_EMAIL').value == undefined) {
        $("#GENERAL_EMAIL_ERROR").show();
        return false;
    }
    else {
        return true;
    }
}

function checkUNIT_CODE() {
    if (document.getElementById('UNIT_CODE').value == ""
        || document.getElementById('UNIT_CODE').value == undefined) {
        $("#UNIT_CODE_ERROR").show();
        return false;
    }
    else {
        return true;
    }
}

function checkCONTRACT_NUMBER() {
    if (document.getElementById('CONTRACT_NUMBER').value == ""
        || document.getElementById('CONTRACT_NUMBER').value == undefined) {
        $("#CONTRACT_NUMBER_ERROR").show();
        return false;
    }
    else {
        return true;
    }
}

function checkCUSTOMER_ACTIVE() {
    if (document.getElementById('CUSTOMER_ACTIVE').value == ""
        || document.getElementById('CUSTOMER_ACTIVE').value == undefined) {
        $("#CUSTOMER_ACTIVE_ERROR").show();
        return false;
    }
    else {
        return true;
    }
}

function checkTOTAL_CUSTOMER_CODE() {
    if (document.getElementById('TOTAL_CUSTOMER_CODE').value == ""
        || document.getElementById('TOTAL_CUSTOMER_CODE').value == undefined) {
        $("#TOTAL_CUSTOMER_CODE_ERROR").show();
        return false;
    }
    else {
        return true;
    }
}

function checkPAYMENT_ADDRESS() {
    if (document.getElementById('PAYMENT_ADDRESS').value == ""
        || document.getElementById('PAYMENT_ADDRESS').value == undefined) {
        $("#PAYMENT_ADDRESS_ERROR").show();
        return false;
    }
    else {
        return true;
    }
}


function checkCONTACT_PROVINCE() {
    if (document.getElementById('CONTACT_PROVINCE').value == ""
        || document.getElementById('CONTACT_PROVINCE').value == undefined) {
        $("#CONTACT_PROVINCE_ERROR").show();
        return false;
    }
    else {
        return true;
    }
}

function checkCONTACT_DISTRICT() {
    if (document.getElementById('CONTACT_DISTRICT').value == ""
        || document.getElementById('CONTACT_DISTRICT').value == undefined) {
        $("#CONTACT_DISTRICT_ERROR").show();
        return false;
    }
    else {
        return true;
    }
}

function checkEMPLOYEE_DEBT_CODE() {
    if (document.getElementById('EMPLOYEE_DEBT_CODE').value == ""
        || document.getElementById('EMPLOYEE_DEBT_CODE').value == undefined) {
        $("#EMPLOYEE_DEBT_CODE_ERROR").show();
        return false;
    }
    else {
        return true;
    }
}

function checkEMPLOYEE_SALE_CODE() {
    if (document.getElementById('EMPLOYEE_SALE_CODE').value == ""
        || document.getElementById('EMPLOYEE_SALE_CODE').value == undefined) {
        $("#EMPLOYEE_SALE_CODE_ERROR").show();
        return false;
    }
    else {
        return true;
    }
}




$(document).ready(function () {
    $("#CHECK_GENERAL_EMAIL").hide();

    $("#CUSTOMER_CODE_ERROR").hide();

    $("#CONTACT_NAME_ERROR").hide();

    $("#DATE_CREATE_ERROR").hide();

    $("#DATE_END_ERROR").hide();

    $("#CONTACT_PHONE_WORK_ERROR").hide();

    $("#BUSINESS_TAX_ERROR").hide();

    $("#GENERAL_EMAIL_ERROR").hide();

    $("#UNIT_CODE_ERROR").hide();

    $("#CONTRACT_NUMBER_ERROR").hide();

    $("#CUSTOMER_ACTIVE_ERROR").hide();

    $("#TOTAL_CUSTOMER_CODE_ERROR").hide();

    $("#PAYMENT_ADDRESS_ERROR").hide();


    $("#CONTACT_PROVINCE_ERROR").hide();

    $("#CONTACT_DISTRICT_ERROR").hide();

    $("#EMPLOYEE_DEBT_CODE_ERROR").hide();

    $("#EMPLOYEE_SALE_CODE_ERROR").hide();

});

$("#btnreportedit").on('click', function () {


    //$("#EMAIL_ERROR").hide();
    //check();
    // goi ham Detail
    if (checkCUSTOMER_CODE() == true && checkCONTACT_NAME() == true && checkDATE_CREATE() == true && checkDATE_END() == true && checkCONTACT_PHONE_WORK() == true && checkBUSINESS_TAX() == true && checkGENERAL_EMAIL() == true && checkUNIT_CODE() == true && checkCONTRACT_NUMBER() == true && checkCUSTOMER_ACTIVE() == true && checkTOTAL_CUSTOMER_CODE() == true && checkPAYMENT_ADDRESS() == true && checkCONTACT_PROVINCE() == true && checkCONTACT_DISTRICT() == true && checkEMPLOYEE_DEBT_CODE() == true && checkEMPLOYEE_SALE_CODE == true) {
        detail();
    }

    if (checkCUSTOMER_CODE() == true) {
        $("#CUSTOMER_CODE_ERROR").hide();
    }
    else {
        $("#CUSTOMER_CODE_ERROR").show();

    }

    if (checkCONTACT_NAME() == true) {
        $("#CONTACT_NAME_ERROR").hide();
    }
    else {
        $("#CONTACT_NAME_ERROR").show();

    }


    if (checkDATE_CREATE() == true) {
        $("#DATE_CREATE_ERROR").hide();
    }
    else {
        $("#DATE_CREATE_ERROR").show();

    }

    if (checkDATE_END() == true) {
        $("#DATE_END_ERROR").hide();
    }
    else {
        $("#DATE_END_ERROR").show();

    }

    if (checkCONTACT_PHONE_WORK() == true) {
        $("#CONTACT_PHONE_WORK_ERROR").hide();
    }
    else {
        $("#CONTACT_PHONE_WORK_ERROR").show();

    }

    if (checkBUSINESS_TAX() == true) {
        $("#BUSINESS_TAX_ERROR").hide();
    }
    else {
        $("#BUSINESS_TAX_ERROR").show();

    }

    if (checkGENERAL_EMAIL() == true) {
        $("#GENERAL_EMAIL_ERROR").hide();
    }
    else {
        $("#GENERAL_EMAIL_ERROR").show();

    }

    if (checkUNIT_CODE() == true) {
        $("#UNIT_CODE_ERROR").hide();
    }
    else {
        $("#UNIT_CODE_ERROR").show();

    }

    if (checkCONTRACT_NUMBER() == true) {
        $("#CONTRACT_NUMBER_ERROR").hide();
    }
    else {
        $("#CONTRACT_NUMBER_ERROR").show();

    }

    if (checkCUSTOMER_ACTIVE() == true) {
        $("#CUSTOMER_ACTIVE_ERROR").hide();
    }
    else {
        $("#CUSTOMER_ACTIVE_ERROR").show();

    }

    if (checkTOTAL_CUSTOMER_CODE() == true) {
        $("#TOTAL_CUSTOMER_CODE_ERROR").hide();
    }
    else {
        $("#TOTAL_CUSTOMER_CODE_ERROR").show();

    }

    if (checkPAYMENT_ADDRESS() == true) {
        $("#PAYMENT_ADDRESS_ERROR").hide();
    }
    else {
        $("#PAYMENT_ADDRESS_ERROR").show();

    }



    if (checkCONTACT_PROVINCE() == true) {
        $("#CONTACT_PROVINCE_ERROR").hide();
    }
    else {
        $("#CONTACT_PROVINCE_ERROR").show();

    }

    if (checkCONTACT_DISTRICT() == true) {
        $("#CONTACT_DISTRICT_ERROR").hide();
    }
    else {
        $("#CONTACT_DISTRICT_ERROR").show();

    }

    if (checkEMPLOYEE_DEBT_CODE() == true) {
        $("#EMPLOYEE_DEBT_CODE_ERROR").hide();
    }
    else {
        $("#EMPLOYEE_DEBT_CODE_ERROR").show();

    }

    if (checkEMPLOYEE_SALE_CODE() == true) {
        $("#EMPLOYEE_SALE_CODE_ERROR").hide();
    }
    else {
        $("#EMPLOYEE_SALE_CODE_ERROR").show();

    }

});