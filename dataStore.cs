using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace mhx_concatenate
{
    class dataStore
    {
        private string header = "";
        private List<string> data = new List<string>();
        private string startAddress = "";
        private Form1 parentForm;

        public dataStore(Form1 pf)
        {
            parentForm = pf;
        }

        private void initialise()
        {
            header = "";
            startAddress = "";
            data.Clear();
        }

        public void processFile(string file)
        {
            initialise();

            using (StreamReader sr = new StreamReader(file))
            {
                parentForm.addLogText("Processing " + file);

                while (sr.Peek() >= 0)
                {
                    addDataLine(sr.ReadLine());
                }

                parentForm.addLogText(getDataCount() + " data lines found");

                if (isDataValid())
                {
                    parentForm.addLogText("File is valid");
                }
                else
                {
                    parentForm.addLogText("File is invalid");
                }

                parentForm.addLogText("");
            }
        }

        public bool isDataValid()
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

        private void addDataLine(string dataLine)
        {
            try
            {
                // make sure this is valid S-Rec
                if (dataLine[0] == 'S')
                {
                    if (dataLine[1] == '0')
                    {
                        // header line
                        parentForm.addLogText("Found header : " + dataLine);
                        header = dataLine;
                    }
                    else if (dataLine[1] == '8')
                    {
                        // start address line
                        parentForm.addLogText("Found start address : " + dataLine);
                        startAddress = dataLine;
                    }
                    else
                    {
                        // data line
                        data.Add(dataLine);
                    }
                }
            }
            catch
            {
                parentForm.addLogText("Exception processing line " + getDataCount());
            }
        }
    }
}
