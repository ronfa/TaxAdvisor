using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using TaxAdvocate.Data.Contexts;
using TaxAdvocate.Data.Model;

namespace TaxAdvocate.Data.Generators
{
    public static class JsonDataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using var context = new InMemoryDatabaseContext(
                serviceProvider.GetRequiredService<DbContextOptions<InMemoryDatabaseContext>>());

            GenerateData(context);
        }

        public static void GenerateData(InMemoryDatabaseContext context)
        {
            if (context.Mutations.Any())
            {
                return;
            }

            // Load all json files into database
            var assembly = typeof(JsonDataGenerator).GetTypeInfo().Assembly;
            var fileNames = assembly.GetManifestResourceNames();

            foreach (var file in fileNames)
            {
                var stream = assembly.GetManifestResourceStream(file);
                if (stream is null) continue;
                var reader = new StreamReader(stream);
                var mutation = JsonConvert.DeserializeObject<Mutation>(reader.ReadToEnd());
                context.Mutations.Add(mutation);
            }

            context.SaveChanges();

            // Create client table and generate data

            var clients = context.Mutations.Select(m => m.ClientId).Distinct().ToList();

            foreach (var clientId in clients)
            {
                context.Clients.Add(new Client
                {
                    ClientId = clientId
                });
            }

            context.SaveChanges();
        }
    }
}
