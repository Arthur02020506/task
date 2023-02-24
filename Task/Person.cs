namespace Task
{
    internal class Person
    {
        public Person(string name,string surname,string separator,string phonenumber)
        {
            Name = name;
            Surname = surname;
            Separator = separator;
            PhoneNumber = phonenumber;
        }
        public override string ToString()
        {
            return Name + " " + Surname + " " + Separator + " " + PhoneNumber;
        }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Separator { get; set; }
        public string PhoneNumber { get; set; }
    }
}
