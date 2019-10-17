using System;
using System.Collections.Generic;
using System.Linq;

namespace BMPHIDE
{
    public class D
    {
        private const uint s_generator = 1611621881u;

        private readonly uint[] m_checksumTable;

        public D()
        {
            m_checksumTable = Enumerable.Range(0, 256).Select(delegate (int i)
            {
                uint num = (uint)i;
                for (int j = 0; j < 8; j++)
                {
                    num = (((num & 1) != 0) ? (0x600F65F9 ^ num >> 1) : (num >> 1));
                }
                return num;
            }).ToArray();
        }

        public uint a<T>(IEnumerable<T> byteStream)
        {
            return ~byteStream.Aggregate(uint.MaxValue, (uint checksumRegister, T currentByte) => m_checksumTable[(checksumRegister & 0xFF) ^ Convert.ToByte(currentByte)] ^ checksumRegister >> 8);
        }
    }
}
