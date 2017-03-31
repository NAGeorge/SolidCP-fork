using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using System.Configuration;

namespace SolidCP.EnterpriseServer
{
    [Serializable]
    public class SEConfig
    {
        public bool WriteLog = Convert.ToBoolean(ConfigurationManager.AppSettings["SpamExpertsWriteLog"]);
        public bool ExtendedLog = Convert.ToBoolean(ConfigurationManager.AppSettings["SpamExpertsExtendedLog"]);

        public string schema = ConfigurationManager.AppSettings["SpamExpertsSchema"];
        public string url = ConfigurationManager.AppSettings["SpamExpertsUrl"];
        public string api = "";
        public string user = ConfigurationManager.AppSettings["SpamExpertsUser"];
        public string password = ConfigurationManager.AppSettings["SpamExpertsPassword"];

        public string MailServer = "";
        public string MailTo = "";
        public string MailFrom = "";

        public string ErrorMailBody = "{0}. Error: {1}";
        public string ErrorMailSubject = "SpamExperts error";

        // default 

        //public static string SettingsFileName
        //{
        //    get
        //    {
        //        return AppDomain.CurrentDomain.BaseDirectory + @"SESettings.config";
        //    }
        //}


        //public static object Deserialize(SEConfig s)
        //{
        //    XmlSerializer myXmlSer = null;
        //    FileStream fileStream = null;

        //    try
        //    {
        //        myXmlSer = new XmlSerializer(s.GetType());
        //        fileStream = new FileStream(SettingsFileName, FileMode.Open, FileAccess.Read, FileShare.Read);
        //    }
        //    catch (Exception exc)
        //    {
        //        return s;
        //    }

        //    try
        //    {
        //        return (object)myXmlSer.Deserialize(fileStream);
        //    }
        //    catch (Exception exc)
        //    {
        //        return s;
        //    }
        //    finally
        //    {
        //        fileStream.Close();
        //    }
        //}

        //public bool Serialize()
        //{
        //    XmlSerializer myXmlSer = null;
        //    FileStream fileStream = null;

        //    try
        //    {
        //        myXmlSer = new XmlSerializer(GetType());
        //        fileStream = new FileStream(SettingsFileName, FileMode.Create, FileAccess.Write, FileShare.Read);

        //    }
        //    catch
        //    {
        //        return false;
        //    }

        //    try
        //    {
        //        myXmlSer.Serialize(fileStream, this);
        //        return true;
        //    }
        //    catch
        //    {
        //        return false;
        //    }
        //    finally
        //    {
        //        fileStream.Close();
        //    }
        //}

        //private static SEConfig instance = null;
        //public static SEConfig Instance
        //{
        //    get
        //    {
        //        if (instance == null)
        //        {
        //            Load();
        //            //if (!File.Exists(SettingsFileName)) 
        //            Save();
        //        }
        //        return instance;
        //    }
        //}


        //public static void Load()
        //{
        //    instance = (SEConfig)Deserialize(new SEConfig());
        //}

        //public static void Save()
        //{
        //    if (instance == null) return;
        //    instance.Serialize();
        //}


    }
}
