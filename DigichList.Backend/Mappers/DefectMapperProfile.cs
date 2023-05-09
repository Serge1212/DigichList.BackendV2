using AutoMapper;
using DigichList.Backend.ViewModel;
using DigichList.Core.Entities;

namespace DigichList.Backend.Mappers
{
    public class DefectMapperProfile : Profile
    {
        public DefectMapperProfile()
        {
            CreateMap<Defect, DefectViewModel>()
                .ForMember(x => x.CreatedAt, 
                    y => y.MapFrom(src => src.CreatedAt.ToShortDateString()))
                
                .ForMember(x => x.Publisher, 
                    y => y.MapFrom(src => src.Publisher.ToString()))

                .ForMember(x => x.Assignee, 
                    y => y.MapFrom(src => src.AssignedDefect.AssignedWorker.ToString()))

                .ForMember(x => x.StatusChangedAt,
                    y => y.MapFrom(src => src.AssignedDefect.StatusChangedAt.HasValue ?
                    src.AssignedDefect.StatusChangedAt.Value.ToShortDateString() :
                    "N/A"))

                .ForMember(x => x.Status, y => 
                {
                    y.MapFrom(src => src.AssignedDefect.Status.ToString());
                    y.NullSubstitute("Not Assigned");
                }).ReverseMap();
        }
    }
}
