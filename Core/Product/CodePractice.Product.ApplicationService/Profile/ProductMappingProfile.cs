using CodePractice.Product.ApplicationService.Features.Person.Commands.CreatePerson;

namespace CodePractice.Product.ApplicationService.Profile;

public class ProductMappingProfile : AutoMapper.Profile
{
    public ProductMappingProfile()
    {
        CreateMap<CreateProductCommand, CodePractice.Product.Domain.Entities.Product>().ReverseMap();
    }
}