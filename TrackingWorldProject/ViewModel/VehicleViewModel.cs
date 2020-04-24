using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using System.Linq;
using System.Web;

namespace TrackingWorldProject.ViewModel
{
    public class VehicleViewModel
    {
       
       
        public long VehicleId { get; set; }

        [StringLength(maximumLength: 50)]
        [System.ComponentModel.DisplayName("Registration No.")]
        public string RegNo { get; set; }

        [StringLength(maximumLength: 50)]
        public string Make { get; set; }

        [StringLength(maximumLength: 50)]
        public string Model { get; set; }

        [StringLength(maximumLength: 50)]
        public string Color { get; set; }

        [StringLength(maximumLength: 50)]
        [System.ComponentModel.DisplayName("Engine No.")]
        public string EngineNo { get; set; }

        [StringLength(maximumLength: 50)]
        public string ChasisNo { get; set; }

        [System.ComponentModel.DisplayName("Date Of Purchase")]
        public String DateOfPurchase { get; set; }


        public bool Active { get; set; }

    }
}