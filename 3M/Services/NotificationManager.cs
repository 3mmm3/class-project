using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _3M.Services
{
    public class NotificationManager
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ITempDataDictionaryFactory _tempDataDictionaryFactory;

        public NotificationManager(IHttpContextAccessor httpContextAccessor, ITempDataDictionaryFactory tempDataDictionaryFactory)
        {
            _httpContextAccessor = httpContextAccessor;
            _tempDataDictionaryFactory = tempDataDictionaryFactory;

        }

        public void Set(Notification notification)
        {
            var httpContext = _httpContextAccessor.HttpContext;
            var tempData = _tempDataDictionaryFactory.GetTempData(httpContext);

            if (notification == null)
                tempData.Remove(Notification.Key);
            else
                tempData[Notification.Key] = JsonConvert.SerializeObject(notification);
        }

        public Notification Get()
        {
            var httpContext = _httpContextAccessor.HttpContext;
            var tempData = _tempDataDictionaryFactory.GetTempData(httpContext);

            var serializedNotification = tempData[Notification.Key] as string;
            if (string.IsNullOrEmpty(serializedNotification))
                return null;
            return JsonConvert.DeserializeObject<Notification>(serializedNotification);
        }
    }
}
