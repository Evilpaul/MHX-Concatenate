using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mhx_concatenate
{
    class dataStore
    {
        private string header = "";
        private List<string> data = new List<string>();
        private string startAddress = "";

        public void initialise()
        {
            header = "";
            startAddress = "";
            data.Clear();
        }

        public bool validData()
        {
            bool returnVal = true;

            if (string.Compare(header, "") == 0) returnVal = false;
            if (string.Compare(startAddress, "") == 0) returnVal = false;
            if (data.Count == 0) returnVal = false;

            return returnVal;
        }

        public string getHeader()
        {
            return header;
        }

        public int getDataCount()
        {
            return data.Count;
        }

        public string getDataLine(int line)
        {
            return data[line];
        }

        public string getStartAddress()
        {
            return startAddress;
        }

        public void addDataLine(string dataLine)
        {
            // make sure this is valid S-Rec
            if (dataLine[0] == 'S')
            {
                if (dataLine[1] == '0')
                {
                    // header line
                    header = dataLine;
                }
                else if (dataLine[1] == '8')
                {
                    // start address line
                    startAddress = dataLine;
                }
                else
                {
                    // data line
                    data.Add(dataLine);
                }
            }
        }
    }
}
