using System.Collections.Generic;
using System.Web.Script.Serialization;
using System.IO;

namespace Maverick.Helpers
{
    internal static class Deserializer
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Request"></param>
        /// <returns></returns>
        internal static Dictionary<string, string> Deserialize (System.Web.HttpRequestBase Request)
        {
            Request.InputStream.Position = 0;
            string Json = new StreamReader(Request.InputStream).ReadToEnd();
            return new JavaScriptSerializer().Deserialize<Dictionary<string, string>>(Json);
        }
    }
}