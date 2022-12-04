using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace BonnieYork.Models
{
    public class Industry
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisplayName(displayName: "編號")]
        public int Id { get; set; }


        [Required(ErrorMessage = "{0}必填")]
        [MaxLength(50)]     //不設長度預設為nvarchar(max)
        [Display(Name = "產業名稱")]
        public string IndustryName { get; set; }


        public virtual ICollection<StoreDetail> StoreDetail { get; set; }
    }
}