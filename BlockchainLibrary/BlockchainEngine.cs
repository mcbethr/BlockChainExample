using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockchainLibrary
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

    }
}
