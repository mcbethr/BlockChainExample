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

        //TODO : Acount for the first block
        public bool VerifyBlock(Block BlockToVerify)
        {

            //Find the Block in the Blockchain
            Block PreviousBlock = Blockchain.Find(BlockToVerify).Previous.Value;

            //Re-generate the BlockToVerify with the hash from the previous block
            //and the data from the current BlockToVerify
            return BlockchainOperations.VerifyBlock(PreviousBlock, BlockToVerify);


            
        }

    }
}
