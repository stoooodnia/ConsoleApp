namespace ConsoleApp
{

    internal class Program
    {
        static void Main(string[] args)
        {
            var reader = new DataReader();
            // reader.import
            // reader.print
            reader.ImportAndPrintData("data.csv");
        }
    }
}
