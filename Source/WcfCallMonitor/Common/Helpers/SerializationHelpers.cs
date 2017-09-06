#region Authoring Description
/* 
* =================================================================================
* Author:		Fabian Andres Moreno chacon
* Create date:  Sept 3, 2017
* Description:	Helper class for Format Conversions.
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
using System.Runtime.Serialization.Json;
using System;
using System.Web;

namespace Common.Helpers
{
    public class SerializationHelpers
    {
        /// <summary>
        /// Convert an Object<T> into a String Json format
        /// </summary>
        /// <param name="obj">Object</param>
        /// <returns>a string json format</returns>
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
                throw new HttpException(400, "Bad Type: ", ex);
            }
        }

        /// <summary>
        /// Convert a String Json format into an Object<T>
        /// </summary>
        /// <param name="jsonString">stream</param>
        /// <returns>an Object T</typeparam></returns>
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
                throw new HttpException(400, "Bad Type: ", ex);
            }
        }

        /// <summary>
        /// Convert a String into a Stream
        /// </summary>
        /// <param name="value">string to convert</param>
        /// <returns>Stream Value</returns>
        public static MemoryStream GenerateStreamFromString(string value)
        {
            return new MemoryStream(Encoding.UTF8.GetBytes(value ?? ""));
        }
    }
}
