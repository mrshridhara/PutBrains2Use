using System.Collections.Generic;

namespace PutBrains2Use.GameOfLife
{
	/// <summary>
	/// Represents a game of life board
	/// </summary>
	public class GameOfLifeBoard
	{
		private bool [,] cells;
		private int columns;
		private int rows;

		/// <summary>
		/// Gets the length.
		/// </summary>
		/// <value>
		/// The length.
		/// </value>
		public int Length
		{
			get { return cells.Length; }
		}

		/// <summary>
		/// Gets the cell status.
		/// </summary>
		/// <param name="row">The row.</param>
		/// <param name="column">The column.</param>
		/// <returns></returns>
		public bool GetCellStatus(int row, int column)
		{
			return cells [row, column];
		}

		/// <summary>
		/// Gets the live neighbors count.
		/// </summary>
		/// <param name="row">The row.</param>
		/// <param name="column">The column.</param>
		/// <returns></returns>
		public int GetLiveNeighborsCount(int row, int column)
		{
			int liveNeighborsCount = 0;
			var increments = new List<int> { 0, 1, -1 };

			foreach (int rowIncrement in increments)
			{
				foreach (int columnIncrement in increments)
				{
					if ((rowIncrement == 0 && columnIncrement == 0) == false)
					{
						var possibleRow = row + rowIncrement;
						var possibleColumn = column + columnIncrement;

						if (possibleRow >= 0 && possibleRow < rows)
						{
							if (possibleColumn >= 0 && possibleColumn < columns)
							{
								if (cells [possibleRow, possibleColumn])
									liveNeighborsCount++;
							}
						}
					}
				}
			}

			return liveNeighborsCount;
		}

		/// <summary>
		/// Initializes the with.
		/// </summary>
		/// <param name="rows">The rows.</param>
		/// <param name="columns">The columns.</param>
		/// <returns></returns>
		public GameOfLifeBoard InitializeWith(int rows, int columns)
		{
			this.rows = rows;
			this.columns = columns;
			cells = new bool [rows, columns];
			return this;
		}

		/// <summary>
		/// Next this instance.
		/// </summary>
		/// <returns></returns>
		public GameOfLifeBoard Next()
		{
			for (int row = 0; row < rows; row++)
			{
				for (int column = 0; column < columns; column++)
				{
					int lifeCount = this.GetLiveNeighborsCount(row, column);

					if (lifeCount < 2)
						cells [row, column] = false;
					else if (lifeCount > 3)
						cells [row, column] = false;
					else if (lifeCount == 3)
						cells [row, column] = true;
				}
			}

			return this;
		}

		/// <summary>
		/// Sets the cell alive.
		/// </summary>
		/// <param name="row">The row.</param>
		/// <param name="column">The column.</param>
		/// <returns></returns>
		public GameOfLifeBoard SetCellAlive(int row, int column)
		{
			cells [row, column] = true;
			return this;
		}
	}
}