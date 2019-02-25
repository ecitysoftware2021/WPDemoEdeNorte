﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPEDENorte.Classes
{
    public class LogDispenser
    {
        public string SendMessage { get; set; }

        public string ResponseMessage { get; set; }

        public string TransactionId { get; set; }

        public DateTime DateDispenser { get; set; }
    }

    public class LogService
    {
        public string NamePath { get; set; }

        public string FileName { get; set; }

        public void CreateLogs<T>(T model)
        {
            var json = JsonConvert.SerializeObject(model);
            if (!Directory.Exists(NamePath))
            {
                Directory.CreateDirectory(NamePath);
            }

            var nameFile = Path.Combine(NamePath, FileName);
            if (!File.Exists(nameFile))
            {
                var archivo = File.CreateText(nameFile);
                archivo.Close();
            }

            using (StreamWriter sw = File.AppendText(nameFile))
            {
                sw.WriteLine(json);
            }
        }

        public void CreateLogsTransactions<T>(T model)
        {
            var json = JsonConvert.SerializeObject(model);
            string fullPath = string.Format(@"C:\\Logs\{0}\", NamePath);
            if (!Directory.Exists(fullPath))
            {
                Directory.CreateDirectory(fullPath);
            }

            var nameFile = Path.Combine(fullPath, FileName);
            if (!File.Exists(nameFile))
            {
                var archivo = File.CreateText(nameFile);
                archivo.Close();
            }

            using (StreamWriter sw = File.AppendText(nameFile))
            {
                sw.WriteLine(json);
            }
        }
    }
}
