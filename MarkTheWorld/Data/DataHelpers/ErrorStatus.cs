using System.Net;

namespace Data.DataHelpers
{
    public class ErrorStatus
    {
        public HttpStatusCode status { get; set; }
        public string message { get; set; }
        public ErrorStatus(UserRegistrationModel userMiniModel)
        {
            if (userMiniModel.message == message2.NoUserName)
            {
                status = HttpStatusCode.NotFound;
                message = "Couldn't find user with such login";
            }else if (userMiniModel.message == message2.PassMissmatch)
            {
                status = HttpStatusCode.NotFound;
                message = "Password didn't match";
            }
            else if (userMiniModel.message == message2.AlreadyMarked)
            {
                status = HttpStatusCode.Found;
                message = "You already marked this spot";
            }
            else if (userMiniModel.message == message2.UserNameTaken)
            {
                status = HttpStatusCode.Found;
                message = "Username already taken";
            }
            else
            {
                status = HttpStatusCode.OK;
                message = null;
            }

        }
    }
}
