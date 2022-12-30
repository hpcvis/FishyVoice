using System;

namespace IronSnappy
{
   public interface IRentedBuffer : IDisposable
   {
      public Span<byte> Span { get; }
   }
}
