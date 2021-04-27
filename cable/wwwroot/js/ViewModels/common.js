var addZoneUrl = "/InsertData/InsertZone";
var editZoneUrl = "/EditData/EditZone";
var addAdminUrl = "/InsertData/InsertAdmin";
var editAdminUrl = "/EditData/EditAdmin";
var addAreaUrl = "/InsertData/InsertArea";
var editAreaUrl = "/EditData/EditArea";
var addProviderUrl = "/InsertData/InsertProvider";
var editProviderUrl = "/EditData/EditProvider";
var addPackageUrl = "/InsertData/InsertPackage";
var editPackageUrl = "/EditData/EditPackage";
var addUserUrl = "/InsertData/InsertUser";
var editUserUrl = "/EditData/EditUser";
var addCustomerUrl = "/InsertData/InsertCustomer";
var editCustomerUrl = "/EditData/EditCustomer";
var AddCustomerSTBUrl = "/InsertData/AddCustomerSTB";
var generateInvoiceUrl = "/InsertData/GenerateInvoice";
var cusInvoicePayUrl = "/InsertData/PayCus";
var emailConfigUrl = "/InsertData/EmailConfigSetting";
var announcementUrl = "/InsertData/Announcement";
var GenerateBulkInvoiceUrl = "/InsertData/GenerateBulkInvoice";
var bulkUploadUrl = "/InsertData/InsertBulk";

function JsonPOST(url, data) {
    $.ajax({
        url: url,
        dataType: 'json',
        type: 'post',
        contentType: 'application/json',
        data: JSON.stringify(data),
        processData: false,
        async: true,
        success: function (response) {
            sweetAlert('Congratulations!', response.status, 'success');
            $(".modal").modal("hide");
        },
        error: function (e) {
            swal("Oops", e.responseText, "error");
            // $(".modal").modal("hide");
        }
    });
}

function DoAction(action, data) {

    if (action === "AddZone") {
        JsonPOST(addZoneUrl, data);
        LoadData();
    }
    if (action === "EditZone") {
        JsonPOST(editZoneUrl, data);
        LoadData();
    }
    if (action === "AddAdmin") {
        JsonPOST(addAdminUrl, data);
        LoadData();
    }
    if (action === "EditAdmin") {
        JsonPOST(editAdminUrl, data);
        LoadData();
    }
    if (action === "AddArea") {
        JsonPOST(addAreaUrl, data);
        LoadData();
    }
    if (action === "EditArea") {
        JsonPOST(editAreaUrl, data);
        LoadData();
    }
    if (action === "AddProvider") {
        JsonPOST(addProviderUrl, data);
        LoadData();
    }
    if (action === "EditProvider") {
        JsonPOST(editProviderUrl, data);
        LoadData();
    }
    if (action === "AddPackage") {
        JsonPOST(addPackageUrl, data);
        LoadData();
    }
    if (action === "EditPackage") {
        JsonPOST(editPackageUrl, data);
        LoadData();
    }
    if (action === "AddUser") {
        JsonPOST(addUserUrl, data);
        LoadData();
    }
    if (action === "EditUser") {
        JsonPOST(editUserUrl, data);
        LoadData();
    }
    if (action === "AddCustomer") {
        JsonPOST(addCustomerUrl, data);
        LoadData();
    }
    if (action === "EditCustomer") {
        JsonPOST(editCustomerUrl, data);
        LoadData();
    }
    if (action === "AddCusSTB") {
        JsonPOST(AddCustomerSTBUrl, data);
        LoadData();
    }
    if (action === "GenerateCusINV") {
        JsonPOST(generateInvoiceUrl, data);
        LoadData();
    }
    if (action === "PayInvoice") {
        JsonPOST(cusInvoicePayUrl, data);
        LoadData();
    }
    if (action === "UploadBulk") {
        JsonPOST(bulkUploadUrl, data);
        LoadData();
    }
    if (action === "EmailSetting") {
        JsonPOST(emailConfigUrl, data);
        LoadData();
    }
    if (action === "Announcement") {
        JsonPOST(announcementUrl, data);
        LoadData();
    }
    if (action === "GenerateBulkInvoice") {
        JsonPOST(GenerateBulkInvoiceUrl, data);
        LoadData();
    }
    
}

