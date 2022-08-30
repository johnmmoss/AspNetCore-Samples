namespace AspNetWebOidcSample.Security;

public class AuthenticationSettings
{
    public static string OpenIdConnectAuthenticationScheme { get; } = "oidc";
    public static string CookieAuthenticationScheme { get; } = "cookie";

    public string Authority { get; set; }
    public string AuthorityClientId { get; set; }
    public string AuthoritySecret { get; set; }
    public bool RequiresHttps { get; set; }
    public string Audience { get; set; }
    public bool DangerousIgnoreUntrustedRootCertificateValidationErrors { get; set; }
}