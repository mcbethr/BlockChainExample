using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockchainLibrary.ChainOperations
{
    public class BlockchainEngine
    {
        LinkedList<Block> _BlockChain;

        public LinkedList<Block> Blockchain { get {return _BlockChain;} } 

        public BlockchainEngine()
        {
            _BlockChain = new LinkedList<Block>();
            _BlockChain.AddLast(BlockchainOperations.CreateGenesisBlock());
        }

        public void AddBlock(string DataToAdd)
        {
            Block NewBlock = new Block(_BlockChain.Last(), DataToAdd);
            _BlockChain.AddLast(NewBlock);
        }

        public Block FindBlockFromHash(string HashR)
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

        //TODO : Account for checking the last item in the linked list.
        public bool VerifyBlock(Block BlockToVerify)
        {

            //Find the next block in the chain
            //This will fail if it's the last block
            LinkedListNode<Block> NextBlockNode = _BlockChain.Find(BlockToVerify).Next;

            return BlockchainOperations.VerifyBlock(BlockToVerify, NextBlockNode.Value);


            
        }

    }
}
