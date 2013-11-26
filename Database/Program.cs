using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.DemoApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            IDatabaseManager databaseManager;
            var database = databaseManager.OpenDatabase("");
        }
    }
}
