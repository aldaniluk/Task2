using System;
using System.Collections.Generic;
using System.Linq;

namespace Logic
{
    public static class UnitService
    {
        public static Dictionary<int, List<Unit>> ToDictionnary(List<Unit> units)
        {
            Dictionary<int, List<Unit>> result = new Dictionary<int, List<Unit>>();
            units.OrderBy(u => u.Id);

            foreach (var unit in units)
            {
                IsNotParentForItself(unit);
                ParentExists(unit, units);
                IsKeyUnique(unit.Id, result);
                
                result.Add(unit.Id, FindChildren(unit, unit, units));
            }

            return result;
        }

        private static List<Unit> FindChildren(Unit mainUnit, Unit unit, List<Unit> units)
        {
            List<Unit> children = new List<Unit>();

            foreach (var child in units)
            {
                if (child.ParentUnitId == unit.Id)
                {
                    IsNotRecursion(unit, child);
                    IsNotRecursion(mainUnit, child);

                    children.Add(child);
                    children.AddRange(FindChildren(mainUnit, child, units));
                }
            }

            return children;
        }

        private static void IsKeyUnique(int key, Dictionary<int, List<Unit>> dictionary)
        {
            if (dictionary.ContainsKey(key))
            {
                throw new Exception($"Unit with this id = {key} already exists.");
            }
        }

        private static void IsNotParentForItself(Unit unit)
        {
            if (unit.ParentUnitId == unit.Id)
            {
                throw new Exception($"Unit {unit.Name} cannot be parent of itself.");
            }
        }

        private static void IsNotRecursion(Unit unit, Unit child)
        {
            if (unit.ParentUnitId == child.Id)
            {
                throw new Exception($"Cycle dependency in units {unit.Name} and {child.Name}.");
            }
        }

        private static void ParentExists(Unit unit, List<Unit> units)
        {
            if (unit.ParentUnitId == null)
            {
                return;
            }
            
            if (BinarySearchById(units.ToArray(), unit) == -1)
            {
                throw new Exception($"Parent does not exists in unit {unit.Name}.");
            }
        }

        private static int BinarySearchById(Unit[] array, Unit element)
        {
            int left = 0;
            int right = array.Length - 1;
            int middle = 0;

            while (left <= right)
            {
                middle = (left + right) / 2;

                if (element.Id < array[middle].Id)
                {
                    right = middle;
                }
                else if (element.Id > array[middle].Id)
                {
                    left = middle + 1;
                }
                else
                {
                    break;
                }
            }

            return (array[middle].Id == element.Id) ? middle : -1;
        }
    }
}
