using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace SolidCP.EnterpriseServer
{
    public class SELog
    {
        static private void WriteStr(string errorlog)
        {
            try
            {
                System.IO.File.AppendAllText(AppDomain.CurrentDomain.BaseDirectory + @"SELog", DateTime.Now.ToString("yyyy.MM.dd HH:mm ") + errorlog + Environment.NewLine);
            }
            catch
            { }
        }

        public static void Write(string log)
        {
            if (Convert.ToBoolean(ConfigurationManager.AppSettings["SpamExpertsWriteLog"]))
                WriteStr(log);
        }


        static public void WriteEnd(string log)
        {
            if (Convert.ToBoolean(ConfigurationManager.AppSettings["SpamExpertsWriteLog"]))
                WriteStr(log);
        }

        static public void WriteError(Exception ex)
        {
            WriteStr("Error: " + ex.ToString());
        }

        static public void WriteError(string error)
        {
            WriteStr(error);
        }

    }
}
