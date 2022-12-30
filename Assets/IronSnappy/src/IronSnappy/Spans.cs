using System;
using System.IO;

namespace IronSnappy
{
   /// <summary>
   /// Contains workarounds to support older NETSTANDARD
   /// </summary>
   static class Spans
   {
      public static void Write(Stream dst, ReadOnlySpan<byte> data)
      {
#if SPANSTREAM
         dst.Write(data);
#else
         byte[] dataArray = data.ToArray();
         dst.Write(dataArray, 0, dataArray.Length);
#endif

      }

      public static int Read(Stream src, Span<byte> buffer)
      {
#if SPANSTREAM
         return src.Read(buffer);
#else
         byte[] result = new byte[buffer.Length];
         int read = src.Read(result, 0, result.Length);
         result.CopyTo(buffer);
         return read;
#endif
      }
   }
}
