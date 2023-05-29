using MediatR;
using System.Net.NetworkInformation;
using CodePractice.Framework.Dto;
using CodePractice.Framework.Dto.ProductService;

namespace CodePractice.Product.ApplicationService.Features.Person.Queries.List;

public class GetProductsQuery : Paging, IRequest<(List<Domain.Entities.Product> list, int count)>
{
    public string? Title { get; set; }
}

