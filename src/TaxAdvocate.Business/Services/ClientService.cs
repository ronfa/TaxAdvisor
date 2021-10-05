using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TaxAdvocate.Business.Evaluators;
using TaxAdvocate.Business.Model;
using TaxAdvocate.Data.Contexts;

namespace TaxAdvocate.Business.Services
{
    public class ClientService : IClientService
    {
        private readonly ILogger<ClientService> _logger;
        private readonly InMemoryDatabaseContext _context;
        private readonly IMapper _mapper;

        public ClientService(ILogger<ClientService> logger, InMemoryDatabaseContext context, IMapper mapper)
        {
            _logger = logger;
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<Client>> GetAllAsync()
        {
            var clients = 
                await _context.Clients.Include(c => c.Mutations).ToListAsync();

            return clients.Select(c => _mapper.Map<Client>(c)).ToList();
        }

        public async Task<List<Client>> GetAllWithAdviceIndicatorAsync(IEvaluator indicator)
        {
            // Run validation
            var clients = await indicator.ValidateAsync();

            // Map clients
            var mapped = clients.Select(c => _mapper.Map<Client>(c)).ToList();

            // Map validations 
            var responses = indicator.Results.Select(c => _mapper.Map<ValidationResponse>(c)).ToList();

            // Return proper validation result per client
            mapped.ForEach(c => c.ValidationResults = responses.Where(r => r.ClientId == c.ClientId));

            return mapped.ToList();
        }
    }
}
