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
        private IProgress<int> output_prg;
        private IProgress<string> output_str;

        private Header testHeader;
        private Data testData;
        private StartAddr testSAddr;

        public FileClass(Form1 pf, IProgress<int> progress, IProgress<string> progress_str)
        {
            parentForm = pf;
            output_prg = progress;
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
                        return -1;
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

        public async Task<int> outputFile(string file, CancellationToken token)
        {
            int no_done = 0;
            int no_total = getDataCount() + 2;
            output_prg.Report(0);

            try
            {
                StreamWriter sw = new StreamWriter(file);
                try
                {
                    output_str.Report("Writing output file");
                    output_str.Report("Writing header : " + getHeaderDecoded());
                    await sw.WriteLineAsync(getHeader());
                    no_done++;
                    output_prg.Report((no_done / no_total) * 100);

                    output_str.Report("Writing " + getDataCount() + " data lines");
                    for (int i = 0; i < getDataCount(); i++)
                    {
                        await sw.WriteLineAsync(getDataLine(i));
                        no_done++;
                        double blah = (double)no_done / (double)no_total;
                        output_prg.Report((int)(blah * 100));

                        if (token.IsCancellationRequested)
                        {
                            output_str.Report("Operation Cancelled");
                            return -1;
                        }
                    }

                    output_str.Report("Writing start address : " + getDecodedStartAddress());
                    await sw.WriteLineAsync(getStartAddress());
                    output_prg.Report(100);

                    output_str.Report("Concatenation complete.");
                }
                catch
                {
                    output_str.Report("Exception caught processing files!");
                }
                finally
                {
                    sw.Close();
                }
            }
            catch
            {
                output_str.Report("Exception caught opening files!");
            }

            return 1;
        }

        private string getHeader()
        {
            return testHeader.getRawHeader();
        }

        private string getHeaderDecoded()
        {
            return testHeader.getHeader();
        }

        private int getDataCount()
        {
            return testData.getDataItems();
        }

        private string getDataLine(int line)
        {
            return testData.getData(line);
        }

        private string getStartAddress()
        {
            return testSAddr.getRawAddr();
        }

        private string getDecodedStartAddress()
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
