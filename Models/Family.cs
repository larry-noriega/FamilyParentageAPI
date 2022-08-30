using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FamilyParentageApi.Models;

public class Family
{
  [BsonId]
  [BsonRepresentation(BsonType.ObjectId)]
  public string? Id { get; set; }
  public string IDFAMILIAR { get; set; } = null!;

  [BsonElement("NOMBRE")]
  [JsonPropertyName("NOMBRE")]
  public string FamilyName { get; set; } = null!;

}