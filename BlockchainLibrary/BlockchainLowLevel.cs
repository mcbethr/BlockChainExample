using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace BlockchainLibrary.ChainOperations
{
    static internal class BlockchainLowLevel
    {
        const string GenesisBlockText = "GenesisBlock";

        public static Block CreateGenesisBlock()
        {
            byte[] PreviousBlockHash = BitConverter.GetBytes(0);
            byte[] Blockhash = HashBlock(BitConverter.GetBytes(0), GenesisBlockText, 0);
            Block GenesisBlock = new Block(Blockhash, PreviousBlockHash, GenesisBlockText, 0);

            return GenesisBlock;
        }

        public static byte[] HashBlock(byte[] PreviousBlockHash, string BlockData, int Nonce)
        {
            string PreviousHashString = BitConverter.ToString(PreviousBlockHash);
            string NonceString = Nonce.ToString();
            string CompleteBlock = PreviousHashString + NonceString + BlockData;

            byte[] NewBlockHash = SHA1.HashData(Encoding.ASCII.GetBytes(CompleteBlock));
            return NewBlockHash;
        }

        public static bool VerifyBlock(Block BlockUnderTest, Block PreviousBlock, Block NextBlock)
        {

            byte[] CalculatedHashOfBlockUnderTest = null;

            //if the Previous is Null, we are at the genesis block, 
            //so create a new one and comapre
            if (PreviousBlock == null)
            {
                Block NewBlock = CreateGenesisBlock();
                if (NewBlock.BlockHash.SequenceEqual(BlockUnderTest.BlockHash))
                    return true;
                else
                    return false;
            }
            
            // if the nextblock is null, we need to make sure the previous block matches the block to verify
            if (NextBlock == null) 
            {

                CalculatedHashOfBlockUnderTest = HashBlock(PreviousBlock.BlockHash, BlockUnderTest.Data, BlockUnderTest.Nonce);

                if (CalculatedHashOfBlockUnderTest.SequenceEqual(BlockUnderTest.BlockHash))
                    return true;
                else
                    return false;

            }

            //If we're in the middle of the block.

            //Hash the block under test
            CalculatedHashOfBlockUnderTest = HashBlock(PreviousBlock.BlockHash, BlockUnderTest.Data, BlockUnderTest.Nonce);

            //Use the result to hash the next block
            byte[] CalculatedNextBlockHash = HashBlock(CalculatedHashOfBlockUnderTest, NextBlock.Data, NextBlock.Nonce);

            //if the calculated value is equal to the stored value, the block has not been tampered with.
            if (CalculatedNextBlockHash.SequenceEqual(NextBlock.BlockHash))
                return true;
            else
                return false;
        }

    }
}
