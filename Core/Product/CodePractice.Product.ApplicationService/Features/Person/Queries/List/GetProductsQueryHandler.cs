using AutoMapper;
using CodePractice.Framework.Dto.ProductService;
using CodePractice.Product.Repository.GenericRepository.UnitOfWork;
using MediatR;
using System.Globalization;

namespace CodePractice.Product.ApplicationService.Features.Person.Queries.List;

public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, (List<Domain.Entities.Product> list, int count)>
{
    private readonly IMapper _mapper;
    private readonly IProductUnitOfWork _unitOfWork;

    public GetProductsQueryHandler(IMapper mapper, IProductUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    public async Task<(List<Domain.Entities.Product> list, int count)> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        // changed : city id included - area id included - mapping moved inside query - to paged list
        var entities = _unitOfWork.ProductRepository
            .FindAll();
        
        if (request.Title != null)
        {
            entities = entities.Where(c => c.Title.Contains(request.Title));
        }
        var total = entities.Count();

        return (entities.ToList(), total);
    }
}