using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq.Expressions;

using Microsoft.CSharp;

namespace PutBrains2Use
{
	/// <summary>
	/// Evaluate expression specified as string.
	/// </summary>
	public static class ExpressionHelper
	{
		private const string EXPRESSION_PREFIX = "() => ";

		/// <summary>
		/// Evaluates the specified property expression.
		/// </summary>
		/// <param name="propertyExpression">The property expression.</param>
		/// <returns></returns>
		public static object Evaluate(string propertyExpression)
		{
			if (propertyExpression.Contains(EXPRESSION_PREFIX) == false)
				propertyExpression = String.Concat(EXPRESSION_PREFIX, propertyExpression);

			Dictionary<string, string> providerOptions = new Dictionary<string, string>();
			providerOptions.Add("CompilerVersion", "v4.0");

			CSharpCodeProvider provider = new CSharpCodeProvider(providerOptions);

			CompilerResults results = provider.CompileAssemblyFromSource(new CompilerParameters(new [] { "System.Core.dll" }),
							  @"using System;
                                using System.Linq.Expressions;

                                class TrialClass { public static Expression<Func<object>> GetExpression() { return " + propertyExpression + "; } }");

			Expression<Func<object>> expression = (Expression<Func<object>>) results.CompiledAssembly.GetType("TrialClass").GetMethod("GetExpression").Invoke(null, null);
			Func<object> func = expression.Compile();

			return func();
		}
	}
}