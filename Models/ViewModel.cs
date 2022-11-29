using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BonnieYork.Models
{
    public class ViewModel
    {

        [Required(ErrorMessage = "{0}必填")]
        [Display(Name = "身分別")]
        [MaxLength(10)]
        public string Identity { get; set; }


        [Required(ErrorMessage = "{0}必填")]
        [Display(Name = "帳號")]
        [EmailAddress(ErrorMessage = "Email格式不符")]
        [MaxLength(200)]
        [DataType(DataType.EmailAddress)]
        public string Account { get; set; }
    }

    public class SignUpUserDataView: ViewModel
    {
        // Customer Store Staff共用
        [Required(ErrorMessage = "{0}必填")]
        [Display(Name = "密碼")]
        [StringLength(100, ErrorMessage = "{0} 長度至少必須為 {2} 個字元。", MinimumLength = 6)]  //最多100個字，最少6個字
        [DataType(DataType.Password)]
        public string Password { get; set; }


        // Customer Store Staff共用
        [Required(ErrorMessage = "{0}必填")]
        [Display(Name = "密碼")]
        [StringLength(100, ErrorMessage = "{0} 長度至少必須為 {2} 個字元。", MinimumLength = 6)]  //最多100個字，最少6個字
        [DataType(DataType.Password)]
        public string CheckPassword { get; set; }


        // Customer Store Staff共用
        [Required(ErrorMessage = "{0}必填")]
        [Display(Name = "手機號碼")]
        public string CellphoneNumber { get; set; }


        //Customer
        [Required(ErrorMessage = "{0}必填")]
        [MaxLength(50)]     //不設長度預設為nvarchar(max)
        [Display(Name = "顧客名稱")]
        public string CustomerName { get; set; }


        //Customer
        [Display(Name = "生日")]
        [MaxLength(50)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "yyyy-MM-dd")]
        [DataType(DataType.Date)]
        public string BirthDay { get; set; }
        

        //Store
        [Required(ErrorMessage = "{0}必填")]
        [MaxLength(50)]     //不設長度預設為nvarchar(max)
        [Display(Name = "店家名稱")]
        public string StoreName { get; set; }


        //Store
        [Display(Name= "產業別編號")]
        public int IndustryId { get; set; }



        //Store
        [Required(ErrorMessage = "{0}必填")]
        [MaxLength(50)]     //不設長度預設為nvarchar(max)
        [Display(Name = "縣市")]
        public string City { get; set; }


        //Store
        [Required(ErrorMessage = "{0}必填")]
        [MaxLength(50)]     //不設長度預設為nvarchar(max)
        [Display(Name = "區域")]
        public string District { get; set; }


        //Store
        [Required(ErrorMessage = "{0}必填")]
        [MaxLength(50)]     //不設長度預設為nvarchar(max)
        [Display(Name = "地址")]
        public string Address { get; set; }


        //Store
        [MaxLength(15)]
        [Display(Name = "市話")]
        public string PhoneNumber { get; set; }


        //Store
        [MaxLength(20)]
        [Display(Name = "員工稱謂")]
        public string StaffTitle { get; set; }


        //Store
        [Display(Name = "首頁圖")]
        public string BannerPath { get; set; }


        //Store、Staff
        [Display(Name = "描述")]
        public string Description { get; set; }


        //Store、Staff
        [Display(Name = "Facebook連結")]
        public string FacebookLink { get; set; }


        //Store、Staff
        [Display(Name = "Instagram連結")]
        public string InstagramLink { get; set; }


        //Store、Staff
        [Display(Name = "Line連結")]
        public string LineLink { get; set; }


        //Staff
        [Display(Name = "店鋪ID")]  //ForeignKey類別名稱
        public int StoreId { get; set; }



        //Staff
        [Required(ErrorMessage = "{0}必填")]
        [MaxLength(20)]     //不設長度預設為nvarchar(max)
        [Display(Name = "員工名稱")]
        public string StaffName { get; set; }


        //Staff
        [MaxLength(20)]     //不設長度預設為nvarchar(max)
        [Display(Name = "員工職稱")]
        public string JobTitle { get; set; }


        //Staff
        [Display(Name = "員工照片")]
        public string PicturePath { get; set; }


        [Display(Name = "員工工作項目ID")]
        public int BusinessItemsId { get; set; }
    }

    public class ResetPasswordView : ViewModel
    {
        [Required(ErrorMessage = "{0}必填")]
        [Display(Name = "密碼")]
        [StringLength(100, ErrorMessage = "{0} 長度至少必須為 {2} 個字元。", MinimumLength = 6)]  //最多100個字，最少6個字
        [DataType(DataType.Password)]
        public string OriginalPassword { get; set; }


        [Required(ErrorMessage = "{0}必填")]
        [Display(Name = "密碼")]
        [StringLength(100, ErrorMessage = "{0} 長度至少必須為 {2} 個字元。", MinimumLength = 6)]  //最多100個字，最少6個字
        [DataType(DataType.Password)]
        public string Password { get; set; }


        [Required(ErrorMessage = "{0}必填")]
        [Display(Name = "密碼")]
        [StringLength(100, ErrorMessage = "{0} 長度至少必須為 {2} 個字元。", MinimumLength = 6)]  //最多100個字，最少6個字
        [DataType(DataType.Password)]
        public string CheckPassword { get; set; }
    }
}