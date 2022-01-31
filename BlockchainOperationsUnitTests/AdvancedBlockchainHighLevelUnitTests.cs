using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BlockchainLibrary.ChainOperations;

namespace BlockchainOperationsUnitTests
{
    [TestClass]
    public class AdvancedBlockchainHighLevelUnitTests
    {

        [TestMethod]
        public void TestFindABlockWithADifficultyOf1(){
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
        public void TestFindABlockWithADifficultyOf2()
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

        [TestMethod]
        public void TestFindABlockWithADifficultyOf3()
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



    }
}
