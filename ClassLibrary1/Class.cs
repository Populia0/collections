namespace ClassLibrary
{
    public class Class
    {
        public int number;
        public char letter;
        public int id;

        public List<Student> students;
        public virtual List<Student> Students { get; set; } = new();
        public int Id { get; set; }
        public int Number
        {
            get => number;
            set
            {
                if (value >= 1 && value <= 12)
                    number = value;
                else
                    number = 1;
            }
        }
        public char Letter
        {
            get => letter;
            set
            {
                if (value >= 1040 && value <= 1103 || value >= 65 && value <= 90 || value >= 97 && value <= 122)
                    letter = value;
                else
                    letter = 'А';
            }
        }
        public Class() { }
        public Class(int id, int number, char letter, List<Student> students)
        {
            Id = id;
            Number = number;
            Letter = letter;
            Students = students;
        }
    }
}
