using System;

namespace FileClass
{
    class StartAddr
    {
        private uint startAddr;
        private string rawStartAddr;

        public StartAddr()
        {
            startAddr = 0xFFFFFFFF;
            rawStartAddr = "";
        }

        public static uint processAddress(byte[] data, int offset, int addrSize)
        {
            byte[] tempSize = new byte[sizeof(UInt32)];
            if (BitConverter.IsLittleEndian)
            {
                int temp;

                for (int i = 0; i < sizeof(UInt32); i++)
                {
                    temp = addrSize - (i + 1);
                    if (temp >= 0)
                    {
                        tempSize[i] = data[temp];

                    }
                    else
                    {
                        tempSize[i] = 0x00;
                    }
                }
            }
            else
            {
                // untested
                Array.Copy(data, 0, tempSize, sizeof(UInt32) - addrSize, addrSize);
            }

            uint addr = BitConverter.ToUInt32(tempSize, 0);

            return addr;
        }

        public bool setStartAddr(uint addr, string rawAddr)
        {
            bool retVal = false;

            if (startAddr > addr)
            {
                startAddr = addr;
                rawStartAddr = rawAddr;
                retVal = true;
            }

            return retVal;
        }

        public string getStartAddr()
        {
            return String.Format("{0:X8}", startAddr);
        }

        public string getRawAddr()
        {
            return rawStartAddr;
        }
    }
}
