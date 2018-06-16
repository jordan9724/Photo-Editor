using UnityEngine;

namespace Picture_Editor_v2.Scripts.Commands
{
	/// <summary>
	/// Inverts the colors
	/// </summary>
	public class Negative : EditCommand {

		// Described in the inherited class
		public override Color GetPixel(int pos)
		{
			Color color = _getPixel(pos);
			color.r = 1 - color.r;
			color.g = 1 - color.g;
			color.b = 1 - color.b;
			return color;
		}

	}
}
