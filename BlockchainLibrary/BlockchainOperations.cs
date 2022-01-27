using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace BlockchainLibrary.ChainOperations
{
    internal class BlockchainOperations
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

        public static bool VerifyBlock(Block BlockToVerify, Block NextBlock)
        {
            
            byte[] BlockToVerifyHash = BlockToVerify.Hash;
            
            byte[] CalculatedHashOfNextBlock = HashBlock(BlockToVerifyHash, NextBlock.Data);

            if (CalculatedHashOfNextBlock.SequenceEqual(NextBlock.Hash))
                return true;
            else
                return false;

        }

    }
}
