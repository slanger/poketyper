using System;

namespace PokeTyper
{
	public class API
	{
		public static PokemonType MakeType(params string[] types)
		{
			return MakeType(GetTokens(types));
		}

		public static PokemonType MakeType(params TypeToken[] types)
		{
			return PokemonType.MakeType(types);
		}

		public static Coverage MakeCoverage(params string[] types)
		{
			return MakeCoverage(GetTokens(types));
		}

		public static Coverage MakeCoverage(params TypeToken[] types)
		{
			return Coverage.MakeCoverage(types);
		}

		private static TypeToken[] GetTokens(string[] types)
		{
			var tokens = new TypeToken[types.Length];
			for (int i = 0; i < tokens.Length; i++)
			{
				tokens[i] = GetType(types[i]);
			}

			return tokens;
		}

		private static TypeToken GetType(string type)
		{
			type = type.ToLower();
			foreach (TypeToken t in Enum.GetValues(typeof(TypeToken)))
			{
				if (type.Equals(t.ToString().ToLower()))
				{
					return t;
				}
			}

			throw new ArgumentException("Invalid type string.");
		}
	}
}
