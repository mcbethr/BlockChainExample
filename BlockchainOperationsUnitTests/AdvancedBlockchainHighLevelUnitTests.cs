using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BlockchainLibrary.ChainOperations;
using BlockchainLibrary;

namespace BlockchainOperationsUnitTests
{
    [TestClass]
    public class AdvancedBlockchainHighLevelUnitTests
    {

        [TestMethod]
        public void TestFindABlockWithADifficultyOf1Test(){
            int Difficulty = 1;
            AdvancedBlockchainHighLevel BE = new AdvancedBlockchainHighLevel("RyanMiner", Difficulty);
            BE.AddTransaction("Ryan Pays Seymore 10BTC for Lunch");
            BE.AddTransaction("Rayanne Pays Starbucks 8 BTC for Coffee");
            BE.AddTransaction("Seymore Pays 5 BTC for VPN subscription");
            ///The transaction should trigger automatically after the 4th transaction
            BE.AddTransaction("POSH Ryan pays 35BTC for a new shirt");
            Assert.AreEqual(0, BE.Blockchain.Last.Value.Hash[0]);

        }

        [TestMethod]
        public void TestFindABlockWithADifficultyOf2Test()
        {
            int Difficulty = 2;
            AdvancedBlockchainHighLevel BE = new AdvancedBlockchainHighLevel("RyanMiner", Difficulty);
            BE.AddTransaction("Ryan Pays Seymore 10BTC for Lunch");
            BE.AddTransaction("Rayanne Pays Starbucks 8 BTC for Coffee");
            BE.AddTransaction("Seymore Pays 5 BTC for VPN subscription");
            ///The transaction should trigger automatically after the 4th transaction
            BE.AddTransaction("POSH Ryan pays 35BTC for a new shirt");
            Assert.AreEqual(0, BE.Blockchain.Last.Value.Hash[1]);

        }

        //Only uncomment this if you are willing to wait about 30 seconds for an answer
        /*
        [TestMethod]
        public void TestFindABlockWithADifficultyOf3Test()
        {
            int Difficulty = 3;
            AdvancedBlockchainHighLevel BE = new AdvancedBlockchainHighLevel("RyanMiner", Difficulty);
            BE.AddTransaction("Ryan Pays Seymore 10BTC for Lunch");
            BE.AddTransaction("Rayanne Pays Starbucks 8 BTC for Coffee");
            BE.AddTransaction("Seymore Pays 5 BTC for VPN subscription");
            ///The transaction should trigger automatically after the 4th transaction
            BE.AddTransaction("POSH Ryan pays 35BTC for a new shirt");
            Assert.AreEqual(0, BE.Blockchain.Last.Value.Hash[2]);

        }
        */


        [TestMethod]
        public void VerifyTransactionTest()
        {
            int Difficulty = 1;
            AdvancedBlockchainHighLevel BE = new AdvancedBlockchainHighLevel("RyanMiner", Difficulty);
            BE.AddTransaction("Ryan Pays Seymore 10BTC for Lunch");
            BE.AddTransaction("Rayanne Pays Starbucks 8 BTC for Coffee");
            BE.AddTransaction("Seymore Pays 5 BTC for VPN subscription");
            ///The transaction should trigger automatically after the 4th transaction
            BE.AddTransaction("POSH Ryan pays 35BTC for a new shirt");

            bool IsValid = BE.VerifyBlock(BE.Blockchain.Last.Value,BE.Blockchain.First.Value);

        }

        [TestMethod]
        public void VerifyEntireBlockChainPassTest()
        {
            int Difficulty = 1;
            AdvancedBlockchainHighLevel BE = new AdvancedBlockchainHighLevel("RyanMiner", Difficulty);
            BE.AddTransaction("Ryan Pays Seymore 10BTC for Lunch");
            BE.AddTransaction("Rayanne Pays Starbucks 8 BTC for Coffee");
            BE.AddTransaction("Seymore Pays 5 BTC for VPN subscription");
            ///The transaction should trigger automatically after the 4th transaction
            BE.AddTransaction("POSH Ryan pays 35BTC for a new shirt");

            BE.AddTransaction("Rayanne Pays Seymore 10BTC for Lunch");
            BE.AddTransaction("Rayanne Pays Starbucks 8 BTC for Coffee");
            BE.AddTransaction("Seymore Pays 5 BTC for Luncg");
            ///The transaction should trigger automatically after the 4th transaction
            BE.AddTransaction("POSH Ryan pays 40BTC for a new pants");

            Block FoundBlock = BE.VerifyBlocks(BE.Blockchain.First.Value);
            Assert.IsNull(FoundBlock);

        }

        [TestMethod]
        public void VerifyEntireBlockChainFailTest()
        {        

            int Difficulty = 1;
            AdvancedBlockchainHighLevel BE = new AdvancedBlockchainHighLevel("RyanMiner", Difficulty);
            BE.AddTransaction("Ryan Pays Seymore 10BTC for Lunch");
            BE.AddTransaction("Rayanne Pays Starbucks 8 BTC for Coffee");
            BE.AddTransaction("Seymore Pays 5 BTC for VPN subscription");
            ///The transaction should trigger automatically after the 4th transaction
            BE.AddTransaction("POSH Ryan pays 35BTC for a new shirt");

            LinkedListNode<Block> SecondBlockNode = BE.Blockchain.Last;

            BE.AddTransaction("Rayanne Pays Seymore 10BTC for Lunch");
            BE.AddTransaction("Rayanne Pays Starbucks 8 BTC for Coffee");
            BE.AddTransaction("Seymore Pays 5 BTC for Luncg");
            ///The transaction should trigger automatically after the 4th transaction
            BE.AddTransaction("POSH Ryan pays 40BTC for a new pants");


            //Tamper with the Second Block and create a replacement block
            Block BlockToTamper = new Block(SecondBlockNode.Value.Hash, SecondBlockNode.Previous.Value.Hash, "Tampered");
            LinkedListNode<Block> TamperedBlockNode = new LinkedListNode<Block>(BlockToTamper);

            BE.Blockchain.AddBefore(BE.Blockchain.Find(SecondBlockNode.Value), TamperedBlockNode);

            //Delete the true block
            BE.Blockchain.Remove(BE.Blockchain.FindLast(SecondBlockNode.Value));


            Block FoundBlock = BE.VerifyBlocks(BE.Blockchain.First.Value);
            Assert.IsNull(FoundBlock);

        }


    }
}
