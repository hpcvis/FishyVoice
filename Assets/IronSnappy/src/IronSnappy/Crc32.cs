using System;
using System.Collections.Generic;

namespace IronSnappy
{
   /// <summary>
   /// Implements a 32-bit CRC hash algorithm compatible with Zip etc.
   /// </summary>
   /// <remarks>
   /// Crc32 should only be used for backward compatibility with older file formats
   /// and algorithms. It is not secure enough for new applications.
   /// If you need to call multiple times for the same data either use the HashAlgorithm
   /// interface or remember that the result of one Compute call needs to be ~ (XOR) before
   /// being passed in as the seed for the next Compute call.
   /// </remarks>
   static class Crc32
   {
      const uint DefaultPolynomial = 0xedb88320u;
      const uint DefaultSeed = 0xffffffffu;

      static uint[] defaultTable;

      public static uint Compute(byte[] buffer)
      {
         return Compute(DefaultSeed, buffer);
      }

      public static uint Compute(ReadOnlySpan<byte> buffer)
      {
         return ~CalculateHash(InitializeTable(DefaultPolynomial), DefaultSeed, buffer);
      }

      public static uint Compute(uint seed, ReadOnlySpan<byte> buffer)
      {
         return Compute(DefaultPolynomial, seed, buffer);
      }

      public static uint Compute(uint polynomial, uint seed, ReadOnlySpan<byte> buffer)
      {
         return ~CalculateHash(InitializeTable(polynomial), seed, buffer);
      }

      static uint[] InitializeTable(uint polynomial)
      {
         if(polynomial == DefaultPolynomial && defaultTable != null)
            return defaultTable;

         uint[] createTable = new uint[256];
         for(int i = 0; i < 256; i++)
         {
            uint entry = (uint)i;
            for(int j = 0; j < 8; j++)
               if((entry & 1) == 1)
                  entry = (entry >> 1) ^ polynomial;
               else
                  entry >>= 1;
            createTable[i] = entry;
         }

         if(polynomial == DefaultPolynomial)
            defaultTable = createTable;

         return createTable;
      }

      static uint CalculateHash(uint[] table, uint seed, ReadOnlySpan<byte> buffer)
      {
         uint hash = seed;
         for(int i = 0; i < buffer.Length; i++)
            hash = (hash >> 8) ^ table[buffer[i] ^ hash & 0xff];
         return hash;
      }
   }
}
