namespace TDP.Main.Helpers;

/// <summary>
/// Handles the interaction during the running of the application.
/// </summary>
public static class Handler
{
    public static void Handle(int command)
    {
        switch (command)
        {
            case 1:
                Update();
                break;
            case 2:
                Reset();
                break;
        }
    }

    private static void Reset()
    {

    }

    private static void Update()
    {

    }
}
