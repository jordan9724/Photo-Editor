using UnityEngine;

namespace Picture_Editor_v2.Scripts.Commands
{
	/// <summary>
	/// Creates a vignette around the image
	/// </summary>
	public class Vignette : EditCommand
	{
		/// <summary>
		/// Distance to begin vignette
		/// </summary>
		private float _beginDistance;
		/// <summary>
		/// Distance to end vignette
		/// </summary>
		private float _endDistance;
		/// <summary>
		/// If it should inverse the vignette
		/// </summary>
		private readonly bool _backwards;
		/// <summary>
		/// The middle of the width
		/// </summary>
		private int _centerX;
		/// <summary>
		/// The middle of the height
		/// </summary>
		private int _centerY;

		/// <summary>
		/// Initializes the Vignette objects
		/// </summary>
		/// <param name="begin">Where to begin the vignette</param>
		/// <param name="end">Where to end the vignette</param>
		/// <param name="backwards">Should the vignette be backwords</param>
		public Vignette(float begin=0.5f, float end=1, bool backwards=false)
		{
			_beginDistance = begin;
			_endDistance = end;
			_backwards = backwards;
		}

		// Described in the inherited class
		public override Color GetPixel(int pos)
		{
			int y = Mathf.FloorToInt((float)pos / _texture2DColor.Width);
			float xDist = Mathf.FloorToInt((float)pos - y * _texture2DColor.Width) - _centerX;
			float yDist = y - _centerY;
			float dist = Mathf.Sqrt(xDist * xDist + yDist * yDist);

			bool isBeyond = dist > _beginDistance;
			bool isWithin = dist < _endDistance;
			bool isInRange = isBeyond && isWithin;

			if (!isInRange)
				return (isBeyond) ? Color.black : _getPixel(pos);

			float intensity = (!_backwards) ?
				(dist - _endDistance) / (_beginDistance - _endDistance) :
				(dist - _beginDistance) / (_endDistance - _beginDistance);

			Color c = _getPixel(pos);
			c.r *= intensity;
			c.g *= intensity;
			c.b *= intensity;
			return c;
		}

		// Described in the inherited class
		public override void Initialize()
		{
			float r = (_texture2DColor.Height < _texture2DColor.Width) ? _texture2DColor.Height / 2f : _texture2DColor.Width / 2f;

			_beginDistance *= r;
			_endDistance *= r;

			_centerX = _texture2DColor.Width / 2;
			_centerY = _texture2DColor.Height / 2;
		}

	}
}
