using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NUnit.Framework;

namespace Intech.Business.Tests
{
    [TestFixture]
    class FagTests
    {
        [Test]
        public void TestEssai()
        {
            //Arrange
            int x1 = 1, x2 = 3, sum;
            //Act
            sum = x1 + x2;
            //Assert
            Assert.That(sum > x1 && sum > x2, "heu, problème");
        }
    }
}
