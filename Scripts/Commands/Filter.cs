using System;
using UnityEngine;

namespace Picture_Editor_v2.Scripts.Commands
{
	/// <summary>
	/// Uses percentages on a scale of 0 - 1 to determine a new pixel off of its rgb values
	/// </summary>
	public class Filter : EditCommand
	{
		/// <summary>
		/// Holds either a 3x3 or a 4x4 array of the filter to apply
		/// </summary>
		private readonly float[,] _filter;
		/// <summary>
		/// True if it is a 3x3 array, false if 4x4 array
		/// </summary>
		private readonly bool _is3X3;

		/// <summary>
		/// Initializes the filter
		/// </summary>
		/// <param name="f">Filter used to adjust a pixel based off of ratios of its rgb values</param>
		/// <exception cref="ArgumentException">Filter must be a 3x3 array or a 4x4 array</exception>
		public Filter(float[,] f)
		{
			int rows = f.GetLength(0);
			int cols = f.GetLength(1);
			bool is3X3 = (rows == 3) && (cols == 3);
			bool is4X4 = (rows == 4) && (cols == 4); // Manipulate alpha

			if (!(is3X3 || is4X4)) throw new ArgumentException("The filter must be a 3x3 array or a 4x4 array.");

			_filter = f;
			_is3X3 = is3X3;
		}

		// Described in the inherited class
		public override Color GetPixel(int pos)
		{
			Color color = _getPixel(pos);

			if (_is3X3)
				Handle3X3(ref color);
			else
				Handle4X4(ref color);

			ClampColor(ref color);
			return color;
		}

		/// <summary>
		/// Handles a 3x3 filter
		/// </summary>
		/// <param name="c">Reference to a color that will be edited</param>
		private void Handle3X3(ref Color c)
		{
			float r = c.r;
			float g = c.g;
			float b = c.b;

			for (int j = 0; j < 3; j++)
			{
				float redWeight = r * _filter[j, 0];
				float greenWeight = g * _filter[j, 1];
				float blueWeight = b * _filter[j, 2];

				float newColor = redWeight + greenWeight + blueWeight;

				switch (j)
				{
					case 0:
						c.r = newColor;
					break;
					case 1:
						c.g = newColor;
					break;
					case 2:
						c.b = newColor;
					break;
				}
			}
		}

		/// <summary>
		/// Handles a 4x4 filter
		/// </summary>
		/// <param name="c">Reference to a color that will be edited</param>
		private void Handle4X4(ref Color c)
		{
			float r = c.r;
			float g = c.g;
			float b = c.b;
			float a = c.a;

			for (int j = 0; j < 4; j++)
			{
				float redWeight = r * _filter[j, 0];
				float greenWeight = g * _filter[j, 1];
				float blueWeight = b * _filter[j, 2];
				float alphaWeight = a * _filter[j, 3];

				float newColor = redWeight + greenWeight + blueWeight + alphaWeight;

				switch (j)
				{
					case 0:
						c.r = newColor;
					break;
					case 1:
						c.g = newColor;
					break;
					case 2:
						c.b = newColor;
					break;
					case 3:
						c.a = newColor;
					break;
				}
			}
		}

	}
}
