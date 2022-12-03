using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BonnieYork.Models
{
    public class BusinessItems
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisplayName(displayName: "編號")]
        public int Id { get; set; }


        [Required(ErrorMessage = "{0}必填")]
        [Display(Name = "店鋪ID")] //ForeignKey類別名稱
        public int StoreId { get; set; }


        [ForeignKey("StoreId")] //綁關聯
        public virtual StoreDetail StoreDetail { get; set; }


        [Required(ErrorMessage = "{0}必填")]
        [Display(Name = "項目名稱")]
        [MaxLength(10)]
        public string ItemName { get; set; }


        [Required(ErrorMessage = "{0}必填")]
        [Display(Name = "所需時間")]
        [MaxLength(10)]
        public string SpendTime { get; set; }


        [Required(ErrorMessage = "{0}必填")]
        [Display(Name = "金額")]
        [MaxLength(10)]
        public string Price { get; set; }


        [Display(Name = "描述")] public string Describe { get; set; }


        [Display(Name = "備註")] public string Remark { get; set; }


        [Display(Name = "項目圖片")] public string PicturePath { get; set; }
    }
}