using System;
using System.Collections.Generic;
using System.Linq;

namespace Logic
{
    public static class UnitService
    {
        private static List<Unit> _units;
        private static Dictionary<int, List<Unit>> _result;

        public static Dictionary<int, List<Unit>> ToDictionnary(List<Unit> units)
        {
            _units = units;
            _result = new Dictionary<int, List<Unit>>();

            FindChildsOfFirstNesting();
            FindChildsOfSecondNesting();

            return _result;
        }

        private static void FindChildsOfFirstNesting()
        {
            foreach (var unit in _units)
            {
                IsNotParentFotItself(unit);

                List<Unit> firstNestingChilds = new List<Unit>();
                foreach (var child in _units)
                {
                    if (unit.Id == child.ParentUnitId)
                    {
                        IsNotRecursion(unit, child);
                        firstNestingChilds.Add(child);
                    }
                }
                _result.Add(unit.Id, firstNestingChilds);
            }
        }

        private static void FindChildsOfSecondNesting()
        {
            foreach (var unit in _result)
            {
                if (unit.Value.Count == 0) continue;

                List<Unit> secondNestingChilds = new List<Unit>();
                foreach (var firstNestingChild in unit.Value)
                {
                    foreach (var secondNestingChild in _result[firstNestingChild.Id])
                    {
                        IsNotRecursion(_units.Find(u => u.Id == unit.Key), secondNestingChild);
                        secondNestingChilds.Add(secondNestingChild);
                    }
                }
                unit.Value.AddRange(secondNestingChilds);
            }
        }

        #region validation
        private static void IsNotParentFotItself(Unit unit)
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
        #endregion
    }
}
