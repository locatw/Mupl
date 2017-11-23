using System.Collections.Generic;

namespace Mupl.Dlna
{
    public class Filter
    {
        private List<string> values = new List<string>();

        public Filter(IEnumerable<string> values)
        {
            this.values.AddRange(values);
        }

        public override string ToString()
        {
            return string.Join(",", values);
        }

        public static Filter All
        {
            get
            {
                return new Filter(new string[] { "*" });
            }
        }

    }
}
