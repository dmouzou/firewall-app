using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace _3M_Firewall.Helpers
{
    class NSLookup
    {
        /// <summary>
        ///
        /// <description>Application entry point.</description>
        /// <param name="args">Host Address, IP Address or -help command line options</param>
        /// <return>int Return code 0 for success -1 for failure or error</return>
        ///
        /// </summary>
        [STAThread]
        public static string[]? NSLookupAttr(string[] args)
        {
            //Make sure we were passed something, otherwise return help.
            if (args.Length < 1 || args[0].Equals("-help"))
            {
                return null;
            }
            else
            {
                //We have something, try to look it up....
                try
                {
                    //The IP or Host Entry to lookup
                    IPHostEntry ipEntry;
                    //The IP Address Array. Holds an array of resolved Host Names.
                    IPAddress[] ipAddr;
                    //Value of alpha characters
                    char[] alpha = "aAbBcCdDeEfFgGhHiIjJkKlLmMnNoOpPqQrRsStTuUvVwWxXyYzZ-".ToCharArray();
                    //If alpha characters exist we know we are doing a forward lookup
                    if (args[0].IndexOfAny(alpha) != -1)
                    {
                        ipEntry = Dns.GetHostEntry(args[0]);
                        ipAddr = ipEntry.AddressList;
                        string[] names = new string[ipAddr.Length];
                        for (int i = 0; i < ipAddr.Length; i++)
                        {
                            names[i] = ipAddr[i].ToString();
                        }

                        return names;
                    }
                    //If no alpha characters exist we do a reverse lookup
                    else
                    {
                        ipEntry = Dns.Resolve(args[0]);
                        return null;
                    }
                }
                catch (System.Net.Sockets.SocketException)
                {
                    return null;
                }
                catch (System.FormatException)
                {
                    return null;
                }
            }
        }
    }
}
