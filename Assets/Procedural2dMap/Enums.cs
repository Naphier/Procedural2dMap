namespace NG
{
	public enum Direction { None, N, E, S, W }

	public static class DirectionExt
	{
		public static Direction GetOpposite(this Direction direction)
		{
			switch (direction)
			{
				case Direction.N:
					return Direction.S;
				case Direction.E:
					return Direction.W;
				case Direction.S:
					return Direction.N;
				case Direction.W:
					return Direction.E;
				default:
					throw new System.Exception("Invalid Direction: " + direction.ToString());
			}
		}
	}
}