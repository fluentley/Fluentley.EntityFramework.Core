using System;
using System.Linq;
using System.Threading.Tasks;
using Fluentley.EntityFramework.Core;
using Fluentley.EntityFramework.Core.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Test.Fluentley.EntityFramework.Core.Entity;

namespace Test.Fluentley.EntityFramework.Core
{
    [TestClass]
    public class DataAccessTest
    {
        private readonly IRepository<Company, Guid> _repository;

        public DataAccessTest()
        {
            var optionsBuilder = new DbContextOptionsBuilder();
            optionsBuilder.UseInMemoryDatabase("SampleDb");
            var context = new CoreDataContext(optionsBuilder.Options);
            _repository = context.Repository<Company, Guid>();
        }

        private Company CreateCommand => new Company
        {
            Website = "Test1WebSite",
            Name = "TestCompany1"
        };


        [TestMethod]
        public async Task RevertViaAudit()
        {
            var result = await _repository.Create(CreateCommand);

            var data = result.Data;
            data.Website = "UpdatedWebsite";

            var updateResult = await _repository.Update(data);
            var findResult = await _repository.Find(result.Data.Id);

            Assert.AreEqual("UpdatedWebsite", findResult.Data.Website);


            await _repository.Update(result.Data.Id, updateResult.Audit.OldValues);
            var findResult2 = await _repository.Find(result.Data.Id);
            Assert.AreEqual(CreateCommand.Website, findResult2.Data.Website);
        }

        #region Main Repository Tests

        [TestMethod]
        public async Task CreateViaRepository()
        {
            var result = await _repository.Create(CreateCommand);

            Assert.AreEqual(true, result.IsSuccess, "Create Call failed");
            Assert.AreNotEqual(null, result.Data, "Data should not return null");
            Assert.AreNotEqual(default(Guid), result.Data.Id, "Created Id should not return null");
        }


        [TestMethod]
        public async Task UpdateViaRepository()
        {
            var result = await _repository.Create(CreateCommand);

            var data = result.Data;
            data.Website = "UpdatedWebsite";

            await _repository.Update(data);
            var findResult = await _repository.Find(result.Data.Id);

            Assert.AreEqual("UpdatedWebsite", findResult.Data.Website);
        }

        [TestMethod]
        public async Task DeleteViaRepository()
        {
            var result = await _repository.Create(CreateCommand);
            var result2 = await _repository.Create(CreateCommand);


            Assert.AreNotEqual(null, result.Data);

            await _repository.Delete(result.Data);

            var countResult2 = await _repository.Query(option =>
                option.QueryBy(query => query.Where(filter => filter.Id == result.Data.Id))
            );
            Assert.AreEqual(0, countResult2.Data.Count());
        }

        #endregion

        #region AuditLog Test

        [TestMethod]
        public async Task UpdateAuditLogTest()
        {
            var result = await _repository.Create(CreateCommand);

            var data = result.Data;
            data.Website = "UpdatedWebsite";

            var updateResult = await _repository.Update(data);

            Assert.AreEqual(1, updateResult.Audit.NewValues.Count);
            Assert.AreEqual("UpdatedWebsite", updateResult.Audit.NewValues.FirstOrDefault().Value);
            Assert.AreEqual(1, updateResult.Audit.OldValues.Count);
            Assert.AreEqual("Test1WebSite", updateResult.Audit.OldValues.FirstOrDefault().Value);
        }

        [TestMethod]
        public async Task CreateAuditLogTest()
        {
            var result = await _repository.Create(CreateCommand);
            Assert.AreEqual(2, result.Audit.NewValues.Count);
            Assert.AreEqual(0, result.Audit.OldValues.Count);
        }

        [TestMethod]
        public async Task DeleteAuditLogTest()
        {
            var result = await _repository.Create(CreateCommand);
            var deleteResult = await _repository.Delete(result.Data);
            Assert.AreEqual(0, deleteResult.Audit.NewValues.Count);
            Assert.AreEqual(2, deleteResult.Audit.OldValues.Count);
        }

        #endregion
    }
}