using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEc.DataAccess.Context
{
    public class AutherManagerConfiguration
    {
        public AutherManagerConfiguration(string autherString)
        {
            AutherString = autherString;
        }

        public string AutherString { get; set; }
    }
}
