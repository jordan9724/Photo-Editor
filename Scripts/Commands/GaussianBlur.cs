using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Picture_Editor_v2.Scripts.Commands
{
    /*
     * Adopted from: https://github.com/mdymel/superfastblur/blob/master/SuperfastBlur/GaussianBlur.cs
     */
    /// <summary>
    /// Blurs an image using the gaussian algorithm
    /// </summary>
    public class GaussianBlur : EditCommand
    {
        /// <summary>
        /// Intensity of the blur
        /// </summary>
        private readonly int _radial;
        /// <summary>
        /// Array of all the edited colors
        /// </summary>
        private Color[] _texColors;


        /// <summary>
        /// Initializes the gaussian blur
        /// </summary>
        /// <param name="radial">The intensity of the blur</param>
        public GaussianBlur(int radial)
        {
            _radial = radial;
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

        // Methods taken from the url above
        #region Blur Algorithm
        /// <summary>
        /// Finds all the new pixels at once
        /// </summary>
        private void process()
        {
            int length = _texture2DColor.Width * _texture2DColor.Height;

            float[] red = new float[length];
            float[] green = new float[length];
            float[] blue = new float[length];
            float[] alpha = new float[length];

            for (int i = 0; i < length; i++)
            {
                red[i] = _texColors[i].r;
                green[i] = _texColors[i].g;
                blue[i] = _texColors[i].b;
                alpha[i] = _texColors[i].a;
            }

            float[] newRed = new float[length];
            float[] newGreen = new float[length];
            float[] newBlue = new float[length];
            float[] newAlpha = new float[length];

            gaussBlur_4(red, newRed, _radial);
            gaussBlur_4(green, newGreen, _radial);
            gaussBlur_4(blue, newBlue, _radial);
            gaussBlur_4(alpha, newAlpha, _radial);

            for (int i = 0; i < length; i++)
            {
                _texColors[i].r = newRed[i];
                _texColors[i].g = newGreen[i];
                _texColors[i].b = newBlue[i];
                _texColors[i].a = newAlpha[i];
            }
        }

        private void gaussBlur_4(float[] source, float[] dest, int r)
        {
            int[] bxs = boxesForGauss(r, 3);
            boxBlur_4(source, dest, _texture2DColor.Width, _texture2DColor.Height, (bxs[0] - 1) / 2);
            boxBlur_4(dest, source, _texture2DColor.Width, _texture2DColor.Height, (bxs[1] - 1) / 2);
            boxBlur_4(source, dest, _texture2DColor.Width, _texture2DColor.Height, (bxs[2] - 1) / 2);
        }

        private int[] boxesForGauss(int sigma, int n)
        {
            float wIdeal = Mathf.Sqrt((12 * sigma * sigma / n) + 1);
            int wl = (int) Mathf.Floor(wIdeal);
            if (wl % 2 == 0) wl--;
            int wu = wl + 2;

            float mIdeal = (float) (12 * sigma * sigma - n * wl * wl - 4 * n * wl - 3 * n) / (-4 * wl - 4);
            float m = Mathf.Round(mIdeal);

            List<int> sizes = new List<int>();
            for (int i = 0; i < n; i++) sizes.Add(i < m ? wl : wu);
            return sizes.ToArray();
        }

        private void boxBlur_4(float[] source, float[] dest, int w, int h, int r)
        {
            for (int i = 0; i < source.Length; i++) dest[i] = source[i];
            boxBlurH_4(dest, source, w, h, r);
            boxBlurT_4(source, dest, w, h, r);
        }

        private void boxBlurH_4(float[] source, float[] dest, int w, int h, int r)
        {
            float iar = (float) 1 / (r + r + 1);
            for(int i = 0; i < h; i++)
            {
                int ti = i * w;
                int li = ti;
                int ri = ti + r;
                float fv = source[ti];
                float lv = source[ti + w - 1];
                float val = (r + 1) * fv;
                for (int j = 0; j < r; j++) val += source[ti + j];
                for (int j = 0; j <= r; j++)
                {
                    val += source[ri++] - fv;
                    dest[ti++] = val * iar;
                }
                for (int j = r + 1; j < w - r; j++)
                {
                    val += source[ri++] - dest[li++];
                    dest[ti++] = val * iar;
                }
                for (int j = w - r; j < w; j++)
                {
                    val += lv - source[li++];
                    dest[ti++] = val * iar;
                }
            }
        }

        private void boxBlurT_4(float[] source, float[] dest, int w, int h, int r)
        {
            float iar = (float) 1 / (r + r + 1);
            for (int i = 0; i < w; i++)
            {

                int ti = i;
                int li = ti;
                int ri = ti + r * w;
                float fv = source[ti];
                float lv = source[ti + w * (h - 1)];
                float val = (r + 1) * fv;
                for (int j = 0; j < r; j++) val += source[ti + j * w];
                for (int j = 0; j <= r; j++)
                {
                    val += source[ri] - fv;
                    dest[ti] = val * iar;
                    ri += w;
                    ti += w;
                }
                for (int j = r + 1; j < h - r; j++)
                {
                    val += source[ri] - source[li];
                    dest[ti] = val * iar;
                    li += w;
                    ri += w;
                    ti += w;
                }
                for (int j = h - r; j < h; j++)
                {
                    val += lv - source[li];
                    dest[ti] = val * iar;
                    li += w;
                    ti += w;
                }
            }
        }
        #endregion
    }
}
