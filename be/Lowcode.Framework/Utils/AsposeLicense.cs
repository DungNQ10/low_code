using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Lowcode.Framework.Utils
{
    public static class  AsposeLicense
    {
        
        public const string Key =
            @"PExpY2Vuc2U+CjxEYXRhPgo8TGljZW5zZWRUbz5TaGFuZ2hhaSBIdWR1biBJbmZvcm1hdGlvbiBUZWNobm9sb2d5IENvLiwgTHRkPC9MaWNlbnNlZFRvPgo8RW1haWxUbz4zMTc3MDE4MDlAcXEuY29tPC9FbWFpbFRvPgo8TGljZW5zZVR5cGU+RGV2ZWxvcGVyIE9FTTwvTGljZW5zZVR5cGU+CjxMaWNlbnNlTm90ZT5MaW1pdGVkIHRvIDEgZGV2ZWxvcGVyLCB1bmxpbWl0ZWQgcGh5c2ljYWwgbG9jYXRpb25zPC9MaWNlbnNlTm90ZT4KPE9yZGVySUQ+MTgwNTE0MjAxMTE2PC9PcmRlcklEPgo8VXNlcklEPjI2NjE2NjwvVXNlcklEPgo8T0VNPlRoaXMgaXMgYSByZWRpc3RyaWJ1dGFibGUgbGljZW5zZTwvT0VNPgo8UHJvZHVjdHM+CjxQcm9kdWN0PkFzcG9zZS5Ub3RhbCBmb3IgLk5FVDwvUHJvZHVjdD4KPC9Qcm9kdWN0cz4KPEVkaXRpb25UeXBlPkVudGVycHJpc2U8L0VkaXRpb25UeXBlPgo8U2VyaWFsTnVtYmVyPjIxMGVjOGU3LTgxZTEtNDUzNy1iNDQ2LTY5MmRlNDk4MTIxNzwvU2VyaWFsTnVtYmVyPgo8U3Vic2NyaXB0aW9uRXhwaXJ5PjIwMTkwNTE3PC9TdWJzY3JpcHRpb25FeHBpcnk+CjxMaWNlbnNlVmVyc2lvbj4zLjA8L0xpY2Vuc2VWZXJzaW9uPgo8TGljZW5zZUluc3RydWN0aW9ucz5odHRwOi8vd3d3LmFzcG9zZS5jb20vY29ycG9yYXRlL3B1cmNoYXNlL2xpY2Vuc2UtaW5zdHJ1Y3Rpb25zLmFzcHg8L0xpY2Vuc2VJbnN0cnVjdGlvbnM+CjwvRGF0YT4KPFNpZ25hdHVyZT5jdEozeUx4U0FQc0JRZDBKY3FmN0NBNTNGek4xWXJ2YUE1ZFNyVHBkRlcvQWZoMGh5S0t3cnkrQzF0aldJT0VGeXpLWVdIK05nbi9IZVhVek1RSkEwUm9vd2NxMTEyblYvUW5yU1NxRG02RkpWTnNzSDRwL1ltWFJqbDdMQml4d1Y4QWJ5V1g4bGhWb3lvazdsSTVrNUs4YmJhSytUOFVyK2pJd1NaQWNtVkE9PC9TaWduYXR1cmU+CjwvTGljZW5zZT4=";
        public static Stream LStream = (Stream)new MemoryStream(Convert.FromBase64String(AsposeLicense.Key));
        public static Stream GetLStream()
        {
            LStream.Seek(0, SeekOrigin.Begin);
            return LStream;
        }
    }
}
