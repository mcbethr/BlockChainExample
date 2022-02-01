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
            Block GenesisBlock = new Block(GenesisBlockText);

            return GenesisBlock;
        }

        public static byte[] CreateGenesisBlock(string GenesisBlockData)
        {
            return HashGenesisBlock( GenesisBlockData);

        }
        private static byte[] HashGenesisBlock(string GenesisBlockData)
        {
            byte[] GenesisHash = MD5.HashData(Encoding.ASCII.GetBytes(GenesisBlockData));
            return GenesisHash;
        }

        public static byte[] HashBlock(byte[] Hash, string BlockData)
        {
            string HashString = BitConverter.ToString(Hash);
            byte[] NewBlockHash = MD5.HashData(Encoding.ASCII.GetBytes(HashString + BlockData));
            return NewBlockHash;
        }

        public static byte[] HashBlock(byte[] Hash, string BlockData, int Nonce)
        {
            string HashString = BitConverter.ToString(Hash);
            string NonceString = Nonce.ToString();
            byte[] NewBlockHash = MD5.HashData(Encoding.ASCII.GetBytes(HashString + NonceString +BlockData));
            return NewBlockHash;
        }

        public static bool VerifyBlock(Block BlockToVerify, Block NextBlock)
        {
            
            byte[] BlockToVerifyHash = BlockToVerify.BlockHash;
            
            byte[] CalculatedHashOfNextBlock = HashBlock(BlockToVerifyHash, NextBlock.Data);

            if (CalculatedHashOfNextBlock.SequenceEqual(NextBlock.BlockHash))
                return true;
            else
                return false;

        }

    }
}
