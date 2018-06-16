using UnityEngine;

namespace Picture_Editor_v2.Scripts
{
	public class TextureColors {
		
		public float[] Red { get; }
		public float[] Green { get; }
		public float[] Blue { get; }
		public float[] Alpha { get; }

		public int Width { get; }
		public int Height { get; }
		
		public TextureColors(Texture2D tex)
		{
			Width = tex.width;
			Height = tex.height;
			
			int length = Width * Height;
			Red = new float[length];
			Green = new float[length];
			Blue = new float[length];
			Alpha = new float[length];

			Color[] pixels = tex.GetPixels();

			for (int i = 0; i < Width * Height; i++)
			{
				Color px = pixels[i];
				Red[i] = px.r;
				Green[i] = px.g;
				Blue[i] = px.b;
				Alpha[i] = px.a;
			}
		}
		
	}
}
