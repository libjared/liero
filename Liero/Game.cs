using System;
using SFML.Graphics;
using SFML.Window;
using SFML.Audio;

namespace Liero
{
	public static class Game
	{
		static World world;
		static Sprite worldSprite;

		public static void LoadContentInitialize ()
		{
			world = new World (512, 512);
			world.GenMap ();
			worldSprite = new Sprite ();
		}

		public static void UpdateDraw (RenderWindow window)
		{
			View viewPlayerOne = new View (window.DefaultView);
			viewPlayerOne.Size /= 4;
			viewPlayerOne.Center = new Vector2f (64, 256);
			viewPlayerOne.Viewport = new FloatRect (0f, 0f, 0.5f, 1f);
			viewPlayerOne.Size = new Vector2f (viewPlayerOne.Size.X / 2f, viewPlayerOne.Size.Y); //fix aspect ratio

			View viewPlayerTwo = new View (window.DefaultView);
			viewPlayerTwo.Size /= 4;
			viewPlayerTwo.Center = new Vector2f(128+64, 256);
			viewPlayerTwo.Viewport = new FloatRect (0.5f, 0f, 0.5f, 1f);
			viewPlayerTwo.Size = new Vector2f (viewPlayerTwo.Size.X / 2f, viewPlayerTwo.Size.Y); //fix aspect ratio

			if (Mouse.IsButtonPressed(Mouse.Button.Left)) {
				var transMousePos = window.MapPixelToCoords (Mouse.GetPosition (window), viewPlayerOne);
				if (world.IsInBounds ((int)transMousePos.Y, (int)transMousePos.X))
					world.DeletePixel ((int)transMousePos.Y, (int)transMousePos.X);
			}

			if (Mouse.IsButtonPressed(Mouse.Button.Right)) {
				var transMousePos = window.MapPixelToCoords (Mouse.GetPosition (window), viewPlayerTwo);
					if (world.IsInBounds ((int)transMousePos.Y, (int)transMousePos.X))
						world.DeletePixel ((int)transMousePos.Y, (int)transMousePos.X);
			}
			
			worldSprite.Texture = world.Spit ();

			window.SetView (viewPlayerOne);
			window.Draw (worldSprite);

			window.SetView (viewPlayerTwo);
			window.Draw (worldSprite);
		}
	}
}

