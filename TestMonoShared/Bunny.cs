using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Storage;

using Monocle;

namespace TestMonoShared
{
	[Pooled, Tracked]
	public class Bunny : Entity
	{
		public float posX = 0;
		public float posY = 0;
		public float speedX = 0;
		public float speedY = 0;
		public Bunny () : base()
		{
		}
	}
}

