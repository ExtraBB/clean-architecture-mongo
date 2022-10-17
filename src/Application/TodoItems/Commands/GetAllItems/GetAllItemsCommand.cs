using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Events;
using MediatR;

namespace CleanArchitecture.Application.TodoItems.Commands.GetAllItems;

public record GetAllItemsCommand : IRequest<List<TodoItem>>
{
}

public class GetAllItemsCommandHandler : IRequestHandler<GetAllItemsCommand, List<TodoItem>>
{
    private readonly IApplicationDbContext _context;

    public GetAllItemsCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<TodoItem>> Handle(GetAllItemsCommand request, CancellationToken cancellationToken)
    {
        return await _context.TodoItems.FindAsync(item => true);
    }
}
