using System;

namespace PokeTyper
{
	public class API
	{
		public static bool TryConvertStringToType(string type, out TypeToken token)
		{
			type = type.ToLower();
			foreach (TypeToken t in Enum.GetValues(typeof(TypeToken)))
			{
				if (type.Equals(t.ToString().ToLower()))
				{
					token = t;
					return true;
				}
			}
			// Invalid input
			token = TypeToken.Normal; // TODO: Create TypeToken.None, and use it here.
			return false;
		}

		public static LocPokemonType MakeType(params TypeToken[] types)
		{
			return LocPokemonType.MakeType(types);
		}

		public static Coverage MakeCoverage(params TypeToken[] types)
		{
			return Coverage.MakeCoverage(types);
		}
	}
}
