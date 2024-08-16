using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PasswordlessIdentityServer.Models;
using Rsk.AspNetCore.Fido;

namespace PasswordlessIdentityServer.Pages.Account.Register;

[AllowAnonymous]
public class IndexModel(UserManager<ApplicationUser> userManager, IFidoAuthentication fidoAuthentication) : PageModel
{
    [Required]
    [EmailAddress]
    [Display(Name = "Email")]
    [BindProperty]
    public string Email { get; set; }

    [Required]
    [BindProperty]
    [Display(Name = "Device Name")]
    public string DeviceName { get; set; }

    [BindProperty(SupportsGet = true)] 
    public String ReturnUrl { get; set; }


    public IActionResult OnGet()
    {
        return Page();
    }

    public async Task<IActionResult> OnPost()
    {
        if (await userManager.FindByEmailAsync(Email) != null)
        {
            return BadRequest("A user with that username already exists.");
        }

        var challenge = await fidoAuthentication.InitiateRegistration(Email, DeviceName);
        var encoded = challenge.ToBase64Dto();

        return RedirectToPage("Register", new
        {
            challenge.UserId,
            encoded.Base64UserHandle,
            encoded.Base64Challenge,
            encoded.DeviceFriendlyName,
            encoded.Base64ExcludedKeyIds,
            encoded.RelyingPartyId,
            ReturnUrl
        });
    }
}