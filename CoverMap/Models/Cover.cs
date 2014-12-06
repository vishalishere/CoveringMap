using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CoverMap.Models
{
    public class Cover
    {
        public int CoverID { get; set; }

        [Required]
        [StringLength(50)]
        [RegularExpression(".{1,}")]
        public string NetworkName { get; set; }

        [Required]
        public float Longitude { get; set; }

        [Required]
        public float Lattitude { get; set; }

        [Required]
        [Range(1,5)]
        public int SignalStrength { get; set; }

        [Required]
        public DateTime Created { get; set; }

        [Required]
        public DateTime Updated { get; set; }

        [Required]
        //[StringLength(100)]
        public string Technology { get; set; }
    }
}