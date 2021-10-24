using FluentValidation;
using System;
using System.Linq;

namespace LibraryApi.Models.Validators
{
    public class PublicationQueryValidator : AbstractValidator<PublicationQuery>
    {
        private int[] pageSizes =  { 5, 10, 15 };
        public PublicationQueryValidator()
        {
            RuleFor(r => r.PageNumber).GreaterThanOrEqualTo(1);
            RuleFor(r => r.PageSize).Custom((value, context) =>
            {
                if (!pageSizes.Contains(value))
                {
                    context.AddFailure("PageSize", $"PageSize must be in [{string.Join(",", pageSizes)}]");
                }
            });
        }
    }
}
