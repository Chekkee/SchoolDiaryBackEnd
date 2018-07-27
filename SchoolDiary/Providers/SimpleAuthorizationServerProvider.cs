﻿using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security.OAuth;
using SchoolDiary.Models;
using SchoolDiary.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Unity;

namespace SchoolDiary.Providers
{
    public class SimpleAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        private UnityContainer container;

        public SimpleAuthorizationServerProvider(UnityContainer container)
        {
            this.container = container;
        }

        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });

            IdentityUser user = null;
            IEnumerable<string> roles = null;
            IAuthRepository _repo = container.Resolve<IAuthRepository>();

            user = await _repo.FindUser(context.UserName, context.Password);

            if (user == null)
            {
                context.SetError("invalid_grant", "The user name or password is incorrect.");
                return;
            }

            roles = await _repo.FindRoles(user.Id);

            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            //identity.AddClaim(new Claim(ClaimTypes.Name, ((UserModel)user)?.FirstName));
            //identity.AddClaim(new Claim(ClaimTypes.Surname, ((UserModel)user)?.LastName));
            //identity.AddClaim(new Claim(ClaimTypes.Email, context.UserName));
            identity.AddClaim(new Claim("id", user.Id));
            identity.AddClaim(new Claim("firstName", ((UserModel)user).FirstName));
            identity.AddClaim(new Claim("lastName", ((UserModel)user).LastName));
            identity.AddClaim(new Claim("email", ((UserModel)user)?.Email));
            identity.AddClaim(new Claim(ClaimTypes.Role, string.Join(",", roles)));

            context.Validated(identity);
            _repo.Dispose();
        }

        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            Dictionary<string, string> additionalUserInfo = new Dictionary<string, string>();
            foreach (Claim claim in context.Identity.Claims)
            {
                context.AdditionalResponseParameters.Add(claim.Type.Split('/').Last(), claim.Value);
            }

            foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }

            return Task.FromResult<object>(null);
        }
    }
}