using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;
using E.S.Data.Query.Context.Queries;
using E.S.Data.Query.Context.Tests.SampleData.Create;
using E.S.Data.Query.Context.Tests.SampleData.Update;
using E.S.Data.Query.DataAccess.Interfaces;
using FluentAssertions;
using Moq;
using Xunit;

namespace E.S.Data.Query.Context.Tests
{
    public class DataUpdateQueryTests
    {
        [Theory]
        [AutoData]
        public async Task Update_ShouldReturnFormattedSQLString_WithNoAttributes(IFixture fixture,
            [Frozen] Mock<IDataAccessQuery> dataAccessQueryMoq)
        {
            var sampleCreateModel = new SampleUpdate1Model
            {
                Name = "Name",
                Surname = null
            };

            var expectedSqlString =
                "UPDATE Sample1 SET [Name]=@Name,[Surname]=@Surname WHERE Id = @Id SELECT 1";

            fixture.Customize(new AutoMoqCustomization());

            dataAccessQueryMoq.Setup(a => a.FirstOrDefaultQueryAsync<int>(It.IsAny<string>(), It.IsAny<object>()))
                .ReturnsAsync(() => 1);

            var dataQuery = fixture.Create<DataUpdateQuery>();
            var newDataQuery = dataQuery
                .Update(sampleCreateModel)
                .WithIdKey("Id")
                .WithIdValue(1);

            await newDataQuery.UpdateAsync();

            var sql = newDataQuery.Sql;

            sql.Should().Be(expectedSqlString);
        }
        
        [Theory]
        [AutoData]
        public async Task Update_ShouldReturnFormattedSQLString_WithAttributes(IFixture fixture,
            [Frozen] Mock<IDataAccessQuery> dataAccessQueryMoq)
        {
            var sampleCreateModel = new SampleUpdate2Model
            {
                Name = "Name",
                Surname = null
            };

            var expectedSqlString =
                "UPDATE Sample2 SET [Name]=@Name,[Surname]=@Surname WHERE Key = @Key SELECT 1";

            fixture.Customize(new AutoMoqCustomization());

            dataAccessQueryMoq.Setup(a => a.FirstOrDefaultQueryAsync<int>(It.IsAny<string>(), It.IsAny<object>()))
                .ReturnsAsync(() => 1);

            var dataQuery = fixture.Create<DataUpdateQuery>();
            var newDataQuery = dataQuery
                .Update(sampleCreateModel);

            await newDataQuery.UpdateAsync();

            var sql = newDataQuery.Sql;

            sql.Should().Be(expectedSqlString);
        }
        
        [Theory]
        [AutoData]
        public async Task Update_ShouldReturnFormattedSQLString_WithMultipleAttributes(IFixture fixture,
            [Frozen] Mock<IDataAccessQuery> dataAccessQueryMoq)
        {
            var sampleCreateModel = new SampleUpdate3Model
            {
                Name = "Name",
                Surname = "Surname",
                Ignore = "Ignore",
                IgnoreNull = null,
                Key = "1"
            };

            var expectedSqlString =
                "UPDATE AnotherTable SET [AnotherName]=@AnotherName,[Surname]=@Surname WHERE Key = @Key SELECT 1";

            fixture.Customize(new AutoMoqCustomization());

            dataAccessQueryMoq.Setup(a => a.FirstOrDefaultQueryAsync<int>(It.IsAny<string>(), It.IsAny<object>()))
                .ReturnsAsync(() => 1);

            var dataQuery = fixture.Create<DataUpdateQuery>();
            var newDataQuery = dataQuery
                .Update(sampleCreateModel)
                .IgnoreNullValues(true)
                .WithTableName("AnotherTable");

            await newDataQuery.UpdateAsync();

            var sql = newDataQuery.Sql;

            sql.Should().Be(expectedSqlString);
        }
        
        [Theory]
        [AutoData]
        public async Task Upsert_ShouldReturnFormattedSQLString_WithMultipleAttributes(IFixture fixture,
            [Frozen] Mock<IDataAccessQuery> dataAccessQueryMoq)
        {
            var sampleCreateModel = new SampleUpdate3Model
            {
                Name = "Name",
                Surname = "Surname",
                Ignore = "Ignore",
                IgnoreNull = null,
                Key = "1"
            };

            var expectedSqlString =
                "UPDATE AnotherTable SET [AnotherName]=ISNULL(@AnotherName,AnotherName),[Surname]=ISNULL(@Surname,Surname) WHERE Key = @Key SELECT 1";

            fixture.Customize(new AutoMoqCustomization());

            dataAccessQueryMoq.Setup(a => a.FirstOrDefaultQueryAsync<int>(It.IsAny<string>(), It.IsAny<object>()))
                .ReturnsAsync(() => 1);

            var dataQuery = fixture.Create<DataUpdateQuery>();
            var newDataQuery = dataQuery
                .Update(sampleCreateModel)
                .IgnoreNullValues(true)
                .WithTableName("AnotherTable")
                .WithUpsert(true);

            await newDataQuery.UpdateAsync();

            var sql = newDataQuery.Sql;

            sql.Should().Be(expectedSqlString);
        }
    }
}