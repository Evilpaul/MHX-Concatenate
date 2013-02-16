namespace mhx_concatenate
{
    class Checksum
    {
        public static byte calcChecksum(byte byteCount, byte[] data)
        {
            byte checksum = byteCount;

            // add all data bytes toghter
            for (int i = 0; i < data.Length; i++)
            {
                checksum += data[i];
            }

            // mask LSB and preform one's complement
            checksum = (byte) ~(checksum & 0xFF);

            return checksum;
        }
    }
}
