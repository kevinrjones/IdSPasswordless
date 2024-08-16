using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PasswordlessIdentityServer.Models;
using Rsk.AspNetCore.Fido;
using Rsk.AspNetCore.Fido.Dtos;

namespace PasswordlessIdentityServer.Pages.Account.Register;

[AllowAnonymous]
public class Register(IFidoAuthentication fidoAuthentication, UserManager<ApplicationUser> userManager) : PageModel
{
    [BindProperty(SupportsGet = true)] 
    public Base64FidoRegistrationChallenge Challenge { get; set; }
    
    [BindProperty(SupportsGet = true)] 
    public String ReturnUrl { get; set; }

    public IActionResult OnGet()
    {
        return Page();
    }

    public async Task<IActionResult> OnPost([FromBody] Base64FidoRegistrationResponse response)
    {
        var result = await fidoAuthentication.CompleteRegistration(response.ToFidoResponse());

        if (result.IsError)
        {
            return BadRequest(result.ErrorDescription);
        }

        ApplicationUser user = new()
        {
            UserName = result.UserId,
            Email = result.UserId
        };

        var creationResult =  await userManager.CreateAsync(user);

        if (creationResult.Succeeded)
            return new EmptyResult();
        else 
            return BadRequest(String.Join(',', creationResult.Errors.Select(e => e.Description)));
    }
}