using AutoMapper;
using CreditManagementSystem.Data.Models;
using CreditManagementSystem.Domain.CommandCredit;
using CreditManagementSystem.WebApi.Models.Credit;
using CreditManagementSystem.WebApi.Models.CreditStatus;

namespace CreditManagementSystem.WebApi.Settings
{
    public sealed class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreditStatus, CreditStatusDto>();
            CreateMap<Credit, CreditResultDto>();
            CreateMap<CreditCreateDto, CreditCreateCommand>();
            CreateMap<CreditUpdateDto, CreditUpdateCommand>();
            CreateMap<CreditDeleteDto, CreditDeleteCommand>();
        }
    }
}
