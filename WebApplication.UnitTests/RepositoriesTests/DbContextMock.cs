using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace WebApplication.UnitTests.RepositoriesTests
{
    public static class DbContextMock
    {
        public static DbSet<T> GetQueryableMockDbSet<T>(List<T> sourceList) where T : class
        {
            var queryable = sourceList.AsEnumerable();
            var dbSet = new Mock<DbSet<T>>();
            dbSet.As<IEnumerable<T>>().Setup(m
                => m.GetEnumerator()).Returns(() => queryable.GetEnumerator());
            dbSet.Setup(d
                => d.Add(It.IsAny<T>())).Callback<T>((s) => sourceList.Add(s));
            return dbSet.Object;
        }
    }
}
