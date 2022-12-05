using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptimizeLab
{
    public struct OrgMaxOutput
    {
        public int Si;
        public List<int> X;
        public double maxZ;
    }
    internal class Program
    {
        //Вариант 1
        private static int[] Org1 = {5, 9, 12, 14, 15, 18, 20, 24, 27 };
        private static int[] Org2 = {7, 9, 11, 13, 16, 19, 21, 22, 25 };
        private static int[] Org3 = {6, 10, 13,15, 16, 18, 21, 22, 25 };
        public static List<OrgMaxOutput> orgMaxOutputs1 = new List<OrgMaxOutput>();
        public static List<OrgMaxOutput> orgMaxOutputs2 = new List<OrgMaxOutput>();
        public static List<OrgMaxOutput> orgMaxOutputs3 = new List<OrgMaxOutput>();
        private static int S0 = 9;
        static void Main(string[] args)
        {
            for (int i = 0; i <= S0; i++)
            {
                orgMaxOutputs3.Add(new OrgMaxOutput() 
                {
                        Si= i,
                        X = new List<int> { i }, 
                        maxZ = BelmanLast(i)
                }
                );
            }
            for (int i = 1; i <= S0; i++)
            {
                var res = Belman(i, 2);
                orgMaxOutputs2.Add(new OrgMaxOutput()
                {
                    X = res.Keys.ToList(),
                    maxZ = res.Values.FirstOrDefault(),
                    Si = i,
                }
               );
            }
            for (int i = 1; i <= S0; i++)
            {
                var res = Belman(i, 1);
                orgMaxOutputs1.Add(new OrgMaxOutput()
                {
                    X = res.Keys.ToList(),
                    maxZ = res.Values.FirstOrDefault(),
                    Si = i,
                }
               );
            }
            
            var x1O = orgMaxOutputs1.OrderByDescending(c=> c.maxZ).First();
            var maxVal1 = x1O.maxZ;
            
            foreach(var x1 in x1O.X)
            {
                var x2O = orgMaxOutputs2.First(c => c.Si == S0 - x1);
                foreach (var x2 in x2O.X)
                {
                  var x3O = orgMaxOutputs3.First(c => c.Si == x2O.Si - x2);
                  foreach (var x3 in x3O.X)
                    {
                      Console.WriteLine($"x1 = {x1} x2= {x2} x3= {x3} maxZ = {x1O.maxZ}");
                    }

                }
            }
            
            Console.ReadLine();
        }

        private static double BelmanLast(int si)
        {
            return si == 0 ? 0 : Org3[si - 1];
        }

        private static Dictionary<int, double> Belman(int si, int orgN)
        {
            Dictionary<int,double> res = new Dictionary<int, double> ();
            int j = si;
            switch (orgN)
            {
                case 2:
                    
                    for (int i = 0; i <= si; i++)
                    {
                        var x = i == 0 ? 0 : Org2[i-1];
                        res.Add(i,orgMaxOutputs3.FirstOrDefault(a => a.Si == j).maxZ + x);
                        j--;
                    }
                    break;
                case 1:
                    for (int i = 0; i <= si; i++)
                    {
                        var x = i == 0 ? 0 : Org1[i -1 ];
                        res.Add(i, orgMaxOutputs2.FirstOrDefault(a => a.Si == j).maxZ + x);
                        j--;
                    }
                    break;

            }

            var maxVal = res.Max(c => c.Value);
            var outRes = res.Where(c => c.Value == maxVal).ToList();
            var A = new Dictionary<int, double>();
            foreach (var x in outRes)
            {
                A.Add(x.Key, x.Value);
            }
            return A;
        }
    }
}
