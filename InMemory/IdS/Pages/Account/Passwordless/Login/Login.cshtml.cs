using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PasswordlessIdentityServer.Models;
using Rsk.AspNetCore.Fido;
using Rsk.AspNetCore.Fido.Dtos;

namespace PasswordlessIdentityServer.Pages.Account.Passwordless;

[AllowAnonymous]

public class LoginModel(IFidoAuthentication fidoAuthentication, 
    UserManager<ApplicationUser> userManager,
    SignInManager<ApplicationUser> signInManager) : PageModel
{
    [BindProperty(SupportsGet = true)] 
    public Base64FidoAuthenticationChallenge Challenge { get; set; } 
    
    [BindProperty(SupportsGet = true)] 
    public String ReturnUrl { get; set; }
    
    public IActionResult OnGet()
    {
        return Page();
    }

    public async Task<IActionResult> OnPost([FromBody] Base64FidoAuthenticationResponse authenticationResponse)
    {
        var result = await fidoAuthentication.CompleteAuthentication(authenticationResponse.ToFidoResponse()); 

        if (result.IsError) return BadRequest(result.ErrorDescription); 

        var user = await userManager.FindByEmailAsync(result.UserId); 
        
        if (user is null) 
        { 
            return BadRequest("No user exists with that id."); 
        }

        signInManager.SignInAsync(user, false);

        return new EmptyResult();
    }
}