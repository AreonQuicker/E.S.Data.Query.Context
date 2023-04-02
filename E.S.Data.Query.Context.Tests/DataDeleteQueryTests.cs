using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;
using E.S.Data.Query.Context.Queries;
using E.S.Data.Query.Context.Tests.SampleData.Delete;
using E.S.Data.Query.DataAccess.Interfaces;
using FluentAssertions;
using Moq;
using Xunit;

namespace E.S.Data.Query.Context.Tests
{
    public class DataDeleteQueryTests
    {
        [Theory]
        [AutoData]
        public async Task Delete_ShouldReturnFormattedSQLString_WithNoAttributes(IFixture fixture,
            [Frozen] Mock<IDataAccessQuery> dataAccessQueryMoq)
        {
            var sampleCreateModel = new SampleDelete1Model
            {
                Name = "Name"
            };

            var expectedSqlString =
                "DELETE FROM Sample1 WHERE Id = @Id SELECT 1";

            fixture.Customize(new AutoMoqCustomization());

            dataAccessQueryMoq.Setup(a => a.FirstOrDefaultQueryAsync<int>(It.IsAny<string>(), It.IsAny<object>()))
                .ReturnsAsync(() => 1);

            var dataQuery = fixture.Create<DataDeleteQuery>();
            var newDataQuery = dataQuery
                .Delete(sampleCreateModel)
                .WithIdKey("Id")
                .WithIdValue(1);

            await newDataQuery.DeleteAsync();

            var sql = newDataQuery.Sql;

            sql.Should().Be(expectedSqlString);
        }

        [Theory]
        [AutoData]
        public async Task Delete_ShouldReturnFormattedSQLString_WithAttributes(IFixture fixture,
            [Frozen] Mock<IDataAccessQuery> dataAccessQueryMoq)
        {
            var sampleCreateModel = new SampleDelete2Model
            {
                Name = "Name"
            };

            var expectedSqlString =
                "DELETE FROM Sample2 WHERE Key = @Key SELECT 1";

            fixture.Customize(new AutoMoqCustomization());

            dataAccessQueryMoq.Setup(a => a.FirstOrDefaultQueryAsync<int>(It.IsAny<string>(), It.IsAny<object>()))
                .ReturnsAsync(() => 1);

            var dataQuery = fixture.Create<DataDeleteQuery>();
            var newDataQuery = dataQuery
                .Delete(sampleCreateModel);

            await newDataQuery.DeleteAsync();

            var sql = newDataQuery.Sql;

            sql.Should().Be(expectedSqlString);

        }
    }
}