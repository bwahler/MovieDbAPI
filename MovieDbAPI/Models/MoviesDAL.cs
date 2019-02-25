﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace MovieDbAPI.Models
{
    public class MoviesDAL
    {
        public static string GetData(string Url)
        {
            HttpWebRequest request = WebRequest.CreateHttp(Url);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            StreamReader rd = new StreamReader(response.GetResponseStream());
            string data = rd.ReadToEnd();

            return data;
        }

        public static Movies GetPost(int i)
        {
            string output = GetData("http://www.omdbapi.com/?i=tt3896198&apikey=459c139");
            Movies m = new Movies();

            return m;
        }
    }
}