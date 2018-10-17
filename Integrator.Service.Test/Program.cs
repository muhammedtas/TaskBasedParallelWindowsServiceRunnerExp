using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integrator.Service.Test
{
    class Program
    {
        public static Service1 service = new Service1();
        public static Logger Logger = LogManager.GetCurrentClassLogger();
        static void Main(string[] args)
        {
            try
            {
                throw new Exception();
            }
            catch (Exception ex)
            {
                Logger.Warn(ex, "Test brow");
            }
            // Service1 service = new Service1();

            service.DebugStart(true);
            Console.ReadLine();
        }
    }
}
