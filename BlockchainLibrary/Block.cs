using BlockchainLibrary.ChainOperations;
using System;
using System.Security.Cryptography;
namespace BlockchainLibrary
{
    public class Block
    {

        private byte[] _Hash;
        private byte[] _PreviousHash;
        private string _Data;
        private int _nonce;

        public byte[] Hash { get { return _Hash; } }
        public string HashR { get { return BitConverter.ToString(Hash); } }

        public byte[] PreviousHash { get { return _PreviousHash; } }
        public string PreviousHashR { get { return BitConverter.ToString(PreviousHash); } }

        public string Data { get { return _Data; } }

        /// <summary>
        /// Only used in the advanced example.
        /// </summary>
        public int Nonce { get { return _nonce; } }

        //Create Genesis Block
        public Block(string Data) 
        {
            _Data = Data;
            _Hash = BlockchainLowLevel.CreateGenesisBlock(Data);
            _PreviousHash = BitConverter.GetBytes(0);
        }

        public Block(Block PreviousBlockFromList, string Data)
        {

            GenerateNewBlock(PreviousBlockFromList.Hash, Data);

        }

        public void GenerateNewBlock(byte[] PreviousBlockHash, string Data)
        {
            _PreviousHash = PreviousBlockHash;
            _Hash = BlockchainLowLevel.HashBlock(PreviousBlockHash, Data);
            _Data = Data;
            _nonce = 0;
        }

    }
}
