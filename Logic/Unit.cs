namespace Logic
{
    public class Unit
    {
        private static int count = 1;

        public int Id { get; }
        public string Name { get; }
        public int? ParentUnitId { get; }

        public Unit(string name, int? parentUnitId)
        {
            Id = count++;
            Name = name;
            ParentUnitId = parentUnitId;
        }

        public override bool Equals(object obj)
        {
            Unit unit = obj as Unit;
            return Equals(unit);
        }

        public bool Equals(Unit unit)
        {
            if (unit == null) return false;
            bool isEquals = (Id == unit.Id && 
                Name == unit.Name && 
                ParentUnitId == unit.ParentUnitId);
            return isEquals;
        }
    }
}
