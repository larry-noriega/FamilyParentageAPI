// <snippet_UsingSystemTextJsonSerialization>
using System.Text.Json.Serialization;
// </snippet_UsingSystemTextJsonSerialization>
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

public class FamilyUser
{
  [BsonId]
  [BsonRepresentation(BsonType.ObjectId)]
  public string? Id { get; set; }
  public int USERID { get; set; }
  public string NOMBRE { get; set; } = null!;
  public string DIRECCION { get; set; } = null!;
  public int EDAD { get; set; }
  public string FAMILIA {get; set;} = null!;
  public string IDFAMILIAR { get; set; } = null!;
  public string PARENTESCO { get; set; } = null!;

  // [BsonConstructor]
  // public FamilyUser(string idFamiliar, string parentesco)
  // {
  //   IDFAMILIAR = idFamiliar;
  //   PARENTESCO = parentesco;
  // }
}   