using System;
using System.Collections.Generic;
using System.Linq;

namespace PutBrains2Use.MedalTableProblem
{
	/// <summary>
	/// Represents a medal table
	/// </summary>
	public class MedalTable
	{
		/// <summary>
		/// Generates the medal table for the specified results.
		/// </summary>
		/// <param name="results">The results.</param>
		/// <returns>
		/// The medal table as string array.
		/// </returns>
		public string [] Generate(string [] results)
		{
			Constraints.ValidateResultsForNotNull(results);
			Constraints.ValidateResultsForElementsNotNull(results);
			Constraints.ValidateResultsForAllowedCount(results);

			var countryCollection = new CountryCollection();

			foreach (string medalOrder in results)
			{
				string [] medalOrderArray = medalOrder.Split(Constraints.DefaultSeparator);

				countryCollection.AddMedalCountries(medalOrderArray);
			}

			Constraints.ValidateCountryCollectionForAllowedCount(countryCollection);

			countryCollection.Sort();
			return countryCollection.Select(countryName => countryName.ToString()).ToArray();
		}

		private class Constraints
		{
			/// <summary>
			/// Represents the default separator
			/// </summary>
			public const char DefaultSeparator = ' ';

			private const string CountryFormat = "{0} {1} {2} {3}";
			private const int MaxCountriesCount = 50;
			private const int MaxResultsCount = 50;
			private const int MinResultsCount = 1;

			/// <summary>
			/// Gets the country in allowed format.
			/// </summary>
			/// <returns>
			/// Formatted string representation of the specified country.
			/// </returns>
			public static string GetCountryInAllowedFormat(Country country)
			{
				return String.Format(CountryFormat, country.Name, country.Gold, country.Silver, country.Bronze);
			}

			/// <summary>
			/// Validates the country collection for allowed count.
			/// </summary>
			/// <param name="countryCollection">The country collection.</param>
			/// <exception cref="System.ArgumentException"></exception>
			public static void ValidateCountryCollectionForAllowedCount(CountryCollection countryCollection)
			{
				if (countryCollection.Count > MaxCountriesCount)
				{
					throw new ArgumentException(String.Format("Maximum number of countries supported is {0}.", MaxCountriesCount), "results");
				}
			}

			/// <summary>
			/// Validates the medal order for allowed count.
			/// </summary>
			/// <param name="medalOrderArray">The medal order array.</param>
			/// <exception cref="System.ArgumentException"></exception>
			public static void ValidateMedalOrderForAllowedCount(string [] medalOrderArray)
			{
				if (medalOrderArray == null || medalOrderArray.Length != 3 || medalOrderArray.Any(val => val.Length != 3))
				{
					throw new ArgumentException(String.Format("The medal order provided is invalid: {0}.", String.Join(Constraints.DefaultSeparator.ToString(), medalOrderArray)), "results");
				}
			}

			/// <summary>
			/// Validates the results for allowed count.
			/// </summary>
			/// <param name="results">The results.</param>
			/// <exception cref="System.ArgumentException"></exception>
			public static void ValidateResultsForAllowedCount(string [] results)
			{
				if (results.Length < MinResultsCount || results.Length > MaxResultsCount)
				{
					throw new ArgumentException(String.Format("Number of elements should be between {0} and {1} (inclusive).", MinResultsCount, MaxResultsCount), "results");
				}
			}

			/// <summary>
			/// Validates the results for elements not null.
			/// </summary>
			/// <param name="results">The results.</param>
			/// <exception cref="System.ArgumentException"></exception>
			public static void ValidateResultsForElementsNotNull(string [] results)
			{
				if (results.Any(val => val == null))
				{
					throw new ArgumentException("Any element in the array cannot be null.", "results");
				}
			}

			/// <summary>
			/// Validates the results for not null.
			/// </summary>
			/// <param name="results">The results.</param>
			/// <exception cref="System.ArgumentNullException"></exception>
			public static void ValidateResultsForNotNull(string [] results)
			{
				if (results == null)
				{
					throw new ArgumentNullException("results");
				}
			}
		}

		private class Country : IComparable<Country>
		{
			/// <summary>
			/// Initializes a new instance of the <see cref="Country" /> class.
			/// </summary>
			/// <param name="countryName">Name of the country.</param>
			public Country(string countryName)
			{
				this.Name = countryName;
			}

			/// <summary>
			/// Gets the bronze.
			/// </summary>
			/// <value>
			/// The bronze.
			/// </value>
			public int Bronze { get; private set; }

			/// <summary>
			/// Gets the gold.
			/// </summary>
			/// <value>
			/// The gold.
			/// </value>
			public int Gold { get; private set; }

			/// <summary>
			/// Gets the name.
			/// </summary>
			/// <value>
			/// The name.
			/// </value>
			public string Name { get; private set; }

			/// <summary>
			/// Gets the silver.
			/// </summary>
			/// <value>
			/// The silver.
			/// </value>
			public int Silver { get; private set; }

			/// <summary>
			/// Adds bronze.
			/// </summary>
			public void AddBronze()
			{
				this.Bronze++;
			}

			/// <summary>
			/// Adds gold.
			/// </summary>
			public void AddGold()
			{
				this.Gold++;
			}

			/// <summary>
			/// Adds silver.
			/// </summary>
			public void AddSilver()
			{
				this.Silver++;
			}

			/// <summary>
			/// Compares the current object with another object of the same type.
			/// </summary>
			/// <param name="other">An object to compare with this object.</param>
			/// <returns>
			/// A 32-bit signed integer that indicates the relative order of the objects being compared.
			/// The return value has the following meanings: Value Meaning Less than zero
			/// This object is less than the <paramref name="other" /> parameter.
			/// Zero This object is equal to <paramref name="other" />.
			/// Greater than zero This object is greater than <paramref name="other" />.
			/// </returns>
			public int CompareTo(Country other)
			{
				if (this.Gold > other.Gold)
				{
					return -1;
				}

				if (this.Gold < other.Gold)
				{
					return 1;
				}

				if (this.Silver > other.Silver)
				{
					return -1;
				}

				if (this.Silver < other.Silver)
				{
					return 1;
				}

				if (this.Bronze > other.Bronze)
				{
					return -1;
				}

				if (this.Bronze < other.Bronze)
				{
					return 1;
				}

				return this.Name.CompareTo(other.Name);
			}

			/// <summary>
			/// Returns a <see cref="System.String" /> that represents this instance.
			/// </summary>
			/// <returns>
			/// A <see cref="System.String" /> that represents this instance.
			/// </returns>
			public override string ToString()
			{
				return Constraints.GetCountryInAllowedFormat(this);
			}
		}

		private class CountryCollection : List<Country>
		{
			/// <summary>
			/// Adds the medal countries.
			/// </summary>
			/// <param name="medalOrderArray">The medal order array.</param>
			public void AddMedalCountries(string [] medalOrderArray)
			{
				Constraints.ValidateMedalOrderForAllowedCount(medalOrderArray);

				this.AddGoldCountry(medalOrderArray);
				this.AddSilverCountry(medalOrderArray);
				this.AddBronzeCountry(medalOrderArray);
			}

			private void AddBronzeCountry(string [] medalOrderArray)
			{
				string countryName = medalOrderArray [2];
				Country requitredCountry = this.GetCountry(countryName);
				requitredCountry.AddBronze();
			}

			private void AddGoldCountry(string [] medalOrderArray)
			{
				string countryName = medalOrderArray [0];
				Country requitredCountry = this.GetCountry(countryName);
				requitredCountry.AddGold();
			}

			private void AddSilverCountry(string [] medalOrderArray)
			{
				string countryName = medalOrderArray [1];
				Country requitredCountry = this.GetCountry(countryName);
				requitredCountry.AddSilver();
			}

			private Country GetCountry(string countryName)
			{
				Country requiredCountry
					= this.FirstOrDefault(country => country.Name == countryName);

				if (requiredCountry == null)
				{
					requiredCountry = new Country(countryName);
					this.Add(requiredCountry);
				}

				return requiredCountry;
			}
		}
	}
}