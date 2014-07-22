﻿using Merp.Accountancy.CommandStack.Events;
using Merp.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Merp.Accountancy.QueryStack.Model
{
    public class TimeAndMaterialJobOrderDenormalizer : 
        IHandleMessage<TimeAndMaterialJobOrderRegisteredEvent>
    {
        public void Handle(TimeAndMaterialJobOrderRegisteredEvent message)
        {
            var timeAndMaterialJobOrder = new TimeAndMaterialJobOrder();
            timeAndMaterialJobOrder.OriginalId = message.JobOrderId;
            timeAndMaterialJobOrder.CustomerId = message.CustomerId;
            timeAndMaterialJobOrder.DateOfStart = message.DateOfStart;
            timeAndMaterialJobOrder.DateOfExpiration = message.DateOfExpiration;
            timeAndMaterialJobOrder.Name = message.JobOrderName;
            timeAndMaterialJobOrder.Number = message.JobOrderNumber;
            timeAndMaterialJobOrder.Value = message.Value;
            timeAndMaterialJobOrder.IsCompleted = false;
            timeAndMaterialJobOrder.IsTimeAndMaterial = true;
            timeAndMaterialJobOrder.IsFixedPrice = false;

            using(var db = new AccountancyContext())
            {
                db.JobOrders.Add(timeAndMaterialJobOrder);
                db.SaveChanges();
            }
        }

        //public void Handle(FixedPriceJobOrderExtendedEvent message)
        //{
        //    using(var db = new MerpContext())
        //    {
        //        var jobOrder = db.JobOrders.Select(jo => jo.Id).OfType<FixedPriceJobOrder>().Single();
        //        jobOrder.DueDate = message.NewDueDate;
        //        jobOrder.Price = message.Price;
        //        db.SaveChanges();
        //    }
        //}
    }
}
