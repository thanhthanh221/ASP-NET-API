using System;
using Domain_Layer.Base;

namespace Domain_Layer.Entities.Order
{
    public class DeliveryInformation 
    {
        public DeliveryInformation(DateTimeOffset timeLine , string status)
        {
            TimeLine = timeLine;
            Status = status;
        }

        public DateTimeOffset TimeLine {get; set;}
        public string Status {get; set;}
        
    }
}