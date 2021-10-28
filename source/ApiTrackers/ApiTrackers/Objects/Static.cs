﻿using Newtonsoft.Json;
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


        internal static string jsonResponseObject(int _status, Type _type, object _obj)
        {
            IContractResolver resolver = new DefaultContractResolver();
            string json = resolver.StringifyJsonObjectProperties(_type, _obj);
            return Static.jsonResp(_status, json);
        }
        internal static string jsonResponseArray(int _status, Type _type, object _obj)
        {
            IContractResolver resolver = new DefaultContractResolver();
            string json = resolver.StringifyJsonArrayProperties(_type, _obj);
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

        public static string StringifyJsonArrayProperties(this IContractResolver resolver, Type typeChild, object list_objs)
        {
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
        public class JsonAttribute
        {
            public string name;
            public object value;
        }
        public class JsonRespRootMessage
        {
            public int status;
            public string message;
            public object response;
        }
        public class JsonRespRoot
        {
            public int status;
            public object response;
        }

        internal static string convertToString(object value)
        {
            if (value == null) return "";
            return value.ToString();
        }
        internal static int convertToInteger(object value)
        {
            if (value == null) return 0;
            return Convert.ToInt32(value);
        }
        internal static double convertToDouble(object value)
        {
            if (value == null) return 1;
            try
            {
                return Convert.ToDouble(value);
            }
            catch
            {
                return 1;
            }
        }
    }
}
