namespace Logic
{
    public class Unit
    {
        public int Id { get; }
        public string Name { get; }
        public int? ParentUnitId { get; }

        public Unit(int id, string name, int? parentUnitId)
        {
            Id = id;
            Name = name;
            ParentUnitId = parentUnitId;
        }

        public override bool Equals(object obj)
        {
            Unit unit = obj as Unit;
            return Equals(unit);
        }

        public override int GetHashCode()
        {
            return Id;
        }

        public bool Equals(Unit unit)
        {
            if (unit == null)
            {
                return false;
            }
            return Id == unit.Id;
        }
    }
}
