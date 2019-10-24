using System;
using System.Collections.Generic;
using System.Text;
using PokeTyper;

namespace PokeTyperConsole
{
	public class Program
	{
		private const string ExitCommand = "exit";
		private const string HelpCommand = "help";
		private const string ErrorInvalidCommand = "Invalid command.";
		private const string ErrorInvalidType = "Invalid type.";

		private delegate void DoCommandDelegate(string[] arguments);

		private class CommandData
		{
			public DoCommandDelegate DoCommand { get; private set; }
			public string Arguments { get; private set; }
			public string Description { get; private set; }

			public CommandData(DoCommandDelegate doCommand, string arguments, string description)
			{
				this.DoCommand = doCommand;
				this.Arguments = arguments;
				this.Description = description;
			}
		}

		/// <summary>
		/// Keys represents available commands. Values are tuples of 2 strings. The first is the
		/// arguments of the command. The second is the description.
		/// </summary>
		private static readonly Dictionary<string, CommandData> Commands =
			new Dictionary<string, CommandData>
		{
			{ ExitCommand, new CommandData(null, string.Empty, "Exits the program.") },
			{ HelpCommand, new CommandData(DisplayHelp, string.Empty, "Shows the available commands and descriptions for those commands.") },
			{ "type", new CommandData(DisplayType, "<type1> [<type2>]", "Displays the weaknesses, resistances, and immunities of the type combination.") },
			{ "coverage", new CommandData(DisplayCoverage, "<type1> [<type2>] [<type3>] [<type4>]", "Displays the type coverage of the given attacking types.") },
			{ "resist", new CommandData(DisplayResistances, "<num_resistances>", "Displays all the types that have that many resistances (including immunities).") },
			{ "best", new CommandData(DisplayBest, string.Empty, "Displays the types that have that the most number of resistances (including immunities).") },
		};

		public static void Main(string[] args)
		{
			Console.WriteLine("Welcome to PokeTyper!");
			Console.WriteLine(string.Format("Enter \"{0}\" to see the available commands.", HelpCommand));

			while (true)
			{
				Console.Write("\n> ");
				var commandLine = Console.ReadLine().ToLower().Split(' ');
				var command = commandLine[0];

				if (command.Equals(string.Empty))
				{
					continue;
				}
				else if (command.Equals(ExitCommand))
				{
					break;
				}

				var arguments = new string[commandLine.Length - 1];
				Array.Copy(commandLine, 1, arguments, 0, arguments.Length);

				bool executedCommand = false;
				foreach (var cmd in Commands)
				{
					if (cmd.Key.Equals(command))
					{
						cmd.Value.DoCommand(arguments);
						executedCommand = true;

						break;
					}
				}

				if (!executedCommand)
				{
					Console.WriteLine(ErrorInvalidCommand);
				}
			}
		}

		private static void DisplayHelp(string[] arguments)
		{
			if (arguments.Length != 0)
			{
				Console.WriteLine(ErrorInvalidCommand);

				return;
			}

			var sb = new StringBuilder(
				"Available commands:\n",
				Commands.Count * 100); // ~100 chars per command and description

			foreach (var command in Commands)
			{
				sb.AppendFormat(
					"{0} {1}\n    {2}\n",
					command.Key,
					command.Value.Arguments,
					command.Value.Description);
			}

			Console.Write(sb);
		}

		private static void DisplayType(string[] arguments)
		{
			if (arguments.Length <= 0 || arguments.Length > 2)
			{
				Console.WriteLine(ErrorInvalidCommand);

				return;
			}

			try
			{
				var type = PokeTyperMethods.MakeType(arguments);
				Console.WriteLine(type.ToString());
			}
			catch (ArgumentException)
			{
				Console.WriteLine(ErrorInvalidType);
			}
		}

		private static void DisplayCoverage(string[] arguments)
		{
			if (arguments.Length <= 0 || arguments.Length > 4)
			{
				Console.WriteLine(ErrorInvalidCommand);

				return;
			}

			try
			{
				var coverage = PokeTyperMethods.MakeCoverage(arguments);
				Console.WriteLine(coverage.ToString());
			}
			catch (ArgumentException)
			{
				Console.WriteLine(ErrorInvalidType);
			}
		}

		private static void DisplayResistances(string[] arguments)
		{
			if (arguments.Length != 1)
			{
				Console.WriteLine(ErrorInvalidCommand);

				return;
			}

			uint numDesiredResistances = 0U;
			const string badArg = "Argument must be a non-negative integer.";
			try
			{
				bool parsed = uint.TryParse(arguments[0], out numDesiredResistances);
				if (!parsed)
				{
					Console.WriteLine(badArg);
				}
			}
			catch (ArgumentException ex)
			{
				Console.WriteLine(badArg + "\n" + ex);

				return;
			}

			var types = new List<PokemonType>();
			var typesLength = Enum.GetValues(typeof(TypeToken)).Length;
			int numResistances;
			PokemonType t;
			for (int i = 0; i < typesLength; i++)
			{
				var type1 = (TypeToken)i;
				t = PokeTyperMethods.MakeType(type1);
				numResistances = t.Resist2x.Length + t.Resist4x.Length + t.Immune.Length;
				if (numResistances == numDesiredResistances)
				{
					types.Add(t);
				}

				for (int j = i + 1; j < typesLength; j++)
				{
					var type2 = (TypeToken)j;
					t = PokeTyperMethods.MakeType(type1, type2);
					numResistances = t.Resist2x.Length + t.Resist4x.Length + t.Immune.Length;
					if (numResistances == numDesiredResistances)
					{
						types.Add(t);
					}
				}
			}

			Console.WriteLine("Number of types with {0} resistances: {1}", numDesiredResistances, types.Count);
			foreach (var type in types)
			{
				Console.WriteLine("\n" + type.ToString());
			}
		}

		public static void DisplayBest(string[] arguments)
		{
			if (arguments.Length != 0)
			{
				Console.WriteLine(ErrorInvalidCommand);

				return;
			}

			var typesLength = Enum.GetValues(typeof(TypeToken)).Length;
			int numTypes = 0;
			int maxNumResistances = -1;
			int numTypesWithMaxResistances = -1;
			int numResistances;
			PokemonType t;

			for (int i = 0; i < typesLength; i++)
			{
				var type1 = (TypeToken)i;
				numTypes++;
				t = PokeTyperMethods.MakeType(type1);
				numResistances = t.Resist2x.Length + t.Resist4x.Length + t.Immune.Length;
				if (numResistances > maxNumResistances)
				{
					maxNumResistances = numResistances;
					numTypesWithMaxResistances = 1;
				}
				else if (numResistances == maxNumResistances)
				{
					numTypesWithMaxResistances++;
				}

				for (int j = i + 1; j < typesLength; j++)
				{
					var type2 = (TypeToken)j;
					numTypes++;
					t = PokeTyperMethods.MakeType(type1, type2);
					numResistances = t.Resist2x.Length + t.Resist4x.Length + t.Immune.Length;
					if (numResistances > maxNumResistances)
					{
						maxNumResistances = numResistances;
						numTypesWithMaxResistances = 1;
					}
					else if (numResistances == maxNumResistances)
					{
						numTypesWithMaxResistances++;
					}
				}
			}

			if (maxNumResistances != -1)
			{
				Console.WriteLine("Number of types: " + numTypes);
				Console.WriteLine("Max number of resistances: " + maxNumResistances);
				Console.WriteLine("Number of types of max number of resistances: " + numTypesWithMaxResistances);
			}
		}
	}
}
