using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using T41.Areas.Admin.Model.DataModel;

namespace T41.Areas.Admin.Model.BusinessModel
{
    public class DataInitializer:DropCreateDatabaseIfModelChanges<DTPowerDBContext>
    {
        protected override void Seed(DTPowerDBContext context)
        {
            var admin = new Administrator()
            {
                UserName = "admin",
                PassWord = "e124de33f57200fa115638f890882219",  //123123
                Active = 1,
                IsAdmin = 1,
                Email = "trungndems@gmail.com",
                FullName = "Nguyen Duc Trung",
                Avatar = "~/Areas/Admin/Content/Image/avatar1.jpg"
            };
            context.Administrator.Add(admin);

            var nv1 = new Administrator()
            {
                UserName = "nv1",
                PassWord = "e69de489e0fc381cd7ebae2e6c5eae7c",
                Active = 1,
                IsAdmin = 0,
                Email = "trungndems@gmail.com",
                FullName = "Nguyen Duc Trung",
                Avatar = "~/Areas/Admin/Content/Image/avatar3-128x128.png"
            };
            context.Administrator.Add(nv1);
            context.SaveChanges();
        }
    }
}