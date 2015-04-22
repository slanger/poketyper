using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace PokeTyper
{
	public class PokemonType : IComparable<PokemonType>
	{
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
				if (!this.HasTwoTypes)
				{
					throw new InvalidOperationException("This type does not have two types.");
				}

				return this.types[1];
			}
		}

		public bool HasTwoTypes
		{
			get
			{
				return this.types.Length >= 2;
			}
		}

		public string Name
		{
			get
			{
				return this.name;
			}
		}

		public readonly TypeToken[] Resist4x;
		public readonly TypeToken[] Resist2x;
		public readonly TypeToken[] Normal;
		public readonly TypeToken[] WeakTo2x;
		public readonly TypeToken[] WeakTo4x;
		public readonly TypeToken[] Immune;

		private readonly TypeToken[] types;
		private readonly string name;

		private PokemonType(
			TypeToken[] resist4x,
			TypeToken[] resist2x,
			TypeToken[] normal,
			TypeToken[] weakTo2x,
			TypeToken[] weakTo4x,
			TypeToken[] immune,
			params TypeToken[] types)
		{
			Debug.Assert(types.Length > 0 && types.Length <= 2);

			this.types = types;
			this.Resist4x = resist4x;
			this.Resist2x = resist2x;
			this.Normal = normal;
			this.WeakTo2x = weakTo2x;
			this.WeakTo4x = weakTo4x;
			this.Immune = immune;

			Array.Sort(this.types);
			Array.Sort(this.Resist4x);
			Array.Sort(this.Resist2x);
			Array.Sort(this.Normal);
			Array.Sort(this.WeakTo2x);
			Array.Sort(this.WeakTo4x);
			Array.Sort(this.Immune);

			var sb = new StringBuilder();
			const char separator = '/';
			foreach (var type in this.types)
			{
				sb.Append(type.ToString() + separator);
			}

			this.name = sb.ToString().TrimEnd(separator);
		}

		public int CompareTo(PokemonType other)
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
			var other = obj as PokemonType;
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

			sb.Append("\nx1/4");
			foreach (var t in this.Resist4x)
			{
				sb.Append(" " + t.ToString());
			}

			sb.Append("\nx1/2");
			foreach (var t in this.Resist2x)
			{
				sb.Append(" " + t.ToString());
			}

			sb.Append("\nx1  ");
			foreach (var t in this.Normal)
			{
				sb.Append(" " + t.ToString());
			}

			sb.Append("\nx2  ");
			foreach (var t in this.WeakTo2x)
			{
				sb.Append(" " + t.ToString());
			}

			sb.Append("\nx4  ");
			foreach (var t in this.WeakTo4x)
			{
				sb.Append(" " + t.ToString());
			}

			sb.Append("\nx0  ");
			foreach (var t in this.Immune)
			{
				sb.Append(" " + t.ToString());
			}

			return sb.ToString();
		}

		internal static PokemonType MakeType(params TypeToken[] types)
		{
			Debug.Assert(types.Length > 0 && types.Length <= 2);

			var resist4x = new HashSet<TypeToken>();
			var resist2x = new HashSet<TypeToken>();
			var normal = new HashSet<TypeToken>();
			var weakTo2x = new HashSet<TypeToken>();
			var weakTo4x = new HashSet<TypeToken>();
			var immune = new HashSet<TypeToken>();

			foreach (var type in types)
			{
				MakeTypeHelper(type, resist4x, resist2x, normal, weakTo2x, weakTo4x, immune);
			}

			var arrResist4x = new TypeToken[resist4x.Count];
			var arrResist2x = new TypeToken[resist2x.Count];
			var arrNormal = new TypeToken[normal.Count];
			var arrWeakTo2x = new TypeToken[weakTo2x.Count];
			var arrWeakTo4x = new TypeToken[weakTo4x.Count];
			var arrImmune = new TypeToken[immune.Count];

			resist4x.CopyTo(arrResist4x);
			resist2x.CopyTo(arrResist2x);
			normal.CopyTo(arrNormal);
			weakTo2x.CopyTo(arrWeakTo2x);
			weakTo4x.CopyTo(arrWeakTo4x);
			immune.CopyTo(arrImmune);

			return new PokemonType(
				arrResist4x,
				arrResist2x,
				arrNormal,
				arrWeakTo2x,
				arrWeakTo4x,
				arrImmune,
				types);
		}

		private static void MakeTypeHelper(
			TypeToken defendingType,
			HashSet<TypeToken> resist4x,
			HashSet<TypeToken> resist2x,
			HashSet<TypeToken> normal,
			HashSet<TypeToken> weakTo2x,
			HashSet<TypeToken> weakTo4x,
			HashSet<TypeToken> immune)
		{
			var column = (int)defendingType;
			for (int row = 0; row < TypeChart.Chart.GetLength(0); row++)
			{
				var attackingType = (TypeToken)row;
				if (immune.Contains(attackingType))
				{
					continue;
				}

				var effect = TypeChart.Chart[row, column];
				switch (effect)
				{
					case Effect.xHalf: // this type resists attacking type
						if (resist2x.Remove(attackingType))
						{
							resist4x.Add(attackingType);
						}
						else if (weakTo2x.Remove(attackingType))
						{
							normal.Add(attackingType);
						}
						else
						{
							normal.Remove(attackingType);
							resist2x.Add(attackingType);
						}

						break;
					case Effect.xOne: // this type is neither resistant nor weak to attacking type
						if (!resist2x.Contains(attackingType) && !weakTo2x.Contains(attackingType))
						{
							normal.Add(attackingType);
						}

						break;
					case Effect.xTwo: // this type is weak to attacking type
						if (resist2x.Remove(attackingType))
						{
							normal.Add(attackingType);
						}
						else if (weakTo2x.Remove(attackingType))
						{
							weakTo4x.Add(attackingType);
						}
						else
						{
							normal.Remove(attackingType);
							weakTo2x.Add(attackingType);
						}

						break;
					case Effect.xZero: // this type is immune to attacking type
						resist2x.Remove(attackingType);
						normal.Remove(attackingType);
						weakTo2x.Remove(attackingType);

						immune.Add(attackingType);

						break;
				}
			}
		}
	}
}
