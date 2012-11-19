using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SemiParser;

namespace SemiParserTests
{
    public class ExtenderTests
    {
        [Test]
        [TestCase("|ueyrt|", "|*")]
        [TestCase("(ueyrt|qwqw)", "(*qwqw)")]
        public void ReplaceVariables(string expression, string expectExpressiont)
        {
            //act
            var result = expression.ReplaceVariables("*");

            //assert
            Assert.AreEqual(expectExpressiont, result);
        }

        [Test]
        [TestCase("|(sda)|", "|*|")]
        [TestCase("sadjh|(sda)|xc", "sadjh|*|xc")]
        public void ReplaceWrapExpression(string expression, string expectExpressiont)
        {
            //act
            var result = expression.ReplaceWrapExpression("*");

            //assert
            Assert.AreEqual(expectExpressiont, result);
        }

        [Test]
        [TestCase("(sda)", "*")]
        [TestCase("sadjh (sda) xc", "sadjh * xc")]
        public void ReplaceWrap(string expression, string expectExpressiont)
        {
            //act
            var result = expression.ReplaceWrap("*");

            //assert
            Assert.AreEqual(expectExpressiont, result);
        }

        [Test]
        [TestCase("( 214|", "(*|")]
        [TestCase("sadjh |122| xc", "sadjh |*| xc")]
        public void ReplaceConstants(string expression, string expectExpressiont)
        {
            //act
            var result = expression.ReplaceConstants("*");

            //assert
            Assert.AreEqual(expectExpressiont, result);
        }

        [Test]
        [TestCase("|sad(sd)|", "|*|")]
        [TestCase("(sad(87676)|", "(*|")]
        [TestCase("|sad(\"sf dsadsd\"))", "|*)")]
        public void ReplaceFunctions(string expression, string expectExpressiont)
        {
            //act
            var result = expression.ReplaceFunctions("*");

            //assert
            Assert.AreEqual(expectExpressiont, result);
        }

        [Test]
        [TestCase("sad(sd| dsf)", "sad(sd*")]
        [TestCase("sad(sd| 23412)", "sad(sd*")]
        [TestCase("sad(sd| \"sda sa\")", "sad(sd*")]
        public void ReplaceLastParameters(string expression, string expectExpressiont)
        {
            //act
            var result = expression.ReplaceLastParameters("*");

            //assert
            Assert.AreEqual(expectExpressiont, result);
        }

        [Test]
        [TestCase("sad(sd *dsf)", "sad(sd |dsf)")]
        [TestCase("sad(sd + dsf)", "sad(sd | dsf)")]
        [TestCase("sad(sd - dsf)", "sad(sd | dsf)")]
        [TestCase("sad(sd / dsf)", "sad(sd | dsf)")]
        public void ReplaceArithmeticActs(string expression, string expectExpressiont)
        {
            //act
            var result = expression.ReplaceArithmeticActs();

            //assert
            Assert.AreEqual(expectExpressiont, result);
        }

        [Test]
        [TestCase("sad ( sd *dsf )", "sad(sd*dsf)")]
        public void ReplaceSpace(string expression, string expectExpressiont)
        {
            //act
            var result = expression.ReplaceSpace();

            //assert
            Assert.AreEqual(expectExpressiont, result);
        }

        [Test]
        [TestCase("sad(sd*dsf)", "(sad(sd*dsf))")]
        [TestCase("(sad(sd*dsf)", "(sad(sd*dsf)")]
        [TestCase("sad(sd*dsf", "(sad(sd*dsf)")]
        public void AddBrackets(string expression, string expectExpressiont)
        {
            //act
            var result = expression.AddBrackets();

            //assert
            Assert.AreEqual(expectExpressiont, result);
        }

        [Test]
        [TestCase("sad(sd*dsf)", "(sad(sd|dsf))")]
        [TestCase("(sad(sd+dsf)", "(sad(sd|dsf)")]
        [TestCase("sad(sd-dsf", "(sad(sd|dsf)")]
        public void Prepare(string expression, string expectExpressiont)
        {
            //act
            var result = expression.Prepare();

            //assert
            Assert.AreEqual(expectExpressiont, result);
        }

        [Test]
        [TestCase("()", "")]
        [TestCase("(()", "(")]
        [TestCase("((()()())", "(")]
        [TestCase("((()()(((()))))", "(")]
        public void RemoveBrackets(string expression, string expectExpressiont)
        {
            //act
            var result = expression.RemoveBrackets();

            //assert
            Assert.AreEqual(expectExpressiont, result);
        }
    }
}
