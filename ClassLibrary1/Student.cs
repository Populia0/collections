namespace ClassLibrary
{
    public class Student
    {
        public string? lastName, name, patronymic;
        public Class stClass;
        public int id;
        public int Id { get; set; }
        public int ClassId { get; set; }
        public virtual Class StClass { get; set; }
        public string? LastName { get; set; }
        public string? Name { get; set; }
        public string? Patronymic { get; set; }

        public Student()
        {
        }
        public Student(int id, string lastName, string name, string patronymic, Class _class, int classId)
        {
            Id = id;
            LastName = lastName;
            Name = name;
            Patronymic = patronymic;
            StClass = _class;
            ClassId = classId;
        }
    }
}
