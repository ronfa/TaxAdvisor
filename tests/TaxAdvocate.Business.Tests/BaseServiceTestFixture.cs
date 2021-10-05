using System;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TaxAdvocate.Business.Mapping;
using TaxAdvocate.Data.Contexts;
using TaxAdvocate.Data.Generators;

namespace TaxAdvocate.Business.Tests
{
    public class BaseServiceTestFixture : IDisposable
    {
        public InMemoryDatabaseContext _context;
        public IMapper _mapper;


        public BaseServiceTestFixture()
        {
            if (_context == null)
            {
                _context = CreateContext();
            }
            _mapper = CreateMapper();
        }

        public InMemoryDatabaseContext CreateContext()
        {
            var context = new InMemoryDatabaseContext(CreateOptions());

            // We are using the same data generator for unit testing
            // Instead we could be creating sample data sets

            JsonDataGenerator.GenerateData(context);

            return context;
        }

        private DbContextOptions<InMemoryDatabaseContext> CreateOptions()
        {
            return new DbContextOptionsBuilder<InMemoryDatabaseContext>().UseInMemoryDatabase("TaxAdvisor").Options;
        }

        public IMapper CreateMapper()
        {
            // Auto Mapper Configurations
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            return mapperConfig.CreateMapper();
        }


        public void Dispose()
        {
            _context.Dispose();
            _context = null;
            _mapper = null;
        }
    }
}
