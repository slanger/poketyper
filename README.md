# PokeTyper

[PokeTyper](https://slanger.github.io/PokeTyper/) is a Pokemon **fan-made** website where you can
explore the different types and type combinations of Pokemon. You find the website at
<https://slanger.github.io/PokeTyper/>.

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

To build the code and run it locally, first install
[Visual Studio](https://visualstudio.microsoft.com/). Then, open up the
[Visual Studio solution file](src/PokeTyper.sln) and click the run button on the project you want
to run (either the [web app](src/PokeTyperWeb) or the [console app](src/PokeTyperConsole)).
