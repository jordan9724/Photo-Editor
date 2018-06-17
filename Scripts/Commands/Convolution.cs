using System;
using UnityEngine;

namespace Picture_Editor_v2.Scripts.Commands
{
	/// <summary>
	/// Uses a 3x3 convolution to find a new pixel based off of the surrounding pixels
	/// </summary>
	public class Convolution : EditCommand
	{

		/// <summary>
		/// Contains all the edited colors for the image
		/// </summary>
		private Color[] _texColors;

		/// <summary>
		/// Convolution used for this command
		/// </summary>
		private readonly float[,] _convolution;

		/// <summary>
		/// Used to offset the rgb values
		/// </summary>
		private readonly float _bias;

		/// <summary>
		/// Initializes the convolution
		/// </summary>
		/// <param name="convolution">Filter used to adjust a pixel based on the pixels around it</param>
		/// <param name="bias">Added to each RGB value after convolution is calculated</param>
		/// <exception cref="ArgumentException">Convolution must be a 3x3 array</exception>
		public Convolution(float[,] convolution, float bias=0)
		{
			if (convolution.GetLength(0) != 3 || convolution.GetLength(1) != 3)
				throw new ArgumentException("Convolution must be a 3x3 array");
			_convolution = convolution;
			_bias = bias;
		}

		// Described in the inherited class
		public override Color GetPixel(int pos)
		{
			if (_texColors == null)
			{
				_texColors = new Color[_texture2DColor.Width * _texture2DColor.Height];
				for (int i = 0; i < _texColors.Length; i++)
					_texColors[i] = _getPixel(i);
				process();
			}
			return _texColors[pos];
		}

		/// <summary>
		/// Finds all the new pixels at once
		/// </summary>
		private void process()
		{
			int width = _texture2DColor.Width;
			int height = _texture2DColor.Height;
			Color[] oldColors = new Color[width * height];
			for (int i = 0; i < _texColors.Length; i++)
				oldColors[i] = _texColors[i];

			for (int i = 0; i < _texColors.Length; i++)
			{
				int y = Mathf.FloorToInt((float)i / _texture2DColor.Width);
				int x = Mathf.FloorToInt((float)i - y * _texture2DColor.Width);
				int xl = (x == 0) ? x : x - 1;
				int xr = (x == width - 1) ? x : x + 1;
				int yt = (y == height - 1) ? y : y + 1;
				int yb = (y == 0) ? y : y - 1;

				int tli = xl + yt * width;
				int tmi = x + yt * width;
				int tri = xr + yt * width;

				int li = xl + y * width;
				int mi = x + y * width;
				int ri = xr + y * width;

				int bli = xl + yb * width;
				int bmi = x + yb * width;
				int bri = xr + yb * width;

				_texColors[i].r = oldColors[tli].r * _convolution[0, 0] + oldColors[tmi].r * _convolution[0, 1] +
				                  oldColors[tri].r * _convolution[0, 2] +
				                  oldColors[li].r * _convolution[1, 0] + oldColors[mi].r * _convolution[1, 1] +
				                  oldColors[ri].r * _convolution[1, 2] +
				                  oldColors[bli].r * _convolution[2, 0] + oldColors[bmi].r * _convolution[2, 1] +
				                  oldColors[bri].r * _convolution[2, 2] + _bias;
				_texColors[i].g = oldColors[tli].g * _convolution[0, 0] + oldColors[tmi].g * _convolution[0, 1] +
				                  oldColors[tri].g * _convolution[0, 2] +
				                  oldColors[li].g * _convolution[1, 0] + oldColors[mi].g * _convolution[1, 1] +
				                  oldColors[ri].g * _convolution[1, 2] +
				                  oldColors[bli].g * _convolution[2, 0] + oldColors[bmi].g * _convolution[2, 1] +
				                  oldColors[bri].g * _convolution[2, 2] + _bias;
				_texColors[i].b = oldColors[tli].b * _convolution[0, 0] + oldColors[tmi].b * _convolution[0, 1] +
				                  oldColors[tri].b * _convolution[0, 2] +
				                  oldColors[li].b * _convolution[1, 0] + oldColors[mi].b * _convolution[1, 1] +
				                  oldColors[ri].b * _convolution[1, 2] +
				                  oldColors[bli].b * _convolution[2, 0] + oldColors[bmi].b * _convolution[2, 1] +
				                  oldColors[bri].b * _convolution[2, 2] + _bias;

				ClampColor(ref _texColors[i]);
			}
		}

	}
}
