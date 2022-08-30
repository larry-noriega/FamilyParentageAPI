// <snippet_UsingSystemTextJsonSerialization>
using System.Text.Json.Serialization;
// </snippet_UsingSystemTextJsonSerialization>
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FamilyParentageApi.Models;

public class Parentage
{
  private int PADRE;
  private int MADRE;
  private int HIJO;

  public Parentage(int Uno, int Dos, int Tres)
  {
    PADRE = Uno;
    MADRE = Dos;
    HIJO = Tres;
  }
}

public class Family
{
  [BsonId]
  [BsonRepresentation(BsonType.ObjectId)]
  public string? Id { get; set; }

  public string IDFAMILIAR { get; set; } = null!;

  [BsonElement("NOMBRE")]
  [JsonPropertyName("NOMBRE")]
  public string FamilyName { get; set; } = null!;

  List<Parentage> PARENTESCO = new List<Parentage>();
}


