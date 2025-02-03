using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static expedia.com.functionHelper;

namespace expedia.com
{
    internal class AirLine
    {
       
         internal string Name { get; set; }                
         internal string Code { get; set; }
       
            public AirLine(string name, string code)
            {
                Name = name;
                Code = code;
              
            }

       
        
    }
    
}
