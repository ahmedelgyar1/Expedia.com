using expedia.com;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace expedia
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DataGenerator.startGenrateFlights();
            DataGenerator.LoadHotels();
            IUserManager userManager = new UserManager();
            Expedia d = new Expedia(userManager);
            d.Start();

        }

    }
}
    
    


