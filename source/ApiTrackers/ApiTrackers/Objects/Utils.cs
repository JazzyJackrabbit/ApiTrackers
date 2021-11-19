using ApiTrackers.Exceptions;
using System;
using System.Collections.Generic;

    public class Utils
    {
        internal static string ConvertToString(object value)
        {
            try
            {
                if (value == null) return "";
                //if (value.GetType().Name == "String")
                return value.ToString();
            }
            catch { }
            throw new OwnException();
        }
        internal static int ConvertToInteger(object value)
        {
            try
            {
                if (value == null) throw new OwnException();
                return Convert.ToInt32(value);
            }
            catch
            {
                throw new OwnException();
            }
        }
        internal static double ConvertToDouble(object value)
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

        static public bool AddIfNotPresent<T>(List<T> _list, T _value)
        {
            if (!_list.Contains(_value))
            {
                _list.Add(_value);
                return true;
            }
            return false;

        }
    }
  
