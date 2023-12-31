﻿using CityInfo.API.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CityInfo.API.Entities
{
    public class PointOfInterest
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [MaxLength(200)]
        public string Description { get; set; }

        [ForeignKey("CityId")]
        public City? City { get; set; }
        public int CityId { get; set; }

        public ICollection<PointOfInterestDto> PointsOfInterests { get; set; }
            = new List<PointOfInterestDto>();

        public PointOfInterest(string name)
        {
            Name = name;
        }
    }
}
