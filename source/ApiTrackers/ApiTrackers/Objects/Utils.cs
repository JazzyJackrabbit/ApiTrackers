using ApiTrackers.Exceptions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

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


    public static string MakeValidFileName(string name)
    {
        string invalidChars = System.Text.RegularExpressions.Regex.Escape(new string(System.IO.Path.GetInvalidFileNameChars()));
        string invalidRegStr = string.Format(@"(?!([a-zA-Z]|[0-9]|(-|_))|\s).", invalidChars);

        // "azerty1234567890_- "       _

        return System.Text.RegularExpressions.Regex.Replace(name, invalidRegStr, "_");
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

}

