using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BonnieYork.Models
{
    public class StoreDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisplayName(displayName: "編號")]
        public int Id { get; set; }


        [Display(Name = "產業別ID")]  //ForeignKey類別名稱
        public int? IndustryId { get; set; }


        [ForeignKey("IndustryId")]  //綁關聯
        public virtual Industry Industry { get; set; }



        [Display(Name = "營業資訊ID")]  //ForeignKey類別名稱
        public int? BusinessInformationId { get; set; }


        [ForeignKey("BusinessInformationId")]  //綁關聯
        public virtual BusinessInformation BusinessInformation { get; set; }



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
        [MaxLength(50)]     //不設長度預設為nvarchar(max)
        [Display(Name = "店鋪名稱")]
        public string StoreName { get; set; }


        [Required(ErrorMessage = "{0}必填")]
        [MaxLength(50)]     //不設長度預設為nvarchar(max)
        [Display(Name = "縣市")]
        public string City { get; set; }


        [Required(ErrorMessage = "{0}必填")]
        [MaxLength(50)]     //不設長度預設為nvarchar(max)
        [Display(Name = "區域")]
        public string District { get; set; }


        [Required(ErrorMessage = "{0}必填")]
        [MaxLength(50)]     //不設長度預設為nvarchar(max)
        [Display(Name = "地址")]
        public string Address { get; set; }


        [Required(ErrorMessage = "{0}必填")]
        [MaxLength(15)]
        [Display(Name = "手機號碼")]
        public string CellphoneNumber { get; set; }


        [MaxLength(15)]
        [Display(Name = "市話")]
        public string PhoneNumber { get; set; }


        [MaxLength(20)]
        [Display(Name = "員工稱謂")]
        public string StaffTitle { get; set; }


        [Display(Name = "店鋪描述")]
        public string Description { get; set; }


        [Display(Name = "首頁圖")]
        public string BannerPath { get; set; }

        
        [Display(Name = "Facebook連結")]
        public string FacebookLink { get; set; }


        [Display(Name = "Instagram連結")]
        public string InstagramLink { get; set; }


        [Display(Name = "Line連結")]
        public string LineLink { get; set; }



        [Display(Name = "大頭照")]
        public string HeadShot { get; set; }


        [Display(Name = "公休日期")]
        public string HolidayDate { get; set; }



        public virtual ICollection<StaffDetail> StaffDetail { get; set; }
        public virtual ICollection<BusinessItems> BusinessItems { get; set; }

    }
}