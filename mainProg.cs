using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// new line and new word
// 4
//5
//6
//7



namespace ConsoleApp2
{
    class mainProg
    {
        static void Main(string[] args)
        {
            //CSVReader c = new CSVReader();
            //List<double> l = c.getRow(162);
            Console.WriteLine("works");
            GraphModel Gm = new GraphModel();
            Gm.setGraphCol(37,140);
            Gm.setCorrelativeGraphCol(37,140);
            List<double> l = Gm.CorrelativeGraphCol;
            List<double> l2 = Gm.GraphCol;
            foreach(double d in l )
            {
                Console.WriteLine(d);
            }
            string userName = Console.ReadLine();
        }
    }
}
