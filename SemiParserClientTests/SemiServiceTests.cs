using NUnit.Framework;
using Rhino.Mocks;
using SemiParser;
using SemiParserClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemiParserClientTests
{
    public class SemiServiceTests
    {
        private Parser<Variable[]> _parser;
        private IValidator _validator;

        [SetUp]
        public void Setup()
        {
            _parser = MockRepository.GenerateStub<Parser<Variable[]>>();
            _validator = MockRepository.GenerateStub<IValidator>();
        }

        private SemiService CreateService()
        {
            return new SemiService(_parser, _validator);
        }
        
        [Test]
        public void Verify_CalledParser()
        {
            //arrange
            string expression = "bla-bla-bla";
            _parser = MockRepository.GenerateStrictMock<Parser<Variable[]>>();
            _parser.Expect(x=>x.Verify(expression)).Return(false);

            //act
            CreateService().Verify(expression);

            //assert
            _parser.VerifyAllExpectations();
        }

        [Test]
        public void Parse_CalledParser()
        {
            //arrange
            string expression = "bla-bla-bla";
            _parser = MockRepository.GenerateStrictMock<Parser<Variable[]>>();
            _parser.Expect(x => x.Parse(expression)).Return(null);

            //act
            CreateService().Parse(expression);

            //assert
            _parser.VerifyAllExpectations();
        }

        [Test]
        public void ValidateVariables_CalledValidator()
        {
            //arrange
            string[] variables = { "12", "1223" };
            _validator = MockRepository.GenerateStrictMock<IValidator>();
            _validator.Expect(x => x.ValidateVariables(variables)).Return(false);

            //act
            CreateService().VerifyVariables(variables);

            //assert
            _validator.VerifyAllExpectations();
        }
    }
}
