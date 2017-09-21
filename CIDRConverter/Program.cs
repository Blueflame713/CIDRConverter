using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CIDRConverter
{
    class Program
    {
        static string toip(uint ip)
        {
            return String.Format("{0}.{1}.{2}.{3}", ip >> 24, (ip >> 16) & 0xff, (ip >> 8) & 0xff, ip & 0xff);
        }

        [STAThread]
        static void Main(string[] args)
        {
            bool loop = true;
            while (loop)
            {
                
                string IP = Console.ReadLine();
                if (IP == "q")
                    break;

                if (IP == "help")
                {
                    Console.WriteLine("q to quit, takes in CIDR notation and outputs IP ranges to your clipboard. \nCurrently in alpha if the process hangs just clear lines by hitting enter");
                }
                string[] parts = IP.Split('.', '/');

                try
                {
                    uint ipnum = (Convert.ToUInt32(parts[0]) << 24) |
                        (Convert.ToUInt32(parts[1]) << 16) |
                        (Convert.ToUInt32(parts[2]) << 8) |
                        Convert.ToUInt32(parts[3]);

                    int maskbits = Convert.ToInt32(parts[4]);
                    uint mask = 0xffffffff;
                    mask <<= (32 - maskbits);

                    uint ipstart = ipnum & mask;
                    uint ipend = ipnum | (mask ^ 0xffffffff);

                    string results = toip(ipstart) + "-" + toip(ipend);

                    Clipboard.SetText(results);
                    Console.WriteLine(results);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error type help for help");
                }
            }

        }
    }
}
