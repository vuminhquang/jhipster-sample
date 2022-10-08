using JhipsterSample.Crosscutting.Constants;

namespace JhipsterSample.Crosscutting.Exceptions;

public class LoginAlreadyUsedException : BadRequestAlertException
{
    public LoginAlreadyUsedException() : base(ErrorConstants.LoginAlreadyUsedType, "Login name is already in use!",
        "userManagement", "userexists")
    {
    }
}
