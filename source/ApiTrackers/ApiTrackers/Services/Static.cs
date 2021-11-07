using ApiTrackers.Exceptions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace ApiTrackers
{
    public static class Static
    {

        public static JArray ConvertToJArray<T>(List<T> _arr)
        {
            JArray arrJson = new JArray();
            if (_arr == null) return null;

            foreach (object elmt in _arr)
            {
                JObject elmtJson = ConvertToJObject(elmt);
                arrJson.Add(elmtJson);
            }
            return arrJson;
        }
        public static JObject ConvertToJObject(object _obj)
        {
            if (_obj == null) return null;
            try
            {
                if (_obj.GetType() == "".GetType())
                    return (JObject)JsonConvert.DeserializeObject(_obj.ToString());
            }
            catch { }
            try
            {
                return (JObject)JsonConvert.DeserializeObject(_obj.ToString());
            }
            catch { }
            try
            {
                return (JObject)JsonConvert.DeserializeObject(JsonConvert.SerializeObject(_obj));
            }
            catch { }
            return new JObject();
        }
        /*public static JContainer ConvertToJContainer<T>(T _obj)
        {
           
            if (_obj.GetType() == new List<T>().GetType())
            {
                IList objList = (IList)_obj;
                int count = objList.Count; // The LINQ conversions will lose this information  
                IEnumerable<T> list = objList.Cast<T>();
                List<T> lt = new List<T>();
                foreach (T t in list) lt.Add(t);
                return ConvertToJArray<T>(lt);
            }
            if (_obj.GetType() == typeof(T))
            {
                return ConvertToJObject(_obj);
            }

            try
            {
                if (_obj.GetType() == "".GetType())
                    return (JObject)JsonConvert.DeserializeObject(_obj.ToString());
            }
            catch { }
            try
            {
                return (JObject)JsonConvert.DeserializeObject(_obj.ToString());
            }
            catch { }
            try
            {
                return (JObject)JsonConvert.DeserializeObject(JsonConvert.SerializeObject(_obj));
            }
            catch { }
            return new JObject();
        }*/

        public static string removeLastCharacter(string text)
        {
            return text.Substring(0, text.Length - 1);
        }

        public static string jsonResp(int _status, object _response)
        {
            JContainer jsonResp = (JContainer)JsonConvert.DeserializeObject(_response.ToString());
            //JContainer jsonResp = ConvertToJContainer(_response);
            JObject jsonBody = new JObject
            {
                new JProperty("status", _status),
                new JProperty("response", jsonResp)
            };

            return jsonBody.ToString();
        }

        public static string jsonResp(int _status, string _message, object _response)
        {
            JContainer jsonResp = (JContainer)JsonConvert.DeserializeObject(_response.ToString());
            JObject jsonBody = new JObject
            {
                new JProperty("status", _status),
                new JProperty("message", _message),
                new JProperty("response", jsonResp)
            };

            return jsonBody.ToString();
        }

        internal static string jsonResponseObject(int _status, object _obj)
        {
            string jsonString = JsonConvert.SerializeObject( ConvertToJObject(_obj) );
            return Static.jsonResp(_status, jsonString);
        }

        internal static string jsonResponseArray<T>(int _status, object _arr)
        {
            string jsonString = ConvertToJArray((List<T>)_arr).ToString();            
            return Static.jsonResp(_status, jsonString);
        }

        internal static string jsonResponseError(int _status, string _message)
        {
            return Static.jsonResp(_status, _message, "{}");
        }

        internal static string convertToString(object value)
        {
            try { 
                if (value == null) return "";
                return value.ToString();
            }
            catch { }
            throw new OwnException();
        }
        internal static int convertToInteger(object value)
        {
            try { 
                if (value == null) throw new OwnException();
                return Convert.ToInt32(value);
            }
            catch
            {
                throw new OwnException();
            }
        }
        internal static double convertToDouble(object value)
        {
            if (value == null) throw new OwnException();
            try
            {
                return Convert.ToDouble(value);
            }
            catch
            {
                throw new OwnException();
            }
        }
    }
}
