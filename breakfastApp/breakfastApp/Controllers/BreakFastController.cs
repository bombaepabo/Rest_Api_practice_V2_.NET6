using Microsoft.AspNetCore.Mvc;
using breakfastApp.Contracts.breakfast;
using breakfastApp.Models;
using breakfastApp.Services.Breakfasts;
using ErrorOr;
using breakfastApp.ServiceErrors;
using breakfastApp.Services.breakfast;

namespace breakfastApp.Controllers;

public class BreakFastController : ApiController
{
    private readonly IBreakfastService _breakfastService;
    public BreakFastController(IBreakfastService breakfastService){
        _breakfastService = breakfastService ;
    }
    [HttpPost()]
    public IActionResult CreateBreakfast(CreateBreakFastRequest request){
       ErrorOr<Breakfast> requestToBreakfastResult = Breakfast.From(request);
        if(requestToBreakfastResult.IsError){
            return Problem(requestToBreakfastResult.Errors);
        }
        var breakfast = requestToBreakfastResult.Value;
           ErrorOr<Created> createBreakfastResult =  _breakfastService.CreateBreakfast(breakfast);
       return createBreakfastResult.Match(
        created => CreatedAsGetBreakfast(breakfast),
        errors => Problem(errors)
       );
    }
    [HttpGet("{id:guid}")]
    public IActionResult GetBreakfast(Guid id){
        ErrorOr<Breakfast> getBreakfastResult = _breakfastService.GetBreakfast(id);
        return getBreakfastResult.Match(
            breakfast =>Ok(MapBreakfastResponse(breakfast)),
            errors => Problem(errors));
       // if(getBreakfastResult.IsError && getBreakfastResult.FirstError == Errors.breakfast.NotFound){
       //     return NotFound();
        //}
       // var breakfast = getBreakfastResult.Value;
        //BreakfastResponse response = MapBreakfastResponse(breakfast);
        //return Ok(response);
    }
    
    [HttpPut("{id:guid}")]
    public IActionResult UpsertBreakfast(Guid id,UpsertBreakfastRequest request){
         ErrorOr<Breakfast> requestToBreakfastResult = Breakfast.From(id,request);
           
          
            if(requestToBreakfastResult.IsError){
            return Problem(requestToBreakfastResult.Errors);
        }
        var breakfast = requestToBreakfastResult.Value;
           ErrorOr<UpsertedBreakfast> upsertedresult =  _breakfastService.UpsertBreakfast(breakfast);
        return upsertedresult.Match(
            upserted => upserted.IsNewlyCreated ? CreatedAsGetBreakfast(breakfast) : NoContent(),
            errors => Problem(errors)
        );
    }
     [HttpDelete("{id:guid}")]
    public IActionResult DeleteBreakfast(Guid id){
       ErrorOr<Deleted> deleteResult=  _breakfastService.DeleteBreakfast(id);
        return deleteResult.Match(
            deleted => NoContent(),
            errors => Problem(errors)
        );
    }
    private static BreakfastResponse MapBreakfastResponse(Breakfast breakfast){
        return   new BreakfastResponse(
            breakfast.Id,
            breakfast.Name,
            breakfast.Description,
            breakfast.StartDateTime,
            breakfast.EndDateTime,
            breakfast.LastModifiedDateTime,
            breakfast.Savory,
            breakfast.Sweet
            );
    }
    private CreatedAtActionResult CreatedAsGetBreakfast(Breakfast breakfast){
        return CreatedAtAction(
            actionName: nameof(GetBreakfast),
            routeValues: new {Id = breakfast.Id},
            value: breakfast
            );
    }
}