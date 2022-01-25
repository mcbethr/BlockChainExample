using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockchainLibrary
{
    public class BlockchainEngine
    {
        LinkedList<Block> BlockChain;

        public BlockchainEngine()
        {
            BlockChain = new LinkedList<Block>();
            BlockChain.AddLast(BlockchainOperations.CreateGenesisBlock());
        }

        public void AddBlock(string DataToAdd)
        {
            Block NewBlock = new Block(BlockChain.Last(), DataToAdd);
        }

    }
}
