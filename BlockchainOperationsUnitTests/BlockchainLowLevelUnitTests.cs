using BlockchainLibrary;
using BlockchainLibrary.ChainOperations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace BlockchainLowLevelUnitTests
{
    [TestClass]
    public class BlockchainLowLevelTests
    {
        [TestMethod]
        public void CreateGenesisBlockTest()
        {

            BlockchainHighLevel BC = new BlockchainHighLevel();
            Assert.AreEqual("GenesisBlock", BC.Blockchain.Last.Value.Data);
        }

        [TestMethod]
        public void CreateSecondBlockTest()
        {
            BlockchainHighLevel BC = new BlockchainHighLevel();
            BC.AddBlock("Second Test");
            //Get the data for the second Block
            Assert.AreEqual(BC.Blockchain.Last.Value.Data, "Second Test");
        }


    }
}
