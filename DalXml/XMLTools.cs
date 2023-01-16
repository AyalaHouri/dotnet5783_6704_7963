using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Dal;
internal class XMLTools
{

    const string s_dir = @"Data";
    static XMLTools()
    {
        if (!Directory.Exists(s_dir)) Directory.CreateDirectory(s_dir);
    }
    #region SaveLoadWithXMLSerializer
    public static void SaveListToXMLSerializer<T>(List<T?> list, String entity)
    {
        string filePath = $"{s_dir + entity}.xml";
        try
        {
            using FileStream file = new(filePath, FileMode.Create, FileAccess.Write, FileShare.None);
            XmlSerializer serializer = new(typeof(List<T?>));
            serializer.Serialize(file, list);
        }
        catch (Exception ex)
        {
            // DO.XMLFileLoadCreateException (filePath, $"fail to create xml file: {filePath}", ex);
            throw new Exception($"fail to create xml file: {filePath}", ex);
        }
    }
    public static List<T?> LoudListFromXMLSerializer<T>(String entity) where T : struct
    {
        string filePath = $"{s_dir + entity}.xml";
        try
        {
            if (!File.Exists(filePath)) return new();
            using FileStream file = new(filePath, FileMode.Open);
            XmlSerializer x = new(typeof(List<T?>));
            return x.Deserialize(file) as List<T?> ?? new();
        }
        catch (Exception ex)
        {
            // DO.XMLFileLoadCreateException (filePath, $"fail to load xml file: (filePath)", ax);
            throw new Exception($"fail to load xml file: {filePath}", ex);
        }

    }
}
#endregion