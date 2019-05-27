using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Text;

namespace BankApp.Infrastructure.Services
{
    public interface IDateTimeNowProvider
    {
        DateTime Now { get; }
    }

    public class DateTimeNowProvider : IDateTimeNowProvider
    {
        public DateTime Now { get { return DateTime.Now; } }
    }

    public class CustomDateTimeNowProvider : IDateTimeNowProvider
    {
        public DateTime GivenDate { get; set; }
        public DateTime Now { get { return GivenDate; } }
    }
}

