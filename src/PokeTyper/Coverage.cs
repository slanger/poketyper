using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace PokeTyper
{
	public class Coverage : IComparable<Coverage>
	{
		private const string errorNotEnoughTypes =
			"This Coverage does not have enough PokemonTypes.";

		public readonly LocPokemonType[] Effective4x;
		public readonly LocPokemonType[] Effective2x;
		public readonly LocPokemonType[] Effective1x;
		public readonly LocPokemonType[] Effective05x;
		public readonly LocPokemonType[] Effective025x;
		public readonly LocPokemonType[] Effective0x;

		private readonly TypeToken[] types;
		private readonly string name;

		public TypeToken Type1
		{
			get
			{
				return this.types[0];
			}
		}

		public TypeToken Type2
		{
			get
			{
				if (this.NumTypes < 2)
				{
					throw new InvalidOperationException(errorNotEnoughTypes);
				}

				return this.types[1];
			}
		}

		public TypeToken Type3
		{
			get
			{
				if (this.NumTypes < 3)
				{
					throw new InvalidOperationException(errorNotEnoughTypes);
				}

				return this.types[2];
			}
		}

		public TypeToken Type4
		{
			get
			{
				if (this.NumTypes < 4)
				{
					throw new InvalidOperationException(errorNotEnoughTypes);
				}

				return this.types[3];
			}
		}

		public int NumTypes
		{
			get
			{
				return this.types.Length;
			}
		}

		public string Name
		{
			get
			{
				return this.name;
			}
		}

		private Coverage(
			LocPokemonType[] effective4x,
			LocPokemonType[] effective2x,
			LocPokemonType[] effective1x,
			LocPokemonType[] effective05x,
			LocPokemonType[] effective025x,
			LocPokemonType[] effective0x,
			params TypeToken[] types)
		{
			Debug.Assert(types.Length > 0 && types.Length <= 4);

			this.types = types;
			this.Effective4x = effective4x;
			this.Effective2x = effective2x;
			this.Effective1x = effective1x;
			this.Effective05x = effective05x;
			this.Effective025x = effective025x;
			this.Effective0x = effective0x;

			Array.Sort(this.types);
			Array.Sort(this.Effective4x);
			Array.Sort(this.Effective2x);
			Array.Sort(this.Effective1x);
			Array.Sort(this.Effective05x);
			Array.Sort(this.Effective025x);
			Array.Sort(this.Effective0x);

			var sb = new StringBuilder();
			const char separator = '/';
			foreach (var type in this.types)
			{
				sb.Append(type.ToString() + separator);
			}

			this.name = sb.ToString().TrimEnd(separator);
		}

		public int CompareTo(Coverage other)
		{
			if (other == null)
			{
				throw new ArgumentNullException();
			}

			// TODO: Find out if there is a .NET function that can do this comparison
			var lengthDiff = this.types.Length - other.types.Length;
			if (lengthDiff != 0)
			{
				return lengthDiff;
			}

			for (int i = 0; i < this.types.Length; i++)
			{
				var typeDiff = (int)this.types[i] - (int)other.types[i];
				if (typeDiff != 0)
				{
					return typeDiff;
				}
			}

			// If we made it here, they must be equal
			return 0;
		}

		public override bool Equals(object obj)
		{
			var other = obj as Coverage;
			if (other == null)
			{
				return false;
			}

			return this.types.SequenceEqual(other.types);
		}

		public override int GetHashCode()
		{
			return this.Name.GetHashCode();
		}

		public override string ToString()
		{
			var sb = new StringBuilder(this.Name);

			sb.Append("\nx4  ");
			foreach (var t in this.Effective4x)
			{
				sb.Append(" " + t.Name);
			}

			sb.Append("\nx2  ");
			foreach (var t in this.Effective2x)
			{
				sb.Append(" " + t.Name);
			}

			sb.Append("\nx1  ");
			foreach (var t in this.Effective1x)
			{
				sb.Append(" " + t.Name);
			}

			sb.Append("\nx1/2");
			foreach (var t in this.Effective05x)
			{
				sb.Append(" " + t.Name);
			}

			sb.Append("\nx1/4");
			foreach (var t in this.Effective025x)
			{
				sb.Append(" " + t.Name);
			}

			sb.Append("\nx0  ");
			foreach (var t in this.Effective0x)
			{
				sb.Append(" " + t.Name);
			}

			return sb.ToString();
		}

		internal static Coverage MakeCoverage(params TypeToken[] types)
		{
			Debug.Assert(types.Length > 0 && types.Length <= 4);

			var effective4x = new HashSet<LocPokemonType>();
			var effective2x = new HashSet<LocPokemonType>();
			var effective1x = new HashSet<LocPokemonType>();
			var effective05x = new HashSet<LocPokemonType>();
			var effective025x = new HashSet<LocPokemonType>();
			var effective0x = new HashSet<LocPokemonType>();

			var typesLength = Enum.GetValues(typeof(TypeToken)).Length;
			LocPokemonType t;
			for (int i = 0; i < typesLength; i++)
			{
				var t1 = (TypeToken)i;
				t = API.MakeType(t1);

				foreach (var type in types)
				{
					MakeCoverageHelper(
						type,
						t,
						effective4x,
						effective2x,
						effective1x,
						effective05x,
						effective025x,
						effective0x);
				}

				for (int j = i + 1; j < typesLength; j++)
				{
					var t2 = (TypeToken)j;
					t = API.MakeType(t1, t2);

					foreach (var type in types)
					{
						MakeCoverageHelper(
							type,
							t,
							effective4x,
							effective2x,
							effective1x,
							effective05x,
							effective025x,
							effective0x);
					}
				}
			}

			var arrEffective4x = new LocPokemonType[effective4x.Count];
			var arrEffective2x = new LocPokemonType[effective2x.Count];
			var arrEffective1x = new LocPokemonType[effective1x.Count];
			var arrEffective05x = new LocPokemonType[effective05x.Count];
			var arrEffective025x = new LocPokemonType[effective025x.Count];
			var arrEffective0x = new LocPokemonType[effective0x.Count];

			effective4x.CopyTo(arrEffective4x);
			effective2x.CopyTo(arrEffective2x);
			effective1x.CopyTo(arrEffective1x);
			effective05x.CopyTo(arrEffective05x);
			effective025x.CopyTo(arrEffective025x);
			effective0x.CopyTo(arrEffective0x);

			return new Coverage(
				arrEffective4x,
				arrEffective2x,
				arrEffective1x,
				arrEffective05x,
				arrEffective025x,
				arrEffective0x,
				types);
		}

		private static void MakeCoverageHelper(
			TypeToken attackingType,
			LocPokemonType defendingType,
			HashSet<LocPokemonType> effective4x,
			HashSet<LocPokemonType> effective2x,
			HashSet<LocPokemonType> effective1x,
			HashSet<LocPokemonType> effective05x,
			HashSet<LocPokemonType> effective025x,
			HashSet<LocPokemonType> effective0x)
		{
			if (effective4x.Contains(defendingType))
			{
				return;
			}

			foreach (var t in defendingType.WeakTo4x)
			{
				if (t == attackingType)
				{
					effective4x.Add(defendingType);
					effective2x.Remove(defendingType);
					effective1x.Remove(defendingType);
					effective05x.Remove(defendingType);
					effective025x.Remove(defendingType);
					effective0x.Remove(defendingType);

					return;
				}
			}

			if (effective2x.Contains(defendingType))
			{
				return;
			}

			foreach (var t in defendingType.WeakTo2x)
			{
				if (t == attackingType)
				{
					effective2x.Add(defendingType);
					effective1x.Remove(defendingType);
					effective05x.Remove(defendingType);
					effective025x.Remove(defendingType);
					effective0x.Remove(defendingType);

					return;
				}
			}

			if (effective1x.Contains(defendingType))
			{
				return;
			}

			foreach (var t in defendingType.Normal)
			{
				if (t == attackingType)
				{
					effective1x.Add(defendingType);
					effective05x.Remove(defendingType);
					effective025x.Remove(defendingType);
					effective0x.Remove(defendingType);

					return;
				}
			}

			if (effective05x.Contains(defendingType))
			{
				return;
			}

			foreach (var t in defendingType.Resist2x)
			{
				if (t == attackingType)
				{
					effective05x.Add(defendingType);
					effective025x.Remove(defendingType);
					effective0x.Remove(defendingType);

					return;
				}
			}

			if (effective025x.Contains(defendingType))
			{
				return;
			}

			foreach (var t in defendingType.Resist4x)
			{
				if (t == attackingType)
				{
					effective025x.Add(defendingType);
					effective0x.Remove(defendingType);

					return;
				}
			}

			if (effective0x.Contains(defendingType))
			{
				return;
			}

			foreach (var t in defendingType.Immune)
			{
				if (t == attackingType)
				{
					effective0x.Add(defendingType);

					return;
				}
			}

			throw new ArgumentException();
		}
	}
}
