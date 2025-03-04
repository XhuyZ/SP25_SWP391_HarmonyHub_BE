using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Constant
{
    public class VnPayConstant
    {
        public const string VERSION = "2.1.0";

        public const string PAY_COMMAND = "pay";

        public const string TMN_CODE = "NYXZKEE7";

        public const string HASH_SECRET = "OPCRQ8YH1HYF1788UAV2P4BE5VF1DR5T";

        public const string CURRENCY_CODE = "VND";

        public const string LOCALE = "vn";

        public const string ORDER_TYPE = "other";

        public const string PAY_URL = "https://sandbox.vnpayment.vn/paymentv2/vpcpay.html";

        public const string RETURN_URL = "https://localhost:5264/api/Payment/vnpay-return";
    }
}
