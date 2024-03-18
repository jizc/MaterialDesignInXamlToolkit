using System.CommandLine;
using System.CommandLine.Parsing;

namespace MaterialDesign.Shared;

internal static class CommandLineOptions
{
    private static readonly Option<string> PageOption =
        new(aliases: new[] {"--page", "-p"},
            getDefaultValue: () => "Home",
            description: "Sets the startup page of the Demo app.");

    private static readonly Option<FlowDirection> FlowDirectionOption =
        new(aliases: new[] { "--flowDirection", "-f" },
            getDefaultValue: () => FlowDirection.LeftToRight,
            description: "Sets the startup flow direction of the Demo app.");

    private static readonly RootCommand RootCommand =
        new(description: "MaterialDesignInXamlToolkit Demo app command line options.")
        {
            PageOption,
            FlowDirectionOption
        };

    public static (string? StartPage, FlowDirection FlowDirection) ParseCommandLine(string[] args)
    {
        ParseResult parseResult = RootCommand.Parse(args);

        return new(
            parseResult.GetValueForOption(PageOption),
            parseResult.GetValueForOption(FlowDirectionOption)
        );
    }
}
