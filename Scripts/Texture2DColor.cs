using UnityEngine;

namespace Picture_Editor_v2.Scripts
{
	/// <summary>
	/// Contains the information needed to edit a texture
	/// </summary>
	public class Texture2DColor
	{
		/// <summary>
		/// All the colors in the texture
		/// </summary>
		private readonly Color[] _texColors;

		/// <summary>
		/// Width of the texture
		/// </summary>
		public int Width { get; }
		/// <summary>
		/// Height of the texture
		/// </summary>
		public int Height { get; }

		/// <summary>
		/// Initializes variables
		/// </summary>
		/// <param name="tex">Texture to be edited</param>
		public Texture2DColor(Texture2D tex)
		{
			Width = tex.width;
			Height = tex.height;
			_texColors = tex.GetPixels();
		}

		/// <summary>
		/// Gets the unedited color of a pixel
		/// </summary>
		/// <param name="pos">Position of the color</param>
		/// <returns>Color at the location</returns>
		public Color GetPixel(int pos)
		{
			return _texColors[pos];
		}

	}
}
