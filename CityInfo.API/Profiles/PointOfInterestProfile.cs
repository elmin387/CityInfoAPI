using AutoMapper;

namespace CityInfo.API.Profiles
{
    public class PointOfInterestProfile:Profile 
    {
        public PointOfInterestProfile()
        {
            CreateMap<Entities.PointOfInterest, Models.PointOfInterestDto>();
            CreateMap<Models.PointOfInterestDto, Entities.PointOfInterest>();
            CreateMap<Models.PointOfInterestForCreationDto, Entities.PointOfInterest>();
            CreateMap<Entities.PointOfInterest, Models.PointOfInterestForCreationDto>();
            CreateMap<Models.PointOfInterestForCreationDto, Models.PointOfInterestDto>();
            CreateMap<Models.PointOfInterestForUpdateDto, Entities.PointOfInterest>();

        }
    }
}
