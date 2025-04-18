using ContextEf;
using MediatR;

namespace Repository.Commands;

public record TestMediator() : IRequest<int>;

public class TestMediatorCommand : IRequestHandler<TestMediator, int>
{
    private readonly AppDbContext _context;

    public TestMediatorCommand(AppDbContext context)
    {
        _context = context;
    }


    public Task<int> Handle(TestMediator notification, CancellationToken cancellationToken)
    {
        return Task.FromResult(1);
    }
}

public class ServiceTestMediator
{
    private readonly IMediator _mediator;

    public ServiceTestMediator(IMediator mediator)
    {
        _mediator = mediator;
    }

    public Task<int> Go()
    {
        return _mediator.Send(new TestMediator());
    }
}