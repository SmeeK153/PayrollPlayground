using API.Controllers.Companies.v1.Responses;
using Domain.Entities.Dependents;

namespace API.Extensions
{
    public static partial class DependentExtensions
    {
        public static DependentSummary ToSummary(this Dependent dependent)
        {
            return new DependentSummary
            {
                Id = dependent.Id,
                Name = dependent.Person.Name.ToString()
            };
        }
    }
}