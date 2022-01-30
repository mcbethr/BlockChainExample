using BlockchainLibrary;
using BlockchainLibrary.ChainOperations;
using NFT_Library;
using System;

namespace BlockChainExample
{
    class Program
    {
        static void Main()
        {


            //Create a blockchain and add some transactions
            BlockchainHighLevel BE = new BlockchainHighLevel();
            BE.AddBlock("SecondTest");
            BE.AddBlock("ThirdTest");

            int itemNumber = 0;
            foreach (var item in BE.Blockchain)
            {

                Console.WriteLine("---------------------------");
                Console.WriteLine("Hash: " + item.HashR);
                Console.WriteLine("Previous Hash: " + item.PreviousHashR);
                Console.WriteLine("Data: " + item.Data);
                Console.WriteLine("---------------------------");
                Console.WriteLine("");
                itemNumber++;
            }

        }

     
    }

    public class BasicBlockchain
    {

    }

}
