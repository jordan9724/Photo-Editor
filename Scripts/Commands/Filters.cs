namespace Picture_Editor_v2.Scripts.Commands
{
	/// <summary>
	/// Used as filters for the Filter class
	/// </summary>
	public static class Filters
	{

		/// <summary>
		/// Only keeps the red in each pixel
		/// </summary>
		public static readonly float[,] Red =
		{
			{1, 0, 0},
			{0, 0, 0},
			{0, 0, 0}
		};

		/// <summary>
		/// Only keeps the green in each pixel
		/// </summary>
		public static readonly float[,] Green =
		{
			{0, 0, 0},
			{0, 1, 0},
			{0, 0, 0}
		};

		/// <summary>
		/// Only keeps the blue in each pixel
		/// </summary>
		public static readonly float[,] Blue =
		{
			{0, 0, 0},
			{0, 0, 0},
			{0, 0, 1}
		};

		/// <summary>
		/// Gives the image a yellow look
		/// </summary>
		public static readonly float[,] Yellow =
		{
			{1, 0, 0},
			{0, 0.92f, 0},
			{0, 0, 0.016f}
		};

		/// <summary>
		/// Only keeps the blue and green in each pixel
		/// </summary>
		public static readonly float[,] Cyan =
		{
			{0, 0, 0},
			{0, 1, 0},
			{0, 0, 1}
		};

		/// <summary>
		/// Only keeps the red and blue in each pixel
		/// </summary>
		public static readonly float[,] Magenta =
		{
			{1, 0, 0},
			{0, 0, 0},
			{0, 0, 1}
		};

		/// <summary>
		/// Makes the image look "older"
		/// </summary>
		public static readonly float[,] Sepia =
		{
			{0.393f, 0.769f, 0.189f},
			{0.349f, 0.686f, 0.168f},
			{0.272f, 0.534f, 0.131f}
		};

		/// <summary>
		/// Switches around the colors
		/// r -> r, b -> g, g -> b
		/// </summary>
		public static readonly float[,] RotateHue0 =
		{
			{1, 0, 0},
			{0, 0, 1},
			{0, 1, 0}
		};

		/// <summary>
		/// Switches around the colors
		/// g -> r, r -> g, b -> b
		/// </summary>
		public static readonly float[,] RotateHue1 =
		{
			{0, 1, 0},
			{1, 0, 0},
			{0, 0, 1}
		};

		/// <summary>
		/// Switches around the colors
		/// g -> r, b -> g, r -> b
		/// </summary>
		public static readonly float[,] RotateHue2 =
		{
			{0, 1, 0},
			{0, 0, 1},
			{1, 0, 0}
		};

		/// <summary>
		/// Switches around the colors
		/// g -> r, r -> g, g -> b
		/// </summary>
		public static readonly float[,] RotateHue3 =
		{
			{0, 0, 1},
			{1, 0, 0},
			{0, 1, 0}
		};

		/// <summary>
		/// Switches around the colors
		/// b -> r, g -> g, r -> b
		/// </summary>
		public static readonly float[,] RotateHue4 =
		{
			{0, 0, 1},
			{0, 1, 0},
			{1, 0, 0}
		};

		/// <summary>
		/// Colors become more vibrant
		/// </summary>
		public static readonly float[,] Saturate =
		{
			{1.2f, 0, 0},
			{0, 1.75f, 0},
			{0, 0, 2}
		};

		/// <summary>
		/// Creates a grey scale image
		/// </summary>
		public static readonly float[,] Grey =
		{
			{0.33f, 0.33f, 0.33f},
			{0.33f, 0.33f, 0.33f},
			{0.33f, 0.33f, 0.33f}
		};

		/// <summary>
		/// Creates a gray scale image
		/// </summary>
		public static readonly float[,] Gray =
		{
			{0.33f, 0.33f, 0.33f},
			{0.33f, 0.33f, 0.33f},
			{0.33f, 0.33f, 0.33f}
		};

	}
}
