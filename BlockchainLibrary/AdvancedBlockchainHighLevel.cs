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
        const int transactionLimit = 5; //<-- Change this to increase transaction limit
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
            if (_Transactions.Count == (transactionLimit - 1))
                {

                //Add the reward transaction as the last transaction
                _Transactions.Add(_MinerName + " Rewards " + _MinerName + " " + reward.ToString() + "BTC");

                //Set up the values to send to FindHash
                //Flatten the transactions
                string PreHashedTransactions = string.Join(Environment.NewLine,_Transactions);
                Block PreviousBlock = _BlockChain.Last.Value;

                _BlockChain.AddLast(FindHashAndReturnBlock(PreHashedTransactions,PreviousBlock));
                _Transactions.Clear(); //Reset the transaction
                }

                
        }

        private Block FindHashAndReturnBlock(string Transactions, Block PreviousBlock)
        {
            int Nonce = 0;
            bool hashFound = false;
            byte[] CurrentBlockHash = new byte[16];


            while ((hashFound != true) && (Nonce != int.MaxValue))
            {
            
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

        /// <summary>
        /// Walks the hash and makes sure the hash has the appropriate zeros for the difficulty
        /// </summary>
        /// <param name="Hash"></param>
        /// <returns></returns>
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


        public bool VerifyBlock(Block BlockUnderTest, Block PreviousBlock, Block NextBlock)
        {
            byte[] PreviousHash = null;
            byte[] BlockUnderTestHash = null;
            string Transactions = null;

            //Do a special accounting for the Genesis Block
            if (PreviousBlock == null)
            {
                Block GenesisBlock = BlockchainLowLevel.CreateGenesisBlock();
                if (GenesisBlock.BlockHash.SequenceEqual(BlockUnderTest.BlockHash) == true)
                {
                    return true;
                }
                else 
                { 
                    return false;
                }
            }

            //Do special Accounting for the last block

            if (NextBlock == null)
            {

                PreviousHash = PreviousBlock.BlockHash;
                BlockUnderTestHash = BlockUnderTest.BlockHash;
                Transactions = BlockUnderTest.Data;
                int Nonce = BlockUnderTest.Nonce;

                byte[] CalculatedHash = BlockchainLowLevel.HashBlock(PreviousHash, Transactions, Nonce);
                if (CalculatedHash.SequenceEqual(BlockUnderTestHash) == true)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            //If we're in the middle of the block.

            PreviousHash = PreviousBlock.BlockHash;
            BlockUnderTestHash = BlockUnderTest.BlockHash;
            Transactions = BlockUnderTest.Data;

            //Hash the block under test
            byte[] CalculatedHashOfBlockUnderTest = BlockchainLowLevel.HashBlock(PreviousBlock.BlockHash, BlockUnderTest.Data, BlockUnderTest.Nonce);

            //Use the result to hash the next block
            byte[] CalculatedNextBlockHash = BlockchainLowLevel.HashBlock(CalculatedHashOfBlockUnderTest, NextBlock.Data, NextBlock.Nonce);

            //if the calculated value is equal to the stored value, the block has not been tampered with.
            if (CalculatedNextBlockHash.SequenceEqual(NextBlock.BlockHash))
                return true;
            else
                return false;


        }

        

    }
}
