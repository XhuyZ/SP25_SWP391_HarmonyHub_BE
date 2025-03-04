using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Constants
{
    public enum TransactionConstant
    {
        CHARGE = 1,
        TRANSFER = 2,
        POST = 3,
        PURCHASE = 4,
        REFUND = 0
    }
}
