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
        public string CONTACT_NAME { get; set; }
        public string DATE_CREATE { get; set; }
        public string DATE_END { get; set; }
        public string CONTACT_PHONE_WORK { get; set; }
        public string GENERAL_EMAIL { get; set; }
        public string CONTACT_ADDRESS { get; set; }
        public string BUSINESS_TAX { get; set; }
        public string UNIT_CODE { get; set; }
        public string CONTRACT_NUMBER { get; set; }
        public string CUSTOMER_ACTIVE { get; set; }
        public string TOTAL_CUSTOMER_CODE { get; set; }
        public string PAYMENT_ADDRESS { get; set; }
        public string PAYMENT_METHOD { get; set; }
        public string CONTACT_PROVINCE { get; set; }
        public string CONTACT_DISTRICT { get; set; }
        public string EMPLOYEE_DEBT_CODE { get; set; }
        public string EMPLOYEE_SALE_CODE { get; set; }
        
    }

    //Parameter truyền vào DB để edit theo thông tin người dùng
    public class PARAMETER_EDIT
    {
        public int edit_id { get; set; }
    }

    //Lấy mã quận 
    public class GETDISTRICT
    {
        public string DISTRICTCODE { get; set; }

        public string DISTRICTNAME { get; set; }

    }

    //Parameter truyền vào DB để delete theo thông tin người dùng
    public class PARAMETER_DELETE
    {
        public int delete_id { get; set; }
    }

    //Phần lấy dữ iệu của bảng business_profile
    public class UserManagementDetail
    {
        public int CUSTOMER_ID { get; set; }
        public String CUSTOMER_CODE { get; set; }
        public String CONTACT_NAME { get; set; }
        public String DATE_CREATE { get; set; }
        public String DATE_END { get; set; }
        public String CONTACT_PHONE_WORK { get; set; }
        public String GENERAL_EMAIL { get; set; }
        public String CONTACT_ADDRESS { get; set; }
        public String BUSINESS_TAX { get; set; }
        public String UNIT_CODE { get; set; }
        public String CONTRACT_NUMBER { get; set; }
        public String CUSTOMER_ACTIVE { get; set; }
        public String TOTAL_CUSTOMER_CODE { get; set; }
        public String PAYMENT_ADDRESS { get; set; }
        public String PAYMENT_METHOD { get; set; }
        public String CONTACT_PROVINCE { get; set; }
        public String CONTACT_DISTRICT { get; set; }
        public String EMPLOYEE_DEBT_CODE { get; set; }
        public String EMPLOYEE_SALE_CODE { get; set; }



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