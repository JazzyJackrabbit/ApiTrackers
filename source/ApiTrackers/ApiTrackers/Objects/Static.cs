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

        public static string firstCharacter(string str)
        {
            return str.Substring(0, 1);
        }
        public static string fromSecondCharacter(string str)
        {
            return str.Substring(1);
        }

        public static string ObjectToJsonString_Converter(Type _type, object _obj)
        {
            IContractResolver resolver = new DefaultContractResolver();
            string json = resolver.StringifyJsonObjectProperties(_type, _obj);
            json.Replace("\n", "");
            json.Replace("  ", " ");
            return json;
        }
        public static string ArrayToJsonString_Converter(Type _type, object _obj)
        {
            IContractResolver resolver = new DefaultContractResolver();
            string json = resolver.StringifyJsonArrayProperties(_type, _obj);
            json.Replace("\n", "");
            json.Replace("  ", " ");
            return json;
        }
        
        internal static string jsonResponseObject(int _status, Type _type, object _obj)
        {
            IContractResolver resolver = new DefaultContractResolver();
            string json = resolver.StringifyJsonObjectProperties(_type, _obj);
            json.Replace("\n", "");
            json.Replace("  ", " ");
            return Static.jsonResp(_status, json);
        }

        internal static string jsonResponseArray(int _status, Type _type, object _obj)
        {
            IContractResolver resolver = new DefaultContractResolver();
            string json = resolver.StringifyJsonArrayProperties(_type, _obj);
            json.Replace("\n", "");
            json.Replace("  ", " ");
            return Static.jsonResp(_status, json);
        }

        internal static string jsonResponseError(int _status, string _message)
        {
            return Static.jsonResp(_status, _message, "{}");
        }

        public static List<object> castListObject(object list_objs)
        {
            IList collection = (IList)list_objs;
            List<object> result = new List<object>();
            foreach (object obj in collection)
                result.Add(obj);
            return result;
        }

        private static string StringifyJsonArrayProperties(this IContractResolver resolver, Type typeChild, object list_objs)
        {
            // JObject 
            // JArray

            // TODO !!! 

            /*JObject test =
            {
                test = new JObject()
            }*/

            List<object> listObjsConvrt = castListObject(list_objs);
            string arrayJson = "[ ";
            foreach(object obj in listObjsConvrt)
            {
                string jsonObjStringify = StringifyJsonObjectProperties(resolver, typeChild, obj);
                arrayJson += jsonObjStringify;
                arrayJson += ",";
            }
            removeLastCharacter(arrayJson);
            arrayJson += "]";
            return arrayJson;
        }
        private static string StringifyJsonObjectProperties(this IContractResolver resolver, Type type, object obj)
        {
            if (resolver == null || type == null)
                throw new ArgumentNullException();
            var contract = resolver.ResolveContract(type) as JsonObjectContract;
            if (contract == null || obj == null)
                return "";

            IList<object> propertiesValueArray = contract.Properties.Where(p => !p.Ignored).Select(p => p.ValueProvider.GetValue(obj)).ToArray();
            IList<string> propertiesNameArray = contract.Properties.Where(p => !p.Ignored).Select(p => p.PropertyName).ToArray();

            string stringifyProperties = "{";

            int posI = 0;
            foreach (string name in propertiesNameArray)
            {
                object value = propertiesValueArray[posI];


                if (value != null && firstCharacter(name.ToString()) == JsonClassIndicator)
                {
                    string subJsonObjectValue = StringifyJsonObjectProperties(resolver, value.GetType(), value);

                    stringifyProperties += ("\"" + fromSecondCharacter(name) + "\":" + subJsonObjectValue + "");
                    stringifyProperties += ",";
                }
                else
                {
                    if( type.Name.ToUpper()=="TRACKERCONTENT" && name.ToUpper() == "NOTES" )
                        stringifyProperties += ("\"" + name + "\":" + value + "");
                    else
                        stringifyProperties += ("\"" + name + "\":\"" + value + "\"");
                    stringifyProperties += ",";
                }
     
                ++posI;
            }

            // remove last ","
            stringifyProperties = removeLastCharacter(stringifyProperties);

            return stringifyProperties + "}";
        }

        public static string removeLastCharacter(string text)
        {
            return text.Substring(0, text.Length - 1);
        }

        public static string jsonResp(int _status, object _response)
        {
            var json = "{"
                + "\"status\":" + _status + ","
                + "\"response\":" + _response + ""
                + "}";
            return json;
        }

        public static string jsonResp(int _status, string _message, object _response)
        {
            var json = "{"
                + "\"status\":" + _status + ","
                + "\"message\":" + _message + ","
                + "\"response\":" + _response + ""
                + "}";
            return json;
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
