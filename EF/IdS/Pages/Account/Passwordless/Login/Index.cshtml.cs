using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Rsk.AspNetCore.Fido;

namespace PasswordlessIdentityServer.Pages.Account.Passwordless.Login;

[AllowAnonymous]
public class Index(IFidoAuthentication fidoAuthentication) : PageModel
{
    [BindProperty]
    [Required]
    [EmailAddress]
    public string EMailAddress { get; set; }
    
    [BindProperty(SupportsGet = true)] 
    public String ReturnUrl { get; set; }


    public IActionResult OnGet()
    {
        return Page();
    }

    public async Task<IActionResult> OnPost()
    {
        try
        {
            var challenge = await fidoAuthentication.InitiateAuthentication(EMailAddress);
            var dto = challenge.ToBase64Dto();

            return RedirectToPage("Login", new
            {
                dto.UserId,
                dto.Base64Challenge,
                dto.RelyingPartyId,
                dto.Base64KeyIds,
                ReturnUrl
            });
        }
        catch (PublicKeyCredentialException exception)
        {
            return BadRequest(exception.Message);
        }
    }
}
















