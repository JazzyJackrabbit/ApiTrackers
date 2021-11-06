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
        internal const string JsonClassIndicator = "*";
        
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
            return (JObject)JsonConvert.DeserializeObject(JsonConvert.SerializeObject(_obj));
        }

        public static string removeLastCharacter(string text)
        {
            return text.Substring(0, text.Length - 1);
        }

        public static string jsonResp(int _status, object _response)
        {
            JObject jsonResp = ConvertToJObject(_response);
            JObject jsonBody = new JObject
            {
                new JProperty("status", _status),
                new JProperty("response", jsonResp)
            };

            return jsonBody.ToString();
        }

        public static string jsonResp(int _status, string _message, object _response)
        {
            JObject jsonResp = ConvertToJObject(_response);
            JObject jsonBody = new JObject
            {
                new JProperty("status", _status),
                new JProperty("message", _message),
                new JProperty("response", jsonResp)
            };

            return jsonBody.ToString();
        }

        internal static string jsonResponseObject(int _status, Type _unused, object _obj)
        {
            string jsonString = JsonConvert.SerializeObject( ConvertToJObject(_obj) );
            return Static.jsonResp(_status, jsonString);
        }

        internal static string jsonResponseArray(int _status, Type _unused, object _arr)
        {
            string jsonString = ConvertToJArray((List<object>)_arr).ToString();
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
                //if (value.GetType().Name == "String")
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
