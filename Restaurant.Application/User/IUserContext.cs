namespace Restaurant.Application.User;

public interface IUserContext
{
    CurrentUser? GetCurrentUser();

}
