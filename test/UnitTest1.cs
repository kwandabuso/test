using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nancy;
using Nancy.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework.Internal;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using RestSharp.Deserializers;
using RestSharp.Serialization.Json;
using RestSharp.Extensions;

namespace test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            
            var client = new RestClient("https://reqres.in");

            var request = new RestRequest("/api/users?page=2", Method.GET);
            var response = client.Execute(request);

            var des = new JsonDeserializer();
            var output = des.Deserialize<Dictionary<string,string>>(response);
            //var result = output["first_name"];
            //CreateFileIfMissing("kwanda", "buso", "9011115613083");
            foreach (KeyValuePair<string, string> item in output)
            {
                var res = (item.Key.Equals("data") + "=>" + item.Value);
               
                
                if (res.Contains("first_name"))
                {
                    var TF = res.Split('}');
                    foreach (var val in TF)
                    {
                        if (val.Equals("]"))
                        {
                            continue;
                        }

                        var name = val.Split(':')[3].Split(',')[0].Replace('\\', ' ').Replace('"', ' ').Trim();
                        var last = val.Split(':')[4].Split(',')[0].Replace('\\', ' ').Replace('"', ' ').Trim();
                        var id = val.Split(':')[1].Split(',')[0].Replace('\\', ' ').Replace('"', ' ').Trim();
                        var avatar = val.Split("\":")[5].Split(',')[0].Replace('\\', ' ').Replace('"', ' ').Trim();
                        var path = CreateFileIfMissing(name, last, id);

                        downloadFile(path, avatar);

                    }
                    
                }
                    
            }

            

        }

         public void writeToFile(params string[] items)
            {

            var fileName = "Results - " + DateTime.Now.ToString("ddMMyyyyHHmmss");//create a file, making it unique with datetime string

                //html file to write report 
                var TestResultReport = "path" + fileName + ".html";

                for (int i = 0; i < items.Length; i++)
                {
                    //write to html file 
                    System.IO.File.AppendAllText(TestResultReport, items[i] + " <br> ");
                }
            }
        public string CreateFileIfMissing(string firstName, string LastName, string id)
        {
            //create folder if missing
            String server = Environment.UserName;
            string folderLocation = "C:\\" + firstName + ""+ LastName+""+id;
            bool exists = System.IO.Directory.Exists(folderLocation);

            if (!exists)
            {
                Directory.CreateDirectory(folderLocation);
            }
            return folderLocation;
        }
        public void downloadFile(string path,string avatar)
        {
            RestClient restClient = new RestClient(avatar);
            var fileBytes = restClient.DownloadData(new RestRequest(Method.GET));
            File.WriteAllBytes(Path.Combine(path, "avatar.png"), fileBytes);

        }

    }
}
