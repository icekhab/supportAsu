using System;
using System.Collections.Generic;
using System.Web;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Infrastructure;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json;
using UserManagment.Managers;
using SupportAsu.Model;

namespace SupportAsu.Identity
{
    public class ApplicationOAuthProvider : OAuthAuthorizationServerProvider
    {
        private readonly string _publicClientId;

        public ApplicationOAuthProvider(string publicClientId)
        {
            if (publicClientId == null)
            {
                throw new ArgumentNullException("publicClientId");
            }

            _publicClientId = publicClientId;
        }

        public override async System.Threading.Tasks.Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            try
            {
                var userManager = (IUserManager)DependencyResolver.Current.GetService(typeof(IUserManager));
                string messageValidate = string.Empty;
                if (!userManager.ValidateUser(context.UserName, context.Password, out messageValidate))
                {
                    context.SetError("invalid_grant", messageValidate);
                    return;
                }

                var user = userManager.GetUser(context.UserName);

                //User user = new User
                //{
                //    Email = "lol@gmail.com",
                //    Login = "lol",
                //    Name = "Olol Lol Ololosha",
                //    Phone = "0734188552"
                //};

                //if (user == null)
                //{
                //    context.SetError("invalid_grant", "The user name or password is incorrect.");
                //    return;
                //}

                //if (user.LockoutEnabled || user.State == EnumPersonState.Deleted)
                //{
                //    context.SetError("access_denied", "The user does not have access to the system");
                //    return;
                //}

                var oAuthIdentity = user.GenerateUserIdentity();

                var cookiesIdentity = user.GenerateClaimsIdentityApplicationCookie();

                var co = new CookieOptions { HttpOnly = true, Expires = DateTime.UtcNow.AddDays(90) };

                if (HttpContext.Current != null && HttpContext.Current.Request.IsSecureConnection)
                {
                    co.Secure = true;
                }

                var properties = CreateProperties(user);
                var ticket = new AuthenticationTicket(oAuthIdentity, properties);


                context.Validated(ticket);
                context.Request.Context.Authentication.SignIn(cookiesIdentity);
            }
            catch (Exception ex)
            {
                throw new Exception("Ошибка вычисления прав доступа");
            }
        }

        public override System.Threading.Tasks.Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }

            return System.Threading.Tasks.Task.FromResult<object>(null);
        }

        public override System.Threading.Tasks.Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            // Resource owner password credentials does not provide a client ID.
            if (context.ClientId == null)
            {
                context.Validated();
            }

            return System.Threading.Tasks.Task.FromResult<object>(null);
        }

        public override System.Threading.Tasks.Task ValidateClientRedirectUri(OAuthValidateClientRedirectUriContext context)
        {
            if (context.ClientId == _publicClientId)
            {
                Uri expectedRootUri = new Uri(context.Request.Uri, "/");

                if (expectedRootUri.AbsoluteUri == context.RedirectUri)
                {
                    context.Validated();
                }
            }

            return System.Threading.Tasks.Task.FromResult<object>(null);
        }

        public static AuthenticationProperties CreateProperties(Model.User user)
        {
            IDictionary<string, string> data = new Dictionary<string, string>
            {
                   { "login", user.Login },
                   { "name", user.Name },
                   { "role", user.Role },
                   { "user", JsonConvert.SerializeObject(user) }

            };
            return new AuthenticationProperties(data);
        }

    }
    /// <summary>
    /// Refresh token provider
    /// </summary>
    public class ApplicationRefreshTokenProvider : AuthenticationTokenProvider
    {
        public override void Create(AuthenticationTokenCreateContext context)
        {

            int expire = 60;
            context.Ticket.Properties.ExpiresUtc = new DateTimeOffset(DateTime.Now.AddMinutes(expire));
            context.SetToken(context.SerializeTicket());
        }

        public override void Receive(AuthenticationTokenReceiveContext context)
        {
            context.DeserializeTicket(context.Token);
        }
    }
}

