using MediatR;

namespace Restaurant.Application.User.Commands;

public class UpdateUserDetailsCommand : IRequest
{
    public DateOnly? DateOfBirth { get; set; }
    public string? Nationality { get; set; }
}
