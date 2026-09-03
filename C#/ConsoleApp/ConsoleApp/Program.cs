namespace ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("===== Start =====");
            JSONLine.JsonLine.Build();
            Console.WriteLine("===== End =====");

            Console.ReadLine();
        }
    }
}
