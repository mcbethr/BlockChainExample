using BlockchainLibrary;
using System;
using System.IO;
using System.Text;

namespace NFT_Library
{
    public static class NFTHighLevel
    {

        public static string CreateNFT(string[] args)
        {
            //Find the file
            string Filename = args[3];

            //Grab the file into a byte array
            byte[] byteArray = File.ReadAllBytes(Filename);

            //Create the NFT
            NFT MyNFT = new NFT(args[0], args[1], args[2], byteArray);

            return MyNFT.TransactionString;
        }

    }
}
