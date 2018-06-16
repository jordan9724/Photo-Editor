using UnityEngine;

namespace Picture_Editor_v2.Scripts.Commands
{
	/// <summary>
	/// Carves an ellipse out of the image, keeping the original width and height
	/// </summary>
	public class EllipseCrop : Crop
	{

		/// <summary>
		/// Half of the width, squared
		/// </summary>
		private float _radiusWidthSquared;
		/// <summary>
		/// Half of the height, squared
		/// </summary>
		private float _radiusHeightSquared;
		/// <summary>
		/// Horizontal center of the image
		/// </summary>
		private int _centerX;
		/// <summary>
		/// Vertical venter of the image
		/// </summary>
		private int _centerY;

		// Described in the inherited class
		protected override bool keepPixel(int pos)
		{
			int y = Mathf.FloorToInt((float)pos / _texture2DColor.Width);
			float xDist = Mathf.FloorToInt((float)pos - y * _texture2DColor.Width) - _centerX;
			float yDist = y - _centerY;

			return 1 >= xDist * xDist / _radiusWidthSquared + yDist * yDist / _radiusHeightSquared;
		}

		// Described in the inherited class
		public override void Initialize()
		{
			float radiusWidth = _texture2DColor.Width / 2f;
			float radiusHeight = _texture2DColor.Height / 2f;
			_radiusWidthSquared = radiusWidth * radiusWidth;
			_radiusHeightSquared = radiusHeight * radiusHeight;
			_centerX = _texture2DColor.Width / 2;
			_centerY = _texture2DColor.Height / 2;
		}

	}
}
