using BlockchainLibrary.ChainOperations;
using System;
using System.Security.Cryptography;

namespace BlockchainLibrary
{
    public class Block
    {

        private byte[] _BlockHash;
        private byte[] _PreviousBlockHash;
        private string _Data;
        private int _nonce;

        public byte[] BlockHash { get { return _BlockHash; } }
        public string HashR { get { return BitConverter.ToString(_BlockHash); } }

        public byte[] PreviousBlockHash { get { return _PreviousBlockHash; } }
        public string PreviousHashR { get { return BitConverter.ToString(_PreviousBlockHash); } }

        public string Data { get { return _Data; } }

        /// <summary>
        /// Only used in the advanced example.
        /// </summary>
        public int Nonce { get { return _nonce; } }

        public Block(byte[] BlockHash, byte[] PreviousBlockHash, string Data, int Nonce = 0)
        {

            _BlockHash = BlockHash;
            _PreviousBlockHash = PreviousBlockHash;
            _Data = Data;
            _nonce = Nonce;
        }

    }
}
