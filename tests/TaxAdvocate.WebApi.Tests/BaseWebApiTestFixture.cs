using System;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using TaxAdvocate.Business.Mapping;
using TaxAdvocate.Business.Services;
using TaxAdvocate.Data.Contexts;
using TaxAdvocate.Data.Generators;

namespace TaxAdvocate.WebApi.Tests
{
    public class BaseWebApiTestFixture : IDisposable
    {
        public InMemoryDatabaseContext Context;
        public IMapper Mapper;
        public IClientService ClientService;
        public IAdviceService AdviceService;


        public BaseWebApiTestFixture()
        {
            if (Context == null)
            {
                Context = CreateContext();
            }
            Mapper = CreateMapper();
            ClientService = new ClientService(new NullLogger<ClientService>(), Context, Mapper);
            AdviceService = new AdviceService(new NullLogger<AdviceService>(), Context);
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
            Context.Dispose();
            Context = null;
            Mapper = null;
        }
    }
}
