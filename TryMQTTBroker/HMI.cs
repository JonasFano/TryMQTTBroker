using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TryMQTTBroker
{
    internal class HMI
    {
        public static void Main(string[] args)
        {
            var server = new BrokerServer();
            var v = "";
            while (true)
            {
                if (v == "k") {
                    Console.WriteLine(server.Payload);
                }
                if (v == "c")
                {
                    break;
                }

                v = Console.ReadLine();
            }

            server.Close();
        }
    }
}
