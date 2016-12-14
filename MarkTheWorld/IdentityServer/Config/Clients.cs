namespace IdentityServer.Config
{
    using IdentityServer3.Core.Models;
    using System.Collections.Generic;

    public static class Clients
    {

        private static readonly string ClientAdress = "http://localhost:5045/";

        public static IEnumerable<Client> Get()
        {
            return new[]
             {
                new Client
                {
                    Enabled = true,
                    ClientName = "JS Client",
                    ClientId = "js",
                    Flow = Flows.Implicit,

                    RedirectUris = new List<string>
                    {
                        "http://localhost:56668/popup.html"
                    },

                    AllowedCorsOrigins = new List<string>
                    {
                        "http://localhost:56668"
                    },

                    PostLogoutRedirectUris = new List<string>
                    {
                        "http://localhost:56668/index.html"
                    },


                    AllowAccessToAllScopes = true
                }
            };
        }
    }
}