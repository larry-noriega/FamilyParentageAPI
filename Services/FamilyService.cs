using FamilyParentageApi.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace FamilyParentageApi.Services;

public class FamilyParentageService
{
  private readonly IMongoCollection<Family> _familyCollection;

  public FamilyParentageService(
    IOptions<FamilyParentageDatabaseSettings> familyParentageDatabaseSettings)
  {
    var mongoClient = new MongoClient(
        familyParentageDatabaseSettings.Value.ConnectionString);

    var mongoDatabase = mongoClient.GetDatabase(
        familyParentageDatabaseSettings.Value.DatabaseName);

    _familyCollection = mongoDatabase.GetCollection<Family>(
        familyParentageDatabaseSettings.Value.ParentageCollectionName);
  }

  public async Task<List<Family>> GetAsync() =>
      await _familyCollection.Find(_ => true).ToListAsync();
  public async Task<Family?> GetAsync(string id) =>
      await _familyCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

  public async Task CreateAsync(Family newFamily) =>
      await _familyCollection.InsertOneAsync(newFamily);

  public async Task UpdateAsync(string id, Family updatedFamily) =>
      await _familyCollection.ReplaceOneAsync(x => x.Id == id, updatedFamily);

  public async Task RemoveAsync(string id) =>
      await _familyCollection.DeleteOneAsync(x => x.Id == id);
}