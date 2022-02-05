using BlockchainLibrary;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace NFT_Library
{
    public static class NFTHighLevel
    {

        public static string MintNFT(string[] args)
        {
            //Find the file
            string Filename = args[3];

            //Grab the file into a byte array
            byte[] byteArray = File.ReadAllBytes(Filename);

            byte[] NFTHash = SHA1.HashData(byteArray);

            //Create the NFT
            NFT MyNFT = new NFT(args[0], args[1], args[2], NFTHash);

            return MyNFT.TransactionString;
        }

    }
}
