using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SemiParserClient
{
    public interface IValidator
    {
        bool ValidateVariables(string[] variables);
    }
}