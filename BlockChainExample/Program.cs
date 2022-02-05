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
            //Go to "Debug" and "Application Arguments". for older versions
            //
            //Or go to Debug > General > Open Debug Launch Profiles UI > Application Arguments
            //Paste the
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
            BE.AddBlock("Ryan Pays PoshRyan 10 BTC for lunch");
            BE.AddBlock("Seymore pays 20 BTC to Moo2You for Milkshake.");

            int itemNumber = 0;
            foreach (var item in BE.Blockchain)
            {

                Console.WriteLine("---------------------------");
                Console.WriteLine("Block Hash: " + item.HashR);
                Console.WriteLine("Previous Hash: " + item.PreviousHashR);
                Console.WriteLine("Transaction Data: " + item.Data);
                Console.WriteLine("---------------------------");
                Console.WriteLine("");
                itemNumber++;
            }

        }

            public static void GenerateBasicNFTFromCommandLine(string[] args)
            {
                BlockchainHighLevel BE = new BlockchainHighLevel();

                string From = args[0];
                string Action = args[1];
                string To = args[2];
                string Filename = args[3];


                string NFT =  NFTHighLevel.MintNFT(From,Action,To,Filename);
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
