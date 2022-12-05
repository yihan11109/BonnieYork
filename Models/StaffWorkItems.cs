using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BonnieYork.Models
{
    public class StaffWorkItems
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisplayName(displayName: "編號")]
        public int Id { get; set; }



        [Display(Name = "營業項目ID")]  //ForeignKey類別名稱
        public int BusinessItemsId { get; set; }


        [ForeignKey("BusinessItemsId")]  //綁關聯
        public virtual BusinessItems BusinessItems { get; set; }


        [Display(Name = "員工ID")] 
        public int StaffId { get; set; }


        [Display(Name = "員工名稱")]
        public string StaffName { get; set; }

    }
}