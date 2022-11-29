using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BonnieYork.Models
{
    public class StaffHoliday
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisplayName(displayName: "編號")]
        public int Id { get; set; }


        [Display(Name = "員工ID")]  //ForeignKey類別名稱
        public int? StaffId { get; set; }


        [ForeignKey("StaffId")]  //綁關聯
        public virtual StaffDetail StaffDetail { get; set; }



        [Display(Name = "員工休假日")]
        public string StaffDaysOff { get; set; } 
    }
}