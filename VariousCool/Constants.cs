using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VariousCool
{
    public static class Constants
    {
        public static class DataBase
        {
            public static string DATABASE_FILENAME { get; } = "data.sqlite";
            public static string DATABASE_USER_TABLE { get; } = "USER_TAB";
            public static string DATABASE_TIPS_TABLE { get; } = "TIPS_TAB";
        }
    }
}
