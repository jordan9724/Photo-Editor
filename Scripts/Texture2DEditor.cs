using System;
using System.IO;
using Picture_Editor_v2.Scripts.Commands;
using UnityEngine;

namespace Picture_Editor_v2.Scripts
{
	/// <summary>
	/// Houses the instructions of how to edit an image
	/// </summary>
	public class Texture2DEditor
	{
		/// <summary>
		/// The last edit command added
		/// </summary>
		private EditCommand _command;
		/// <summary>
		/// Contains information about the texture's colors
		/// </summary>
		private readonly Texture2DColor _texture2DColor;

		/// <summary>
		/// Sets the variables
		/// </summary>
		/// <param name="tex">Texture to be edited</param>
		public Texture2DEditor(Texture2D tex)
		{
			_texture2DColor = new Texture2DColor(tex);
			_command = _texture2DColor;
		}

		/// <summary>
		/// Adds a command, and sets it up
		/// </summary>
		/// <param name="command">Command to be added</param>
		public void AddCommand(EditCommand command)
		{
			command.SetPreviousGetPixel(_command.GetPixel);
			command.SetTexture2DColor(_texture2DColor);
			command.Initialize();
			_command = command;
		}

		/// <summary>
		/// Gets the edited version of the original texture
		/// </summary>
		/// <param name="trim">True - Trims clear pixels off the sides</param>
		/// <param name="fileName">If not null, writes a PNG file to the game data folder. Exclude '.png' in the file name</param>
		/// <returns></returns>
		public Texture2D GetTexture2D(bool trim=true, string fileName=null)
		{
			Color[] newTexColor = new Color[_texture2DColor.Width * _texture2DColor.Height];
			for (int y = 0; y < _texture2DColor.Height; y++)
			for (int x = 0; x < _texture2DColor.Width; x++)
			{
				int pos = (y * _texture2DColor.Width) + x;
				newTexColor[pos] = _command.GetPixel(pos);
			}
			int width = _texture2DColor.Width;
			int height = _texture2DColor.Height;
			if (trim)
				newTexColor = this.trim(newTexColor, out width, out height);
			Texture2D newTex = new Texture2D(width, height);
			newTex.SetPixels(newTexColor);
			newTex.Apply();
			if (fileName != null)
				SaveFile(fileName, newTex);
			return newTex;
		}

		/// <summary>
		/// Trims clear pixels off the sides
		/// </summary>
		/// <param name="colors">Original colors</param>
		/// <param name="width">Sets new width</param>
		/// <param name="height">Sets new height</param>
		/// <returns>New set of colors after it is trimmed</returns>
		private Color[] trim(Color[] colors, out int width, out int height)
		{
			Vector4 bounds = getBounds(colors);
			width = (int) (bounds.z - bounds.x) + 1;
			height = (int) (bounds.w - bounds.y) + 1;
			int length = width * height;
			Color[] newColors = new Color[length];

			for (int y = (int)bounds.y; y <= (int)bounds.w; y++)
			for(int x = (int)bounds.x; x <= (int)bounds.z; x++)
			{
				int origPos = x + y * _texture2DColor.Width;
				int newPos = (x - (int)bounds.x) + (y - (int)bounds.y) * width;
				newColors[newPos] = colors[origPos];
			}

			return newColors;
		}

		/// <summary>
		/// Retrieves the new bounds of the image, cutting off rows or columns of clear pixels
		/// </summary>
		/// <param name="colors">Set of colors in the original image</param>
		/// <returns>New bounds of the image to be trimmed</returns>
		/// <exception cref="ArgumentException">Image must contain at least one pixel that isn't the same as the local variable colorToTrim</exception>
		private Vector4 getBounds(Color[] colors)
		{
			// x - left : y - bottom : z - right : w - top
			Vector4 bounds = new Vector4(float.PositiveInfinity, float.PositiveInfinity, 0, 0);
			Color colorToTrim = Color.clear;

			for (int y = 0; y < _texture2DColor.Height; y++)
			{
				int smallestX = int.MaxValue;
				int largestX = int.MinValue;
				for (int x = 0; x < _texture2DColor.Width; x++)
				{
					int pos = x + y * _texture2DColor.Width;

					bool smallestXFound = smallestX != int.MaxValue;
					bool isNotColorToTrim = colors[pos] != colorToTrim;

					if (smallestXFound && isNotColorToTrim)
						largestX = x;
					else if (!smallestXFound && isNotColorToTrim)
					{
						smallestX = x;
						largestX = x;
					}
				}

				bool justFoundSmallerX = smallestX < bounds.x;
				bool justFoundLargerX = largestX > bounds.z;
				bool alreadyFoundBottom = !float.IsPositiveInfinity(bounds.y);
				bool rowContainsColorNotToTrim = smallestX != int.MaxValue;

				if (justFoundSmallerX) bounds.x = smallestX;
				if (justFoundLargerX) bounds.z = largestX;

				if (alreadyFoundBottom && rowContainsColorNotToTrim) bounds.w = y;
				else if (!alreadyFoundBottom && rowContainsColorNotToTrim)
				{
					bounds.y = y;
					bounds.w = y;
				}
			}

			if (float.IsPositiveInfinity(bounds.x)) throw new ArgumentException("There is nothing left of the image to trim");

			return bounds;
		}

		/// <summary>
		/// Writes a PNG file to the game data folder
		/// </summary>
		/// <param name="fileName">Name of the file (exclude the .png)</param>
		/// <param name="tex">Texture to save</param>
		public void SaveFile(string fileName, Texture2D tex)
		{
			// Encode texture into PNG
			byte[] bytes = tex.EncodeToPNG();

			File.WriteAllBytes(Application.dataPath + "/" + fileName +".png", bytes);
		}

	}
}
