using FamilyParentageApi.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace FamilyParentageApi.Services;

public class FamilyUserParentageService
{
  private readonly IMongoCollection<FamilyUser> _familyUserCollection;

  public FamilyUserParentageService(
    IOptions<FamilyUserParentageDatabaseSettings> familyUserParentageDatabaseSettings)
  {
    var mongoClient = new MongoClient(
      familyUserParentageDatabaseSettings.Value.ConnectionString);

    var mongoDatabase = mongoClient.GetDatabase(familyUserParentageDatabaseSettings.Value.DatabaseName);

    _familyUserCollection = mongoDatabase.GetCollection<FamilyUser>("ParentageUser");
  }

  public async Task<List<FamilyUser>> GetAsync() =>
      await _familyUserCollection.Find(_ => true).ToListAsync();
  public async Task<FamilyUser?> GetAsync(string id) =>
      await _familyUserCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

  public async Task CreateAsync(FamilyUser newFamilyUser) =>
      await _familyUserCollection.InsertOneAsync(newFamilyUser);

  public async Task UpdateAsync(string id, FamilyUser updatedFamilyUser) =>
      await _familyUserCollection.ReplaceOneAsync(x => x.Id == id, updatedFamilyUser);

  public async Task RemoveAsync(string id) =>
      await _familyUserCollection.DeleteOneAsync(x => x.Id == id);
}