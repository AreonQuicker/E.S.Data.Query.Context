using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;
using E.S.Data.Query.Context.Queries;
using E.S.Data.Query.Context.Tests.SampleData.Create;
using E.S.Data.Query.DataAccess.Interfaces;
using FluentAssertions;
using Moq;
using Xunit;

namespace E.S.Data.Query.Context.Tests
{
    public class DataCreateQueryTests
    {
        [Theory]
        [AutoData]
        public async Task Create_ShouldReturnFormattedSQLString_WithNoAttributes(IFixture fixture,
            [Frozen] Mock<IDataAccessQuery> dataAccessQueryMoq)
        {
            var sampleCreateModel = new SampleCreate1Model
            {
                Name = "Name",
                Surname = null
            };

            var expectedSqlString =
                "INSERT INTO Sample1 ([Name],[Surname]) VALUES(@Name,@Surname) SELECT CAST(SCOPE_IDENTITY() AS INT)";

            fixture.Customize(new AutoMoqCustomization());

            dataAccessQueryMoq.Setup(a => a.FirstOrDefaultQueryAsync<int>(It.IsAny<string>(), It.IsAny<object>()))
                .ReturnsAsync(() => 1);

            var dataQuery = fixture.Create<DataCreateQuery>();
            var newDataQuery = dataQuery
                .Create(sampleCreateModel);

            var result = await newDataQuery.CreateAsync();

            var sql = newDataQuery.Sql;

            result.Should().Be(1);
            sql.Should().Be(expectedSqlString);
        }

        [Theory]
        [AutoData]
        public async Task Create_ShouldReturnFormattedSQLString_WithAttributes(IFixture fixture,
            [Frozen] Mock<IDataAccessQuery> dataAccessQueryMoq)
        {
            var sampleCreateModel = new SampleCreate2Model
            {
                Name = "Name"
            };

            var expectedSqlString = "INSERT INTO Sample2 ([Name]) VALUES(@Name) SELECT CAST(SCOPE_IDENTITY() AS INT)";

            fixture.Customize(new AutoMoqCustomization());

            dataAccessQueryMoq.Setup(a => a.FirstOrDefaultQueryAsync<int>(It.IsAny<string>(), It.IsAny<object>()))
                .ReturnsAsync(() => 1);

            var dataQuery = fixture.Create<DataCreateQuery>();
            var newDataQuery = dataQuery
                .Create(sampleCreateModel);

            var result = await newDataQuery.CreateAsync();

            var sql = newDataQuery.Sql;

            result.Should().Be(1);
            sql.Should().Be(expectedSqlString);
        }

        [Theory]
        [AutoData]
        public async Task Create_ShouldReturnFormattedSQLString_WithMultipleAttributes(IFixture fixture,
            [Frozen] Mock<IDataAccessQuery> dataAccessQueryMoq)
        {
            var sampleCreateModel = new SampleCreate3Model
            {
                Name = "Name",
                Surname = "SurName"
            };

            var expectedSqlString =
                "INSERT INTO Sample3 ([Name],[Surname]) VALUES(@Name,@Surname) SELECT CAST(SCOPE_IDENTITY() AS INT)";

            fixture.Customize(new AutoMoqCustomization());

            dataAccessQueryMoq.Setup(a => a.FirstOrDefaultQueryAsync<int>(It.IsAny<string>(), It.IsAny<object>()))
                .ReturnsAsync(() => 1);

            var dataQuery = fixture.Create<DataCreateQuery>();
            var newDataQuery = dataQuery
                .Create(sampleCreateModel);

            var result = await newDataQuery.CreateAsync();

            var sql = newDataQuery.Sql;

            result.Should().Be(1);
            sql.Should().Be(expectedSqlString);
        }

        [Theory]
        [AutoData]
        public async Task Create_ShouldReturnFormattedSQLString_WithMultipleAttributesAndDifferentNames(
            IFixture fixture, [Frozen] Mock<IDataAccessQuery> dataAccessQueryMoq)
        {
            var sampleCreateModel = new SampleCreate4Model
            {
                Name = "Name",
                Surname = "SurName"
            };

            var expectedSqlString =
                "INSERT INTO Sample4 ([UName],[USurname]) VALUES(@UName,@USurname) SELECT CAST(SCOPE_IDENTITY() AS INT)";

            fixture.Customize(new AutoMoqCustomization());

            dataAccessQueryMoq.Setup(a => a.FirstOrDefaultQueryAsync<int>(It.IsAny<string>(), It.IsAny<object>()))
                .ReturnsAsync(() => 1);

            var dataQuery = fixture.Create<DataCreateQuery>();
            var newDataQuery = dataQuery
                .Create(sampleCreateModel);

            var result = await newDataQuery.CreateAsync();

            var sql = newDataQuery.Sql;

            result.Should().Be(1);
            sql.Should().Be(expectedSqlString);
        }

        [Theory]
        [AutoData]
        public async Task Create_ShouldReturnFormattedSQLString_WithMultipleAttributesAndIgnoreNullValues(
            IFixture fixture, [Frozen] Mock<IDataAccessQuery> dataAccessQueryMoq)
        {
            var sampleCreateModel = new SampleCreate5Model
            {
                Name = "Name",
                Surname = "SurName",
                NickName = null
            };

            var expectedSqlString =
                "INSERT INTO Sample5 ([Name],[Surname]) VALUES(@Name,@Surname) SELECT CAST(SCOPE_IDENTITY() AS INT)";

            fixture.Customize(new AutoMoqCustomization());

            dataAccessQueryMoq.Setup(a => a.FirstOrDefaultQueryAsync<int>(It.IsAny<string>(), It.IsAny<object>()))
                .ReturnsAsync(() => 1);

            var dataQuery = fixture.Create<DataCreateQuery>();
            var newDataQuery = dataQuery
                .Create(sampleCreateModel);

            var result = await newDataQuery.IgnoreNullValues(true).CreateAsync();

            var sql = newDataQuery.Sql;

            result.Should().Be(1);
            sql.Should().Be(expectedSqlString);
        }

        [Theory]
        [AutoData]
        public async Task
            Create_ShouldReturnFormattedSQLString_WithMultipleAttributesAndIgnoreNullValuesAndDifferentTableName(
                IFixture fixture, [Frozen] Mock<IDataAccessQuery> dataAccessQueryMoq)
        {
            var sampleCreateModel = new SampleCreate5Model
            {
                Name = "Name",
                Surname = "SurName",
                NickName = null
            };

            var expectedSqlString =
                "INSERT INTO NoSample ([Name],[Surname]) VALUES(@Name,@Surname) SELECT CAST(SCOPE_IDENTITY() AS INT)";

            fixture.Customize(new AutoMoqCustomization());

            dataAccessQueryMoq.Setup(a => a.FirstOrDefaultQueryAsync<int>(It.IsAny<string>(), It.IsAny<object>()))
                .ReturnsAsync(() => 1);

            var dataQuery = fixture.Create<DataCreateQuery>();
            var newDataQuery = dataQuery
                .Create(sampleCreateModel);

            var result = await newDataQuery
                .IgnoreNullValues(true)
                .WithTableName("NoSample")
                .CreateAsync();

            var sql = newDataQuery.Sql;

            result.Should().Be(1);
            sql.Should().Be(expectedSqlString);
        }
    }
}