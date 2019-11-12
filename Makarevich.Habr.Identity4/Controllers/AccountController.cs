using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityModel;
using IdentityServer4.Events;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using IdentityServer4.Stores;
using IdentityServer4.Test;
using Makarevich.Habr.Identity4.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Makarevich.Habr.Identity4.Controllers {
    /// <summary>
    /// This sample controller implements a typical login/logout/provision workflow for local and external accounts.
    /// The login service encapsulates the interactions with the user data store. This data store is in-memory only and cannot be used for production!
    /// The interaction service provides a way for the UI to communicate with identityserver for validation and context retrieval
    /// </summary>
    [Produces("application/json")]
    [Route("api/account/")]
    [AllowAnonymous]
    public class AccountController : ControllerBase {
        private readonly TestUserStore _users;
        private readonly IIdentityServerInteractionService _interaction;
        private readonly IClientStore _clientStore;
        private readonly IAuthenticationSchemeProvider _schemeProvider;
        private readonly IEventService _events;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(
            IIdentityServerInteractionService interaction,
            IClientStore clientStore,
            IAuthenticationSchemeProvider schemeProvider,
            IEventService events,
            SignInManager<ApplicationUser> signInManager,
            TestUserStore users = null
        ) {
            // if the TestUserStore is not in DI, then we'll just use the global users collection
            // this is where you would plug in your own custom identity management library (e.g. ASP.NET Identity)
            _users = users ?? new TestUserStore(TestUsers.Users);

            _interaction = interaction;
            _clientStore = clientStore;
            _schemeProvider = schemeProvider;
            _events = events;
            _signInManager = signInManager;
        }

        /// <summary>
        /// Handle postback from username/password login
        /// </summary>
        [HttpPost("login")]
//        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model) {
            //            model.UserName = "alice";
            //            model.Password = "alice";
            //            model.Email = "alice@gmail.com";
            model.UserName = "makarevich";
            model.Password = "q26s4hcxz2332Q!";
            model.Email = "makarevich@gmail.com";
            // check if we are in the context of an authorization request
            var context = await _interaction.GetAuthorizationContextAsync(model.ReturnUrl);
//
//            // the user clicked the "cancel" button
//            if (button != "login") {
//                if (context != null) {
//                    // if the user cancels, send a result back into IdentityServer as if they 
//                    // denied the consent (even if this client does not require consent).
//                    // this will send back an access denied OIDC error response to the client.
//                    await _interaction.GrantConsentAsync(context, ConsentResponse.Denied);
//
//                    // we can trust model.ReturnUrl since GetAuthorizationContextAsync returned non-null
//                    if (await _clientStore.IsPkceClientAsync(context.ClientId)) {
//                        // if the client is PKCE then we assume it's native, so this change in how to
//                        // return the response is for better UX for the end user.
//                        return View("Redirect", new RedirectViewModel {RedirectUrl = model.ReturnUrl});
//                    }
//
//                    return Redirect(model.ReturnUrl);
//                }
//                else {
//                    // since we don't have a valid context, then we just go back to the home page
//                    return Redirect("~/");
//                }
//            }

            if (ModelState.IsValid) {
                // validate username/password against in-memory store
                if (_users.ValidateCredentials(model.UserName, model.Password)) {
                    var user = _users.FindByUsername(model.UserName);
                    await _events.RaiseAsync(new UserLoginSuccessEvent(user.Username, user.SubjectId, user.Username,
                        clientId: context?.ClientId));

                    // only set explicit expiration here if user chooses "remember me".
                    // otherwise we rely upon expiration configured in cookie middleware.
                    AuthenticationProperties props = null;
                    props = new AuthenticationProperties {
                        IsPersistent = true,
                        ExpiresUtc = DateTimeOffset.UtcNow.Add(new TimeSpan(10, 10, 10, 10))
                    };

                    // issue authentication cookie with subject ID and username
                    await HttpContext.SignInAsync(user.SubjectId, user.Username, props);
                    var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, true, lockoutOnFailure: false);

                    if (context != null) {
                        // we can trust model.ReturnUrl since GetAuthorizationContextAsync returned non-null
                        return Redirect(model.ReturnUrl);
                    }

                    // request for a local page
                    if (Url.IsLocalUrl(model.ReturnUrl)) {
                        return Redirect(model.ReturnUrl);
                    }
                    else if (string.IsNullOrEmpty(model.ReturnUrl)) {
                        return Redirect("~/");
                    }
                    else {
                        // user might have clicked on a malicious link - should be logged
                        throw new Exception("invalid return URL");
                    }
                }

                await _events.RaiseAsync(new UserLoginFailureEvent(model.UserName, "invalid credentials",
                    clientId: context?.ClientId));
//                ModelState.AddModelError(string.Empty, AccountOptions.InvalidCredentialsErrorMessage);
            }

            // something went wrong, show form with error
            //            var vm = await BuildLoginViewModelAsync(model);
            //            return Ok(vm);
            return Ok();
        }

        /// <summary>
        /// Handle logout page postback
        /// </summary>
        [HttpPost("account/logout")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout(dynamic model) {
            return Ok(model);
        }

    }
}