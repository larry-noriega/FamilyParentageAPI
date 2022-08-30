using FamilyParentageApi.Models;
using FamilyParentageApi.Services;
using FamilyParentageApi.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace FamilyParentageApi.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class FamilyUserController : ControllerBase
{
  private readonly FamilyUserParentageService _familyUserService;

  public FamilyUserController(FamilyUserParentageService familyService) =>
      _familyUserService = familyService;

  [HttpGet]
  public async Task<List<FamilyUser>> Get() =>
      await _familyUserService.GetAsync();

  [HttpGet("{id:length(24)}")]
  public async Task<ActionResult<FamilyUser>> Get(string id)
  {
    var familyUser = await _familyUserService.GetAsync(id);

    if (familyUser is null)
    {
      return NotFound();
    }

    return familyUser;
  }

  [HttpPost]
  public async Task<IActionResult> Post(FamilyUser newFamilyUser)
  {
    await _familyUserService.CreateAsync(newFamilyUser);

    return CreatedAtAction(nameof(Get), new { id = newFamilyUser.Id}, newFamilyUser);
  }

  [HttpPut("{id:length(24)}")]
  public async Task<IActionResult> Update(string id, FamilyUser updatedFamilyUser)
  {
    var familyUser = await _familyUserService.GetAsync(id);

    if (familyUser is null)
    {
      return NotFound();
    }

    updatedFamilyUser.Id = familyUser.Id;

    await _familyUserService.UpdateAsync(id, updatedFamilyUser);

    return NoContent();
  }

  [HttpDelete("{id:length(24)}")]
  public async Task<IActionResult> Delete(string id)
  {
    var familyUser = await _familyUserService.GetAsync(id);

    if (familyUser is null)
    {
      return NotFound();
    }

    await _familyUserService.RemoveAsync(id);

    return NoContent();
  }
}
