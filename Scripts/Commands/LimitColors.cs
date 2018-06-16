using UnityEngine;

namespace Picture_Editor_v2.Scripts.Commands
{
	/// <summary>
	/// Clamps each of the color's rgb values to a set amount of colors
	/// </summary>
	public class LimitColors : EditCommand
	{
		/// <summary>
		/// Highest number of colors that can exist per each rgb value
		/// </summary>
		private readonly int _highestNumber;

		/// <summary>
		/// Initializes the LimitColors objects
		/// </summary>
		/// <param name="numColors">Number of colors to limit to</param>
		public LimitColors(int numColors)
		{
			_highestNumber = numColors - 1;
		}

		// Described in the inherited class
		public override Color GetPixel(int pos)
		{
			Color c = _getPixel(pos);
			c.r = Mathf.Round(c.r * _highestNumber) / _highestNumber;
			c.g = Mathf.Round(c.g * _highestNumber) / _highestNumber;
			c.b = Mathf.Round(c.b * _highestNumber) / _highestNumber;
			return c;
		}

	}
}
