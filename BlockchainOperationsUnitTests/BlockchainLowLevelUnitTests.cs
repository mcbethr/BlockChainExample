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

            Block GenesisBlock = new Block("GenesisBlock");
            Assert.AreEqual("GenesisBlock", GenesisBlock.Data);
        }

        [TestMethod]
        public void CreateSecondBlockTest()
        {
            Block GenesisBlock = new Block("This is a test");
            Block NewBlock = new Block(GenesisBlock.BlockHash,GenesisBlock.PreviousBlockHash, "Second Test");
            //Get the data for the second Block
            Assert.AreEqual(NewBlock.Data, "Second Test");
        }


    }
}
