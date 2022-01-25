using BlockchainLibrary;
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

            byte[] HashedBlock = BlockchainOperations.CreateGenesisBlock("This is a test");
            string bitString = BitConverter.ToString(HashedBlock);
            Assert.AreEqual("CE-11-4E-45-01-D2-F4-E2-DC-EA-3E-17-B5-46-F3-39", bitString);
        }

        [TestMethod]
        public void CreateSecondBlockTest()
        {
            Block GenesisBlock = new Block("This is a test");
            Block NewBlock = new Block(GenesisBlock, "Second Test");
            Assert.AreEqual("78-C1-CD-6C-34-AB-1D-D5-34-56-DA-2D-5C-D7-98-E5", NewBlock.HashR);
        }

        [TestMethod]
        public void VerifyBlockTest()
        {
            Block GenesisBlock = new Block("GenesisBlock");
            Block NewBlock = new Block(GenesisBlock, "Second Test");

            BlockchainOperations.VerifyBlock(NewBlock,GenesisBlock);

        }
    }
}
