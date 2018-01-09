using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GarbageCollector
{
    public class PickupCalendar
    {
        public string day = "";
        //enum days { Sunday, Monday, Tuesday, Wednesday, Thursday, Friday, Saturday };

        public PickupCalendar(string day)
        {
            this.day = day;
        }

        public List<DateTime> CreatePickupList()
        {
            List<DateTime> pickupDateList = new List<DateTime> { };

            for (int i =0; i<90; i++)
            {
                DateTime date = DateTime.Now.AddDays(i);
                string calendarDay = date.ToString("dddd");
                if (calendarDay == day)
                {
                    pickupDateList.Add(DateTime.Parse(date.ToString("yyyy-MM-dd")));
                }
            }
            return pickupDateList;
        }
    }
}