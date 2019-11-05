namespace MT940Data
{
    using AutoMapper;
    using programmersdigest.MT940Parser.Model;

    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Statement, MT940Data.Entities.Statement>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<Balance, MT940Data.Entities.StatementBalance>()
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => (byte)src.Type))
                .ForMember(dest => dest.Mark, opt => opt.MapFrom(src => (byte)src.Mark));
            CreateMap<Information, MT940Data.Entities.StatementInformation>();
            CreateMap<StatementLine, MT940Data.Entities.StatementLine>()
                .ForMember(dest => dest.Mark, opt => opt.MapFrom(src => (byte)src.Mark));
            CreateMap<Information, MT940Data.Entities.StatementLineInformation>();
        }
    }
}
