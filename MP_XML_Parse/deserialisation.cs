using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MP_XML_Parse
{
    public class deserialisation
    {
    }


	[XmlRoot(ElementName = "record")]
	public class Record
	{
		[XmlElement(ElementName = "item")]
		public List<Item> Item { get; set; }
	}

	[XmlRoot(ElementName = "category")]
	public class Category
	{
		[XmlElement(ElementName = "record")]
		public List<Record> Record { get; set; }
		[XmlAttribute(AttributeName = "type")]
		public string Type { get; set; }
		[XmlAttribute(AttributeName = "name")]
		public string Name { get; set; }
	}

	[XmlRoot(ElementName = "item")]
	public class Item
	{
		[XmlElement(ElementName = "br")]
		public List<string> Br { get; set; }
		[XmlElement(ElementName = "strong")]
		public Strong Strong { get; set; }
	}

	[XmlRoot(ElementName = "regmem")]
	public class Regmem
	{
		[XmlElement(ElementName = "category")]
		public List<Category> Category { get; set; }
		[XmlAttribute(AttributeName = "personid")]
		public string Personid { get; set; }
		[XmlAttribute(AttributeName = "membername")]
		public string Membername { get; set; }
		[XmlAttribute(AttributeName = "date")]
		public string Date { get; set; }
	}

	[XmlRoot(ElementName = "em")]
	public class Em
	{
		[XmlElement(ElementName = "br")]
		public List<string> Br { get; set; }
	}

	[XmlRoot(ElementName = "strong")]
	public class Strong
	{
		[XmlElement(ElementName = "em")]
		public Em Em { get; set; }
	}

	[XmlRoot(ElementName = "publicwhip")]
	public class Publicwhip
	{
		[XmlElement(ElementName = "regmem")]
		public List<Regmem> Regmem { get; set; }
	}

}
