using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GarbageCollector.Models
{
    public class PickupModels
    {
        [Key]
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual CustomerModels User { get; set; }

        [Display(Name = "PickupDate")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime PickupDate { get; set;}
    }
}