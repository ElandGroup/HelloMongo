using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace HelloMongo.Contexts
{
    public  class PangContext
    {
        private PangContext()
        {

        }
        private static PangContext current;

        public static PangContext Current
        {
            get
            {
                if (current == null)
                    current = new PangContext();
                return current;
            }
        }
        public  IConfigurationRoot Configuration { get; set; }
        public  int MaxJsonLength
        {

            get
            {
                return int.MaxValue;
            }
        }
    }
}
