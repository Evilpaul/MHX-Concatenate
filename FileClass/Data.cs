using System.Collections.Generic;

namespace FileClass
{
    class Data
    {
        private List<string> data;

        public Data()
        {
            data = new List<string>();
        }

        public void addData(string dataLine)
        {
            data.Add(dataLine);
        }

        public int getDataItems()
        {
            return data.Count;
        }

        public string getData(int index)
        {
            return data[index];
        }
    }
}
