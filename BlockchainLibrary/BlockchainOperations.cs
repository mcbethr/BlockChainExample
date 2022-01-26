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

        public static byte[] HashBlock(byte[] PreviousHash, string BlockData)
        {
            string PreviousBlockHashAsString = BitConverter.ToString(PreviousHash);
            byte[] GenesisHash = MD5.HashData(Encoding.ASCII.GetBytes(PreviousHash+BlockData));
            return GenesisHash;
        }

        public static bool VerifyBlock(Block BlockToVerify, Block NextBlock)
        {
            
            byte[] BlockToVerifyHash = BlockToVerify.Hash;

            byte[] HashOfNextBlock = HashBlock(BlockToVerifyHash, NextBlock.Data);

            if (BlockToVerifyHash == HashOfNextBlock)
                return true;
            else
                return false;

        }

    }
}
