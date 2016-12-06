using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentSystem.Models
{
    public static class TagTransformer
    {
        public static string Transform(string tag)
        {
            bool startsWithPound = tag.StartsWith("#");
            bool containsSpace = tag.Contains(" ");
            bool tagLengthIsValid = tag.Length <= 20;

            if (!startsWithPound)
            {
                tag = string.Concat("#", tag);
            }

            if (containsSpace)
            {
                tag = tag.Replace(" ", "");
            }

            if (!tagLengthIsValid)
            {
                tag = tag.Substring(0, 20);
            }

            return tag;
        }
    }
}
