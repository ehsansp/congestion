using MediatR;

namespace CodePractice.Product.ApplicationService.Features.Person.Commands.CreatePerson;

public class CreateProductCommand : IRequest<long>
{
    public string Title { get; set; }
    public int Weight { get; set; }
    public decimal Price { get; set; }
    public int Inventory { get; set; }
}