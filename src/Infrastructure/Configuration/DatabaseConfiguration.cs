namespace CleanArchitecture.Infrastructure.Configuration;
public class DatabaseConfiguration
{ 
    public string ConnectionString { get; set; } = null!;
    public string DatabaseName { get; set; } = null!;
    public string TodoItemsCollectionName { get; set; } = null!;
}
