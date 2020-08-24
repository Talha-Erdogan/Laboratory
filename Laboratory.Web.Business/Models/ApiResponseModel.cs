using System;
using System.Collections.Generic;
using System.Text;

namespace Laboratory.Web.Business.Models
{
    public class ApiResponseModel<TData> : BaseResponseModel
    {
        public TData Data { get; set; }

    }
}
