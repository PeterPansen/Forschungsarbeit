using UnityEngine;
using System.Collections;
using System.Xml;
using System.IO;

namespace FAR{
	
	public class DFGParser {
	
	    XmlDocument root;
	
	    public DFGParser(string dfg)
	    {
	        dfg = dfg.Replace("xsi:schemaLocation=\"http://ar.in.tum.de/ubitrack/utql http://ar.in.tum.de/files/ubitrack/utql/utql_types.xsd\" xmlns=\"http://ar.in.tum.de/ubitrack/utql\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\"", "");
	        dfg = dfg.Replace("xsi:type=\"utql:PrimitiveAttributeType\"", "");
	        dfg = dfg.Replace("xmlns:utql=\"http://ar.in.tum.de/ubitrack/utql\"", "");
	        root = new XmlDocument();
	        //StringReader stringReader = new StringReader(dfg);
	        
	        root.LoadXml(dfg);
	        
	        //root.Load(stringReader);
	    }
	
	    public string getDFG()
	    {
	        StringWriter stringWriter = new StringWriter();
	        XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter);
	        root.WriteTo(xmlTextWriter);
	        
	        //XmlNode appPosePattern = root.SelectSingleNode("//Pattern[@id='pattern_1']");
	
	
	        return stringWriter.ToString();
	    }
	
	
	    internal XmlDocument getRoot()
	    {
	        return root;
	    }
	}

}
