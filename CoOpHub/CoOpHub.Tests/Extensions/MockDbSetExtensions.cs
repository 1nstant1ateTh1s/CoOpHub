using Moq;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace CoOpHub.Tests.Extensions
{
	public static class MockDbSetExtensions
	{
		/// <summary>
		/// Allows us to give a DbSet a source, such as a list, in order to populate the DbSet.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="mockSet">The Mock<DbSet<T>> class being extended.</param>
		/// <param name="source">The list to set as the source.</param>
		public static void SetSource<T>(this Mock<DbSet<T>> mockSet, IList<T> source) where T : class
		{
			// Note: This is template code from MSDN

			var data = source.AsQueryable();

			// Setup an existing MockSet to populate the DbSet with a list of data:
			mockSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(data.Provider);
			mockSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(data.Expression);
			mockSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(data.ElementType);
			mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
		}
	}
}
