using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NSpec;
using NSpec.Domain;
using NUnit.Framework;

[TestFixture]
public class CodeCoverageShim
{
	private IEnumerable<string> nspecCollection;
	private string targetLocation;

	/*****************************************************************************
		Use the below methods to find code coverage of all the nspec tests.
	 *****************************************************************************/

	[TestFixtureSetUp]
	public void GetSpecifications()
	{
		var targetAssembly = Assembly.GetExecutingAssembly();
		targetLocation = targetAssembly.Location;

		nspecCollection = (from type in targetAssembly.GetTypes()
						   where typeof(nspec).IsAssignableFrom(type)
						   select type.Name);
	}

	[Test]
	public void NSpec()
	{
		if (nspecCollection != null)
		{
			foreach (var tagOrClassName in nspecCollection)
			{
				var invocation = new RunnerInvocation(targetLocation, tagOrClassName);

				var contexts = invocation.Run();

				// Uncomment the below line if the test invocation is to be stopped
				// as soon as a failure is detected.

				contexts.Failures().Count().should_be(0);
			}
		}
	}
}