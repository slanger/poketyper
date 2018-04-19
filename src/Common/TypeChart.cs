namespace PokeTyper
{
	internal class TypeChart
	{
		internal static readonly Effect[,] Chart = new Effect[,]
		{
/*               NORMAL        FIGHT         FLYING        POISON        GROUND        ROCK          BUG           GHOST         STEEL         FIRE          WATER         GRASS         ELECTR        PSYCHC        ICE           DRAGON        DARK          FAIRY     */
/* NORMAL */{ Effect.xOne,  Effect.xOne,  Effect.xOne,  Effect.xOne,  Effect.xOne,  Effect.xHalf, Effect.xOne,  Effect.xZero, Effect.xHalf, Effect.xOne,  Effect.xOne,  Effect.xOne,  Effect.xOne,  Effect.xOne,  Effect.xOne,  Effect.xOne,  Effect.xOne,  Effect.xOne  },
/* FIGHT  */{ Effect.xTwo,  Effect.xOne,  Effect.xHalf, Effect.xHalf, Effect.xOne,  Effect.xTwo,  Effect.xHalf, Effect.xZero, Effect.xTwo,  Effect.xOne,  Effect.xOne,  Effect.xOne,  Effect.xOne,  Effect.xHalf, Effect.xTwo,  Effect.xOne,  Effect.xTwo,  Effect.xHalf },
/* FLYING */{ Effect.xOne,  Effect.xTwo,  Effect.xOne,  Effect.xOne,  Effect.xOne,  Effect.xHalf, Effect.xTwo,  Effect.xOne,  Effect.xHalf, Effect.xOne,  Effect.xOne,  Effect.xTwo,  Effect.xHalf, Effect.xOne,  Effect.xOne,  Effect.xOne,  Effect.xOne,  Effect.xOne  },
/* POISON */{ Effect.xOne,  Effect.xOne,  Effect.xOne,  Effect.xHalf, Effect.xHalf, Effect.xHalf, Effect.xOne,  Effect.xHalf, Effect.xZero, Effect.xOne,  Effect.xOne,  Effect.xTwo,  Effect.xOne,  Effect.xOne,  Effect.xOne,  Effect.xOne,  Effect.xOne,  Effect.xTwo  },
/* GROUND */{ Effect.xOne,  Effect.xOne,  Effect.xZero, Effect.xTwo,  Effect.xOne,  Effect.xTwo,  Effect.xHalf, Effect.xOne,  Effect.xTwo,  Effect.xTwo,  Effect.xOne,  Effect.xHalf, Effect.xTwo,  Effect.xOne,  Effect.xOne,  Effect.xOne,  Effect.xOne,  Effect.xOne  },
/* ROCK   */{ Effect.xOne,  Effect.xHalf, Effect.xTwo,  Effect.xOne,  Effect.xHalf, Effect.xOne,  Effect.xTwo,  Effect.xOne,  Effect.xHalf, Effect.xTwo,  Effect.xOne,  Effect.xOne,  Effect.xOne,  Effect.xOne,  Effect.xTwo,  Effect.xOne,  Effect.xOne,  Effect.xOne  },
/* BUG    */{ Effect.xOne,  Effect.xHalf, Effect.xHalf, Effect.xHalf, Effect.xOne,  Effect.xOne,  Effect.xOne,  Effect.xHalf, Effect.xHalf, Effect.xHalf, Effect.xOne,  Effect.xTwo,  Effect.xOne,  Effect.xTwo,  Effect.xOne,  Effect.xOne,  Effect.xTwo,  Effect.xHalf },
/* GHOST  */{ Effect.xZero, Effect.xOne,  Effect.xOne,  Effect.xOne,  Effect.xOne,  Effect.xOne,  Effect.xOne,  Effect.xTwo,  Effect.xOne,  Effect.xOne,  Effect.xOne,  Effect.xOne,  Effect.xOne,  Effect.xTwo,  Effect.xOne,  Effect.xOne,  Effect.xHalf, Effect.xOne  },
/* STEEL  */{ Effect.xOne,  Effect.xOne,  Effect.xOne,  Effect.xOne,  Effect.xOne,  Effect.xTwo,  Effect.xOne,  Effect.xOne,  Effect.xHalf, Effect.xHalf, Effect.xHalf, Effect.xOne,  Effect.xHalf, Effect.xOne,  Effect.xTwo,  Effect.xOne,  Effect.xOne,  Effect.xTwo  },
/* FIRE   */{ Effect.xOne,  Effect.xOne,  Effect.xOne,  Effect.xOne,  Effect.xOne,  Effect.xHalf, Effect.xTwo,  Effect.xOne,  Effect.xTwo,  Effect.xHalf, Effect.xHalf, Effect.xTwo,  Effect.xOne,  Effect.xOne,  Effect.xTwo,  Effect.xHalf, Effect.xOne,  Effect.xOne  },
/* WATER  */{ Effect.xOne,  Effect.xOne,  Effect.xOne,  Effect.xOne,  Effect.xTwo,  Effect.xTwo,  Effect.xOne,  Effect.xOne,  Effect.xOne,  Effect.xTwo,  Effect.xHalf, Effect.xHalf, Effect.xOne,  Effect.xOne,  Effect.xOne,  Effect.xHalf, Effect.xOne,  Effect.xOne  },
/* GRASS  */{ Effect.xOne,  Effect.xOne,  Effect.xHalf, Effect.xHalf, Effect.xTwo,  Effect.xTwo,  Effect.xHalf, Effect.xOne,  Effect.xHalf, Effect.xHalf, Effect.xTwo,  Effect.xHalf, Effect.xOne,  Effect.xOne,  Effect.xOne,  Effect.xHalf, Effect.xOne,  Effect.xOne  },
/* ELECTR */{ Effect.xOne,  Effect.xOne,  Effect.xTwo,  Effect.xOne,  Effect.xZero, Effect.xOne,  Effect.xOne,  Effect.xOne,  Effect.xOne,  Effect.xOne,  Effect.xTwo,  Effect.xHalf, Effect.xHalf, Effect.xOne,  Effect.xOne,  Effect.xHalf, Effect.xOne,  Effect.xOne  },
/* PSYCHC */{ Effect.xOne,  Effect.xTwo,  Effect.xOne,  Effect.xTwo,  Effect.xOne,  Effect.xOne,  Effect.xOne,  Effect.xOne,  Effect.xHalf, Effect.xOne,  Effect.xOne,  Effect.xOne,  Effect.xOne,  Effect.xHalf, Effect.xOne,  Effect.xOne,  Effect.xZero, Effect.xOne  },
/* ICE    */{ Effect.xOne,  Effect.xOne,  Effect.xTwo,  Effect.xOne,  Effect.xTwo,  Effect.xOne,  Effect.xOne,  Effect.xOne,  Effect.xHalf, Effect.xHalf, Effect.xHalf, Effect.xTwo,  Effect.xOne,  Effect.xOne,  Effect.xHalf, Effect.xTwo,  Effect.xOne,  Effect.xOne  },
/* DRAGON */{ Effect.xOne,  Effect.xOne,  Effect.xOne,  Effect.xOne,  Effect.xOne,  Effect.xOne,  Effect.xOne,  Effect.xOne,  Effect.xHalf, Effect.xOne,  Effect.xOne,  Effect.xOne,  Effect.xOne,  Effect.xOne,  Effect.xOne,  Effect.xTwo,  Effect.xOne,  Effect.xZero },
/* DARK   */{ Effect.xOne,  Effect.xHalf, Effect.xOne,  Effect.xOne,  Effect.xOne,  Effect.xOne,  Effect.xOne,  Effect.xTwo,  Effect.xOne,  Effect.xOne,  Effect.xOne,  Effect.xOne,  Effect.xOne,  Effect.xTwo,  Effect.xOne,  Effect.xOne,  Effect.xHalf, Effect.xHalf },
/* FAIRY  */{ Effect.xOne,  Effect.xTwo,  Effect.xOne,  Effect.xHalf, Effect.xOne,  Effect.xOne,  Effect.xOne,  Effect.xOne,  Effect.xHalf, Effect.xHalf, Effect.xOne,  Effect.xOne,  Effect.xOne,  Effect.xOne,  Effect.xOne,  Effect.xTwo,  Effect.xTwo,  Effect.xOne  }
		};
	}
}
