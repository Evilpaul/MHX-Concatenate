using System;
using System.IO;
using FileClass;
using System.Threading;
using System.Threading.Tasks;

namespace mhx_concatenate
{
    class FileClass
    {
        private static int ADDR_SIZE_2 = 2;
        private static int ADDR_SIZE_3 = 3;
        private static int ADDR_SIZE_4 = 4;
        private static int CHKSUM_SIZE = 2;

        private uint errorCount = 0;
        private uint lineCount = 0;
        private Form1 parentForm;
        private IProgress<string> output_str;

        private Header testHeader;
        private Data testData;
        private StartAddr testSAddr;

        public FileClass(Form1 pf, IProgress<string> progress_str)
        {
            parentForm = pf;
            output_str = progress_str;

            errorCount = 0;
            lineCount = 0;

            testHeader = new Header();
            testData = new Data();
            testSAddr = new StartAddr();
        }

        public async Task<int> processFile(string file, CancellationToken token)
        {
            errorCount = 0;
            lineCount = 0;

            using (StreamReader sr = new StreamReader(file))
            {
                output_str.Report("Processing " + file);
                while (sr.Peek() >= 0)
                {
                    if (token.IsCancellationRequested)
                    {
                        output_str.Report("Operation Cancelled");
                        break;
                    }

                    addDataLine(await sr.ReadLineAsync());
                }

                output_str.Report(lineCount + " data lines found");

                if (errorCount > 0)
                {
                    output_str.Report(errorCount + " errors while processing file");
                }

                output_str.Report("");
            }

            return 1;
        }

        public string getHeader()
        {
            return testHeader.getRawHeader();
        }

        public string getHeaderDecoded()
        {
            return testHeader.getHeader();
        }

        public int getDataCount()
        {
            return testData.getDataItems();
        }

        public string getDataLine(int line)
        {
            return testData.getData(line);
        }

        public string getStartAddress()
        {
            return testSAddr.getRawAddr();
        }

        public string getDecodedStartAddress()
        {
            return testSAddr.getStartAddr();
        }

        private void addDataLine(string dataLine)
        {
            try
            {
                char startCode = dataLine[0];
                char recordType = dataLine[1];
                byte byteCount = Convert.ToByte(dataLine.Substring(2, 2), 16);
                byte[] value = new byte[byteCount - 1];
                byte checksum = Convert.ToByte(dataLine.Substring(dataLine.Length - 2, CHKSUM_SIZE), 16);

                for (int i = 0; i < byteCount - 1; i++)
                {
                    value[i] = Convert.ToByte(dataLine.Substring(4 + (i * 2), 2), 16);
                }

                byte calcSum = Checksum.calcChecksum(byteCount, value);

                if (calcSum != checksum)
                    output_str.Report("Invalid Checksum : " + checksum + ", should be :" + calcSum);

                // make sure this is valid S-Rec
                if ((startCode == 'S') && (calcSum == checksum))
                {
                    switch (recordType)
                    {
                        case '0':
                            {
                                // header line
                                string header = Header.decodeData(value, ADDR_SIZE_2, value.Length - CHKSUM_SIZE);
                                testHeader.setHeader(header, dataLine);

                                output_str.Report("Found header : " + header);
                            }
                            break;
                        case '1':
                            {
                                // data sequence line (2 byte address)
                                lineCount++;
                                testData.addData(dataLine);
                            }
                            break;
                        case '2':
                            {
                                // data sequence line (3 byte address)
                                lineCount++;
                                testData.addData(dataLine);
                            }
                            break;
                        case '3':
                            {
                                // data sequence line (4 byte address)
                                lineCount++;
                                testData.addData(dataLine);
                            }
                            break;
                        case '5':
                            {
                                // record count, ignore
                            }
                            break;
                        case '7':
                            {
                                // end of block (4 byte address)
                                uint addr = StartAddr.processAddress(value, 0, ADDR_SIZE_4);
                                testSAddr.setStartAddr(addr, dataLine);
                                output_str.Report("Found start address : " + String.Format("{0:X8}", addr));
                            }
                            break;
                        case '8':
                            {
                                // end of block (3 byte address)
                                uint addr = StartAddr.processAddress(value, 0, ADDR_SIZE_3);
                                testSAddr.setStartAddr(addr, dataLine);
                                output_str.Report("Found start address : " + String.Format("{0:X8}", addr));
                            }
                            break;
                        case '9':
                            {
                                // end of block (2 byte address)
                                uint addr = StartAddr.processAddress(value, 0, ADDR_SIZE_2);
                                testSAddr.setStartAddr(addr, dataLine);
                                output_str.Report("Found start address : " + String.Format("{0:X8}", addr));
                            }
                            break;
                        default:
                            {
                                errorCount++;
                            }
                            break;
                    }
                }
                else
                {
                    errorCount++;
                }
            }
            catch
            {
                errorCount++;
            }
        }
    }
}
