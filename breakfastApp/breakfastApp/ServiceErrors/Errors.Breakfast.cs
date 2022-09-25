namespace breakfastApp.ServiceErrors;
using ErrorOr ;
public static class Errors
{
    public static class breakfast{
        public static Error NotFound => Error.NotFound(
            code:"Breakfast.NotFound",
            description:"Breakfast not Found");
        public static Error InvalidName => Error.Validation(
            code:"Breakfast.InvalidName",
            description:$"Breakfast name must be at least {Models.Breakfast.MinNameLength}"+
            $"character long at most {Models.Breakfast.MaxNameLength} character ");
             public static Error InvalidDescription => Error.Validation(
            code:"Breakfast.InvalidName",
            description:$"Description must be at least {Models.Breakfast.MindescriptionLength}"+
            $"character long at most {Models.Breakfast.MaxdescriptionLength} character ");
    }
}