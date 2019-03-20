namespace Livit
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    
    public class Restaurant
    {
        public const int weekLength = 7;

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
            string[] element;

            for(int i = 0; i < groupedHours.Count; i++)
            {
                element = groupedHours[i];

                if(i != 0) result = result+", ";

                result = result+parseDays(element[0])+": "+element[1];
            }

            return(result);
        }

        private string parseDays(string inString)
        {
            string[] weekdays = {"Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat"};
            int firstDayNo = inString[0]-'0';
            string firstDayString = weekdays[firstDayNo];
            int lastDayIndex = inString.Length-1;
            int lastDayNo = inString[lastDayIndex]-'0';
            string lastDayString = weekdays[lastDayNo];
            string result = firstDayString;

            if(inString.Length == 1) return(result);
            else
            {
                result = firstDayString+" - "+lastDayString;
                return(result);
            }
        }

        private List<string[]> GroupHours(List<string> inputHours)
        {
            List<string[]> result = new List<string[]>();
            string days = "", hours = "";
            string[] element;

            for(int i = 0; i < inputHours.Count; i++)
            {
                if(IsFirstOfItsKind(i, inputHours[i], inputHours))
                {
                    days = "";
                    hours = inputHours[i];
                    for(int j = 0; j < inputHours.Count; j++)
                    {
                        if(inputHours[i].Equals(inputHours[j]))
                        {
                            days = days+j;
                        }
                    }
                    element = new string[] {days, hours};
// Console.WriteLine("["+element[0]+", "+element[1]+"]");
                    result.Add(element);
                }
            }

            return(result);
        }

        private bool IsFirstOfItsKind(int n, string s, List<string> array)
        {
            if(n > array.Count)
            {
                Console.WriteLine("Specified index out of range.");
                return(false);
            }

            for(int i = 0; i < n; i++)
            {
                if(array[i].Equals(s)) return(false);
            }
            return(true);
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

        private void claim(int i, bool b)
        {
            if(!b) throw new Exception("Test "+i+" failed.");
            else Console.WriteLine("Test "+i+" passed!");
        }

        private void test1()
        {
            var restaurant = new Restaurant(
                new OpeningHour(8,16), // Sunday
                new OpeningHour(8,17), // Monday
                new OpeningHour(8,18), // Tuesday
                new OpeningHour(8,19), // Wednesday
                new OpeningHour(8,20), // Thursday
                new OpeningHour(8,21), // Friday
                new OpeningHour(8,22)  // Saturday
            );

            claim(1, ("Sun: 8-16, Mon: 8-17, Tue: 8-18, Wed: 8-19, Thu: 8-20, Fri: 8-21, Sat: 8-22").Equals(restaurant.GetOpeningHours()));
        }

        private void test2()
        {
            var restaurant = new Restaurant(
                new OpeningHour(8,16), // Sunday
                new OpeningHour(8,16), // Monday
                new OpeningHour(8,16), // Tuesday
                new OpeningHour(8,16), // Wednesday
                new OpeningHour(8,20), // Thursday
                new OpeningHour(8,21), // Friday
                new OpeningHour(8,22)  // Saturday
            );

            claim(2, ("Sun - Wed: 8-16, Thu: 8-20, Fri: 8-21, Sat: 8-22").Equals(restaurant.GetOpeningHours()));
        }

        private void test3()
        {
            var restaurant = new Restaurant(
                new OpeningHour(8,16), // Sunday
                new OpeningHour(8,16), // Monday
                new OpeningHour(8,16), // Tuesday
                new OpeningHour(8,17), // Wednesday
                new OpeningHour(8,18), // Thursday
                new OpeningHour(8,20), // Friday
                new OpeningHour(8,20)  // Saturday
        );

            claim(3, ("Sun - Tue: 8-16, Wed: 8-17, Thu: 8-18, Fri - Sat: 8-20").Equals(restaurant.GetOpeningHours()));
        }

        private void run()
        {
            test1();
            test2();
            test3();
        }


        public static void Main(string [] args)
        {
            Restaurant program = new Restaurant();
            Console.WriteLine("Running Restaurant...");
            program.run();
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
