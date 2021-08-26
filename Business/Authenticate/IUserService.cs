using apiMensaje.Entities.Authenticate;

namespace apiMensaje.Business
{
    public interface IUserService
    {
        AuthenticateResponse Authenticate(AuthenticateRequest model);
    }
}