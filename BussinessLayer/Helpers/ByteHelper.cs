using System;
using System.Text;
using BussinessLayer;

namespace BusinessLayer.Helpers
{
    public static class ByteHelper
    {
        public static string ByteArrayToString(byte[] array)
        {
            var stringBuilder = new StringBuilder(string.Empty);

            foreach (var item in array)
            {
                stringBuilder.Append($"{item}{Consts.QuerySeparator}");
            }

            stringBuilder = stringBuilder.Remove(stringBuilder.Length - 1, 1);

            return stringBuilder.ToString();
        }

        public static byte[] StringToByteArray(string stringWithBytes)
        {
            var encryptedBytesString = stringWithBytes.Split(Consts.QuerySeparator);
            byte[] result = new byte[encryptedBytesString.Length];
            for (int i = 0; i < encryptedBytesString.Length; i++)
            {
                if (!byte.TryParse(encryptedBytesString[i], out result[i]))
                {
                    throw new Exception();
                }
            }

            return result;
        }
    }
}
