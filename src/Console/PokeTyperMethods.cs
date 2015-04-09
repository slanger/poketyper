
namespace PokeTyper
{
    using System;
    using System.Collections.Generic;

    public class PokeTyperMethods
    {
        public static Type MakeType(string type)
        {
            return MakeType(GetType(type));
        }

        public static Type MakeType(string type1, string type2)
        {
            return MakeType(GetType(type1), GetType(type2));
        }

        public static Type MakeType(Types type)
        {
            return MakeType(type, Types.Normal, false);
        }

        public static Type MakeType(Types type1, Types type2)
        {
            return MakeType(type1, type2, true);
        }

        public static Coverage MakeCoverage(string type)
        {
            return MakeCoverage(GetType(type));
        }

        public static Coverage MakeCoverage(string type1, string type2)
        {
            return MakeCoverage(GetType(type1), GetType(type2));
        }

        public static Coverage MakeCoverage(string type1, string type2, string type3)
        {
            return MakeCoverage(GetType(type1), GetType(type2), GetType(type3));
        }

        public static Coverage MakeCoverage(string type1, string type2, string type3, string type4)
        {
            return MakeCoverage(GetType(type1), GetType(type2), GetType(type3), GetType(type4));
        }

        public static Coverage MakeCoverage(Types type)
        {
            return MakeCoverage(type, Types.Normal, Types.Normal, Types.Normal, 1);
        }

        public static Coverage MakeCoverage(Types type1, Types type2)
        {
            return MakeCoverage(type1, type2, Types.Normal, Types.Normal, 2);
        }

        public static Coverage MakeCoverage(Types type1, Types type2, Types type3)
        {
            return MakeCoverage(type1, type2, type3, Types.Normal, 3);
        }

        public static Coverage MakeCoverage(Types type1, Types type2, Types type3, Types type4)
        {
            return MakeCoverage(type1, type2, type3, type4, 4);
        }

        private static Types GetType(string type)
        {
            type = type.ToLower();
            foreach (Types t in Enum.GetValues(typeof(Types)))
            {
                if (type.Equals(t.ToString().ToLower()))
                {
                    return t;
                }
            }

            throw new ArgumentException();
        }

        private static Type MakeType(Types type1, Types type2, bool hasTwoTypes)
        {
            HashSet<Types> resist4x = new HashSet<Types>();
            HashSet<Types> resist2x = new HashSet<Types>();
            HashSet<Types> normal = new HashSet<Types>();
            HashSet<Types> weakTo2x = new HashSet<Types>();
            HashSet<Types> weakTo4x = new HashSet<Types>();
            HashSet<Types> immune = new HashSet<Types>();

            MakeTypeHelper(type1, resist4x, resist2x, normal, weakTo2x, weakTo4x, immune);

            if (hasTwoTypes)
            {
                MakeTypeHelper(type2, resist4x, resist2x, normal, weakTo2x, weakTo4x, immune);
            }

            Types[] arrResist4x = new Types[resist4x.Count];
            Types[] arrResist2x = new Types[resist2x.Count];
            Types[] arrNormal = new Types[normal.Count];
            Types[] arrWeakTo2x = new Types[weakTo2x.Count];
            Types[] arrWeakTo4x = new Types[weakTo4x.Count];
            Types[] arrImmune = new Types[immune.Count];

            resist4x.CopyTo(arrResist4x);
            resist2x.CopyTo(arrResist2x);
            normal.CopyTo(arrNormal);
            weakTo2x.CopyTo(arrWeakTo2x);
            weakTo4x.CopyTo(arrWeakTo4x);
            immune.CopyTo(arrImmune);

            return new Type(
                type1,
                type2,
                hasTwoTypes,
                arrResist4x,
                arrResist2x,
                arrNormal,
                arrWeakTo2x,
                arrWeakTo4x,
                arrImmune);
        }

        private static Coverage MakeCoverage(
            Types type1,
            Types type2,
            Types type3,
            Types type4,
            int numTypes)
        {
            HashSet<Type> effective4x = new HashSet<Type>();
            HashSet<Type> effective2x = new HashSet<Type>();
            HashSet<Type> effective1x = new HashSet<Type>();
            HashSet<Type> effective05x = new HashSet<Type>();
            HashSet<Type> effective025x = new HashSet<Type>();
            HashSet<Type> effective0x = new HashSet<Type>();

            int typesLength = Enum.GetValues(typeof(Types)).Length;
            Type t;
            for (int i = 0; i < typesLength; i++)
            {
                Types t1 = (Types)i;
                t = PokeTyperMethods.MakeType(t1);

                MakeCoverageHelper(type1, t, effective4x, effective2x, effective1x, effective05x, effective025x, effective0x);

                if (numTypes >= 2)
                {
                    MakeCoverageHelper(type2, t, effective4x, effective2x, effective1x, effective05x, effective025x, effective0x);
                }

                if (numTypes >= 3)
                {
                    MakeCoverageHelper(type3, t, effective4x, effective2x, effective1x, effective05x, effective025x, effective0x);
                }

                if (numTypes >= 4)
                {
                    MakeCoverageHelper(type4, t, effective4x, effective2x, effective1x, effective05x, effective025x, effective0x);
                }

                for (int j = i + 1; j < typesLength; j++)
                {
                    Types t2 = (Types)j;
                    t = PokeTyperMethods.MakeType(t1, t2);

                    MakeCoverageHelper(type1, t, effective4x, effective2x, effective1x, effective05x, effective025x, effective0x);

                    if (numTypes >= 2)
                    {
                        MakeCoverageHelper(type2, t, effective4x, effective2x, effective1x, effective05x, effective025x, effective0x);
                    }

                    if (numTypes >= 3)
                    {
                        MakeCoverageHelper(type3, t, effective4x, effective2x, effective1x, effective05x, effective025x, effective0x);
                    }

                    if (numTypes >= 4)
                    {
                        MakeCoverageHelper(type4, t, effective4x, effective2x, effective1x, effective05x, effective025x, effective0x);
                    }
                }
            }

            Type[] arrEffective4x = new Type[effective4x.Count];
            Type[] arrEffective2x = new Type[effective2x.Count];
            Type[] arrEffective1x = new Type[effective1x.Count];
            Type[] arrEffective05x = new Type[effective05x.Count];
            Type[] arrEffective025x = new Type[effective025x.Count];
            Type[] arrEffective0x = new Type[effective0x.Count];

            effective4x.CopyTo(arrEffective4x);
            effective2x.CopyTo(arrEffective2x);
            effective1x.CopyTo(arrEffective1x);
            effective05x.CopyTo(arrEffective05x);
            effective025x.CopyTo(arrEffective025x);
            effective0x.CopyTo(arrEffective0x);

            return new Coverage(
                type1,
                type2,
                type3,
                type4,
                numTypes,
                arrEffective4x,
                arrEffective2x,
                arrEffective1x,
                arrEffective05x,
                arrEffective025x,
                arrEffective0x);
        }

        private static void MakeTypeHelper(
            Types defendingType,
            HashSet<Types> resist4x,
            HashSet<Types> resist2x,
            HashSet<Types> normal,
            HashSet<Types> weakTo2x,
            HashSet<Types> weakTo4x,
            HashSet<Types> immune)
        {
            int column = (int)defendingType;
            for (int row = 0; row < TypeChart.Chart.GetLength(0); row++)
            {
                Types attackingType = (Types)row;
                if (immune.Contains(attackingType))
                {
                    continue;
                }

                Effect effect = TypeChart.Chart[row, column];
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

        private static void MakeCoverageHelper(
            Types attackingType,
            Type defendingType,
            HashSet<Type> effective4x,
            HashSet<Type> effective2x,
            HashSet<Type> effective1x,
            HashSet<Type> effective05x,
            HashSet<Type> effective025x,
            HashSet<Type> effective0x)
        {
            if (effective4x.Contains(defendingType))
            {
                return;
            }

            foreach (Types t in defendingType.WeakTo4x)
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

            foreach (Types t in defendingType.WeakTo2x)
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

            foreach (Types t in defendingType.Normal)
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

            foreach (Types t in defendingType.Resist2x)
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

            foreach (Types t in defendingType.Resist4x)
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

            foreach (Types t in defendingType.Immune)
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
