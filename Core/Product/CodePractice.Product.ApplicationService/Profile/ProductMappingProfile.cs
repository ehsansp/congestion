using CodePractice.Product.ApplicationService.Features.Person.Commands.CreatePerson;
using CodePractice.Product.ApplicationService.Features.Person.Queries.List;

namespace CodePractice.Product.ApplicationService.Profile;

public class ProductMappingProfile : AutoMapper.Profile
{
    public ProductMappingProfile()
    {
        CreateMap<CreateProductCommand, CodePractice.Product.Domain.Entities.Product>().ReverseMap();
        CreateMap<GetProductsQuery, CodePractice.Product.Domain.Entities.Product>().ReverseMap();
    }
}