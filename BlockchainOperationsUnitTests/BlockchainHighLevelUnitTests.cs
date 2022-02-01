using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BlockchainLibrary;
using BlockchainLibrary.ChainOperations;

namespace BlockchainLowLevelUnitTests
{
    [TestClass]
    public class BlockchainHighLevelUnitTests
    {
        [TestMethod]
        public void EngineTestCreateGenesisBlock()
        {
            BlockchainHighLevel BE = new BlockchainHighLevel();
            Assert.AreEqual("GenesisBlock", BE.Blockchain.First.Value.Data);
        }

        [TestMethod]
        public void VerifyBlockSearch()
        {
            BlockchainHighLevel BE = new BlockchainHighLevel();
            BE.AddBlock("SecondTest");
            //Get the data for the second Block
            Block SecondBlock = BE.Blockchain.Last.Value;
            BE.AddBlock("ThirdTest");
            Block FoundBlock = BE.FindBlock(SecondBlock.BlockHash);
            Assert.AreEqual("SecondTest", FoundBlock.Data);
        }

        [TestMethod]
        public void TamperWithSecondBlockAndTest()
        {
            BlockchainHighLevel BE = new BlockchainHighLevel();
            BE.AddBlock("SecondTest");
            //Get the data for the second Block
            Block SecondBlock = BE.Blockchain.Last.Value;
            BE.AddBlock("ThirdTest");

            //Get the second block to tamper with it
            Block FoundBlock = BE.FindBlock(SecondBlock.BlockHash);
            LinkedListNode<Block> FoundBlockNode = new LinkedListNode<Block>(FoundBlock);

            //Construct a tampered block
            Block TamperedBlock = new Block(FoundBlock.BlockHash,FoundBlockNode.Value.PreviousBlockHash, "Tampered Block");
            BE.Blockchain.AddBefore(BE.Blockchain.Find(FoundBlock), TamperedBlock);

            //Delete the true block
            BE.Blockchain.Remove(BE.Blockchain.FindLast(FoundBlock));

            ///Find the next node after the tampered Block
            Block FindTamperedBlock = BE.FindBlock(TamperedBlock.BlockHash);
            LinkedListNode<Block> FindNodeAfterTamperedBlock= new LinkedListNode<Block>(FoundBlock);

            //Verify the tampered block
            bool BlockIsAuthentic = BE.VerifyBlock(TamperedBlock, FindNodeAfterTamperedBlock.Value);
            Assert.IsFalse(BlockIsAuthentic);

        }

        [TestMethod]
        public void VerifyNoTamperOnSecondBlockTest()
        {
            BlockchainHighLevel BE = new BlockchainHighLevel();
            BE.AddBlock("SecondTest");
            //Get the data for the second Block
            Block SecondBlock = BE.Blockchain.Last.Value;
            BE.AddBlock("ThirdTest");

            //Get the second block
            Block FoundBlock = BE.FindBlock(SecondBlock.BlockHash);

            //Verify the second block
            bool BlockIsAuthentic = BE.VerifyBlock(FoundBlock,BE.Blockchain.Last.Value);
            Assert.IsTrue(BlockIsAuthentic);
        }

        [TestMethod]
        public void VerifyEntireBlockChainTest()
        {
            BlockchainHighLevel BE = new BlockchainHighLevel();
            BE.AddBlock("SecondTest");
            BE.AddBlock("ThirdTest");

            Block ReturnBlock = BE.VerifyBlocks(BE.Blockchain.First.Value);
            Assert.IsNull(ReturnBlock);
        
        
        }

        [TestMethod]
        public void VerifyLastNode()
        {
            BlockchainHighLevel BE = new BlockchainHighLevel();
            BE.AddBlock("SecondTest");
            BE.AddBlock("ThirdTest");

            bool Verified = BE.VerifyBlock(BE.Blockchain.Last.Value,null);
            Assert.IsTrue(Verified);


        }


        [TestMethod]
        public void VerifyEntireBlockChainFromSecondBlockTest()
        {
            BlockchainHighLevel BE = new BlockchainHighLevel();
            BE.AddBlock("SecondTest");
            //Get the data for the second Block
            Block SecondBlock = BE.Blockchain.Last.Value;
            BE.AddBlock("ThirdTest");

            Block ReturnBlock = BE.VerifyBlocks(SecondBlock);
            Assert.IsNull(ReturnBlock);

        }


        [TestMethod]
        public void TamperSecondBlockAndFindDuringVerificationOfBlockChainTest()
        {
            BlockchainHighLevel BE = new BlockchainHighLevel();
            BE.AddBlock("SecondTest");
            //Get the data for the second Block
            Block SecondBlock = BE.Blockchain.Last.Value;
            BE.AddBlock("ThirdTest");

            //Get the second block to tamper with it
            Block FoundBlock = BE.FindBlock(SecondBlock.BlockHash);
            LinkedListNode<Block> FoundBlockNode = new LinkedListNode<Block>(FoundBlock);

            //Construct a tampered block
            Block TamperedBlock = new Block(FoundBlock.BlockHash, FoundBlockNode.Value.PreviousBlockHash, "Tampered Block");
            BE.Blockchain.AddBefore(BE.Blockchain.Find(FoundBlock), TamperedBlock);

            //Delete the true block
            BE.Blockchain.Remove(BE.Blockchain.FindLast(FoundBlock));

            Block ReturnBlock = BE.VerifyBlocks(BE.Blockchain.First.Value);
            Assert.AreEqual(ReturnBlock, TamperedBlock);

        }


    }
}
