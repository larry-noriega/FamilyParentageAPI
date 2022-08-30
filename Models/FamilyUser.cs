using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Bson.Serialization.Attributes;

namespace FamilyParentageApi.Models;

public class Parentage
{
  public string PARENTESCO { get; private set; }
  public string IDFAMILIAR { get; private set; }

  [BsonConstructor]
  public Parentage(string idFamiliar, string parentesco)
  {
    IDFAMILIAR = idFamiliar;
    PARENTESCO = parentesco;
  }

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

  [BsonExtraElements]
  public Parentage FAMILIA { get; set; } = null!;
}