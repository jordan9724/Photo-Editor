using System;
using UnityEngine;

namespace Picture_Editor_v2.Scripts.Commands
{
	/// <summary>
	/// Carves a perfect circle from the center to the closest edge
	/// </summary>
	public class CropCircle : Crop
	{
		/// <summary>
		/// Smallest distance from the center to the edge, squared
		/// </summary>
		private float _radiusSquared;
		/// <summary>
		/// Horizontal center of the image
		/// </summary>
		private int _centerX;
		/// <summary>
		/// Vertical center of the image
		/// </summary>
		private int _centerY;

		/// <inheritdoc />
		/// <summary>
		/// Keeps pixels within a distance of the radius
		/// </summary>
		protected override bool keepPixel(int pos)
		{
			int y = Mathf.FloorToInt((float)pos / _texture2DColor.Width);
			float xDist = Mathf.FloorToInt((float)pos - y * _texture2DColor.Width) - _centerX;
			float yDist = y - _centerY;

			return _radiusSquared >= xDist * xDist + yDist * yDist;
		}

		/// <inheritdoc />
		/// <summary>
		/// Initializes variables from _texture2DColor
		/// </summary>
		public override void Initialize()
		{
			float r = (_texture2DColor.Height < _texture2DColor.Width) ? _texture2DColor.Height / 2f : _texture2DColor.Width / 2f;
			_radiusSquared = r * r;
			_centerX = _texture2DColor.Width / 2;
			_centerY = _texture2DColor.Height / 2;
		}
	}
}
