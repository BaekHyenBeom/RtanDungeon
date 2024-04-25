namespace ConsoleApp1
{
    internal class Program
    {
        class Person
        {
            private string name;
            public int age { get; }

            public Person()
            {
                name = "문어";
                name = "age";
            }

            public Person(string str)
            {
                name = str;
            }

            public Person(string str, int num)
            {
                name = str;
                age = num;
            }

            public void PrintInfo()
            {
                Console.WriteLine($"Name: {name}, Age: {age}");
            }
        }




        static void Main(string[] args)
        {
            Person person1 = new Person();
            person1.PrintInfo();
            Person person2 = new Person("설탕");
            person2.PrintInfo();
            Person person3 = new Person("사탕", 20);
            person3.PrintInfo();
        }
    }
}
