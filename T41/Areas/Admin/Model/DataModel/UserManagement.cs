using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace T41.Areas.Admin.Model.DataModel
{
       
    //Phần lấy dữ liệu của bảng crm_sales_order
    public class UserManagement_CRM_Detail
    {
        public int STT { get; set; }
        public String ACCOUNT_ID { get; set; }
        public String DEAL_ID { get; set; }
        public String CONTACT_ID { get; set; }
        public String SALES_ORDER_OWNER_ID { get; set; }
        public String PO_ACCEPTANCE { get; set; }
        public String CUSTOMER_NO { get; set; }
        public String PICKUP_NAME { get; set; }
        public String PICKUP_FULL_ADDRESS { get; set; }
        
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

        public UserManagement_CRM_Detail UserManagement_CRM_Report { get; set; }
        public List<UserManagement_CRM_Detail> ListUserManagement_CRM_Report;

        


    }
}