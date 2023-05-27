using AutoMapper;
using CodePractice.Product.Repository.GenericRepository.UnitOfWork;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace CodePractice.Product.ApplicationService.Features.Person.Commands.CreatePerson;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, long>
{
    private readonly IMapper _mapper;
    private readonly IProductUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _contextAccessor;

    public CreateProductCommandHandler(IMapper mapper, IProductUnitOfWork unitOfWork, IHttpContextAccessor contextAccessor)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _contextAccessor = contextAccessor;
    }
    public async Task<long> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var existst = _unitOfWork.ProductRepository.GetCount(x => x.Title == request.Title);
        if (existst > 0)
        {
            throw new Exception("عنوان محصول تکراری است.");
        }
        var mapped = _mapper.Map<Domain.Entities.Product>(request);
        _unitOfWork.ProductRepository.Create(mapped);
        var res = await _unitOfWork.Save(cancellationToken);
        return mapped.ProductId;
    }
}