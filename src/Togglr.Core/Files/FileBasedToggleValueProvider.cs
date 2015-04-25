using System;
using System.IO;
using System.Linq;

namespace Togglr.Files
{
    public class FileBasedToggleValueProvider : IToggleValueProvider
    {
        private readonly string _filePath;

        public FileBasedToggleValueProvider(string filePath)
        {
            _filePath = filePath;
        }

        public IToggleValue Get(string id)
        {
            var content = File.ReadAllLines(_filePath);
            
            var parser = new FileValueParser();
            var toggleValues = parser.Parse(content);

            return toggleValues.FirstOrDefault(x => x.Name.Equals(id, StringComparison.InvariantCultureIgnoreCase));
        }
    }
}