namespace TDP.Main.Helpers;

/// <summary>
/// Displays messages in the console window.
/// </summary>
public static class Communicator
{
    /// <summary>
    /// Displays confirmation message.
    /// </summary>
    /// <param name="message">Short description of the action.</param>
    public static void Confirm(string message)
    {
        Console.WriteLine($"{message} Are you sure? (Y/N)");
    }

    /// <summary>
    /// Displays the menu options.
    /// </summary>
    public static void DisplayMenu()
    {
        Console.Write(Constant.UpdateCommandText.PadLeft(Constant.UpdateCommandText.Length + Constant.PadLeft));
        Console.WriteLine(Constant.UpdateCommand.PadLeft(Constant.PadBetween));

        Console.Write(Constant.ResetCommandText.PadLeft(Constant.ResetCommandText.Length + Constant.PadLeft));
        Console.WriteLine(Constant.ResetCommand.PadLeft(Constant.UpdateCommandText.Length + Constant.PadBetween - Constant.ResetCommandText.Length));

        Console.Write(Constant.ExitCommandText.PadLeft(Constant.ExitCommandText.Length + Constant.PadLeft));
        Console.WriteLine(Constant.ExitCommand.PadLeft(Constant.UpdateCommandText.Length + Constant.PadBetween - Constant.ExitCommandText.Length));
    }

    /// <summary>s
    /// Displays a short introduction text.
    /// </summary>
    public static void Introduce()
    {
        Console.WriteLine("\nWaste Data Processor.\n");
        Console.WriteLine("Please choose what you would like to do:\n");
    }

    /// <summary>
    /// Displays a message when the requested proccess is cancelled.
    /// </summary>
    public static void ShowCancelMessage()
    {
        Console.WriteLine("Cancelled. Anything else? You can also display the menu (M/m)");
    }

    /// <summary>
    /// Displays a message when the requested proccess is finished.
    /// </summary>
    public static void ShowDoneMessage()
    {
        Console.WriteLine("Done. Anything else? You can also display the menu (M/m)");
    }

    /// <summary>
    /// Displays a message when the input from console is invalid.
    /// </summary>
    /// <param name="input">Value from the console window.</param>
    public static void ShowInvalidMessage(string? input)
    {
        Console.WriteLine($"Your input ({input}) is not an available command. Please try again.");
    }

    /// <summary>
    /// Displays a farewell message.
    /// </summary>
    public static void SoLong()
    {
        Console.WriteLine("Alright. See you next time. Bye!");
    }
}
