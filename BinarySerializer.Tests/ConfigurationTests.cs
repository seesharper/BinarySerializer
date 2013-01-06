using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BinarySerializer.Tests
{
    using System.Configuration;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ConfigurationTests
    {
        [TestMethod]
        public void GetSection()
        {
            var t = new BinarySerializer.Configuration.SerializerOptions();            
            var test = ConfigurationManager.GetSection("binarySerializer");            
        }
    }
}
