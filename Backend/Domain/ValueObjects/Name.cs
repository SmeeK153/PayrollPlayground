using System.Collections.Generic;
using Foundations.Core;

namespace Domain.ValueObjects
{
    public class Name : ValueObject
    {
        public string First { get; }
        public string Last { get; }

        public Name(string first, string last) => (First, Last) = (first, last);
        
        public override string ToString()
        {
            return $"{First} {Last}";
        }

        protected override IEnumerable<object> GetComponentValues()
        {
            yield return ToString();
        }
    }
}