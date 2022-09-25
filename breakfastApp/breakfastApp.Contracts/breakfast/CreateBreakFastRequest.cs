namespace breakfastApp.Contracts.breakfast{
 public record CreateBreakFastRequest(
    string Name,
    string Description,
    DateTime StartDateTime,
    DateTime EndDateTime,
    List<string> Savory,
    List<string>Sweet
    
 );
}