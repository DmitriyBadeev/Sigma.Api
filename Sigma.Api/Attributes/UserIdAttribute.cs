using HotChocolate;

namespace Sigma.Api.Attributes
{
    public class UserIdAttribute : GlobalStateAttribute
    {
        public UserIdAttribute() : base("UserId") { }
    }
}