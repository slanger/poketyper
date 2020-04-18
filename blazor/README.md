# PokeTyper

[PokeTyper](https://slanger.github.io/poketyper/) is a Pokemon **fan-made** website where you can
explore the different types and type combinations of Pokemon. You find the website at
<https://slanger.github.io/poketyper/>

There are two pages in this website:

-   Types - Tells you the resistances and weaknesses of any Pokemon type or type combination.
-   Coverage - Tells you which types are "covered by", or weak to, a particular Pokemon move set.

## Code Structure

The code is written in C# and uses [Blazor WebAssembly](http://blazor.net) to run the code directly
in the browser. The code is broken up into three projects:

1. [PokeTyperWeb](src/PokeTyperWeb) - The code for the web app.

2. [PokeTyperConsole](src/PokeTyperConsole) - The code for the console (terminal/command prompt)
   app. This app is used for quick testing of the shared code.

3. [PokeTyper](src/PokeTyper) - The code shared between the above projects. This code contains most
   of the logic of working with Pokemon types.

## How to build the code and run locally

There are two ways to build and run PokeTyper locally: 1) with
[Visual Studio](https://visualstudio.microsoft.com/), and 2) with the `dotnet` command-line
interface (CLI) that comes with [.NET Core](https://dotnet.microsoft.com/).

### Build and run with Visual Studio

To build the code and run PokeTyper locally with Visual Studio, first download and install Visual
Studio 2019 from [here](https://visualstudio.microsoft.com/) (the Community version is free). Make
sure to select the "ASP.NET and web development" workload during the install so that
[.NET Core](https://dotnet.microsoft.com/) and [Blazor WebAssembly](http://blazor.net) are
installed. Then, open up the [Visual Studio solution file](src/PokeTyper.sln) and click the run
button on the project you want to run (either the [web app](src/PokeTyperWeb) or the
[console app](src/PokeTyperConsole)).

### Build and run with the `dotnet` CLI

To build the code and run PokeTyper locally with the `dotnet` command-line interface (CLI), first
download and install the latest version of .NET Core 3.1 from
[here](https://dotnet.microsoft.com/download/dotnet-core/3.1). Then open up the command
prompt/terminal and navigate to one of the project directories (either `src/PokeTyperWeb` for the
web app or `src/PokeTyperConsole` for the console app) and run `dotnet run` to run the project. For
the web app, after running `dotnet run`, open up a web browser and navigate to
http://localhost:65036/ to view the web app in the browser.
