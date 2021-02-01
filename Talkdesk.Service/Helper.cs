using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using Talkdesk.Model;

namespace Talkdesk.Service
{
    public class Helper
    {
        List<string> _prefixes;
        bool bTest;

        public Helper()
        {
            try
            {
                _prefixes = new List<string>();
                _prefixes = ReadPrefixes();
                bTest = false;
            }
            catch (Exception)
            {
                _prefixes = new List<string>();
            }
        }
        public Helper(bool isATest)
        {
            try
            {
                _prefixes = new List<string>();
                _prefixes = ReadPrefixes();
                bTest = isATest;
            }
            catch (Exception)
            {
                _prefixes = new List<string>();
            }
        }

        public string GetNumberData(List<string> lNumbers)
        {
            List<NumberData> res = new List<NumberData>();

            foreach (string num in lNumbers)
            {
                string sNumToSearch = num.Replace("+", "").Replace("00", "");

                string sPrefix = "";
                try
                {
                    sPrefix = _prefixes.First(item => item == sNumToSearch.Substring(0, item.Length));
                }
                catch (Exception)
                {
                    sPrefix = ""; //do nothing
                }

                if (!String.IsNullOrEmpty(sPrefix))
                {
                    BusinessSector bs = GetBusinessSector(num);

                    NumberData nd = new NumberData();
                    nd.BusinessSector = bs.Sector;
                    nd.NumberOfOccur = 1;
                    nd.Prefix = sPrefix;

                    if (res.Exists(item => item.Prefix == nd.Prefix))
                    {
                        NumberData ndItem = res.Find(item => item.Prefix == nd.Prefix);
                        ndItem.NumberOfOccur += 1;
                    }
                    else
                    {
                        res.Add(nd);
                    }
                }
            }

            return JsonConvert.SerializeObject(res);
        }

        private string NumberDataListToJson(List<NumberData> lstN)
        {
            string sResult = "";
            foreach (NumberData nd in lstN)
            {

            }

            return sResult;
        }

        private List<string> ReadPrefixes()
        {
            return File.ReadLines(Directory.GetCurrentDirectory() + "\\Resources\\prefixes.txt").ToList();
        }

        public BusinessSector GetBusinessSector(string number)
        {
            WebClient client = new WebClient();
            try
            {
                string response = "";
                if (bTest)
                {
                    //Test
                    response = @"{ 
                    'number' : '+9872349', 
                    'sector' : 'Banking'}";
                }
                else
                {
                    response = client.DownloadString("https://challenge-business-sector-api.meza.talkdeskstg.com/sector/" + number);
                }

                BusinessSector parsedRes = JsonConvert.DeserializeObject<BusinessSector>(response);

                return parsedRes;
            }
            catch (Exception)
            {
                return new BusinessSector();
            }
        }

    }
}
