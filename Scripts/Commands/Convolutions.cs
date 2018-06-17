namespace Picture_Editor_v2.Scripts.Commands
{
	/// <summary>
	/// Used as convolutions for the Convolution class
	/// Many of these convolutions came from https://softwarebydefault.com/2013/05/01/image-convolution-filters/
	/// </summary>
	public static class Convolutions
	{

		/// <summary>
		/// Keeps the edges in the image
		/// </summary>
		public static readonly float[,] EdgeDetection =
		{
			{ -1, -1, -1},
			{ -1,  8, -1},
			{ -1, -1, -1}
		};

		/// <summary>
		/// Keeps the edges with their depth
		/// </summary>
		public static readonly float[,] EdgeDetectionDepth =
		{
			{ -5, 0, 0},
			{  0, 0, 0},
			{  0, 0, 5}
		};

		/// <summary>
		/// Makes the edges more apparent
		/// </summary>
		public static readonly float[,] EdgeEnhancement =
		{
			{ -1, 0, 0},
			{  0, 2, 0},
			{  0, 0, 0}
		};

		/// <summary>
		/// Embosses the image (it looks like you pushed into it)
		/// </summary>
		public static readonly float[,] Emboss =
		{
			{ 2,  0,  0},
			{ 0, -1,  0},
			{ 0,  0, -1}
		};

		/// <summary>
		/// Embosses the image giving attention to pixels at 45 degrees
		/// </summary>
		public static readonly float[,] Emboss45 =
		{
			{ -1, -1,  0},
			{ -1,  0,  1},
			{  0,  1,  1}

		};

		/// <summary>
		/// Embosses the image with depth
		/// </summary>
		public static readonly float[,] EmbossDepth =
		{
			{ -1, 0, 0},
			{  0, 0, 0},
			{  0, 0, 1}
		};

		/// <summary>
		/// Intensely embosses the image
		/// </summary>
		public static readonly float[,] EmbossIntense =
		{
			{ -1, -2, -1},
			{ -2, 12, -2},
			{ -1, -2, -1}
		};

		/// <summary>
		/// Adds more contrast to the image
		/// </summary>
		public static readonly float[,] Sharpen =
		{
			{ -1, -1, -1},
			{ -1,  9, -1},
			{ -1, -1, -1}
		};

		/// <summary>
		/// Greatly contrasts the image
		/// </summary>
		public static readonly float[,] SharpenIntense =
		{
			{ 1,  1, 1},
			{ 1, -7, 1},
			{ 1,  1, 1}
		};

		/// <summary>
		/// Slightly contrasts the image
		/// </summary>
		public static readonly float[,] SharpenMedium =
		{
			{  0, -1,  0},
			{ -1,  5, -1},
			{  0, -1,  0}
		};

	}
}
