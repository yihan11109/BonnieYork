using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BonnieYork.Models
{
    public class StaffDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisplayName(displayName: "編號")]
        public int Id { get; set; }


        [Display(Name = "店鋪ID")]  //ForeignKey類別名稱
        public int StoreId { get; set; }


        [ForeignKey("StoreId")]  //綁關聯
        public virtual StoreDetail StoreDetail { get; set; }



        [Required(ErrorMessage = "{0}必填")]
        [Display(Name = "帳號")]
        [EmailAddress(ErrorMessage = "Email格式不符")]
        [RegularExpression(@"^[A-Za-z0-9_\-\.\+]*\@[a-z]*[.][a-z]*(.[a-z]*)$", ErrorMessage = "請輸入正確的Email")]
        [MaxLength(100)]
        [DataType(DataType.EmailAddress)]
        public string Account { get; set; }


        [Required(ErrorMessage = "{0}必填")]
        [Display(Name = "密碼")]
        [StringLength(100, ErrorMessage = "{0} 長度至少必須為 {2} 個字元。", MinimumLength = 6)]  //最多100個字，最少4個字
        [DataType(DataType.Password)]
        public string Password { get; set; }


        [Required(ErrorMessage = "{0}必填")]
        [MaxLength(20)]     //不設長度預設為nvarchar(max)
        [Display(Name = "員工名稱")]
        public string StaffName { get; set; }


        [MaxLength(20)]     //不設長度預設為nvarchar(max)
        [Display(Name = "員工職稱")]
        public string JobTitle { get; set; }



        [Display(Name = "員工照片")]
        public string PicturePath { get; set; }


        [MaxLength(20)]     //不設長度預設為nvarchar(max)
        [Display(Name = "手機號碼")]
        public string CellphoneNumber { get; set; }


        [Display(Name = "自我介紹")]
        public string Introduction { get; set; }


        [Display(Name = "Facebook連結")]
        public string FacebookLink { get; set; }


        [Display(Name = "Instagram連結")]
        public string InstagramLink { get; set; }


        [Display(Name = "Line連結")]
        public string LineLink { get; set; }

        //[Display(Name = "工作項目")]
        //public string LineLink { get; set; }


        public virtual ICollection<StaffHoliday> StaffHoliday { get; set; }
    }
}