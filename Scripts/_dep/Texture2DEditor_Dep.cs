using System;
using UnityEngine;

namespace Picture_Editor_v2.Scripts
{
	public class Texture2DEditor_Dep
	{

//		private readonly GaussianBlur _gaussianBlur;

		private readonly TextureColors _orig;
		private readonly TextureColors _manip;

        public Texture2DEditor_Dep(Texture2D tex)
        {
	        if (tex == null)
		        throw new ArgumentException("The texture cannot be null");

	        _orig = new TextureColors(tex);
	        _manip = new TextureColors(tex);

//	        _gaussianBlur = new GaussianBlur(_manip);

        }

		private int matrixPosToVectorPos(int x, int y)
		{
			return (x * _manip.Width) + y;
		}

		#region Modifiers
		public void Negative()
		{
			for (int i = 0; i < _manip.Height; i++)
			for (int k = 0; k < _manip.Width; k++)
			{
				int pos = matrixPosToVectorPos(i, k);
				_manip.Red[pos] = 1f - _manip.Red[pos];
				_manip.Green[pos] = 1f - _manip.Green[pos];
				_manip.Blue[pos] = 1f - _manip.Blue[pos];
			}
		}

		public void Multiply(float s, bool multiplyAlpha=false)
		{
			for (int i = 0; i < _manip.Height; i++)
			for (int k = 0; k < _manip.Width; k++)
			{
				int pos = matrixPosToVectorPos(i, k);
				_manip.Red[pos] *= s;
				_manip.Green[pos] *= s;
				_manip.Blue[pos] *= s;

				if (multiplyAlpha)
					_manip.Alpha[pos] *= s;
			}
		}

		public void Filter(float[,] f)
		{
			int rows = f.GetLength(0);
			int cols = f.GetLength(1);
			bool is3X3 = (rows == 3) && (cols == 3);
			bool is4X4 = (rows == 4) && (cols == 4); // Manipulate alpha

			if (!(is3X3 || is4X4)) throw new ArgumentException("The filter must be a 3x3 array or a 4x4 array.");

			float[][] colorRefs = is3X3 ?
				new[] { _manip.Red, _manip.Green, _manip.Blue } :
				new[] { _manip.Red, _manip.Green, _manip.Blue, _manip.Alpha };

			for (int i = 0; i < _manip.Height; i++)
			for (int k = 0; k < _manip.Width; k++)
			{
				int pos = matrixPosToVectorPos(i, k);
				float r = _manip.Red[pos];
				float g = _manip.Green[pos];
				float b = _manip.Blue[pos];
				float a = _manip.Alpha[pos];

				for (int j = 0; j < colorRefs.Length; j++)
				{
					float redWeight = r * f[j, 0];
					float greenWeight = g * f[j, 1];
					float blueWeight = b * f[j, 2];
					float alphaWeight = is3X3 ? 0 : a * f[j, 3];

					colorRefs[j][pos] = redWeight + greenWeight + blueWeight + alphaWeight;
				}
			}
		}

		public void Blur(int radial)
		{
//			_gaussianBlur.Process(radial);
		}

		public void Reset()
		{
			_orig.Red.CopyTo(_manip.Red, 0);
			_orig.Green.CopyTo(_manip.Green, 0);
			_orig.Blue.CopyTo(_manip.Blue, 0);
			_orig.Alpha.CopyTo(_manip.Alpha, 0);
		}
		#endregion

		private Color[] ToColorArray()
		{
			Color[] result = new Color[_manip.Height * _manip.Width];
			for (int i = 0; i < _manip.Height * _manip.Width; i++)
				result[i] = new Color(_manip.Red[i], _manip.Green[i], _manip.Blue[i], _manip.Alpha[i]);
			return result;
		}

		public Texture2D ToTexture2D()
		{
			Texture2D result = new Texture2D(_manip.Width, _manip.Height);
			result.SetPixels(ToColorArray());
			result.Apply();
			return result;
		}

		public override string ToString()
		{
			string result = "";
			for (int i = 0; i < _manip.Height; i++)
			{
				for (int k = 0; k < _manip.Width; k++)
					result += string.Format(
						"({0}, {1}, {2}, {3})",
						_manip.Red,
						_manip.Green,
						_manip.Blue,
						_manip.Alpha
					);
				result += "\n";
			}
			return result;
		}

	}
}
