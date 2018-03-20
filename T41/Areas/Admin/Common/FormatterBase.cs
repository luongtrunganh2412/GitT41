using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace T41.Areas.Admin.Common
{
    public class FormatterBase
    {
        protected FormatterBase() { }

        public virtual string Message
        {
            get
            {
                return "";
            }
        }
    }
}