using Microsoft.Extensions.DependencyInjection;
using TDP.Domain.Model;
using TDP.Extractor.Interfaces;
using TDP.Http.Interfaces;
using TDP.Main.Helpers;

var p = Starter.Build();
var ws = p.GetRequiredService<IWasteService>();
var cs = p.GetRequiredService<ICalendarService>();
var ds = p.GetRequiredService<IDatabaseService>();

// await ws.DownloadAsync();
// var r = cs.Read(@"/Users/bertalandavid/Documents/projects/trash-data-processor/Calendars/Gardony_XVI.pdf", []);

foreach (List<Collection> collections in cs.Read(areas: await ds.GetAreasAsync()))
{
    cs.Write(collections);
}

// cs.Write(r);

// await ds.GetAreasAsync();

// var url = Environment.GetEnvironmentVariable("SUPABASE_URL");
// Console.WriteLine(url);
// Console.WriteLine(Environment.GetEnvironmentVariable("SUPABASE_KEY"));

Communicator.Introduce();
Communicator.DisplayMenu();

string? input = Console.ReadLine();

do
{
    if (Validator.IsValid(input: input))
    {
        if (input != null && input.ToLower() == Constant.MenuCommand.ToLower())
        {
            Communicator.DisplayMenu();
        }
        else if (input != null && input == Constant.ExitCommand)
        {
            continue;
        }
        else
        {
            if (input != null && input == Constant.ResetCommand)
            {
                Communicator.Confirm(message: Constant.DatabaseResetWarning);
                string? confirmation = Console.ReadLine();
                if (confirmation != null && confirmation.ToLower() == Constant.Yes.ToLower())
                {
                    Handler.Handle(command: int.Parse(input!));
                }
                else if (confirmation != null && confirmation.ToLower() == Constant.No.ToLower())
                {
                    Communicator.ShowCancelMessage();
                }
            }
            else 
            {
                Handler.Handle(command: int.Parse(input!));
            }
            Communicator.ShowDoneMessage();
        }
    }
    else
    {
        Communicator.ShowInvalidMessage(input: input);
    }
    input = Console.ReadLine();
} while (input != null && input != Constant.ExitCommand);

Communicator.SoLong();
