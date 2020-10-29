using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using System.Xml.Serialization;
using tx_mvc_loaddataset.Models;

namespace tx_mvc_loaddataset.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            // create a new instance of the model
            // fill the model from your data source here
            address newAddress = new address()
            {
                Name = "Peter Jackson",
                Street = "9474 Kings Falls Dr"
            };

            // create a new XML document
            XmlDocument xml = SerializeToXmlDocument(newAddress);

            // fill the ViewBag and return the view
            ViewBag.xmlDataSource = xml;
            return View();
        }

        // serial object to XML document
        public XmlDocument SerializeToXmlDocument(object input)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(input.GetType(), "http://schemas.yournamespace.com");

            XmlDocument xmlDoc = null;

            using (MemoryStream stream = new MemoryStream())
            {
                xmlSerializer.Serialize(stream, input);

                stream.Position = 0;

                XmlReaderSettings xmlSettings = new XmlReaderSettings();
                xmlSettings.IgnoreWhitespace = true;

                using (var xtr = XmlReader.Create(stream, xmlSettings))
                {
                    xmlDoc = new XmlDocument();
                    xmlDoc.Load(xtr);
                }
            }

            return xmlDoc;
        }
    }
}