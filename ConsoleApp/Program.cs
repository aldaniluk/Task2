using Logic;
using System;
using System.Collections.Generic;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Unit u1 = new Unit(1, "u1", null);
            Unit u2 = new Unit(2, "u2", 1);
            Unit u3 = new Unit(3, "u3", 1);
            Unit u4 = new Unit(4, "u4", 2);
            Unit u5 = new Unit(5, "u5", 2);
            Unit u6 = new Unit(6, "u6", 4);
            Unit u7 = new Unit(7, "u7", 6); 

            Dictionary<int, List<Unit>> result = UnitService.ToDictionnary(new List<Unit> { u1, u2, u3, u4, u5, u6, u7 });
            foreach (var r in result)
            {
                Console.Write($"{r.Key} => {{");
                foreach (var v in r.Value)
                {
                    Console.Write(v.Id);
                }
                Console.Write("}\n");
            }

        }
    }
}
