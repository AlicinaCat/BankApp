using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Text;

namespace BankApp.Infrastructure.Services
{
    public static class SystemTime
    {
        public static Func<DateTime> Now = () => DateTime.Now;
    }
}

