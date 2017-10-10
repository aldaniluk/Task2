using Logic;
using System;
using System.Collections.Generic;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Unit u1 = new Unit("Unit1", null);
            Unit u2 = new Unit("Unit2", 1); 
            Unit u3 = new Unit("Unit3", 2);
            Unit u4 = new Unit("Unit4", 1);
            Unit u5 = new Unit("Unit5", null);

            Dictionary<int, List<Unit>> result = UnitService.ToDictionnary(new List<Unit> {u1,u2,u3,u4,u5});

            foreach (var r in result)
            {
                Console.Write($"{r.Key} => {{");
                foreach(var v in r.Value)
                {
                    Console.Write(v.Id);
                }
                Console.Write("}\n");
            }
            
        }
    }
}
