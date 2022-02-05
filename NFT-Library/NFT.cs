using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFT_Library
{
    public class NFT
    {

        private string _TransactionString;

        public string TransactionString { get { return _TransactionString; } }

        public NFT(string From, string Action, string To, byte[] NFTImage)
        {

            _TransactionString = From +
                              Environment.NewLine +
                              Action +
                              Environment.NewLine +
                              To +
                              Environment.NewLine
                              +
                              BitConverter.ToString(NFTImage);
        }

        
        
    }
 }



