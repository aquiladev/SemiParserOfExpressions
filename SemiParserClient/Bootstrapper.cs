using SemiParser;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SemiParserClient
{
    public class Bootstrapper
    {
        public static void Run()
        {
            ObjectFactory.Initialize(x =>
            {
                x.For<Parser<Variable[]>>().Use<ExpressionSemiParser>();
                x.For<IValidator>().Use<Validator>();
            });
        }
    }
}