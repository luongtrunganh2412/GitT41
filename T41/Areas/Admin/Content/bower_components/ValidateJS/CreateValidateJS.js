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

//Phần dữ liệu chi tiết người dùng nhập vào
function detail(p) {
    $.ajax({
        url: '@Url.Action("CreateUserReport", "UserManagement")',
        dataType: 'html',
        data:
            {

                CUSTOMER_CODE: $('#CUSTOMER_CODE').val(),

                GENERAL_ACCOUNT_TYPE: $('#GENERAL_ACCOUNT_TYPE').val(),

                GENERAL_FULL_NAME: $('#GENERAL_FULL_NAME').val(),

                GENERAL_SHORT_NAME: $('#GENERAL_SHORT_NAME').val(),

                CONTACT_NAME: $('#CONTACT_NAME').val(),

                ADDRESS: $('#ADDRESS').val(),

                GENERAL_EMAIL: $('#GENERAL_EMAIL').val(),

                CONTACT_PHONE_WORK: $('#CONTACT_PHONE_WORK').val(),

                BUSINESS_TAX: $('#BUSINESS_TAX').val(),

                CONTRACT: $('#CONTRACT').val(),

                CONTACT_ADDRESS: $('#CONTACT_ADDRESS').val(),

                CONTACT_PROVINCE: $('#CONTACT_PROVINCE').val(),

                CONTACT_DISTRICT: $('#CONTACT_DISTRICT').val(),

                STREET: $('#STREET').val(),

                UNIT_CODE: $('#UNIT_CODE').val(),

                SYSTEM_REF_CODE: $('#SYSTEM_REF_CODE').val(),

                //API_KEY: $('#API_KEY').val(),
            },
        success: function (result) {
            alert("Thêm Thành Công");


        }
    })
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

function checkGENERAL_ACCOUNT_TYPE() {
    if (document.getElementById('GENERAL_ACCOUNT_TYPE').value == ""
        || document.getElementById('GENERAL_ACCOUNT_TYPE').value == undefined) {
        $("#GENERAL_ACCOUNT_TYPE_ERROR").show();
        return false;
    }
    else {
        return true;
    }
}

function checkGENERAL_FULL_NAME() {
    if (document.getElementById('GENERAL_FULL_NAME').value == ""
        || document.getElementById('GENERAL_FULL_NAME').value == undefined) {
        $("#GENERAL_FULL_NAME_ERROR").show();
        return false;
    }
    else {
        return true;
    }
}

function checkGENERAL_SHORT_NAME() {
    if (document.getElementById('GENERAL_SHORT_NAME').value == ""
        || document.getElementById('GENERAL_SHORT_NAME').value == undefined) {
        $("#CGENERAL_SHORT_NAME_ERROR").show();
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

function checkADDRESS() {
    if (document.getElementById('ADDRESS').value == ""
       || document.getElementById('ADDRESS').value == undefined) {
        $("#ADDRESS_ERROR").show();
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

function checkCONTRACT() {
    if (document.getElementById('CONTRACT').value == ""
        || document.getElementById('CONTRACT').value == undefined) {
        $("#CONTRACT_ERROR").show();
        return false;
    }
    else {
        return true;
    }
}

function checkCONTACT_ADDRESS() {
    if (document.getElementById('CONTACT_ADDRESS').value == ""
        || document.getElementById('CONTACT_ADDRESS').value == undefined) {
        $("#CONTACT_ADDRESS_ERROR").show();
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
        $("#CONTACT_DISTRICTE_ERROR").show();
        return false;
    }
    else {
        return true;
    }
}

function checkSTREET() {
    if (document.getElementById('STREET').value == ""
        || document.getElementById('STREET').value == undefined) {
        $("#STREET_ERROR").show();
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

function checkSYSTEM_REF_CODE() {
    if (document.getElementById('SYSTEM_REF_CODE').value == ""
        || document.getElementById('SYSTEM_REF_CODE').value == undefined) {
        $("#SYSTEM_REF_CODE_ERROR").show();
        return false;
    }
    else {
        return true;
    }
}




$(document).ready(function () {
    $("#CHECK_GENERAL_EMAIL").hide();

    $("#CUSTOMER_CODE_ERROR").hide();

    $("#GENERAL_ACCOUNT_TYPE_ERROR").hide();

    $("#GENERAL_FULL_NAME_ERROR").hide();

    $("#GENERAL_SHORT_NAME_ERROR").hide();

    $("#CONTACT_NAME_ERROR").hide();

    $("#ADDRESS_ERROR").hide();

    $("#GENERAL_EMAIL_ERROR").hide();

    $("#CONTACT_PHONE_WORK_ERROR").hide();

    $("#BUSINESS_TAX_ERROR").hide();

    $("#CONTRACT_ERROR").hide();

    $("#CONTACT_ADDRESS_ERROR").hide();

    $("#CONTACT_PROVINCE_ERROR").hide();

    $("#CONTACT_DISTRICT_ERROR").hide();

    $("#STREET_ERROR").hide();

    $("#UNIT_CODE_ERROR").hide();

    $("#SYSTEM_REF_CODE_ERROR").hide();

});

$("#btnreportadd").on('click', function () {


    //$("#EMAIL_ERROR").hide();
    //check();
    // goi ham Detail
    if (checkCUSTOMER_CODE() == true && checkGENERAL_ACCOUNT_TYPE() == true && checkGENERAL_FULL_NAME() == true && checkGENERAL_SHORT_NAME() == true && checkCONTACT_NAME() == true && checkADDRESS() == true && checkGENERAL_EMAIL() == true && checkCONTACT_PHONE_WORK() == true && checkBUSINESS_TAX() == true && checkCONTRACT() == true && checkCONTACT_ADDRESS() == true && checkCONTACT_PROVINCE() == true && checkCONTACT_DISTRICT() == true && checkSYSTEM_REF_CODE() == true) {
        detail();
    }

    if (checkCUSTOMER_CODE() == true) {
        $("#CUSTOMER_CODE_ERROR").hide();
    }
    else {
        $("#CUSTOMER_CODE_ERROR").show();

    }

    if (checkGENERAL_ACCOUNT_TYPE() == true) {
        $("#GENERAL_ACCOUNT_TYPE_ERROR").hide();
    }
    else {
        $("#GENERAL_ACCOUNT_TYPE_ERROR").show();

    }


    if (checkGENERAL_FULL_NAME() == true) {
        $("#GENERAL_FULL_NAME_ERROR").hide();
    }
    else {
        $("#GENERAL_FULL_NAME_ERROR").show();

    }

    if (checkGENERAL_SHORT_NAME() == true) {
        $("#GENERAL_SHORT_NAME_ERROR").hide();
    }
    else {
        $("#GENERAL_SHORT_NAME_ERROR").show();

    }

    if (checkCONTACT_NAME() == true) {
        $("#CONTACT_NAME_ERROR").hide();
    }
    else {
        $("#CONTACT_NAME_ERROR").show();

    }

    if (checkADDRESS() == true) {
        $("#ADDRESS_ERROR").hide();
    }
    else {
        $("#ADDRESS_ERROR").show();

    }

    if (checkGENERAL_EMAIL() == true) {
        $("#GENERAL_EMAIL_ERROR").hide();
    }
    else {
        $("#GENERAL_EMAIL_ERROR").show();

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

    if (checkCONTRACT() == true) {
        $("#CONTRACT_ERROR").hide();
    }
    else {
        $("#CONTRACT_ERROR").show();

    }

    if (checkCONTACT_ADDRESS() == true) {
        $("#CONTACT_ADDRESS_ERROR").hide();
    }
    else {
        $("#CONTACT_ADDRESS_ERROR").show();

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

    if (checkCONTACT_DISTRICT() == true) {
        $("#STREET_ERROR").hide();
    }
    else {
        $("#STREET_ERROR").show();

    }

    if (checkUNIT_CODE() == true) {
        $("#UNIT_CODE_ERROR").hide();
    }
    else {
        $("#UNIT_CODE_ERROR").show();

    }

    if (checkSYSTEM_REF_CODE() == true) {
        $("#SYSTEM_REF_CODE_ERROR").hide();
    }
    else {
        $("#SYSTEM_REF_CODE_ERROR").show();

    }

});