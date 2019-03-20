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
            // TODO: Implement
            throw new NotImplementedException();
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
