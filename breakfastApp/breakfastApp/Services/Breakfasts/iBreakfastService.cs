namespace breakfastApp.Services.Breakfasts;
using breakfastApp.Models;
using ErrorOr;
using breakfastApp.Services.breakfast;
public interface IBreakfastService
{
    ErrorOr<Created> CreateBreakfast(Breakfast request);
    ErrorOr<Breakfast> GetBreakfast(Guid id );
    ErrorOr<UpsertedBreakfast> UpsertBreakfast(Breakfast breakfast);
    ErrorOr<Deleted> DeleteBreakfast(Guid id);
}