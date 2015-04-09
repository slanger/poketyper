
namespace PokeTyper
{
    using System;
    using System.Collections.Generic;

    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Welcome to PokeTyper!");
            Console.WriteLine("Enter a type (or types) to see resistances, weaknesses, and immunities.");
            Console.WriteLine("Example: Grass Poison");

            while (true)
            {
                Console.Write("\n> ");
                string commandLine = Console.ReadLine().ToLower();

                if (commandLine.Equals(string.Empty))
                {
                    continue;
                }
                else if (commandLine.Equals("exit"))
                {
                    break;
                }
                else if (commandLine.Equals("best"))
                {
                    int numTypes = 0;
                    int maxNumResistances = -1;
                    int numTypesWithMaxResistances = -1;
                    int typesLength = Enum.GetValues(typeof(Types)).Length;
                    int numResistances;
                    Type t;

                    for (int i = 0; i < typesLength; i++)
                    {
                        Types type1 = (Types)i;
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
                            Types type2 = (Types)j;
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

                    continue;
                }

                string[] commands = commandLine.Split(' ');
                if (commands.Length <= 0 || commands.Length > 5)
                {
                    Console.WriteLine("Command is invalid.");
                    continue;
                }

                if (commands[0].Equals("resist"))
                {
                    if (commands.Length != 2)
                    {
                        Console.WriteLine("Invalid command.");
                        continue;
                    }

                    uint numDesiredResistances;
                    try
                    {
                        numDesiredResistances = uint.Parse(commands[1]);
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Argument must be a nonnegative integer.");
                        continue;
                    }
                    catch (OverflowException)
                    {
                        Console.WriteLine("Argument too large.");
                        continue;
                    }

                    List<Type> types = new List<Type>();
                    int typesLength = Enum.GetValues(typeof(Types)).Length;
                    int numResistances;
                    Type t;
                    for (int i = 0; i < typesLength; i++)
                    {
                        Types type1 = (Types)i;
                        t = PokeTyperMethods.MakeType(type1);
                        numResistances = t.Resist2x.Length + t.Resist4x.Length + t.Immune.Length;
                        if (numResistances == numDesiredResistances)
                        {
                            types.Add(t);
                        }

                        for (int j = i + 1; j < typesLength; j++)
                        {
                            Types type2 = (Types)j;
                            t = PokeTyperMethods.MakeType(type1, type2);
                            numResistances = t.Resist2x.Length + t.Resist4x.Length + t.Immune.Length;
                            if (numResistances == numDesiredResistances)
                            {
                                types.Add(t);
                            }
                        }
                    }

                    Console.WriteLine("Number of types with {0} resistances: {1}", numDesiredResistances, types.Count);
                    foreach (Type type in types)
                    {
                        Console.WriteLine(type.ToString() + "\n");
                    }
                }
                else if (commands[0].Equals("type"))
                {
                    try
                    {
                        Type type;
                        if (commands.Length == 2)
                        {
                            type = PokeTyperMethods.MakeType(commands[1]);
                        }
                        else if (commands.Length == 3)
                        {
                            type = PokeTyperMethods.MakeType(commands[1], commands[2]);
                        }
                        else
                        {
                            Console.WriteLine("Invalid command.");
                            continue;
                        }

                        Console.WriteLine(type.ToString());
                    }
                    catch (ArgumentException)
                    {
                        Console.WriteLine("Invalid type.");
                    }
                }
                else if (commands[0].Equals("coverage"))
                {
                    try
                    {
                        Coverage coverage;
                        if (commands.Length == 2)
                        {
                            coverage = PokeTyperMethods.MakeCoverage(commands[1]);
                        }
                        else if (commands.Length == 3)
                        {
                            coverage = PokeTyperMethods.MakeCoverage(commands[1], commands[2]);
                        }
                        else if (commands.Length == 4)
                        {
                            coverage = PokeTyperMethods.MakeCoverage(commands[1], commands[2], commands[3]);
                        }
                        else if (commands.Length == 5)
                        {
                            coverage = PokeTyperMethods.MakeCoverage(commands[1], commands[2], commands[3], commands[4]);
                        }
                        else
                        {
                            Console.WriteLine("Invalid command.");
                            continue;
                        }

                        Console.WriteLine(coverage.ToString());
                    }
                    catch (ArgumentException)
                    {
                        Console.WriteLine("Invalid type.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid command.");
                }
            }
        }
    }
}
