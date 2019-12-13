using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _3M.Services
{
    public class Notification
    {
        public const string Key = nameof(Notification);

        public const string SuccessType = "success";
        public const string WarningType = "warning";
        public const string ErrorType = "danger";
        public const string InfoType = "info";
        public const string DefaultType = "secondary";

        public Notification(string message, string type)
        {
            Message = message;
            Type = type;
        }

        public string Message { get; set; }

        public string Type { get; set; }
    }
}
