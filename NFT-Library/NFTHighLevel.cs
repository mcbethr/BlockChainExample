using BlockchainLibrary;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace NFT_Library
{
    public static class NFTHighLevel
    {

        public static string MintNFT(string From, string Action, string To, string Filename)
        {
   
            //Grab the file into a byte array
            byte[] byteArray = File.ReadAllBytes(Filename);

            byte[] NFTHash = SHA1.HashData(byteArray);

            //Create the NFT
            NFT MyNFT = new NFT(From, Action, To, NFTHash);

            return MyNFT.TransactionString;
        }

    }
}
