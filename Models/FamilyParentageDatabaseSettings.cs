namespace FamilyParentageApi.Models;

public class FamilyParentageDatabaseSettings
{
  public string ConnectionString { get; set; } = null!;

  public string DatabaseName { get; set; } = null!;

  public string ParentageCollectionName { get; set; } = null!;
}
