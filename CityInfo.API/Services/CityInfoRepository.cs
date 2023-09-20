using AutoMapper;
using CityInfo.API.ApplicationDbContext;
using CityInfo.API.Entities;
using CityInfo.API.Models;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;

namespace CityInfo.API.Services
{
    public class CityInfoRepository : ICityInfoRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        

        public CityInfoRepository(AppDbContext context, IMapper mapper)
        {
            _context=context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<IEnumerable<City>> GetCitiesAsync()
        {
            return await _context.Cities.OrderBy(o=>o.Name).ToListAsync();
        }

        public async Task<(IEnumerable<City>, PaginationMetadata)> GetCitiesAsync(string? name, string? searchQuery, int pageNumber, int pageSize)
        {

            var collection = _context.Cities as IQueryable<City>;

            if (!string.IsNullOrWhiteSpace(name))   
            {
                name = name.Trim();
                collection=collection.Where(o => o.Name == name);
            }

            if (!string.IsNullOrWhiteSpace(searchQuery))
            {
                searchQuery = searchQuery.Trim();
                collection = collection.Where(a =>
                    a.Name.Contains(searchQuery) || a.Description != null && a.Description.Contains(searchQuery));

            }

            var totalItemCount = await collection.CountAsync();
            var paginationMetadata = new PaginationMetadata(totalItemCount, pageSize, pageNumber);

            var collectionToReturn =  await collection.OrderBy(c => c.Name)
                .Skip(pageSize*(pageNumber-1)).Take(pageSize).ToListAsync();

            return (collectionToReturn, paginationMetadata);

        }

        public async Task<bool> CityNameMatchesCityId(string? cityName, int cityId)
        {
            return await _context.Cities.AnyAsync(c => c.Id == cityId && c.Name == cityName);
        }

        public async Task<City?> GetCityAsync(int cityId, bool includePointsOfInterest)
        {
            if (includePointsOfInterest)
            {
                 return await _context.Cities.Include(c => c.PointsOfInterests).Where(c => c.Id == cityId)
                    .FirstOrDefaultAsync();
            }
            return await _context.Cities.Where(c=>c.Id==cityId).FirstOrDefaultAsync();
        }

        public async Task<bool> CityExistsAsync(int cityId)
        {
            return await _context.Cities.AnyAsync(c => c.Id == cityId);
        }

        public async Task<PointOfInterest?> GetPointOfInterestForCityAsync(int cityId, int pointOfInterestId)
        {
            return await _context.PointsOfInterests.Where(p => p.CityId==cityId && p.Id == pointOfInterestId)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<PointOfInterest>> GetPointsOfInterestForCityAsync(int cityId)
        {
            return await _context.PointsOfInterests.Where(p=>p.CityId==cityId).ToListAsync();
        }

        public async Task AddPointOfInterestForCityAsync(int cityId, 
            PointOfInterest pointOfInterest)
        {
            
            var city = await GetCityAsync(cityId, false);
            if (city != null)
            {
                var entityPointOfInterest = _mapper.Map<Models.PointOfInterestDto>(pointOfInterest);

                city.PointsOfInterests.Add(entityPointOfInterest);
            }
        }

        public void DeletePointOfInterest(PointOfInterest pointOfInterest)
        {
            _context.PointsOfInterests.Remove(pointOfInterest);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()>=0);
        }
    }
}
