using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;
using System.Xml.Linq;

namespace ConsoleApp2
{
    class CSVReader
    {
        //fields num
        const int columnNum = 42;
        //field index + values(coulumn)
        Dictionary<int, List<double>> csvData;

        //fields names
        List<string> fieldsNames;
        //fields nodes
        List<string> fieldsNodes;

        public CSVReader()
        {
            csvData = new Dictionary<int, List<double>>();
            List<string> fieldsNames = new List<string>();
            List<string> fieldsNodes = new List<string>();
            //names from XML
            XDocument xml = XDocument.Load(@"playback_small.xml");

            foreach (XElement element in xml.Descendants("name"))
            {
                string e = element.ToString();
                e = e.Replace("<name>", "");
                e = e.Replace("</name>", "");
                fieldsNames.Add(e);
            }

            this.fieldsNames = fieldsNames;

            foreach (XElement element in xml.Descendants("node"))
            {
                string e = element.ToString();
                e = e.Replace("<node>", "");
                e = e.Replace("</node>", "");
                fieldsNodes.Add(e);
            }

            this.fieldsNodes = fieldsNodes;

            //csv read
            for (int i = 0; i < columnNum; i++)
            {
                using (var reader = new StreamReader(@"reg_flight.csv"))
                {
                    List<double> listA = new List<double>();
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        var values = line.Split(',');

                        listA.Add(double.Parse(values[i]));
                    }
                    this.csvData.Add(i, listA);
                }
            }
        }

        // return twice every colmun (84 names for 42 columns)
        public List<string> getFieldsNames()
        {
            return this.fieldsNames;
        }

        public List<string> getFieldsNodes()
        {
            return this.fieldsNodes;
        }

        public List<double> getColumn(int index)
        {
            return this.csvData[index];
        }

        public List<double> getColumn(string node)
        {
            return this.csvData[fieldsNodes.IndexOf(node)];
        }

        public List<double> getRow(int index)
        {
            List<double> row = new List<double>();

            foreach (KeyValuePair<int, List<double>> p in this.csvData)
            {

                row.Add(p.Value[index]);
            }

            return row;
        }

        
        public List<double> getSubCol(int indexCol, int Limit)
        {
            List<double> col = this.getColumn(indexCol);
            List<double> subList = col.GetRange(0, Limit);
            return subList;
        }
    }
}

