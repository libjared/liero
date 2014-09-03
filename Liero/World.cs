using System;
using SFML.Graphics;

namespace Liero
{
	public class World
	{
		//design: update the image directly, then when spit is called,
		//        update the texture with the img

		public int width, height;
		public Texture texture;
		public Image img;

		public World(int width, int height)
		{
			this.width = width;
			this.height = height;
			this.texture = new Texture ((uint)width, (uint)height);
			this.img = new Image ((uint)width, (uint)height);
		}

		public void GenMap()
		{
			Color[,] buff = new Color[height, width];

			for (var y = 0; y < height; y++) {
				for (var x = 0; x < width; x++) {
					double randomFactor = (MainClass.rand.NextDouble () - 0.5) * 8.0;
					byte r = (byte)(142 + randomFactor);
					byte g = (byte)(80 + randomFactor);
					byte b = (byte)(17 + randomFactor);
					byte a = (byte)255;

					buff [x, y] = new Color (r,g,b,a);
				}
			}

			img = new Image (buff);
		}

		public void DeletePixel(int y, int x)
		{
			Color nothing = new Color (0, 0, 0, 0);
			img.SetPixel ((uint)x, (uint)y, nothing);
		}

		public bool IsInBounds(int y, int x)
		{
			return x >= 0 && x < width && y >= 0 && y < height;
		}

		public Texture Spit()
		{
			texture.Update (img);
			return texture;
		}
	}
}

