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
                    y => y.MapFrom(src => src.CreatedBy))

                .ForMember(x => x.Assignee,
                    y => y.MapFrom(src => src.AssignedWorker.FirstName)) // TODO: make more informative.

                .ForMember(x => x.StatusChangedAt,
                    y => y.MapFrom(src => src.StatusChangedAt.HasValue ?
                    src.StatusChangedAt.Value.ToShortDateString() :
                    "N/A"))

                .ForMember(x => x.Status, y =>
                {
                    y.MapFrom(src => src.Status.ToString()); //TODO: make informative.
                    y.NullSubstitute("Not Assigned");
                }).ReverseMap();
        }
    }
}
