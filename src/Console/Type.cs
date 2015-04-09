
namespace PokeTyper
{
    using System;
    using System.Text;
    using System.Collections.Generic;

    public class Type
    {
        /*
        private readonly string[] names = new string[]
        {
            "Normal",
            "Fighting",
            "Flying",
            "Poison",
            "Ground",
            "Rock",
            "Bug",
            "Ghost",
            "Steel",
            "Fire",
            "Water",
            "Grass",
            "Electric",
            "Psychic",
            "Ice",
            "Dragon",
            "Dark",
            "Fairy"
        };
        */

        public Types Type1
        {
            get
            {
                return this.type1;
            }
        }

        public Types Type2
        {
            get
            {
                if (!this.HasTwoTypes)
                {
                    throw new InvalidOperationException("This type does not have two types.");
                }

                return this.type2;
            }
        }

        public readonly bool HasTwoTypes;

        public readonly Types[] Resist4x;
        public readonly Types[] Resist2x;
        public readonly Types[] Normal;
        public readonly Types[] WeakTo2x;
        public readonly Types[] WeakTo4x;
        public readonly Types[] Immune;

        public readonly string Name;

        private readonly Types type1;
        private readonly Types type2;

        internal Type(
            Types type1,
            Types type2,
            bool hasTwoTypes,
            Types[] resist4x,
            Types[] resist2x,
            Types[] normal,
            Types[] weakTo2x,
            Types[] weakTo4x,
            Types[] immune)
        {
            this.type1 = type1;
            this.type2 = hasTwoTypes ? type2 : Types.Normal;
            this.HasTwoTypes = hasTwoTypes;
            this.Resist4x = resist4x;
            this.Resist2x = resist2x;
            this.Normal = normal;
            this.WeakTo2x = weakTo2x;
            this.WeakTo4x = weakTo4x;
            this.Immune = immune;

            string name;

            if (hasTwoTypes)
            {
                if (string.CompareOrdinal(this.type1.ToString(), this.type2.ToString()) >= 0)
                {
                    Types temp = this.type1;
                    this.type1 = this.type2;
                    this.type2 = temp;
                }

                name = this.type1.ToString() + "/" + this.type2.ToString();
            }
            else
            {
                name = this.type1.ToString();
            }

            this.Name = name;
        }

        public override int GetHashCode()
        {
            return this.Name.GetHashCode();
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(this.Name);

            sb.Append("\nx1/4");
            foreach (Types t in this.Resist4x)
            {
                sb.Append(" " + t.ToString());
            }

            sb.Append("\nx1/2");
            foreach (Types t in this.Resist2x)
            {
                sb.Append(" " + t.ToString());
            }

            sb.Append("\nx1  ");
            foreach (Types t in this.Normal)
            {
                sb.Append(" " + t.ToString());
            }

            sb.Append("\nx2  ");
            foreach (Types t in this.WeakTo2x)
            {
                sb.Append(" " + t.ToString());
            }

            sb.Append("\nx4  ");
            foreach (Types t in this.WeakTo4x)
            {
                sb.Append(" " + t.ToString());
            }

            sb.Append("\nx0  ");
            foreach (Types t in this.Immune)
            {
                sb.Append(" " + t.ToString());
            }

            return sb.ToString();
        }
    }
}
