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
        private string headerDecoded = "";
        private List<string> data = new List<string>();
        private string startAddress = "";
        private int errorCount = 0;
        private Form1 parentForm;

        public dataStore(Form1 pf)
        {
            parentForm = pf;
        }

        private void initialise()
        {
            header = "";
            headerDecoded = "";
            startAddress = "";
            errorCount = 0;
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

                if (errorCount > 0)
                {
                    parentForm.addLogText(errorCount + " errors while processing file");
                }

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

            if (string.IsNullOrEmpty(header)) returnVal = false;
            if (string.IsNullOrEmpty(startAddress)) returnVal = false;
            if (data.Count == 0) returnVal = false;

            return returnVal;
        }

        public string getHeader()
        {
            return header;
        }

        public string getHeaderDecoded()
        {
            return headerDecoded;
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
                char startCode = dataLine[0];
                char recordType = dataLine[1];
                int byteCount = Convert.ToInt32(dataLine.Substring(2, 2), 16);
                int[] value = new int[byteCount - 1];
                int checksum = Convert.ToInt32(dataLine.Substring(dataLine.Length - 2, 2), 16);

                for (int i = 0; i < byteCount - 1; i++)
                {
                    value[i] = Convert.ToInt32(dataLine.Substring(4 + (i * 2), 2), 16);
                }

                // make sure this is valid S-Rec
                if (startCode == 'S')
                {
                    switch (recordType)
                    {
                        case '0':
                            {
                                // header line
                                char[] head = new char[value.Length - 2];
                                for (int i = 0; i < value.Length - 2; i++)
                                {
                                    head[i] = (char)value[i + 2];
                                }
                                headerDecoded = new string(head);

                                // header line
                                parentForm.addLogText("Found header : " + headerDecoded);
                                //parentForm.addLogText("checksum : 0x" + checksum.ToString("X2"));
                                header = dataLine;
                            }
                            break;
                        case '1':
                            {
                                // data sequence line (2 byte address)
                                data.Add(dataLine);
                            }
                            break;
                        case '2':
                            {
                                // data sequence line (3 byte address)
                                data.Add(dataLine);
                            }
                            break;
                        case '3':
                            {
                                // data sequence line (4 byte address)
                                data.Add(dataLine);
                            }
                            break;
                        case '5':
                            {
                                // record count
                                data.Add(dataLine);
                            }
                            break;
                        case '7':
                            {
                                // end of block (4 byte address)
                                parentForm.addLogText("Found start address : " + dataLine);
                                startAddress = dataLine;
                            }
                            break;
                        case '8':
                            {
                                // end of block (3 byte address)
                                parentForm.addLogText("Found start address : " + dataLine);
                                startAddress = dataLine;
                            }
                            break;
                        case '9':
                            {
                                // end of block (2 byte address)
                                parentForm.addLogText("Found start address : " + dataLine);
                                startAddress = dataLine;
                            }
                            break;
                        default:
                            {
                                errorCount++;
                            }
                            break;
                    }
                }
            }
            catch
            {
                errorCount++;
            }
        }
    }
}
