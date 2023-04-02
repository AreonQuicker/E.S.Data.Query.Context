using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;
using E.S.Data.Query.Context.Queries;
using E.S.Data.Query.Context.Tests.SampleData.Create;
using E.S.Data.Query.Context.Tests.SampleData.Select;
using E.S.Data.Query.DataAccess.Interfaces;
using FluentAssertions;
using Moq;
using Xunit;

namespace E.S.Data.Query.Context.Tests
{
    public class DataSelectQueryTests
    {
        [Theory]
        [InlineAutoData("SELECT * FROM Sample1", typeof(SampleSelect1Model))]
        [InlineAutoData("SELECT * FROM Sample2", typeof(SampleSelect2Model))]
        [InlineAutoData("SELECT * FROM Sample.Sample3", typeof(SampleSelect3Model))]
        public async Task Select_ShouldReturnFormattedSQLString_WithNoWhere(string expectedSqlString, Type modelType,
            IFixture fixture, [Frozen] Mock<IDataAccessQuery> dataAccessQueryMoq)
        {
            fixture.Build<SampleCreate1Model>();
            
            fixture.Customize(new AutoMoqCustomization());

            dataAccessQueryMoq.Setup(a => a.ListQueryAsync<object>(It.IsAny<string>(), It.IsAny<object>(), 700))
                .ReturnsAsync(() => new List<object>());

            //fixture.Inject(dataAccessQueryMoq.Object);

            var dataSelectQuery = fixture.Create<DataSelectQuery>();
            var newSelectQuery = dataSelectQuery
                .Select(modelType);

            var result = await newSelectQuery.ListAsync<object>();

            var sql = newSelectQuery.Sql;

            result.Should().BeEmpty();
            sql.Should().Be(expectedSqlString);
        }

        [Theory]
        [InlineAutoData("SELECT * FROM Sample1 WHERE Id = @Id", typeof(SampleSelect1Model))]
        [InlineAutoData("SELECT * FROM Sample2 WHERE Id = @Id", typeof(SampleSelect2Model))]
        [InlineAutoData("SELECT * FROM Sample.Sample3 WHERE Id = @Id", typeof(SampleSelect3Model))]
        public async Task Select_ShouldReturnFormattedSQLString_WithWhere(string expectedSqlString, Type modelType,
            IFixture fixture, [Frozen] Mock<IDataAccessQuery> dataAccessQueryMoq)
        {
            fixture.Customize(new AutoMoqCustomization());

            dataAccessQueryMoq.Setup(a => a.ListQueryAsync<object>(It.IsAny<string>(), It.IsAny<object>(), 700))
                .ReturnsAsync(() => new List<object>());

            var dataSelectQuery = fixture.Create<DataSelectQuery>();
            var newSelectQuery = dataSelectQuery
                .Select(modelType);

            var result = await newSelectQuery
                .WhereId(2)
                .ListAsync<object>();

            var sql = newSelectQuery.Sql;

            result.Should().BeEmpty();
            sql.Should().Be(expectedSqlString);
        }

        [Theory]
        [InlineAutoData("SELECT * FROM Sample1 WHERE Name = @Name", typeof(SampleSelect1Model), new[] {"Name"})]
        [InlineAutoData("SELECT * FROM Sample1 WHERE Name = @Name AND Surname = @Surname", typeof(SampleSelect1Model),
            new[] {"Name", "Surname"})]
        [InlineAutoData("SELECT * FROM Sample.Sample3 WHERE Name = @Name AND Surname = @Surname",
            typeof(SampleSelect3Model), new[] {"Name", "Surname"})]
        public async Task Select_ShouldReturnFormattedSQLString_WithWhereValues(string expectedSqlString,
            Type modelType, string[] whereValues, IFixture fixture, [Frozen] Mock<IDataAccessQuery> dataAccessQueryMoq)
        {
            fixture.Customize(new AutoMoqCustomization());

            dataAccessQueryMoq.Setup(a => a.ListQueryAsync<object>(It.IsAny<string>(), It.IsAny<object>(), 700))
                .ReturnsAsync(() => new List<object>());

            var dataSelectQuery = fixture.Create<DataSelectQuery>();
            var newSelectQuery = dataSelectQuery
                .Select(modelType);

            foreach (var whereValue in whereValues)
                newSelectQuery
                    .Where(whereValue, null);

            var result = await newSelectQuery.ListAsync<object>();

            var sql = newSelectQuery.Sql;

            result.Should().BeEmpty();
            sql.Should().Be(expectedSqlString);
        }
    }
}