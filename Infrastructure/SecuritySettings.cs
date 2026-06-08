namespace Infrastructure;

public class SecuritySettings
{
    public string jwtSecretKey { get; set; } = string.Empty;
    public string secretSalt { get; set; } = string.Empty;
}
