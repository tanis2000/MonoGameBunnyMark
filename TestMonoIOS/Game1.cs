﻿#region Using Statements
using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;

#endregion

namespace TestMonoIOS
{
	/// <summary>
	/// This is the main type for your game.
	/// </summary>
	public class Game1 : Game
	{
		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;
		Texture2D image;
		Vector2 pos;
		List<Bunny> bunnies;
		Random rnd;
		float gravity = 0.5f;
		float minX = 0;
		float minY = 0;
		float maxX = 0;
		float maxY = 0;
		int bunniesCount = 0;
		private FrameCounter _frameCounter = new FrameCounter();
		SpriteFont font;


		public Game1 ()
		{
			graphics = new GraphicsDeviceManager (this);
			Content.RootDirectory = "Content";	            
			graphics.IsFullScreen = true;	
			pos = new Vector2 (0, 0);
			bunnies = new List<Bunny> ();
			rnd = new Random ();
		}

		/// <summary>
		/// Allows the game to perform any initialization it needs to before starting to run.
		/// This is where it can query for any required services and load any non-graphic
		/// related content.  Calling base.Initialize will enumerate through any components
		/// and initialize them as well.
		/// </summary>
		protected override void Initialize ()
		{
			// TODO: Add your initialization logic here
			maxX = this.GraphicsDevice.Viewport.Width;
			maxY = this.GraphicsDevice.Viewport.Height;
			addBunnies (1000);

			base.Initialize ();
				
		}

		/// <summary>
		/// LoadContent will be called once per game and is the place to load
		/// all of your content.
		/// </summary>
		protected override void LoadContent ()
		{
			// Create a new SpriteBatch, which can be used to draw textures.
			spriteBatch = new SpriteBatch (GraphicsDevice);
			image = this.Content.Load<Texture2D>("wabbit_alpha");
			font = this.Content.Load<SpriteFont> ("test");
			//TODO: use this.Content to load your game content here 
		}

		/// <summary>
		/// Allows the game to run logic such as updating the world,
		/// checking for collisions, gathering input, and playing audio.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Update (GameTime gameTime)
		{
			// For Mobile devices, this logic will close the Game when the Back button is pressed
			// Exit() is obsolete on iOS
			#if !__IOS__
			if (GamePad.GetState (PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
			    Keyboard.GetState ().IsKeyDown (Keys.Escape)) {
				Exit ();
			}
			#endif
			// TODO: Add your update logic here	
			TouchCollection touchCollection = TouchPanel.GetState();

			if (touchCollection.Count > 0)
			{
				addBunnies (1000);
			}

			foreach (Bunny bunny in bunnies) {
				bunny.posX += bunny.speedX;
				bunny.posY += bunny.speedY;
				bunny.speedY += gravity;

				if( bunny.posX > maxX )
				{
					bunny.speedX *= -1;
					bunny.posX = maxX;
				}
				else if( bunny.posX < minX )
				{
					bunny.speedX *= -1;
					bunny.posX = minX;
				}

				if( bunny.posY > maxY )
				{
					bunny.speedY *= -0.8f;
					bunny.posY = maxY;
					if( rnd.NextDouble() > 0.5f )
						bunny.speedY -= 3 + (float)rnd.NextDouble() * 4;
				}
				else if( bunny.posY < minY )
				{
					bunny.speedY = 0;
					bunny.posY = minY;
				}

			}
			base.Update (gameTime);
		}

		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw (GameTime gameTime)
		{

			var deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

			_frameCounter.Update(deltaTime);

			var fps = string.Format("FPS: {0}", _frameCounter.AverageFramesPerSecond);


			graphics.GraphicsDevice.Clear (Color.CornflowerBlue);
		
			//TODO: Add your drawing code here
			spriteBatch.Begin();
			foreach (Bunny bunny in bunnies) {
				spriteBatch.Draw (image, new Vector2(bunny.posX, bunny.posY));
			}
			spriteBatch.DrawString(font, fps, new Vector2(1, 1), Color.Black);
			spriteBatch.DrawString(font, ""+bunniesCount, new Vector2(this.GraphicsDevice.Viewport.Width-100, 1), Color.White);
			spriteBatch.End ();
			base.Draw (gameTime);
		}

		protected void addBunnies(int count) {
			for (int i = 0; i < count; i++) {
				Bunny b = new Bunny ();
				b.speedX = (float)rnd.NextDouble() * 5;
				b.speedY = (float)rnd.NextDouble() * 5 - 2.5f;
				bunnies.Add(b);
			}
			bunniesCount += count;
		}
	}
}

