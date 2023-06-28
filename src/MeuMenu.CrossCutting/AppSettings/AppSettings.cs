namespace MeuMenu.CrossCutting.AppSettings;

public class AppSettings
{
    public ConnectionStringAppSettings? ConnectionString { get; set; }
    public LoginAppSettings? Login { get; set; }
    public JwtAppSettings? Jwt { get; set; }
    public ApplicationInsightsAppSettings? ApplicationInsights { get; set; }
    public bool EhAmbienteDeProducao { get; set; }
}