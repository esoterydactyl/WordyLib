using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordyLib;
using System.Reflection;
using System.Diagnostics;


namespace WordyLibTests
{
    class Program
    {
        public struct testResult
        {
            public int durationMS;
            public string url;
        }

        static void Main(string[] args)
        {


            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            Version version = assembly.GetName().Version;
            string loadMessage = "Loaded Wordy Lib Tests version: " + version.ToString();
            Console.WriteLine(loadMessage);
            Console.WriteLine("...........");
            Reflector.GetAssemblyInfo();
            Console.WriteLine("Beginning Tests...");
            string[] urls = { "http://stackoverflow.com/questions/20446714/why-is-processing-not-correctly-pulling-data-from-the-serial-port", "http://www.dice.com/job/result/10361248/Python?src=19&q=python", "http://www.disney.com", "http://www.wikipedia.org", "http://www.stackoverflow.com", "http://www.slashdot.org" };
            List<testResult> resultData = new List<testResult>();
            Console.WriteLine("#...........");
            Console.WriteLine("#......#");
            Console.WriteLine("#..#");

            foreach (string url in urls)
            {
                Console.WriteLine("Reference URL is: " + url);
                Console.WriteLine("#...........");
                Stopwatch kWFetchStopwatch = new Stopwatch();
                kWFetchStopwatch.Start();
                List<string> getKWList = Wordy.getKeywords(url);
                Console.WriteLine("#...........");
                foreach (string i in getKWList)
                {
                    if (i == "ERROR")
                    {
                        Console.WriteLine("An ERROR was encountered by Wordy.GetKeywords on URL:  " + url);
                        break;
                    }

                    else
                    {
                        continue;
                    }
                }
                kWFetchStopwatch.Stop();
                int kWCount = getKWList.Count;
                Console.WriteLine("Retrieved " + kWCount.ToString() + " keywords from reference URL.");
                TimeSpan kwFetchDuration = kWFetchStopwatch.Elapsed;
                string kwFetchDurationstring = kwFetchDuration.Milliseconds.ToString();
                Console.WriteLine("This retrieval took: " + kwFetchDurationstring + "ms");
                Console.WriteLine("#...........");
                testResult urlResult = new testResult();
                urlResult.durationMS = kwFetchDuration.Milliseconds;
                urlResult.url = url;
                resultData.Add(urlResult);
            }


            Console.ReadKey();
        }
    }
}
