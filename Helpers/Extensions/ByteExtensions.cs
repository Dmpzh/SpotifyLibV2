﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Utilities;

namespace SpotifyLibV2.Helpers.Extensions
{
    internal static class ByteExtensions
    {
        private static readonly char[] HexArray = "0123456789ABCDEF".ToCharArray();
        internal static byte[] ToByteArray(this int i)
        {
            byte[] bytes = BitConverter.GetBytes(i);
            if (BitConverter.IsLittleEndian)
            {
                return bytes.Reverse().ToArray();
            }

            return bytes;
        }
        public static string BytesToHex(this byte[] bytes) => 
            BytesToHex(bytes, 0, bytes.Length, true, -1);
        public static byte[] ToByteArray(this BigInteger i)
        {
            byte[] array = i.ToByteArray();
            if (array[0] == 0) array = Arrays.CopyOfRange(array, 1, array.Length);
            return array;
        }
        internal static string BytesToHex(this byte[] bytes, int offset, int length, bool trim, int minLength)
        {
            if (bytes == null) return "";

            var newOffset = 0;
            var trimming = trim;
            var hexChars = new char[length * 2];
            for (var j = offset; j < length; j++)
            {
                var v = bytes[j] & 0xFF;
                if (trimming)
                {
                    if (v == 0)
                    {
                        newOffset = j + 1;

                        if (minLength != -1 && length - newOffset == minLength)
                            trimming = false;

                        continue;
                    }
                    else
                    {
                        trimming = false;
                    }
                }

                hexChars[j * 2] = HexArray[(int)((uint)v >> 4)];
                hexChars[j * 2 + 1] = HexArray[v & 0x0F];
            }

            return new String(hexChars, newOffset * 2, hexChars.Length - newOffset * 2);
        }

    }
}
