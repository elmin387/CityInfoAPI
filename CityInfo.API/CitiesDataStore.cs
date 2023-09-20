using CityInfo.API.Models;

namespace CityInfo.API
{
    public class CitiesDataStore
    {
        public List<CityDto> Cities { get; set; }
        //public static CitiesDataStore Current { get; } = new CitiesDataStore();

        public CitiesDataStore()
        {
            Cities = new List<CityDto>()
            {
                new CityDto()
                {
                    Id = 1,
                    Name = "New York city",
                    Description = "The one of that big park",
                    PointsOfInterests = new List <PointOfInterestDto>()
                    {
                        new PointOfInterestDto()
                        {
                            Id=1,
                            Name="Central park",
                            Description="The most visited park in USA"
                        },
                        new PointOfInterestDto()
                        {
                            Id=2,
                            Name="Empire state building",
                            Description="Skyscaper located in Manhatan"
                        },
                        
                    }
                    
                },
                new CityDto()
                {
                    Id = 2,
                    Name = "Antwerpen",
                    Description = "Cathedral",
                    PointsOfInterests = new List<PointOfInterestDto>()
                    {
                        new PointOfInterestDto()
                        {
                            Id=3,
                            Name="Cathedral",
                            Description="A Ghotic style cathedral, conceived by arhitects Jan and Piete"
                        },
                        new PointOfInterestDto()
                        {
                            Id=4,
                            Name="Antwerp Central Station",
                            Description="The finest example of railway arhitecture in Belgium"
                        }

                    }
                },
                new CityDto()
                {
                    Id = 3,
                    Name = "Paris",
                    Description = "Tower",
                    PointsOfInterests = new List<PointOfInterestDto>()
                    {
                        new PointOfInterestDto()
                        {
                            Id=5,
                            Name="Eiffel Tower",
                            Description="A wrought iron lattice tower on the Champ de Mars"
                        },
                        new PointOfInterestDto()
                        {
                            Id=6,
                            Name="The Louvre",
                            Description="The world s largest museum"
                        }

                    }
                }
            };
        }
    }
}
