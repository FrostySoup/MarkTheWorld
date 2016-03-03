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
                status = HttpStatusCode.BadRequest;
                message = "Couldn't find user with such login";
            }else if (userMiniModel.message == message2.PassMissmatch)
            {
                status = HttpStatusCode.BadRequest;
                message = "Password didn't match";
            }
            else if (userMiniModel.message == message2.AlreadyMarked)
            {
                status = HttpStatusCode.BadRequest;
                message = "You already marked this spot";
            }
            else if (userMiniModel.message == message2.UserNameTaken)
            {
                status = HttpStatusCode.BadRequest;
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
