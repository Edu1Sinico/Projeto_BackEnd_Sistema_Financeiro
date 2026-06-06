namespace Infrastructure;

public class SecuritySettings
{
    public string jwtSecretKey { get; set; } = String.Empty;
    public string secretSalt { get; set; } = String.Empty;
}