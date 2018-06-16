using UnityEngine;

namespace Picture_Editor_v2.Scripts.Commands
{
	/// <summary>
	/// Multiplies each value in each color's rgb values
	/// </summary>
	public class Multiplier : EditCommand
	{
		/// <summary>
		/// Amount to multiply each value by
		/// </summary>
		private readonly float _multiplier;

		/// <summary>
		/// Initializes each Multiplier object
		/// </summary>
		/// <param name="m">What each rgb value should be multiplied by</param>
		public Multiplier(float m)
		{
			_multiplier = m;
		}

		// Described in the inherited class
		public override Color GetPixel(int pos)
		{
			Color color = _getPixel(pos);
			color.r = color.r * _multiplier;
			color.g = color.g * _multiplier;
			color.b = color.b * _multiplier;
			ClampColor(ref color);
			return color;
		}

	}
}
