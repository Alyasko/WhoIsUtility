using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WhoIsUtility.Core;

namespace WhoIsUtility.Tests
{
    [TestClass]
    public class WhoIsTests
    {
        [TestMethod]
        public void TestHostInfo()
        {
            IWhoIs whoIs = new WhoIs();
            var resp = whoIs.GetHostInfo("google.com").Result;

            Assert.IsTrue(resp.Length > 0);
        }
    }
}
