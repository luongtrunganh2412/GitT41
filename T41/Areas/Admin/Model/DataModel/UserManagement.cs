using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace T41.Areas.Admin.Model.DataModel
{
    //Parameter truyền vào DB để tạo mới dữ liệu bảng business_profile
    public class PARAMETER_BUSINESS
    {
        public int EDIT_ID { get; set; }
        public string CUSTOMER_CODE { get; set; }
        public string GENERAL_ACCOUNT_TYPE { get; set; }
        public string GENERAL_FULL_NAME { get; set; }
        public string GENERAL_SHORT_NAME { get; set; }
        public string CONTACT_NAME { get; set; }
        public string ADDRESS { get; set; }
        public string GENERAL_EMAIL { get; set; }
        public string CONTACT_PHONE_WORK { get; set; }
        public string BUSINESS_TAX { get; set; }
        public string CONTRACT { get; set; }
        public string CONTACT_ADDRESS { get; set; }
        public string CONTACT_PROVINCE { get; set; }
        public string CONTACT_DISTRICT { get; set; }
        public string STREET { get; set; }
        public string UNIT_CODE { get; set; }
        public string SYSTEM_REF_CODE { get; set; }
        
        
        
    }

    //Parameter truyền vào DB để edit theo thông tin người dùng
    public class PARAMETER_EDIT
    {
        public int edit_id { get; set; }
    }

    //Parameter truyền vào DB để delete theo thông tin người dùng
    public class PARAMETER_DELETE
    {
        public int delete_id { get; set; }
    }

    //Phần lấy dữ iệu của bảng business_profile
    public class UserManagementDetail
    {
        public int Id { get; set; }
        public String Address { get; set; }
        public String BusinessTax { get; set; }
        public String ContactName { get; set; }
        public String ContactAddress { get; set; }
        public String ContactDistrict { get; set; }
        public String ContactProvince { get; set; }
        public String ContactPhoneWork { get; set; }
        public String GeneralShortName { get; set; }
        public String CustomerCode { get; set; }
        public String GeneralEmail { get; set; }
        public String GeneralAccountType { get; set; }
        public String GeneralFullName { get; set; }
        public String Contract { get; set; }
        public String UnitCode { get; set; }
        public String Street { get; set; }
        public String SystemRefCode { get; set; }
        public String ApiKey { get; set; }
        

    }

    public class ReturnUserManagement
    {
        public string Code { get; set; }

        /// <summary>
        /// Message
        /// </summary>
        public string Message { get; set; }

        public string Value { get; set; }
        /// <summary>
        /// Value
        /// </summary>
        public int Total { get; set; }

        public UserManagementDetail UserManagementReport { get; set; }
        public List<UserManagementDetail> ListUserManagementReport;

       
        public MetaData MetaData { get; set; }


    }
}