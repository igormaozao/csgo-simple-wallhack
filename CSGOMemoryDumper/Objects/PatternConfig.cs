using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace CSGOMemoryDumper.Objects {
    public class PatternConfig {
        
        public string Name { get; set; }
        public int ExtraBytes { get; set; }
        public int PatternOffset { get; set; }
        public string Pattern { get; set; }

        public int[] GetPatternValues(int ignoredByteValue = 0x100) {
            var values = new List<int>();

            var regex = Regex.Matches(Pattern, "[\\w|\\d|\\?]*[^\\s|,]");

            regex.ToList().ForEach(v => {
                values.Add(v.Value == "?" ? ignoredByteValue : int.Parse(v.Value, System.Globalization.NumberStyles.HexNumber));
            });

            return values.ToArray();
        }
    }
}
