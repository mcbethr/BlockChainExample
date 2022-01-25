using BlockchainLibrary;
using System;

namespace BlockChainExample
{
    class Program
    {
        static void Main(string[] args)
        {
            BlockchainEngine BE = new BlockchainEngine();
            BE.AddBlock("This is a test");
            BE.AddBlock("SecondTest");

            int itemNumber = 0;
            foreach(var item in BE.Blockchain)
            {

                Console.WriteLine("---------------------------" );
                Console.WriteLine("Hash: " + item.HashR);
                Console.WriteLine("Previous Hash: " + item.PreviousHashR);
                Console.WriteLine("Data: " + item.Data);
                Console.WriteLine("---------------------------");
                Console.WriteLine("");
                itemNumber++;
            }
        }
    }
}
