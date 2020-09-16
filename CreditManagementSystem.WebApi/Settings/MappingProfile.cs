using AutoMapper;
using CreditManagementSystem.Client.Model.Credit;
using CreditManagementSystem.Client.Model.CreditStatus;
using CreditManagementSystem.Client.Model.RiskCenter;
using CreditManagementSystem.Data.Model;
using CreditManagementSystem.Domain.CommandCredit;

namespace CreditManagementSystem.WebApi.Settings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreditStatus, CreditStatusDto>();
            CreateMap<RiskCenter, RiskCenterDto>();
            CreateMap<Credit, CreditResultDto>();
            CreateMap<CreditCreateDto, CreditCreateCommand>();
            CreateMap<CreditUpdateDto, CreditUpdateCommand>();
            CreateMap<CreditDeleteDto, CreditDeleteCommand>();
        }
    }
}
