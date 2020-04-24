using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrackingWorldProject.Models
{
    public class Vehicle
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long VehicleId { get; set; }

        [StringLength(maximumLength:50)]
        
        public string RegNo { get; set; }

        [StringLength(maximumLength: 50)]
        
        public string Make { get; set; }

        [StringLength(maximumLength: 50)]
       
        public string Model { get; set; }

        [StringLength(maximumLength: 50)]
        
        public string Color { get; set; }

        [StringLength(maximumLength: 50)]
       
        public string EngineNo { get; set; }

        [StringLength(maximumLength: 50)]
       
        public string ChasisNo { get; set; }

        
        public DateTime DateOfPurchase{ get; set; }

        
        public bool Active { get; set; }


    }




}
