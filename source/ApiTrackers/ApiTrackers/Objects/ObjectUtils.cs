using ApiTrackers.Exceptions;
using System.Text.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ApiTrackers
{
    public static class ObjectUtils
    {

        public static string removeLastCharacter(string text)
        {
            return text.Substring(0, text.Length - 1);
        }

        internal static string JsonResponseBuilder(int _status, object _object)
        {
            JsonSerializerOptions options = new JsonSerializerOptions
            {
                WriteIndented = true,
                Converters =
                {
                    new JsonStringEnumConverter(JsonNamingPolicy.CamelCase)
                }
            };
            string tmp = JsonSerializer.Serialize(_object, options);
            return JsonResponseBuilder(_status, null, tmp);
        }

        public static string JsonResponseBuilder(int _status, string _message=null, string _response=null)
        {
            var json = "{"
                + "\"status\":" + _status;
            if (_message != null)
                json +=",\"message\":" + _message;
            if (_response != null)
                json += ",\"response\":" + _response;
                
            json += "}";
            return json;
        }

    }
}
