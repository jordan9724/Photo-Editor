using System;
using UnityEngine;

namespace Picture_Editor_v2.Scripts.Commands
{
	/// <summary>
	/// Base class for crop classes
	/// </summary>
	public abstract class Crop : EditCommand
	{

		/// <summary>
		/// Checks if the pixel is to be cropped or not
		/// </summary>
		/// <param name="pos">Position of the pixel</param>
		/// <returns>True if the pixel should be kept, False otherwise</returns>
		protected abstract bool keepPixel(int pos);

		// Described in the inherited class
		public override Color GetPixel(int pos)
		{
			return keepPixel(pos) ?
				_getPixel(pos) : Color.clear;
		}

	}
}
