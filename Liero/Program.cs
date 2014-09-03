using System;
using SFML.Graphics;
using SFML.Window;
using SFML.Audio;
using System.Runtime.InteropServices;

namespace Liero
{
	class MainClass
	{
		public static Random rand;
		static RenderWindow window;
		static DateTime startTime;
		public static Vector2f windowSize;

		public static void Main (string[] args)
		{
			PreRun ();
			LoadContentInitialize ();

			while (window.IsOpen()) {
				UpdateDraw (window);
				GC.Collect ();
				long mem = GC.GetTotalMemory (true);
				Console.WriteLine (mem);
			}
		}

		[DllImport("X11")]
		extern public static int XInitThreads();

		private static void PreRun ()
		{
			startTime = DateTime.Now;
			rand = new Random ();

			Console.WriteLine (System.Environment.OSVersion);

			if (IsLinux())
				XInitThreads ();

			if (!Shader.IsAvailable) {
				Console.WriteLine ("No shader available. Update your grafix drivers or something.");
				Environment.Exit(263);
			}
		}

		//http://stackoverflow.com/a/5117005/1364757
		public static bool IsLinux()
		{
			int p = (int) Environment.OSVersion.Platform;
			return (p == 4) || (p == 6) || (p == 128);
		}

		private static void LoadContentInitialize()
		{
			window = new RenderWindow(
				new VideoMode(640, 480), "new project!");

			windowSize = new Vector2f(640, 480);
			window.SetFramerateLimit(60);
			window.Closed += (a, b) => { window.Close(); };
			window.Size = new Vector2u(640, 480);
			window.Resized += (a, b) => {
				windowSize = new Vector2f(b.Width, b.Height);
			};

			Game.LoadContentInitialize ();
		}

		private static void UpdateDraw(RenderWindow window)
		{
			window.DispatchEvents();
			window.Clear();

			Game.UpdateDraw (window);

			window.Display();
		}

		private static Vector2f UnitMousePosition()
		{
			Vector2i mp = Mouse.GetPosition(window);
			Vector2f unit = new Vector2f(
				mp.X / windowSize.X,
				mp.Y / windowSize.Y
				);
			return unit;
		}

		//take a unit vector and a "size" vector, multiply each component together
		static Vector2f UnitMultiply(Vector2f input, Vector2f units)
		{
			return new Vector2f(input.X * units.X, input.Y * units.Y);
		}

		static Vector2f ToVector2f(Vector2i input)
		{
			return new Vector2f(input.X, input.Y);
		}

		static float SinceEpoch()
		{
			double since = (DateTime.Now - startTime).TotalSeconds;
			float fsince = (float)since;
			return fsince;
		}
	}

	static class Extensions
	{

	}
}