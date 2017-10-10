using System;
using System.Collections.Generic;

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
                List<Unit> childUnits = new List<Unit>();
                foreach (var child in _units)
                {
                    if (unit.Id == child.ParentUnitId)
                    {
                        IsNotRecursion(unit, child);
                        childUnits.Add(child);
                    }
                }
                _result.Add(unit.Id, childUnits);
            }
        }

        private static void FindChildsOfSecondNesting()
        {
            foreach (var unit in _result)
            {
                if (unit.Value.Count != 0)
                {
                    List<Unit> secondNestingChilds = new List<Unit>();
                    foreach (var oneChild in unit.Value)
                    {
                        foreach (var secondNestingChild in _result[oneChild.Id])
                        {
                            IsNotRecursion(unit.Key, secondNestingChild);
                            secondNestingChilds.Add(secondNestingChild);
                        }
                    }
                    unit.Value.AddRange(secondNestingChilds);
                }
            }
        }

        #region validation
        private static void IsNotParentFotItself(Unit unit)
        {
            if (unit.ParentUnitId == unit.Id)
            {
                throw new Exception("Unit cannot be parent of itself.");
            }
        }

        private static void IsNotRecursion(Unit unit, Unit child)
        {
            if (unit.ParentUnitId == child.Id)
            {
                throw new Exception("Recursion isn't appropriate.");
            }
        }

        private static void IsNotRecursion(int unitId, Unit child)
        {
            IsNotRecursion(_units.Find(u => u.Id == unitId), child);
        }
        #endregion
    }
}
