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
    public class BlockchainEngineUnitTests
    {
        [TestMethod]
        public void EngineTestCreateGenesisBlock()
        {
            BlockchainEngine BE = new BlockchainEngine();
            Assert.AreEqual("GenesisBlock", BE.Blockchain.First.Value.Data);
        }

        [TestMethod]
        public void VerifyBlockSearch()
        {
            BlockchainEngine BE = new BlockchainEngine();
            BE.AddBlock("SecondTest");
            //Get the data for the second Block
            Block SecondBlock = BE.Blockchain.Last.Value;
            BE.AddBlock("ThirdTest");
            Block FoundBlock = BE.FindBlock(SecondBlock.Hash);
            Assert.AreEqual("SecondTest", FoundBlock.Data);
        }

        [TestMethod]
        public void TamperWithSecondBlockAndTest()
        {
            BlockchainEngine BE = new BlockchainEngine();
            BE.AddBlock("SecondTest");
            //Get the data for the second Block
            Block SecondBlock = BE.Blockchain.Last.Value;
            BE.AddBlock("ThirdTest");

            //Get the second block to tamper with it
            Block FoundBlock = BE.FindBlock(SecondBlock.Hash);
            LinkedListNode<Block> FoundBlockNode = new LinkedListNode<Block>(FoundBlock);

            //Construct a tampered block
            Block TamperedBlock = new Block(FoundBlock, "Tampered Block");
            BE.Blockchain.AddBefore(BE.Blockchain.Find(FoundBlock), TamperedBlock);

            //Delete the true block
            BE.Blockchain.Remove(BE.Blockchain.FindLast(FoundBlock));

            //Verify the tampered block
            bool BlockIsAuthentic = BE.VerifyBlock(TamperedBlock);
            Assert.IsFalse(BlockIsAuthentic);

        }

        [TestMethod]
        public void VerifyNoTamperOnSecondBlockTest()
        {
            BlockchainEngine BE = new BlockchainEngine();
            BE.AddBlock("SecondTest");
            //Get the data for the second Block
            Block SecondBlock = BE.Blockchain.Last.Value;
            BE.AddBlock("ThirdTest");

            //Get the second block
            Block FoundBlock = BE.FindBlock(SecondBlock.Hash);

            //Verify the second block
            bool BlockIsAuthentic = BE.VerifyBlock(FoundBlock);
            Assert.IsTrue(BlockIsAuthentic);


        }


    }
}
