using System;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using SmartStore.Tests;

namespace SmartStore.Data.Tests
{
    [TestFixture]
    public class SchemaTests
    {
        [Test]
        public void Can_generate_schema()
        {
            var ctx = new SmartObjectContext("Test");
            Database.SetInitializer<SmartObjectContext>(null);
            string result = ctx.CreateDatabaseScript();
            result.ShouldNotBeNull();
            Console.Write(result);
        }
    }
}
