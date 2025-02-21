using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Restaurant.Domain.Exceptions;


namespace Restaurant.Application.User.Commands;

public class UpdateUserDetailsCommandHandler(ILogger<UpdateUserDetailsCommandHandler> _logger,
    IUserContext _iUserContext , IUserStore<Domain.Entities.User> userStore) : IRequestHandler<UpdateUserDetailsCommand>
{
    public async Task Handle(UpdateUserDetailsCommand request, CancellationToken cancellationToken)
    {

        var user = _iUserContext.GetCurrentUser();
        _logger.LogInformation("Updating user: {UserId}, with {@Request}",user!.Id , request);

        var dbUser =await userStore.FindByIdAsync(user!.Id, cancellationToken)
            ?? throw new NotFoundException("not exist ");

        dbUser.Nationality = request.Nationality;
        dbUser.DateOfBirth = request.DateOfBirth;

        await userStore.UpdateAsync(dbUser,cancellationToken);


    }
}
