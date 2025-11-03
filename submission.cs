using System;
using System.Xml.Schema;
using System.Xml;
using Newtonsoft.Json;
using System.IO;
using System.ComponentModel;
/**
 * This template file is created for ASU CSE445 Distributed SW Dev Assignment 4.
 * Please do not modify or delete any existing class/variable/method names. However, you can add more variables and functions.
 * Uploading this file directly will not pass the autograder's compilation check, resulting in a grade of 0.
 * **/
namespace ConsoleApp1
{
    public class Program
    {
        public static string xmlURL = "https://lbmcd11.github.io/cse445-hotels/Hotels.xml";
        public static string xmlErrorURL = "https://lbmcd11.github.io/cse445-hotels/HotelsErrors.xml";
        public static string xsdURL = "https://lbmcd11.github.io/cse445-hotels/Hotels.xsd";
        public static void Main(string[] args)
        {
            string result = Verification(xmlURL, xsdURL);
            Console.WriteLine(result);
            result = Verification(xmlErrorURL, xsdURL);
            Console.WriteLine(result);
            result = Xml2Json(xmlURL);
            Console.WriteLine(result);
        }
        // Global variable to check if an error occured in validation
        private static bool isError = false;
        // Q2.1
        public static string Verification(string xmlUrl, string xsdUrl)
        {
            try
            {
                // Create the new schemea set for the xsd
                XmlSchemaSet schema = new XmlSchemaSet();
                schema.Add(null, xsdUrl);
                // Start up the reader for the xml
                XmlReaderSettings settings = new XmlReaderSettings();
                settings.ValidationType = ValidationType.Schema;
                settings.Schemas = schema;
                settings.ValidationEventHandler += new ValidationEventHandler(ValidationCallBack);
                // Set up the reader to read through the xml file
                XmlReader reader = XmlReader.Create(xmlUrl, settings);
                // Attemp to read through the xml file. If there is a failure validation callback is called
                while (reader.Read());
                if (isError)
                {
                    return "Error Found"; // An error was found during validation
                }
                return "No Error"; // Sucessfuly made it through the xml file
            }
            catch (Exception e)
            {
                return e.Message; // Catch any parsing or schema exceptions
            }
        }
        private static void ValidationCallBack(object sender, ValidationEventArgs e)
        {
            isError = true;
            Console.WriteLine("Validation Error: " + e.Message);
        }
        public static string Xml2Json(string xmlUrl)
        {
            // Load a valid xml
            XmlDocument doc = new XmlDocument();
            doc.Load(xmlUrl);
            // Convert to json
            string jsonText = JsonConvert.SerializeXmlNode(doc);
            return jsonText;
        }
    }
}