namespace BugStore.Configuration;

public static class EnvironmentVariables
{
    private static IConfiguration? _configuration;
    public static ConnectionStrings? ConnectionStrings { get; set; }

    public static void Initialize(this IConfiguration configuration)
    {
        _configuration = configuration;
        ConnectionStrings = TryGetClass<ConnectionStrings>(new ConnectionStrings(), nameof(ConnectionStrings));
    }
    
    private static T TryGetClass<T>(T className, string argument) where T : class
    {
        try
        {   
            return _configuration?.GetSection(argument).Get<T>()!;
        }
        catch (ArgumentException ex)
        {
            throw new ArgumentException("");
        }
    }
}