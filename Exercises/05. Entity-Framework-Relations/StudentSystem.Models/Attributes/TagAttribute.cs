namespace StudentSystem.Models.Attributes
{
    using System.ComponentModel.DataAnnotations;

    public class TagAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            string tag = value.ToString();
            bool startsWithPound = tag.StartsWith("#");
            bool containsSpace = tag.Contains(" ");
            bool tagLengthIsNoMoreThan20 = tag.Length <= 20;


            return startsWithPound && !containsSpace && tagLengthIsNoMoreThan20;
        }
    }
}
