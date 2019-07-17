using System;
using System.Configuration;
using static StreamsDemo.StreamsExtension;

namespace ConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var source = ConfigurationManager.AppSettings["sourceFilePath"];

            var destination = ConfigurationManager.AppSettings["destinationFilePathByByte"];

            Console.WriteLine($"ByteCopy() done. Total bytes: {ByByteCopy(source, destination)}");

            Console.WriteLine(IsContentEquals(source, destination));

            destination = ConfigurationManager.AppSettings["destinationFilePathInMemoryByByte"];

            Console.WriteLine($"InMemoryByteCopy() done. Total bytes: {InMemoryByByteCopy(source, destination)}");

            Console.WriteLine(IsContentEquals(source, destination));

            destination = ConfigurationManager.AppSettings["destinationFilePathByBlock"];

            Console.WriteLine($"BlockCopy() done. Total bytes: {ByBlockCopy(source, destination)}");

            Console.WriteLine(IsContentEquals(source, destination));

            destination = ConfigurationManager.AppSettings["destinationFilePathInMemoryByBlock"];

            Console.WriteLine($"InMemoryBlockCopy() done. Total bytes: {InMemoryByBlockCopy(source, destination)}");

            Console.WriteLine(IsContentEquals(source, destination));

            destination = ConfigurationManager.AppSettings["destinationFilePathBuffered"];

            Console.WriteLine($"BufferedCopy() done. Total bytes: {BufferedCopy(source, destination)}");

            Console.WriteLine(IsContentEquals(source, destination));

            destination = ConfigurationManager.AppSettings["destinationFilePathByLine"];

            Console.WriteLine($"ByLineCopy() done. Total bytes: {ByLineCopy(source, destination)}");

            Console.WriteLine(IsContentEquals(source, destination));

            Console.ReadKey();
            //etc
        }
    }
}
