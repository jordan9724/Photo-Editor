using UnityEngine;

namespace Picture_Editor_v2.Scripts.Commands
{
	/// <summary>
	/// Carves a square starting from the center of the image
	/// </summary>
	public class CropSquare : Crop
	{
		/// <summary>
		/// Left side of the square
		/// </summary>
		private int _startX;
		/// <summary>
		/// Bottom side of the square
		/// </summary>
		private int _startY;
		/// <summary>
		/// Right side of the square
		/// </summary>
		private int _endX;
		/// <summary>
		/// Top side of the square
		/// </summary>
		private int _endY;

		// Described in the inherited class
		protected override bool keepPixel(int pos)
		{
			int y = Mathf.FloorToInt((float)pos / _texture2DColor.Width);
			float x = Mathf.FloorToInt((float)pos - y * _texture2DColor.Width);

			return (y >= _startY) && (y <= _endY) && (x >= _startX) && (x <= _endX);
		}

		// Described in the inherited class
		public override void Initialize()
		{
			float r = (_texture2DColor.Height < _texture2DColor.Width) ? _texture2DColor.Height / 2f : _texture2DColor.Width / 2f;
			_startX = (_texture2DColor.Height > _texture2DColor.Width) ? 0 : (int)((_texture2DColor.Width - (r * 2)) / 2);
			_startY = (_texture2DColor.Height > _texture2DColor.Width) ? (int)((_texture2DColor.Height - (r * 2)) / 2) : 0;
			_endX = _startX + (int)(r * 2);
			_endY = _startY + (int)(r * 2);
		}

	}
}
