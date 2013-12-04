using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;


namespace WordyLib
{
    public static class SimpleCleaner
    {
        public static string[] degreaseString(string args)
        {
            string unCleanInput = args;

            char[] invalid = System.IO.Path.GetInvalidFileNameChars();
            string[] junkData = { "&nbsp;", "&nbsp", ">", "<", "&copy" };
            List<string> invalidList = new List<string>();

            foreach (string junk in junkData)
            {
                unCleanInput = unCleanInput.Replace(junk, " ");
                invalidList.Add(junk);
            }

            foreach (char badChar in invalid)
            {
                unCleanInput = unCleanInput.Replace(badChar, ' ');
                string badCharString = badChar.ToString();
                invalidList.Add(badCharString);
            }

            List<string> CleanWords = new List<string>();
            string[] strippedAndSplit = unCleanInput.Split(' ');
            foreach (string word in strippedAndSplit)
            {
                bool invalidListCheck = invalidList.Contains(word);
                bool regexWordCheck = Regex.IsMatch(word, "^[a-zA-Z]+$");
                bool blackList = BlacklistCheck.IsBlackListed(word);

                if (invalidListCheck == false && blackList == false && regexWordCheck == true)
                {
                    CleanWords.Add(word.Trim());
                }
            }

            string[] degreasedStringArray = CleanWords.ToArray();

            return degreasedStringArray;
        }

    }
    
    //#########################################################################

    public static class SimpleCrawler
    {
        public static string[] GetHREFs(string args)
        {
            List<string> URLTextList = new List<string>();
            Console.WriteLine("loaded SimpleCrawler");
            HtmlAgilityPack.HtmlDocument DocToCrawl = new HtmlDocument();
            DocToCrawl.LoadHtml(args);
            string xpath = "//body//a";
            HtmlNodeCollection URLbody = DocToCrawl.DocumentNode.SelectNodes(xpath);
            Console.WriteLine("simple scraper Processed!");
            foreach (HtmlNode node in URLbody)
            {
                URLTextList.Add(node.InnerText);
            }
            string[] URLArray = URLTextList.ToArray();

            return URLArray;

        }
    }

    //#########################################################################

    public static class SimpleParser
    {
        public static string[] stripHTML(string args)
        {
            List<string> innertextList = new List<string>();
            List<string> final = new List<string>();

            char[] invalid = System.IO.Path.GetInvalidFileNameChars();
            string[] junkData = { "&nbsp", ">", "<", "&copy" };
            List<string> invalidList = new List<string>();
            //Building a string list of bad characters:::::::::::::::::::::::::::::::::::::::::::::::::::
            //Improve this.  FFS.
            foreach (char badChar in invalid)
            {
                string badCharString = badChar.ToString();
                invalidList.Add(badCharString);
            }
            foreach (string junk in junkData)
            {
                invalidList.Add(junk);
            }
            //Done Building the bad list::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
            //Load the HTML doc
            Console.WriteLine("loaded SimpleParser");
            HtmlAgilityPack.HtmlDocument theDoc = new HtmlDocument();
            theDoc.LoadHtml(args);
            string xpath = "//text()[normalize-space() and not(ancestor::a | ancestor::script | ancestor::style | ancestor::form)]";
            HtmlNodeCollection body = theDoc.DocumentNode.SelectNodes(xpath);
            Console.WriteLine("simple scraper Processed!");
            foreach (HtmlNode node in body)
            {
                innertextList.Add(node.InnerText);
            }
            //The Doc is loaded and Xpath executed.  All results are in innertextList.
            //Begin filtering and cleanup::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::

            List<string> splitList = new List<string>();
            foreach (string line in innertextList)
            {
                string[] wordArray = line.Split(' ');
                foreach (string word in wordArray)
                {
                    if (invalidList.Contains(word) == false)
                    {
                        splitList.Add(word.Trim());
                    }
                }
            }

            var cleanList = splitList.Where(p => !invalidList.Any(p2 => p2 == p));
            //reformat for export::::::::::::::::::::::::::::::::::::::::::::
            string[] filteredArray = cleanList.ToArray();
            return filteredArray;


        }
         
    }

    //#########################################################################

    public static class SimpleScraper
    {
        public static string GetWeb(string args)
        {
            Console.WriteLine("loaded simplescraper");
            try
            {
                WebRequest basicWeb = WebRequest.Create(args);
                basicWeb.Headers.Add("myHeader", "Mozilla/5.0 (Windows NT 6.1) AppleWebKit/537.11 (KHTML, like Gecko) Chrome/23.0.1271.97 Safari/537.11");
                WebResponse webText = basicWeb.GetResponse();
                Stream webrStream = webText.GetResponseStream();
                StreamReader reader = new StreamReader(webrStream);
                string responseFromServer = reader.ReadToEnd();
                webrStream.Close();
                MixedCodeDocument page = new MixedCodeDocument();
                page.LoadHtml(responseFromServer);
                string bodyString = page.ToString();
                return responseFromServer;
            }
            catch
            {
                string badResponse = "ERROR";
                return badResponse;
            }
        }
    }
    
    //#########################################################################

    public static class BlacklistCheck
    {
        public static bool IsBlackListed(string args)
        {
            bool IsBlackListedResult;
            const string blackListFilePath = @"C:\Users\david\Documents\GitHub\WordyLib\WordyLib\Blacklist.txt";
            var blackListFileData = File.ReadAllLines(blackListFilePath);
            List<string> BlackListList = new List<string>(blackListFileData);
            BlackListList = BlackListList.ConvertAll(d => d.ToLower());
            string lowerArgs = args.ToLower();

            if ((BlackListList.Contains(lowerArgs)) == false)
            {
                IsBlackListedResult = false;
            }
            else
            {
                IsBlackListedResult = true;
            }

            return IsBlackListedResult;
        }
    }
    
    //#########################################################################

    public static class Wordy
    {
        public static List<string> getKeywords(string url)
        {
            List<string> keywordList = new List<string>();
            var webText = SimpleScraper.GetWeb(url);
            string[] cleanText = SimpleParser.stripHTML(webText);
            foreach (string line in cleanText)
            {
                string[] cleanString = SimpleCleaner.degreaseString(line);
                foreach (string item in cleanString)
                {
                    keywordList.Add(item);
                }
            }

            return keywordList;
        }

    }

    //#########################################################################
}