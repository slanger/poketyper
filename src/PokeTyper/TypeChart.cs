namespace PokeTyper
{
	internal class TypeChart
	{
		// This type chart is based on the Generation 6 Pokemon games. Other generations have
		// different type charts.
		// TODO: Create and use type charts from previous generations.
		internal static readonly Effect[,] Chart = new Effect[,]
		{
/*               NORMAL        FIGHT         FLYING        POISON        GROUND        ROCK          BUG           GHOST         STEEL         FIRE          WATER         GRASS         ELECTR        PSYCHC        ICE           DRAGON        DARK          FAIRY     */
/* NORMAL */{ Effect.OneX,  Effect.OneX,  Effect.OneX,  Effect.OneX,  Effect.OneX,  Effect.HalfX, Effect.OneX,  Effect.ZeroX, Effect.HalfX, Effect.OneX,  Effect.OneX,  Effect.OneX,  Effect.OneX,  Effect.OneX,  Effect.OneX,  Effect.OneX,  Effect.OneX,  Effect.OneX  },
/* FIGHT  */{ Effect.TwoX,  Effect.OneX,  Effect.HalfX, Effect.HalfX, Effect.OneX,  Effect.TwoX,  Effect.HalfX, Effect.ZeroX, Effect.TwoX,  Effect.OneX,  Effect.OneX,  Effect.OneX,  Effect.OneX,  Effect.HalfX, Effect.TwoX,  Effect.OneX,  Effect.TwoX,  Effect.HalfX },
/* FLYING */{ Effect.OneX,  Effect.TwoX,  Effect.OneX,  Effect.OneX,  Effect.OneX,  Effect.HalfX, Effect.TwoX,  Effect.OneX,  Effect.HalfX, Effect.OneX,  Effect.OneX,  Effect.TwoX,  Effect.HalfX, Effect.OneX,  Effect.OneX,  Effect.OneX,  Effect.OneX,  Effect.OneX  },
/* POISON */{ Effect.OneX,  Effect.OneX,  Effect.OneX,  Effect.HalfX, Effect.HalfX, Effect.HalfX, Effect.OneX,  Effect.HalfX, Effect.ZeroX, Effect.OneX,  Effect.OneX,  Effect.TwoX,  Effect.OneX,  Effect.OneX,  Effect.OneX,  Effect.OneX,  Effect.OneX,  Effect.TwoX  },
/* GROUND */{ Effect.OneX,  Effect.OneX,  Effect.ZeroX, Effect.TwoX,  Effect.OneX,  Effect.TwoX,  Effect.HalfX, Effect.OneX,  Effect.TwoX,  Effect.TwoX,  Effect.OneX,  Effect.HalfX, Effect.TwoX,  Effect.OneX,  Effect.OneX,  Effect.OneX,  Effect.OneX,  Effect.OneX  },
/* ROCK   */{ Effect.OneX,  Effect.HalfX, Effect.TwoX,  Effect.OneX,  Effect.HalfX, Effect.OneX,  Effect.TwoX,  Effect.OneX,  Effect.HalfX, Effect.TwoX,  Effect.OneX,  Effect.OneX,  Effect.OneX,  Effect.OneX,  Effect.TwoX,  Effect.OneX,  Effect.OneX,  Effect.OneX  },
/* BUG    */{ Effect.OneX,  Effect.HalfX, Effect.HalfX, Effect.HalfX, Effect.OneX,  Effect.OneX,  Effect.OneX,  Effect.HalfX, Effect.HalfX, Effect.HalfX, Effect.OneX,  Effect.TwoX,  Effect.OneX,  Effect.TwoX,  Effect.OneX,  Effect.OneX,  Effect.TwoX,  Effect.HalfX },
/* GHOST  */{ Effect.ZeroX, Effect.OneX,  Effect.OneX,  Effect.OneX,  Effect.OneX,  Effect.OneX,  Effect.OneX,  Effect.TwoX,  Effect.OneX,  Effect.OneX,  Effect.OneX,  Effect.OneX,  Effect.OneX,  Effect.TwoX,  Effect.OneX,  Effect.OneX,  Effect.HalfX, Effect.OneX  },
/* STEEL  */{ Effect.OneX,  Effect.OneX,  Effect.OneX,  Effect.OneX,  Effect.OneX,  Effect.TwoX,  Effect.OneX,  Effect.OneX,  Effect.HalfX, Effect.HalfX, Effect.HalfX, Effect.OneX,  Effect.HalfX, Effect.OneX,  Effect.TwoX,  Effect.OneX,  Effect.OneX,  Effect.TwoX  },
/* FIRE   */{ Effect.OneX,  Effect.OneX,  Effect.OneX,  Effect.OneX,  Effect.OneX,  Effect.HalfX, Effect.TwoX,  Effect.OneX,  Effect.TwoX,  Effect.HalfX, Effect.HalfX, Effect.TwoX,  Effect.OneX,  Effect.OneX,  Effect.TwoX,  Effect.HalfX, Effect.OneX,  Effect.OneX  },
/* WATER  */{ Effect.OneX,  Effect.OneX,  Effect.OneX,  Effect.OneX,  Effect.TwoX,  Effect.TwoX,  Effect.OneX,  Effect.OneX,  Effect.OneX,  Effect.TwoX,  Effect.HalfX, Effect.HalfX, Effect.OneX,  Effect.OneX,  Effect.OneX,  Effect.HalfX, Effect.OneX,  Effect.OneX  },
/* GRASS  */{ Effect.OneX,  Effect.OneX,  Effect.HalfX, Effect.HalfX, Effect.TwoX,  Effect.TwoX,  Effect.HalfX, Effect.OneX,  Effect.HalfX, Effect.HalfX, Effect.TwoX,  Effect.HalfX, Effect.OneX,  Effect.OneX,  Effect.OneX,  Effect.HalfX, Effect.OneX,  Effect.OneX  },
/* ELECTR */{ Effect.OneX,  Effect.OneX,  Effect.TwoX,  Effect.OneX,  Effect.ZeroX, Effect.OneX,  Effect.OneX,  Effect.OneX,  Effect.OneX,  Effect.OneX,  Effect.TwoX,  Effect.HalfX, Effect.HalfX, Effect.OneX,  Effect.OneX,  Effect.HalfX, Effect.OneX,  Effect.OneX  },
/* PSYCHC */{ Effect.OneX,  Effect.TwoX,  Effect.OneX,  Effect.TwoX,  Effect.OneX,  Effect.OneX,  Effect.OneX,  Effect.OneX,  Effect.HalfX, Effect.OneX,  Effect.OneX,  Effect.OneX,  Effect.OneX,  Effect.HalfX, Effect.OneX,  Effect.OneX,  Effect.ZeroX, Effect.OneX  },
/* ICE    */{ Effect.OneX,  Effect.OneX,  Effect.TwoX,  Effect.OneX,  Effect.TwoX,  Effect.OneX,  Effect.OneX,  Effect.OneX,  Effect.HalfX, Effect.HalfX, Effect.HalfX, Effect.TwoX,  Effect.OneX,  Effect.OneX,  Effect.HalfX, Effect.TwoX,  Effect.OneX,  Effect.OneX  },
/* DRAGON */{ Effect.OneX,  Effect.OneX,  Effect.OneX,  Effect.OneX,  Effect.OneX,  Effect.OneX,  Effect.OneX,  Effect.OneX,  Effect.HalfX, Effect.OneX,  Effect.OneX,  Effect.OneX,  Effect.OneX,  Effect.OneX,  Effect.OneX,  Effect.TwoX,  Effect.OneX,  Effect.ZeroX },
/* DARK   */{ Effect.OneX,  Effect.HalfX, Effect.OneX,  Effect.OneX,  Effect.OneX,  Effect.OneX,  Effect.OneX,  Effect.TwoX,  Effect.OneX,  Effect.OneX,  Effect.OneX,  Effect.OneX,  Effect.OneX,  Effect.TwoX,  Effect.OneX,  Effect.OneX,  Effect.HalfX, Effect.HalfX },
/* FAIRY  */{ Effect.OneX,  Effect.TwoX,  Effect.OneX,  Effect.HalfX, Effect.OneX,  Effect.OneX,  Effect.OneX,  Effect.OneX,  Effect.HalfX, Effect.HalfX, Effect.OneX,  Effect.OneX,  Effect.OneX,  Effect.OneX,  Effect.OneX,  Effect.TwoX,  Effect.TwoX,  Effect.OneX  }
		};
	}
}
