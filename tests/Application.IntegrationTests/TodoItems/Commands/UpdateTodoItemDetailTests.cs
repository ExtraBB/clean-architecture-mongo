using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.TodoItems.Commands.CreateTodoItem;
using CleanArchitecture.Application.TodoItems.Commands.UpdateTodoItem;
using CleanArchitecture.Application.TodoItems.Commands.UpdateTodoItemDetail;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Enums;
using FluentAssertions;
using NUnit.Framework;

namespace CleanArchitecture.Application.IntegrationTests.TodoItems.Commands;

using static Testing;

public class UpdateTodoItemDetailTests : BaseTestFixture
{
    [Test]
    public async Task ShouldRequireValidTodoItemId()
    {
        var command = new UpdateTodoItemCommand { Id = "99", Title = "New Title" };
        await FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<NotFoundException>();
    }

    [Test]
    public async Task ShouldUpdateTodoItem()
    {
        var itemId = await SendAsync(new CreateTodoItemCommand
        {
            Title = "New Item"
        });

        var command = new UpdateTodoItemDetailCommand
        {
            Id = itemId,
            Note = "This is the note.",
            Priority = PriorityLevel.High
        };

        await SendAsync(command);

        var item = await FindAsync<TodoItem>(itemId);

        item.Should().NotBeNull();
        item.Note.Should().Be(command.Note);
        item.Priority.Should().Be(command.Priority);
        item.UpdatedAt.Should().BeCloseTo(DateTime.Now, TimeSpan.FromMilliseconds(10000));
    }
}
