
namespace PokeTyper
{
    using System;
    using System.Text;

    public class Coverage
    {
        private readonly Types type1;
        private readonly Types type2;
        private readonly Types type3;
        private readonly Types type4;
        private readonly int numTypes;
        private readonly string name;

        public readonly Type[] Effective4x;
        public readonly Type[] Effective2x;
        public readonly Type[] Effective1x;
        public readonly Type[] Effective05x;
        public readonly Type[] Effective025x;
        public readonly Type[] Effective0x;

        public int NumTypes
        {
            get
            {
                return this.numTypes;
            }
        }

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
                if (this.numTypes < 2)
                {
                    throw new InvalidOperationException("This type does not have two types.");
                }

                return this.type2;
            }
        }

        public Types Type3
        {
            get
            {
                if (this.numTypes < 3)
                {
                    throw new InvalidOperationException("This type does not have three types.");
                }

                return this.type3;
            }
        }

        public Types Type4
        {
            get
            {
                if (this.numTypes < 4)
                {
                    throw new InvalidOperationException("This type does not have four types.");
                }

                return this.type4;
            }
        }

        public string Name
        {
            get
            {
                return this.name;
            }
        }

        internal Coverage(
            Types type1,
            Types type2,
            Types type3,
            Types type4,
            int numTypes,
            Type[] effective4x,
            Type[] effective2x,
            Type[] effective1x,
            Type[] effective05x,
            Type[] effective025x,
            Type[] effective0x)
        {
            this.type1 = type1;
            this.type2 = numTypes >= 2 ? type2 : Types.Normal;
            this.type3 = numTypes >= 3 ? type3 : Types.Normal;
            this.type4 = numTypes >= 4 ? type4 : Types.Normal;
            this.numTypes = numTypes;
            this.Effective4x = effective4x;
            this.Effective2x = effective2x;
            this.Effective1x = effective1x;
            this.Effective05x = effective05x;
            this.Effective025x = effective025x;
            this.Effective0x = effective0x;

            string name = type1.ToString();
            if (numTypes >= 2)
            {
                name += "/" + type2.ToString();
            }

            if (numTypes >= 3)
            {
                name += "/" + type3.ToString();
            }

            if (numTypes >= 4)
            {
                name += "/" + type4.ToString();
            }

            this.name = name;
        }

        public override string ToString()
        {
 	        StringBuilder sb = new StringBuilder();

            sb.Append(this.Name);

            sb.Append("\nx4  ");
            foreach (Type t in this.Effective4x)
            {
                sb.Append(" " + t.Name);
            }

            sb.Append("\nx2  ");
            foreach (Type t in this.Effective2x)
            {
                sb.Append(" " + t.Name);
            }

            sb.Append("\nx1  ");
            foreach (Type t in this.Effective1x)
            {
                sb.Append(" " + t.Name);
            }

            sb.Append("\nx1/2");
            foreach (Type t in this.Effective05x)
            {
                sb.Append(" " + t.Name);
            }

            sb.Append("\nx1/4");
            foreach (Type t in this.Effective025x)
            {
                sb.Append(" " + t.Name);
            }

            sb.Append("\nx0  ");
            foreach (Type t in this.Effective0x)
            {
                sb.Append(" " + t.Name);
            }

            return sb.ToString();
        }
    }
}
