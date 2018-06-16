using UnityEngine;

namespace Picture_Editor_v2.Scripts.Commands
{
	/// <summary>
	/// Crops an image based off of another black and white image
	/// </summary>
	public class CustomCrop : Crop
	{

		/// <summary>
		/// Colors associated with the black and white image
		/// </summary>
		private readonly Color[] _customColors;
		/// <summary>
		/// Width of the black and white image
		/// </summary>
		private readonly int _width;
		/// <summary>
		/// Height of the black and white image
		/// </summary>
		private readonly int _height;
		/// <summary>
		/// Custom image width / actual image width
		/// </summary>
		private float _widthRatio;
		/// <summary>
		/// Custom image height / actual image height
		/// </summary>
		private float _heightRatio;

		/// <summary>
		/// Takes the black and white image and extracts needed information
		/// </summary>
		/// <param name="tex">Black and white image</param>
		public CustomCrop(Texture2D tex)
		{
			_customColors = tex.GetPixels();
			_width = tex.width;
			_height = tex.height;
		}

		// Described in the inherited class
		protected override bool keepPixel(int pos)
		{
			int y = Mathf.FloorToInt((float)pos / _texture2DColor.Width);
			int customX = (int)(Mathf.FloorToInt((float)pos - y * _texture2DColor.Width) * _widthRatio);
			int customY = (int)(y * _heightRatio);

			int customPos = customY * _width + customX;

			return _customColors[customPos] != Color.black;
		}

		// Described in the inherited class
		public override void Initialize()
		{
			_widthRatio = (float)(_width - 1) / (_texture2DColor.Width - 1);
			_heightRatio = (float)(_height - 1) / (_texture2DColor.Height - 1);
		}

	}
}
