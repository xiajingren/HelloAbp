using System;
using System.Collections.Generic;
using System.Text;

namespace Xhznl.DataDictionary
{
    public class DataDictionaryConsts
    {
        public const int MaxNumeralLength = 16;

        public const int MaxCodeLength = 32;

        public static int MaxNameLength { get; } = 50;

        public const int MaxFullNameLength = 128;

        public static int MaxNotesLength { get; } = 256;
    }
}