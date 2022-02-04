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
        static int _Difficulty;
        const int tranactionLimit = 5; //<-- Change this to increase transaction limit
        const int reward = 6; ///<-- Change this to increase reward



        public LinkedList<Block> Blockchain { get {return _BlockChain;} } 

        public AdvancedBlockchainHighLevel(string MinerName, int Difficulty = 1)
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

                //Add the reward transaction as the last transaction
                _Transactions.Add(_MinerName + " Rewards " + "_MinerName + " + reward.ToString() + "BTC");

                //Set up the values to send to FindHash
                //Flatten the transactions
                string PreHashedTransactions = string.Join(Environment.NewLine,_Transactions);
                Block PreviousBlock = _BlockChain.Last.Value;

                _BlockChain.AddLast(FindHash(PreHashedTransactions,PreviousBlock));
                _Transactions = new List<string>(); //Reset the transaction
                }

                
        }

        private Block FindHash(string Transactions, Block PreviousBlock)
        {
            int Nonce = 0;
            bool hashFound = false;
            byte[] CurrentBlockHash = new byte[16];


            while ((hashFound != true) && (Nonce != int.MaxValue))
            {
                //This should proably be stringbuilder, but we're going for simplification
                //generate the block
                CurrentBlockHash = BlockchainLowLevel.HashBlock(PreviousBlock.BlockHash, Transactions, Nonce);

                if (TestForZeros(CurrentBlockHash) == true)
                {
                    Block FoundBlock = new Block(CurrentBlockHash, PreviousBlock.BlockHash, Transactions, Nonce);
                    return FoundBlock;
                }

                //Always increment the nonce;
                Nonce++;
            }

            //Will have to come up with a more graceful way of returning a failure.
            return null;

        }

        private bool TestForZeros(byte[] Hash)
        {
            byte Zero = 0;
            byte[] ZeroByteArray = new byte[_Difficulty];
            Buffer.BlockCopy(Hash, 0, ZeroByteArray, 0, _Difficulty);
            

            foreach (var item in ZeroByteArray)
            {
                if (item != Zero)
                {
                    return false;
                }
                ;
            }
            return true;
            

        }

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

     
        /// <summary>
        /// This is different than the basic blockchain code
        /// Here we are verifying that the Previous block hash
        /// the nonce and the data all match what is expected
        /// </summary>
        /// <param name="BlockToVerify"></param>
        /// <returns></returns>
        
        
        public bool VerifyBlock(Block BlockToVerify, Block PreviousBlock)
        {

                //Do a special accounting for the Genesis Block
                if (PreviousBlock == null)
                {
                    Block GenesisBlock = BlockchainLowLevel.CreateGenesisBlock();
                    if (GenesisBlock.BlockHash.SequenceEqual(BlockToVerify.BlockHash) == true)
                    {
                    return true;
                    }
                }
                

                byte[] PreviousHash = PreviousBlock.BlockHash;
                byte[] CurrentHash = BlockToVerify.BlockHash;
                string Transactions = BlockToVerify.Data;
                int Nonce = BlockToVerify.Nonce;

                byte[] CalculatedHash = BlockchainLowLevel.HashBlock(PreviousHash, Transactions, Nonce);
                if (CalculatedHash.SequenceEqual(CurrentHash) == true)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            
                      
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

            Block PreviousBlock = null;

            LinkedListNode<Block> StartNode = _BlockChain.Find(StartBlock);

            //if nothing exists, we're at the end of the block.
            if (StartNode.Next == null)
            {
                return null;
            }

            ///If the Previous Value of the start node is
            ///null, then we are at the beginning of 
            ///the blockchain and should make the "previous"
            ///block the Genesis block
            if (StartNode.Previous == null)
            {
                PreviousBlock = null;
            }
            else
            {
                PreviousBlock = StartNode.Previous.Value;
            }

            ///If the node is verified, then move to the next node
            ///else, return the bad block. Yes we're using recursion
            if (VerifyBlock(StartNode.Value, PreviousBlock) == true)
            {
                VerifyBlocks(StartNode.Next.Value);
            }
            else
            {
                return StartNode.Next.Value;
            }

            //If we reached here, The entire blockchain is validated.
            return null;

        }
        

    }
}
