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
            BE.AddBlock("ThirdTest");
            Block FoundBlock = BE.FindBlockFromHash("5B-61-43-0E-0D-34-52-31-D8-2F-C1-56-F0-8F-00-85");
            Assert.AreEqual("SecondTest", FoundBlock.Data);
        }

        [TestMethod]
        public void TamperWithSecondBlockAndTest()
        {
            BlockchainEngine BE = new BlockchainEngine();
            BE.AddBlock("SecondTest");
            BE.AddBlock("ThirdTest");

            //Get the block I want to tamper with
            Block FoundBlock = BE.FindBlockFromHash("5B-61-43-0E-0D-34-52-31-D8-2F-C1-56-F0-8F-00-85");
            LinkedListNode<Block> FoundBlockNode = new LinkedListNode<Block>(FoundBlock);

            //Get the previous block hash
            Block PreviousBlock = BE.FindBlockFromHash(FoundBlock.PreviousHashR);

            //LinkedListNode<Block> FoundBlockNode = new LinkedListNode<Block>(FoundBlock);

            //Construct a tampered block
            Block TamperedBlock = new Block(PreviousBlock, "Tampered Block");
            BE.Blockchain.AddBefore(BE.Blockchain.Find(FoundBlock), TamperedBlock);

            //Delete the true block
            BE.Blockchain.Remove(BE.Blockchain.FindLast(FoundBlock));

            //Verify the tampered block
            bool Tampered = BE.VerifyBlock(TamperedBlock);
            Assert.IsFalse(Tampered);

        }

        [TestMethod]
        public void VerifyNoTamperOnSecondBlockTest()
        {
            BlockchainEngine BE = new BlockchainEngine();
            BE.AddBlock("SecondTest");
            BE.AddBlock("ThirdTest");
        }
    }
}
