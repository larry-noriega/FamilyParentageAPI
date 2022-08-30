using FamilyParentageApi.Models;
using FamilyParentageApi.Services;
using FamilyParentageApi.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace FamilyParentageApi.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class FamilyController : ControllerBase
{
  private readonly FamilyParentageService _familyService;

  public FamilyController(FamilyParentageService familyService) =>
      _familyService = familyService;

  [HttpGet]
  public async Task<List<Family>> Get() =>
      await _familyService.GetAsync();

  [HttpGet("{id:length(24)}")]
  public async Task<ActionResult<Family>> Get(string id)
  {
    var family = await _familyService.GetAsync(id);

    if (family is null)
    {
      return NotFound();
    }

    return family;
  }

  [HttpPost]
  public async Task<IActionResult> Post(Family newFamily)
  {
    await _familyService.CreateAsync(newFamily);

    return CreatedAtAction(nameof(Get), new { id = newFamily.Id}, newFamily);
  }

  [HttpPut("{id:length(24)}")]
  public async Task<IActionResult> Update(string id, Family updatedFamily)
  {
    var family = await _familyService.GetAsync(id);

    if (family is null)
    {
      return NotFound();
    }

    updatedFamily.Id = family.Id;

    await _familyService.UpdateAsync(id, updatedFamily);

    return NoContent();
  }

  [HttpDelete("{id:length(24)}")]
  public async Task<IActionResult> Delete(string id)
  {
    var family = await _familyService.GetAsync(id);

    if (family is null)
    {
      return NotFound();
    }

    await _familyService.RemoveAsync(id);

    return NoContent();
  }
}
