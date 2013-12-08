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
            public float kwCount;
            public float keywordsPerMS;
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
            string[] urls = { "http://stackoverflow.com/questions/20446714/why-is-processing-not-correctly-pulling-data-from-the-serial-port", "http://www.dice.com/job/result/10361248/Python?src=19&q=python", "http://www.wikipedia.org", "http://www.stackoverflow.com", "http://www.slashdot.org" };
            List<testResult> resultData = new List<testResult>();
            Console.WriteLine("#..................................................................");
            Console.WriteLine("#...........................START..................................");
            Console.WriteLine("#..................................................................");

            foreach (string url in urls)
            {
                Console.WriteLine("Reference URL is: ");
                Console.WriteLine(url);
                Stopwatch kWFetchStopwatch = new Stopwatch();
                kWFetchStopwatch.Start();
                List<string> getKWList = Wordy.getKeywords(url);

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
                float kWCount = getKWList.Count;
                Console.WriteLine("Retrieved " + kWCount.ToString() + " keywords from reference URL.");
                TimeSpan kwFetchDuration = kWFetchStopwatch.Elapsed;
                string kwFetchDurationstring = kwFetchDuration.Milliseconds.ToString();
                float kwpms = kWCount / kwFetchDuration.Milliseconds;

                Console.WriteLine("This retrieval took: " + kwFetchDurationstring + "ms");
                Console.WriteLine("Keywords Returned Per MS: " + kwpms);
                Console.WriteLine();


                testResult urlResult = new testResult();
                urlResult.durationMS = kwFetchDuration.Milliseconds;
                urlResult.url = url;
                urlResult.kwCount = kWCount;
                urlResult.keywordsPerMS = kwpms;
                resultData.Add(urlResult);
            }

            Console.WriteLine("#..................................................................");
            Console.WriteLine("#............................END...................................");
            Console.WriteLine("#..................................................................");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Job Stats:");
            Console.WriteLine();

            float kwPerfAvg = resultData.Select(p => p.keywordsPerMS).Average();
            float kwTotal = resultData.Select(p => p.kwCount).Sum();
            int runTime = resultData.Select(p => p.durationMS).Sum();
            double jobTimeAverage = resultData.Select(p => p.durationMS).Average();

            Console.WriteLine("Keyword Performance Average:");
            Console.WriteLine(kwPerfAvg.ToString());
            Console.WriteLine();

            Console.WriteLine("Total keywords mined:");
            Console.WriteLine(kwTotal);
            Console.WriteLine();

            Console.WriteLine("Total Runtime in ms:");
            Console.WriteLine(runTime.ToString());
            Console.WriteLine();

            Console.WriteLine("Number of Jobs:");
            Console.WriteLine(urls.Count().ToString());
            Console.WriteLine();

            Console.WriteLine("Average Job Duration:");
            Console.WriteLine(jobTimeAverage.ToString());

            Console.ReadKey();
        }
    }
}
