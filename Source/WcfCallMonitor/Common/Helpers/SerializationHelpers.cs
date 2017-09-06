#region Authoring Description
/* 
* =================================================================================
* Author:		Fabian Andres Moreno chacon
* Create date:  Sept 3, 2017
* Description:	
* =================================================================================
* ============================= CHANGES ===========================================
* Author:		
* Create date: 
* Description:	
* =================================================================================
*/
#endregion
using System.IO;
using System.Text;
using System.Xml;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System;
using System.Web;
using Common.Entities;

namespace Common.Helpers
{
    public class SerializationHelpers
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string SerializeXml<T>(T obj)
        {
            try
            {
                using (var ms = new MemoryStream())
                {
                    var serializer = new DataContractSerializer(typeof(T));
                    serializer.WriteObject(ms, obj);
                    string retVal = Encoding.Default.GetString(ms.ToArray());
                    return retVal;
                }
            }
            catch (Exception ex)
            {
                throw new HttpException(400, "Bad Request: ", ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="xml"></param>
        /// <returns></returns>
        public static T DeserializeXml<T>(string xml)
        {
            try
            {
                using (var reader = new StringReader(xml))
                {
                    using (var xmlReader = XmlReader.Create(reader))
                    {
                        var serializer = new DataContractSerializer(typeof(T));
                        var obj = (T)serializer.ReadObject(xmlReader);
                        return obj;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new HttpException(400, "Bad Request: ", ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string SerializeJson<T>(T obj)
        {
            try
            {
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
                MemoryStream ms = new MemoryStream();
                ser.WriteObject(ms, obj);
                string jsonString = Encoding.UTF8.GetString(ms.ToArray());
                ms.Close();
                return jsonString;
            }
            catch (Exception ex)
            {
                throw new HttpException(400, "Bad Request: ", ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jsonString"></param>
        /// <returns></returns>
        public static T DeserializeJson<T>(Stream jsonString)
        {
            try
            {
                StreamReader str = new StreamReader(jsonString);
                string text = str.ReadToEnd();
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
                MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(text));
                T obj = (T)ser.ReadObject(ms);
                return obj;
            }
            catch (Exception ex)
            {
                throw new HttpException(400, "Bad Request: ", ex);
            }

        }
    }
}
