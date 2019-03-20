namespace Livit
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    
    public class Restaurant
    {
        public WeekCollection<OpeningHour> OpeningHours { get; private set; }

        public Restaurant() {
            // No opening hours available for restaurant
        }

        public Restaurant(OpeningHour monday, OpeningHour tuesday, OpeningHour wednesday, OpeningHour thursday, OpeningHour friday, OpeningHour saturday, OpeningHour sunday)
        {
            OpeningHours = new WeekCollection<OpeningHour>(monday, tuesday, wednesday, thursday, friday, saturday, sunday);
        }

        public string GetOpeningHours()
        {
            string result = "", dayString = "";
            List<string> hours = BuildHours(OpeningHours);
            List<string[]> groupedHours = GroupHours(hours);
            int i = 0;

            foreach(DayOfWeek day in Enum.GetValues(typeof(DayOfWeek)).OfType<DayOfWeek>().ToList())
            {
                dayString = day.ToString();
                dayString = dayString.Substring(0, 3);
                result = result+dayString+": "+hours[i];
                i++;
                
                if(day != DayOfWeek.Saturday) result = result+", ";
            }

            return(result);
        }
        
        private List<string[]> GroupHours(List<string> InputHours)
        {
            List<string[]> result = new List<string[]>();
            string days = "", hours = "";
            string[] element;
            
            for(int i = 0; i < InputHours.Count; i++)
            {
                days = ""+i;
                for(int j = i+1; j < InputHours.Count; j++)
                {
                    if(InputHours[i] == InputHours[j])
                    {
                        days = days+j;
                        InputHours.RemoveAt(j);
                    }
                }
                hours = InputHours[i];
                element = new string[] {days, hours};
                result.Add(element);
            }
            
            return(result);
        }
        
        private List<string> BuildHours(WeekCollection<OpeningHour> OpeningHours)
        {
            List<string> result = new List<string>();
            
            foreach(DayOfWeek day in Enum.GetValues(typeof(DayOfWeek)).OfType<DayOfWeek>().ToList())
            {
                result.Add(OpeningTimeToString(OpeningHours.Get(day)));
            }
            
            return(result);
        }
        
        private string OpeningTimeToString(OpeningHour openingHour)
        {
            string opens = ParseHour(openingHour.OpeningTime);
            string closes = ParseHour(openingHour.ClosingTime);
            string result = opens+"-"+closes;
            
            return(result);
        }
        
        private string ParseHour(TimeSpan hour)
        {
            string hourString = hour.ToString();
            string result = "";
            
            if(hourString[0] == '0') result = hourString[1].ToString();
            else result = hourString[0].ToString()+hourString[1].ToString();
            
            return(result);
        }
    }

    public class OpeningHour
    {
        public TimeSpan OpeningTime { get; private set; }
        public TimeSpan ClosingTime { get; private set; }

        public OpeningHour(TimeSpan openingTime, TimeSpan closingTime)
        {
            OpeningTime = openingTime;
            ClosingTime = closingTime;
        }

        public OpeningHour(int openingHour, int closingHour)
        {
            OpeningTime = TimeSpan.FromHours(openingHour);
            ClosingTime = TimeSpan.FromHours(closingHour);
        }

    }

    public class WeekCollection<T>
    {
        private Dictionary<DayOfWeek, T> _collection;

        public WeekCollection(T sunday, T monday, T tuesday, T wednesday, T thursday, T friday, T saturday)
        {
            _collection = new Dictionary<DayOfWeek, T>();
            _collection.Add(DayOfWeek.Sunday, sunday);
            _collection.Add(DayOfWeek.Monday, monday);
            _collection.Add(DayOfWeek.Tuesday, tuesday);
            _collection.Add(DayOfWeek.Wednesday, wednesday);
            _collection.Add(DayOfWeek.Thursday, thursday);
            _collection.Add(DayOfWeek.Friday, friday);
            _collection.Add(DayOfWeek.Saturday, saturday);
        }

        public T Get(DayOfWeek dayOfWeek)
        {
            return _collection[dayOfWeek];
        }
    }
}
