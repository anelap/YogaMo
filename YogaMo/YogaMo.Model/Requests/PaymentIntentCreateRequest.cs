using System;
using System.Collections.Generic;
using System.Text;

namespace YogaMo.Model.Requests
{
    public class PaymentIntentCreateRequest
    {
        public string PaymentMethodId { get; set; }
        public int OrderId { get; set; }
    }
}
