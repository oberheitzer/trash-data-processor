namespace TDP.Main.Helpers;

public static class Validator
{
    public static bool IsValid(string? input)
        => !string.IsNullOrEmpty(input) && (input == Constant.ExitCommand || 
        input == Constant.ResetCommand || 
        input == Constant.UpdateCommand || 
        input.ToLower() == Constant.MenuCommand.ToLower());
}
