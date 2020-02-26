using System;
using System.Collections.Generic;

namespace Chart.Api
{
    public abstract class Helper
    {
        private static Random _randIndex = new Random();
        private static string GetRandom( IList<string> randomItems )
        {
            return randomItems[_randIndex.Next(randomItems.Count)]; //make sure it doest go out of bound
        }

        internal static string MakeRandomName()
        {
            string firstName = GetRandom( firstNames );
            string lastName = GetRandom( lastNames );

            return firstName + lastName;
        }



        internal static string MakeRandomEmail(string costumerName)
        {
            return $"contact@{costumerName.ToLower()}.com";
        }

        internal static string MakeRandomState()
        {
            return GetRandom(states);
        }


        internal static decimal GetRandomTotal()
        {
            return _randIndex.Next( 100 , 5000 );
        }


        internal static DateTime GetRandomPlaced()
        {
            var end = DateTime.Now;
            var start = end.AddDays(-90);

            TimeSpan possibleSpan = end- start;
            TimeSpan newSpan = new TimeSpan( 0 , _randIndex.Next( 0 , (int)possibleSpan.TotalMinutes ) ,0 );
            //random 'offset' for a time interval with a max of our possible span

            return start + newSpan;
        }
        
        internal static DateTime? GetRandomCompleted( DateTime orderPlaced )
        {
            var now = DateTime.Now;
            var minLeadTime = TimeSpan.FromDays(7);
            var timePassed = now - orderPlaced;

            if ( timePassed < minLeadTime )
            {
                return null;
            }

            return orderPlaced.AddDays( _randIndex.Next( 7 , 14 ));
        }


        private static readonly List<string> firstNames = new List<string>()
        {
            "Barbara",
            "Ines",
            "Freddi",
            "Ambur",
            "Clemmie",
            "Elka",
            "Ilene",
            "Thacher",
            "Romola"
        };

        private static readonly List<string> lastNames = new List<string>()
        {
            "MacCaffrey",
            "Brushfeild",
            "Boagey",
            "Betchley",
            "Twiddel",
            "Dowson",
            "Nasbey",
            "Rumgay",
            "Mynett"
        };

        private static readonly List<string> states = new List<string>()
        {
            "MA",
            "VA",
            "CO",
            "FL",
            "TX",
            "IL",
        };
    }
}