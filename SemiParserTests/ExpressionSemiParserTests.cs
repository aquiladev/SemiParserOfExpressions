using NUnit.Framework;
using SemiParser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemiParserTests
{
    [TestFixture]
    public class ExpressionSemiParserTests
    {
        private ExpressionSemiParser CreateParser()
        {
            return new ExpressionSemiParser();
        }

        [Test]
        public void Parse_CheckResult()
        {
            //arrange 
            var exp = "x +z";

            //act
            var result = CreateParser().Parse(exp);

            //assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Length);
            Assert.AreEqual(1, result[0].Length);
            Assert.AreEqual(1, result[1].Length);
        }

        [Test]
        [TestCase("x +12.1")]
        [TestCase("x +12 1")]
        [TestCase("xs da+12")]
        [TestCase("xs+12)")]
        [TestCase("xs+as(as)wqe")]
        public void Verify_IncorrectExp_CheckResult(string exp)
        {
            //act
            var result = CreateParser().Verify(exp);

            //assert
            Assert.IsFalse(result);
        }

        [Test]
        [TestCase("x +132048")]
        [TestCase("(x +132048*sdsf)")]
        [TestCase("(x +test(132048,sdsf, \"sadfsaf\"))")]
        [TestCase("(x +1232-test(132048,sdsf, \"sadfsaf\")/1)")]
        [TestCase("x +test(  \"sadfsaf\"  )")]
        [TestCase("x +(asdf+sad)-sda+(qw-as)*df-ds(\"ddsf dsfs\")-sdf+(ds-12)")]
        [TestCase("(as-sdf+(ds-12-(asd-qwqe+sda-(12+qwwq-32*(we+wee)))))")]
        public void Verify_CorrectExp_CheckResult(string exp)
        {
            //act
            var result = CreateParser().Verify(exp);

            //assert
            Assert.IsTrue(result);
        }
    }
}
