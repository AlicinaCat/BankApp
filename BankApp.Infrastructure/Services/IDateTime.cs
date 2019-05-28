using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Text;

namespace BankApp.Infrastructure.Services
{
    public interface ISystemClock
    {
        DateTime GetCurrentTime();
    }

    public class SystemClock : ISystemClock
    {
        public DateTime GetCurrentTime()
        {
            return DateTime.UtcNow;
        }
    }
}

