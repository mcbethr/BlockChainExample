using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace BlockchainLibrary.ChainOperations
{
    internal class BlockchainLowLevel
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
            string HashString = BitConverter.ToString(PreviousBlockHash);
            string NonceString = Nonce.ToString();
            byte[] NewBlockHash = MD5.HashData(Encoding.ASCII.GetBytes(HashString + NonceString +BlockData));
            return NewBlockHash;
        }

        public static bool VerifyBlock(Block BlockToVerify, Block NextBlock)
        {

            //if the NextBlock is Null, we are at the end of the list and can't verify
            //so just return true
            if (NextBlock == null)
                return true;

            byte[] BlockToVerifyHash = BlockToVerify.BlockHash;
            
            byte[] CalculatedHashOfNextBlock = HashBlock(BlockToVerifyHash, NextBlock.Data,0);

            if (CalculatedHashOfNextBlock.SequenceEqual(NextBlock.BlockHash))
                return true;
            else
                return false;

        }

    }
}
