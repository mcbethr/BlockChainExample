using BlockchainLibrary;
using System;
using System.IO;
using System.Text;

namespace NFT_Library
{
    public class NFTHighLevel
    {

        static Block CreateNFT(string[] args)
        {
            //Find the file
            string Filename = args[3];

            //Grab the file into a byte array
            byte[] byteArray = File.ReadAllBytes(Filename);

            //Create the NFT
            NFT MyNFT = new NFT(args[0], args[1], args[2], byteArray);

            //Pleace the NFT in a block
            Block NFTblock = new Block(MyNFT.TransactionString);
            return NFTblock;
        }

    }
}
