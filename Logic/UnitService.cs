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

            HashSet<int> unitsSet = new HashSet<int>(units.Select(u => u.Id));
            foreach (var unit in units)
            {
                ParentExists(unit, unitsSet);
            }

            Dictionary<int, List<Unit>> unitsByParentUnitId = units
                .Where(u => u.ParentUnitId.HasValue)
                .GroupBy(u => u.ParentUnitId.Value)
                .ToDictionary(g => g.Key, g => g.ToList());

            foreach (var unit in units)
            {
                IsNotParentForItself(unit);
                IsKeyUnique(unit.Id, result);

                result.Add(unit.Id, FindChildren(unit, unit, unitsByParentUnitId));
            }

            return result;
        }

        private static List<Unit> FindChildren(Unit mainUnit, Unit unit, Dictionary<int, List<Unit>> unitsByParentUnitId)
        {
            List<Unit> possibleChildren;
            if (!unitsByParentUnitId.TryGetValue(unit.Id, out possibleChildren))
            {
                return new List<Unit>();
            }

            List<Unit> children = new List<Unit>();
            foreach (var child in possibleChildren)
            {
                IsNotRecursion(unit, child);
                IsNotRecursion(mainUnit, child);

                children.Add(child);
                children.AddRange(FindChildren(mainUnit, child, unitsByParentUnitId));
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

        private static void ParentExists(Unit unit, HashSet<int> unitsSet)
        {
            if (unit.ParentUnitId == null)
            {
                return;
            }

            if (!unitsSet.Contains(unit.ParentUnitId.Value))
            {
                throw new Exception($"Parent does not exists in unit {unit.Name}.");
            }
        }
    }
}
