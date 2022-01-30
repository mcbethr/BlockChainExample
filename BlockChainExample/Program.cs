using BlockchainLibrary;
using BlockchainLibrary.ChainOperations;
using NFT_Library;
using System;

namespace BlockChainExample
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                BasicBlockchain.GenerateBasicBlockChain();
            }

            //To activate this in the debugger, right click 
            //the BlockchainExample project, select "properties"
            //Go to "Debug" and "Application Arguments".  Paste the
            //code below inside the Application Arguments box minus the "//"
            //Ryan Sells Seymore .\images\CirclebackJack.bmp
            if (args.Length > 0)
            {
                BasicBlockchain.GenerateBasicNFTFromCommandLine(args);
            }

    

        }

     
    }

    public static class BasicBlockchain
    {

        public static void GenerateBasicBlockChain()
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

            public static void GenerateBasicNFTFromCommandLine(string[] args)
            {
                BlockchainHighLevel BE = new BlockchainHighLevel();

                string NFT =  NFTHighLevel.CreateNFT(args);
                BE.AddBlock(NFT);

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

}
