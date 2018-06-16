using System;
using UnityEngine;

namespace Picture_Editor_v2.Scripts.Commands
{
	/// <summary>
	/// Base class needed to create a command to edit an image
	/// </summary>
	public abstract class EditCommand
	{
		/// <summary>
		/// Previous get pixel function
		/// </summary>
		protected Func<int, Color> _getPixel;
		/// <summary>
		/// Contains information about the texture's colors
		/// </summary>
		protected Texture2DColor _texture2DColor;

		/// <summary>
		/// Sets the previous get pixel function
		/// </summary>
		/// <param name="getPixel"></param>
		public void SetPreviousGetPixel(Func<int, Color> getPixel)
		{
			_getPixel = getPixel;
		}

		/// <summary>
		/// Sets the texture2DColor variable for information about
		///  the texture's colors
		/// </summary>
		/// <param name="texture2DColor">Texture's color information</param>
		public void SetTexture2DColor(Texture2DColor texture2DColor)
		{
			_texture2DColor = texture2DColor;
		}

		/// <summary>
		/// Keeps the color's values in the range of 0 to 1
		/// </summary>
		/// <param name="c">Color to clamp</param>
		protected static void ClampColor(ref Color c)
		{
			c.r = Mathf.Clamp(c.r, 0, 1);
			c.g = Mathf.Clamp(c.g, 0, 1);
			c.b = Mathf.Clamp(c.b, 0, 1);
			c.a = Mathf.Clamp(c.a, 0, 1);
		}

		/// <summary>
		/// Method used to apply the changes to a pixel
		/// </summary>
		/// <param name="pos">The position of the pixel</param>
		/// <returns>Color of a pixel after changes are made</returns>
		public abstract Color GetPixel(int pos);

		/// <summary>
		/// Used to initialize variables which need information
		///  unavailable at the time of instantiating
		/// </summary>
		public virtual void Initialize() {}

	}
}
