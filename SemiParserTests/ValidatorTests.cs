using NUnit.Framework;
using SemiParserClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemiParserClientTests
{
    public class ValidatorTests
    {
        [Test]
        [TestCase("")]
        [TestCase("12.12aaa")]
        [TestCase("0.2")]
        [TestCase("0,2")]
        [TestCase("8463253847625746359846598376")]
        [TestCase("s")]
        public void ValidateVariables_IfIncorrectItem_CheckResult(string item)
        {
            //act
            var result = new Validator().ValidateVariables(new[] { item });

            //assert
            Assert.IsFalse(result);
        }

        [Test]
        [TestCase("0")]
        [TestCase("21")]
        [TestCase("-12")]
        [TestCase("32767672")]
        public void ValidateVariables_IfCorrectItem_CheckResult(string item)
        {
            //act
            var result = new Validator().ValidateVariables(new[] { item });

            //assert
            Assert.IsTrue(result);
        }
    }
}
