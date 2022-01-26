using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BlockchainLibrary;
using BlockchainLibrary.ChainOperations;

namespace BlockchainOperationsUnitTests
{
    [TestClass]
    public class BlockchainEngineUnitTests
    {
        [TestMethod]
        public void EngineTestCreateGenesisBlock()
        {
            BlockchainEngine BE = new BlockchainEngine();
            Assert.AreEqual("GenesisBlock", BE.Blockchain.First.Value.Data);
        }

        [TestMethod]
        public void EngineTestVerifySecondBlock()
        {
            //TODO Takeover here
        }

    }
}
