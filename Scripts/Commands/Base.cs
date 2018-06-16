using UnityEngine;

namespace Picture_Editor_v2.Scripts.Commands
{
	/// <inheritdoc />
	/// <summary>
	/// A simple class to simply pass on the get pixel call
	/// </summary>
	public class Base : EditCommand
	{

		/// <inheritdoc />
		/// <summary>
		/// Passes to the previous command
		/// </summary>
		public override Color GetPixel(int pos)
		{
			return _getPixel(pos);
		}

	}
}
