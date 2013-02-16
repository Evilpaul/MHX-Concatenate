namespace FileClass
{
    class Header
    {
        private string the_header;
        private string raw_header;

        private bool headerSet;

        public Header()
        {
            the_header = "";
            raw_header = "";
            headerSet = false;
        }

        public static string decodeData(byte[] data, int offset, int length)
        {
            char[] head = new char[data.Length];
            for (int i = 0; i < data.Length - offset; i++)
            {
                head[i] = (char)data[i + offset];
            }
            return new string(head);
        }

        public void setHeader(string header, string rawHeader)
        {
            if (headerSet == false)
            {
                the_header = header;
                raw_header = rawHeader;
                headerSet = true;
            }
        }

        public string getHeader()
        {
            return the_header;
        }

        public string getRawHeader()
        {
            return raw_header;
        }
    }
}
