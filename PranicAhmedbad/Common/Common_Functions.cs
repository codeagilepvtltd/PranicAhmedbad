using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PranicAhmedbad.Common
{
    public static class Common_Functions
    {
        public static List<T> ConvertDataSet<T>(DataSet ds)
        {
            List<T> data = new List<T>();
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                T item = GetItem<T>(row);
                data.Add(item);

            }
            return data;
        }

        public static List<T> ConvertDataTable<T>(DataTable dt)
        {
            List<T> data = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T item = GetItem<T>(row);
                data.Add(item);

            }
            return data;
        }

        private static T GetItem<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();

            foreach (DataColumn column in dr.Table.Columns)
            {
                try
                {
                    foreach (PropertyInfo pro in temp.GetProperties())
                    {
                        try
                        {
                            if (pro.Name == column.ColumnName)
                            {
                                pro.SetValue(obj, dr[column.ColumnName], null);
                            }
                            else
                            {
                                continue;
                            }
                        }
                        catch { }
                    }
                }
                catch { }
            }
            return obj;
        }

        public static String getEncrypt(String cleanString)  //getting encrypted string
        {
            Byte[] clearBytes = new UnicodeEncoding().GetBytes(cleanString);
            Byte[] hashedBytes = ((HashAlgorithm)CryptoConfig.CreateFromName("MD5")).ComputeHash(clearBytes);

            return BitConverter.ToString(hashedBytes);
        }


        public static DateTime ConvertToDate(string strDate)
        {
            DateTime dtDate = new DateTime();
            if (!string.IsNullOrEmpty(strDate))
            {
                string[] strSplitDate = strDate.Split('/');
                if (strSplitDate.Length > 2)
                {
                    dtDate = Convert.ToDateTime(Convert.ToString(strSplitDate[2]) + "/" + Convert.ToString(strSplitDate[1]) + "/" + Convert.ToString(strSplitDate[0]));
                }
            }
            return dtDate;
        }

        public static string GetSystemIP()
        {
            string IPAddress = string.Empty;
            IPHostEntry Host = default(IPHostEntry);
            string Hostname = null;
            Hostname = System.Environment.MachineName;
            Host = Dns.GetHostEntry(Hostname);
            foreach (IPAddress IP in Host.AddressList)
            {
                if (IP.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    IPAddress = Convert.ToString(IP);
                }
            }
            return IPAddress + ' ' + Hostname;
        }


    }
}
