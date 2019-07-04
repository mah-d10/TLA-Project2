namespace Project2
{
    class Program
    {
        static void Main()
        {
            var npda = new NPDA("1.txt");
            System.Console.WriteLine(npda.ToCFG().ToString());
        }
    }
}
