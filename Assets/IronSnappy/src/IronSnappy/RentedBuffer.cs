using System;
using System.Buffers;
using System.Collections.Generic;
using System.Text;

namespace IronSnappy
{
   class RentedBuffer : IRentedBuffer
   {
      private static readonly ArrayPool<byte> BytePool = ArrayPool<byte>.Shared;

      private readonly byte[] _buffer;
      private RentedBuffer _parent;
      private readonly int _start;
      private readonly int _count;
      private readonly int _capacity;

      public RentedBuffer(int capacity)
      {
         _buffer = BytePool.Rent(capacity);
         _capacity = capacity;
      }

      public RentedBuffer(RentedBuffer parent, int start, int count)
      {
         _parent = parent;
         _start = start;
         _count = count;
      }

      public Span<byte> Span => _parent == null
         ? _buffer.AsSpan(0, _capacity)
         : _parent.Span.Slice(_start, _count);

      public void Dispose()
      {
         if(_parent != null)
         {
            _parent.Dispose();
         }
         else
         {
            BytePool.Return(_buffer);
         }
      }
   }
}
