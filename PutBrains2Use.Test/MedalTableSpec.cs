using System.Linq;
using PutBrains2Use.MedalTableProblem;
using NSpec;

namespace PutBrains2Use.Test
{
	public class MedalTableSpec : nspec
	{
		private void Given_the_results()
		{
			it ["Countries should not repeat in the medal table"] = () =>
			{
				dynamic medalTable = TestInstance.MedalTable;
				var results = new string [] { "GER AUT SUI", "AUT SUI GER", "SUI GER AUT" };
				string [] table = medalTable.Generate(results);
				table.Distinct().Count().should_be(table.Count());
			};
		}

		private static class TestInstance
		{
			public static MedalTable MedalTable
				= new MedalTable();
		}

		/*
			MedalTable medalTable = new MedalTable();

			string [] results
				////= new [] {"GER AUT SUI", "AUT SUI GER", "SUI GER AUT"};
				= new [] { "ITA JPN AUS", "KOR TPE UKR", "KOR KOR GBR", "KOR CHN TPE" };
			////= new [] {"USA AUT ROM"};

			string[] medalTableResult = medalTable.Generate(results);
	    */
	}
}