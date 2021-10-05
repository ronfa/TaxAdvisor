using AutoMapper;
using TaxAdvocate.Business.Model;
using Client = TaxAdvocate.Data.Model.Client;
using Mutation = TaxAdvocate.Data.Model.Mutation;

namespace TaxAdvocate.Business.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Client, Model.Client>();
            CreateMap<Mutation, Model.Mutation>();
            CreateMap<ValidationResult, ValidationResponse>();
        }
    }
}
