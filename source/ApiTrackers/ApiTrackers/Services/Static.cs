using ApiTrackers.Exceptions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace ApiTrackers
{
    public static class Static
    {
        public static string MakeValidFileName(string name)
        {
            string invalidChars = System.Text.RegularExpressions.Regex.Escape(new string(System.IO.Path.GetInvalidFileNameChars()));
            string invalidRegStr = string.Format(@"(?!([a-zA-Z]|[0-9]|(-|_))|\s).", invalidChars);

            // "azerty1234567890_- "       _

            return System.Text.RegularExpressions.Regex.Replace(name, invalidRegStr, "_");
        }
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


        public static void WriteWavHeader(MemoryStream stream, bool isFloatingPoint, ushort channelCount, ushort bitDepth, int sampleRate, int totalSampleCount)
        {
            stream.Position = 0;

            // RIFF header.
            // Chunk ID.
            stream.Write(Encoding.ASCII.GetBytes("RIFF"), 0, 4);

            // Chunk size.
            stream.Write(BitConverter.GetBytes(((bitDepth / 8) * totalSampleCount) + 36), 0, 4);

            // Format.
            stream.Write(Encoding.ASCII.GetBytes("WAVE"), 0, 4);



            // Sub-chunk 1.
            // Sub-chunk 1 ID.
            stream.Write(Encoding.ASCII.GetBytes("fmt "), 0, 4);

            // Sub-chunk 1 size.
            stream.Write(BitConverter.GetBytes(16), 0, 4);

            // Audio format (floating point (3) or PCM (1)). Any other format indicates compression.
            stream.Write(BitConverter.GetBytes((ushort)(isFloatingPoint ? 3 : 1)), 0, 2);

            // Channels.
            stream.Write(BitConverter.GetBytes(channelCount), 0, 2);

            // Sample rate.
            stream.Write(BitConverter.GetBytes(sampleRate), 0, 4);

            // Bytes rate.
            stream.Write(BitConverter.GetBytes(sampleRate * channelCount * (bitDepth / 8)), 0, 4);

            // Block align.
            stream.Write(BitConverter.GetBytes((ushort)channelCount * (bitDepth / 8)), 0, 2);

            // Bits per sample.
            stream.Write(BitConverter.GetBytes(bitDepth), 0, 2);



            // Sub-chunk 2.
            // Sub-chunk 2 ID.
            stream.Write(Encoding.ASCII.GetBytes("data"), 0, 4);

            // Sub-chunk 2 size.
            stream.Write(BitConverter.GetBytes((bitDepth / 8) * totalSampleCount), 0, 4);
        }

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
