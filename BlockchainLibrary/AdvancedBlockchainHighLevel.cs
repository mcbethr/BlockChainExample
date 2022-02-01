using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockchainLibrary.ChainOperations
{
    public class AdvancedBlockchainHighLevel
    {
        LinkedList<Block> _BlockChain;
        List<string> _Transactions;
        string _MinerName;
        int _Difficulty;
        const int tranactionLimit = 5; //<-- Change this to increase transaction limit
        const int reward = 6; ///<-- Change this to increase reward



        public LinkedList<Block> Blockchain { get {return _BlockChain;} } 

        public AdvancedBlockchainHighLevel(string MinerName, int Difficulty)
        {
            _BlockChain = new LinkedList<Block>();
            _Transactions = new List<string>();
            _MinerName = MinerName;
            _Difficulty = Difficulty;
            _BlockChain.AddLast(BlockchainLowLevel.CreateGenesisBlock());
        }

        public void AddTransaction(string Transaction)
        {
            _Transactions.Add(Transaction);
            if (_Transactions.Count == 4)
                {

                //Add the reward block as the last transaction
                _Transactions.Add("Reward " + _MinerName + " " + reward.ToString() + "BTC");

                //Set up the values to send to FindHash
                //Flatten the transactions
                string PreHashedTransactions = string.Join(Environment.NewLine,_Transactions);
                Block PreviousBlock = _BlockChain.Last.Value;

                _BlockChain.AddLast(FindHash(PreHashedTransactions,PreviousBlock));

                }

                
        }

        private Block FindHash(string Transactions, Block PreviousBlock)
        {
            int nonce = 0;
            bool hashFound = false;

            
            while ((hashFound != true) && (nonce != int.MaxValue))
            {
                //This should proably be stringbuilder, but it's for simplification
                Transactions = Transactions + nonce.ToString();
                //generate the block
                byte[] BlockHash = BlockchainLowLevel.HashBlock(PreviousBlock.BlockHash, Transactions);

                //Always increment the nonce;
                nonce++;
            }

            return(new Block(""));

        }

        //private void TestForZeros()

        private void AddBlock(string DataToAdd)
        {
            //Block NewBlock = new Block(_BlockChain.Last(), DataToAdd);
            //_BlockChain.AddLast(NewBlock);
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

        //TODO : Account for checking the last item in the linked list.
        public bool VerifyBlock(Block BlockToVerify)
        {

            //Find the next block in the chain
            //This will fail if it's the last block
            LinkedListNode<Block> NextBlockNode = _BlockChain.Find(BlockToVerify).Next;

            return BlockchainLowLevel.VerifyBlock(BlockToVerify, NextBlockNode.Value);            
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
            if (VerifyBlock(NodeToCheck.Value) == true)
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
