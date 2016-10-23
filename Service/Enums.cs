using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockExchange.Service
{
    public enum CustomerFilter
    {
        [Description("All")] All,

        [Description("Username")] Username,

        [Description("Email")] Email,

        [Description("Vendor")] MyFollowedGroups
    }
}
