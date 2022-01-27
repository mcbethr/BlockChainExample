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

        public byte[] Hash { get { return _Hash; } }
        public string HashR { get { return BitConverter.ToString(Hash); } }

        public byte[] PreviousHash { get { return _PreviousHash; } }
        public string PreviousHashR { get { return BitConverter.ToString(PreviousHash); } }

        public string Data { get { return _Data; } }

        //Create Genesis Block
        public Block(string Data) 
        {
            _Data = Data;
            _Hash = BlockchainLowLevel.CreateGenesisBlock(Data);
            _PreviousHash = Hash;
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
        }

    }
}
