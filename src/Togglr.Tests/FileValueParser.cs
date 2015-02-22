using System.Collections.Generic;

namespace Togglr.Tests
{
    public class FileValueParser
    {
        public IEnumerable<ToggleValue> Parse(IEnumerable<string> content)
        {
            foreach (var input in content)
            {
                ToggleValue result;

                if (ToggleValue.TryParse(input, out result))
                {
                    yield return result;
                }
            }
        }
    }
}