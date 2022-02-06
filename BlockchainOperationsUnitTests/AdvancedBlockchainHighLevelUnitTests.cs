using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BlockchainLibrary;
using BlockchainLibrary.ChainOperations;

namespace BlockchainOperationsUnitTests
{
    [TestClass]
    public class AdvancedBlockchainHighLevelUnitTests
    {

        [TestMethod]
        public void TestFindABlockWithADifficultyOf1Test() //0
        {
            int Difficulty = 1;
            AdvancedBlockchainHighLevel BE = new AdvancedBlockchainHighLevel("RyanMiner", Difficulty);
            BE.AddTransaction("Ryan Pays Seymore 10BTC for Lunch");
            BE.AddTransaction("Rayanne Pays Starbucks 8 BTC for Coffee");
            BE.AddTransaction("Seymore Pays 5 BTC for VPN subscription");
            ///The transaction should trigger automatically after the 4th transaction
            BE.AddTransaction("POSH Ryan pays 35BTC for a new shirt");
            Assert.AreEqual(0, BE.Blockchain.Last.Value.BlockHash[0]);

        }

        [TestMethod]
        public void TestFindABlockWithADifficultyOf2Test() //00
        {
            int Difficulty = 2;
            AdvancedBlockchainHighLevel BE = new AdvancedBlockchainHighLevel("RyanMiner", Difficulty);
            BE.AddTransaction("Ryan Pays Seymore 10BTC for Lunch");
            BE.AddTransaction("Rayanne Pays Starbucks 8 BTC for Coffee");
            BE.AddTransaction("Seymore Pays 5 BTC for VPN subscription");
            ///The transaction should trigger automatically after the 4th transaction
            BE.AddTransaction("POSH Ryan pays 35BTC for a new shirt");
            Assert.AreEqual(0, BE.Blockchain.Last.Value.BlockHash[1]);

        }

        /// <summary>
        /// IF you go over 3 zeros, be prepared to wait.
        /// </summary>
        [TestMethod]
        public void TestFindABlockWithADifficultyOf3Test() //000
        {
            int Difficulty = 3;
            AdvancedBlockchainHighLevel BE = new AdvancedBlockchainHighLevel("RyanMiner", Difficulty);
            BE.AddTransaction("Ryan Pays Seymore 10BTC for Lunch");
            BE.AddTransaction("Rayanne Pays Starbucks 8 BTC for Coffee");
            BE.AddTransaction("Seymore Pays 5 BTC for VPN subscription");
            ///The transaction should trigger automatically after the 4th transaction
            BE.AddTransaction("POSH Ryan pays 35BTC for a new shirt");
            Assert.AreEqual(0, BE.Blockchain.Last.Value.BlockHash[2]);
           
        }
        


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
            
            bool IsValid = BE.VerifyBlock(BE.Blockchain.Last.Value,BE.Blockchain.First.Value,null);

        }

        [TestMethod]
        public void VerifyLastBlockPass()
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
            BE.AddTransaction("Seymore Pays 5 BTC for Lunch");
            ///The transaction should trigger automatically after the 4th transaction
            BE.AddTransaction("POSH Ryan pays 40BTC for a new pants");

            bool BlockVerified = BE.VerifyBlock(BE.Blockchain.Last.Value,BE.Blockchain.Last.Previous.Value,null);
            Assert.IsTrue(BlockVerified);

        }

        [TestMethod]
        public void VerifySecondBlockTamperTest()
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
            Block BlockToTamper = new Block(SecondBlockNode.Value.BlockHash, SecondBlockNode.Previous.Value.BlockHash, "Tampered");
            LinkedListNode<Block> TamperedBlockNode = new LinkedListNode<Block>(BlockToTamper);

            BE.Blockchain.AddBefore(BE.Blockchain.Find(SecondBlockNode.Value), TamperedBlockNode);

            //Delete the true block
            BE.Blockchain.Remove(BE.Blockchain.FindLast(SecondBlockNode.Value));


            bool VerifiedBlock = BE.VerifyBlock(TamperedBlockNode.Value, TamperedBlockNode.Previous.Value, TamperedBlockNode.Next.Value);
            Assert.IsFalse(VerifiedBlock);

        }

        [TestMethod]
        public void TestHashForVideo()
        {
            int Difficulty = 1;
            AdvancedBlockchainHighLevel BE = new AdvancedBlockchainHighLevel("RyanMiner", Difficulty);
            BE.AddTransaction("Ryan Pays Seymore 10BTC for Lunch");
            BE.AddTransaction("Rayanne Pays Starbucks 8 BTC for Coffee");
            BE.AddTransaction("Seymore Pays 5 BTC for VPN subscription");
            ///The transaction should trigger automatically after the 4th transaction
            BE.AddTransaction("POSH Ryan pays 35BTC for a new shirt");

            bool IsValid = BE.VerifyBlock(BE.Blockchain.Last.Value, BE.Blockchain.First.Value, null);

        }

    }
}
