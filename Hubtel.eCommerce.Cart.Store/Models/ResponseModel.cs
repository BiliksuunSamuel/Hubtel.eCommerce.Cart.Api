using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Hubtel.eCommerce.Cart.Store.Models
{
    public class ResponseModel
    {
        public dynamic Data { get; set; }
        public string Message { get; set; }
        public int Status { get; set; }
    }
}
