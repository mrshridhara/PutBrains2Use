using PutBrains2Use.GameOfLife;
using NSpec;

namespace PutBrains2Use.Test
{
	public class GameOfLifeSpec : nspec
	{
		private dynamic GameOfLifeBoard = null;

		private void before_each()
		{
			GameOfLifeBoard = TestInstance.BlockBoard;
		}

		private void Given_the_board_is_initialized()
		{
			it ["Should be able to set the cells to alive"] = () =>
			{
				bool status = GameOfLifeBoard.GetCellStatus(1, 4);
				status.should_be(true);
			};
		}

		private void Given_the_board_is_initialized_with_live_cells()
		{
			it ["Get the neighbor cells for the given cell"] = () =>
			{
				int count = GameOfLifeBoard.GetLiveNeighborsCount(0, 0);
				count.should_be(0);

				count = GameOfLifeBoard.GetLiveNeighborsCount(1, 3);
				count.should_be(3);

				count = GameOfLifeBoard.GetLiveNeighborsCount(2, 3);
				count.should_be(2);
			};
		}

		private void Given_the_board_is_initialized_with_live_cells_and_proper_neighbour_count()
		{
			it ["Any live cell with fewer than two live neighbors dies, as if caused by under population."] = () =>
			{
				before = () =>
				{
					GameOfLifeBoard = TestInstance.OscillatingBoard;
				};

				GameOfLifeBoard = GameOfLifeBoard.Next();
				bool status = GameOfLifeBoard.GetCellStatus(1, 2);
				status.should_be(false);
			};

			it ["Any live cell with more than three live neighbors dies, as if by overcrowding."] = () =>
			{
				before = () =>
				{
					GameOfLifeBoard = TestInstance.OscillatingBoard;
				};

				GameOfLifeBoard = GameOfLifeBoard.Next();
				bool status = GameOfLifeBoard.GetCellStatus(1, 2);
				status.should_be(false);
			};

			it ["Any live cell with two or three live neighbors lives on to the next generation."] = () =>
			{
				before = () =>
				{
					GameOfLifeBoard = TestInstance.BlockBoard;
				};

				GameOfLifeBoard = GameOfLifeBoard.Next();
				bool status = GameOfLifeBoard.GetCellStatus(1, 4);
				status.should_be(true);
			};

			it ["Any dead cell with exactly three live neighbors becomes a live cell."] = () =>
			{
				before = () =>
				{
					GameOfLifeBoard = TestInstance.BlockBoard;
				};

				GameOfLifeBoard = GameOfLifeBoard.Next();
				bool status = GameOfLifeBoard.GetCellStatus(1, 3);
				status.should_be(true);
			};
		}

		private void When_the_Application_Starts()
		{
			it ["Should initialize the two dimensional grid for the board"] = () =>
			{
				dynamic GameOfLifeBoard = TestInstance.BlockBoard;
				int length = GameOfLifeBoard.InitializeWith(4, 8).Length;
				length.should_be(32);
			};
		}

		private static class TestInstance
		{
			public static GameOfLifeBoard BlockBoard
				= new GameOfLifeBoard()
					.InitializeWith(4, 8)
					.SetCellAlive(1, 4)
					.SetCellAlive(2, 3)
					.SetCellAlive(2, 4);

			public static dynamic OscillatingBoard
				= new GameOfLifeBoard()
					.InitializeWith(4, 8)
					.SetCellAlive(1, 2)
					.SetCellAlive(2, 3)
					.SetCellAlive(2, 4)
					.SetCellAlive(3, 2)
					.SetCellAlive(3, 3)
					.SetCellAlive(3, 4);
		}
	}
}