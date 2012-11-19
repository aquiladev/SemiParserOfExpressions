using System;

namespace SemiParserClient
{
    public class Validator : IValidator
    {
        public bool ValidateVariables(string[] variables)
        {
            foreach (var item in variables)
            {
                int itemTryParse;
                if (string.IsNullOrWhiteSpace(item) 
                    || !Int32.TryParse(item, out itemTryParse))
                    return false;
            }
            return true;
        }
    }
}