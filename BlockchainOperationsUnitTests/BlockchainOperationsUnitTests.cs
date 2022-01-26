using BlockchainLibrary;
using BlockchainLibrary.ChainOperations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace BlockchainOperationsUnitTests
{
    [TestClass]
    public class BlockchainOperationsTests
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
            Block NewBlock = new Block(GenesisBlock, "Second Test");
            Assert.AreEqual("78-C1-CD-6C-34-AB-1D-D5-34-56-DA-2D-5C-D7-98-E5", NewBlock.HashR);
        }


    }
}
