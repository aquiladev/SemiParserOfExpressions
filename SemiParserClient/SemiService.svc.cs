using SemiParser;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Text;

namespace SemiParserClient
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class SemiService : ISemiService
    {
        private Parser<Variable[]> _parser;
        private IValidator _validator;

        public SemiService()
            : this(ObjectFactory.GetInstance<Parser<Variable[]>>(), ObjectFactory.GetInstance<IValidator>())
        {
        }

        public SemiService(Parser<Variable[]> parser, IValidator validator)
        {
            _parser = parser;
            _validator = validator;
        }

        public Variable[] Parse(string expression)
        {
            return _parser.Parse(expression);
        }

        public bool Verify(string expression)
        {
            return _parser.Verify(expression);
        }

        public bool VerifyVariables(string[] variables)
        {
            return _validator.ValidateVariables(variables);
        }
    }
}
