using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockchainLibrary.ChainOperations
{
    public class BlockchainHighLevel
    {
        LinkedList<Block> _BlockChain;

        public LinkedList<Block> Blockchain { get {return _BlockChain;} } 

        public BlockchainHighLevel()
        {
            _BlockChain = new LinkedList<Block>();
            _BlockChain.AddLast(BlockchainLowLevel.CreateGenesisBlock());
        }

        public void AddBlock(string DataToAdd)
        {
            byte[] NewBlockHash = BlockchainLowLevel.HashBlock(_BlockChain.Last.Value.BlockHash, DataToAdd);
            Block NewBlock = new Block(NewBlockHash, _BlockChain.Last.Value.BlockHash, DataToAdd);
            _BlockChain.AddLast(NewBlock);
        }

        /// <summary>
        /// Find block by string hash
        /// </summary>
        /// <param name="HashR"></param>
        /// <returns></returns>
        public Block FindBlock(string HashR)
        {
            //Search Through The Entire Block
            //If we don't find anything, return Null. 
            foreach(var item in Blockchain)
            {
                if (HashR.Equals(item.HashR))
                {
                    return item;
                }
            }

            return null;
        }

        /// <summary>
        /// Find block by binary hash
        /// </summary>
        /// <param name="HashR"></param>
        /// <returns></returns>
        public Block FindBlock(byte[] Hash)
        {
            //Search Through The Entire Block
            //If we don't find anything, return Null. 
            foreach (var item in Blockchain)
            {

                if(item.BlockHash.SequenceEqual(Hash))
                { 
                    return item;
                }
            }

            return null;
        }

        //TODO : Figure out what to do if it's the last block
        public bool VerifyBlock(Block BlockToVerify, Block NextBlock)
        {

            return BlockchainLowLevel.VerifyBlock(BlockToVerify, NextBlock);            
        }

        /// <summary>
        /// Verifies all blocks from a start point.
        /// Returns null if ok, and the first tampered 
        /// block if tampering is detected
        /// We should use a Tuple, a struct or a class, 
        /// but going for simplicity here.
        /// </summary>
        /// <returns></returns>
        public Block VerifyBlocks(Block StartBlock)
        {
            Block TamperedBlock = null;

            LinkedListNode<Block> StartNode = new LinkedListNode<Block>(StartBlock);

            LinkedListNode<Block> NodeToCheck = _BlockChain.Find(StartNode.Value);

            //if nothing exists, we're at the end of the block.
            if (NodeToCheck.Next == null)
            {
                return null;
            }

            ///If the node is verified, then move to the next node
            ///else, return the bad block. Yes we're using recursion
            if (VerifyBlock(NodeToCheck.Value,NodeToCheck.Next.Value) == true)
            {
                VerifyBlocks(NodeToCheck.Next.Value);
            }
            else
            {
                return NodeToCheck.Next.Value;
            }

            return TamperedBlock;
        }

    }
}
