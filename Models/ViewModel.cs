using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BonnieYork.Models
{
    /// <summary>
    /// 
    /// </summary>
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


        
        [Display(Name = "大頭照")]
        public string HeadShot { get; set; }


        [Display(Name = "照片類型")]
        public string ImageType { get; set; }
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
        [Display(Name = "店鋪ID")]  
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
        public int[] BusinessItemsId { get; set; }
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
        [Display(Name = "確認密碼")]
        [StringLength(100, ErrorMessage = "{0} 長度至少必須為 {2} 個字元。", MinimumLength = 6)]  //最多100個字，最少6個字
        [DataType(DataType.Password)]
        public string CheckPassword { get; set; }
    }



    public class InformationDataView : ViewModel
    {
        // Customer Store Staff共用
        [Required(ErrorMessage = "{0}必填")]
        [Display(Name = "手機號碼")]
        public string CellphoneNumber { get; set; }


        //Customer
        [Required(ErrorMessage = "{0}必填")]
        [MaxLength(50)]     //不設長度預設為nvarchar(max)
        [Display(Name = "顧客名稱")]
        public string CustomerName { get; set; }


        ///Customer
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
        [Display(Name = "產業別編號")]
        public int? IndustryId { get; set; }



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



        [Display(Name = "員工ID")]  //ForeignKey類別名稱
        public int StaffId { get; set; }



        //Store
        [MaxLength(20)]
        [Display(Name = "員工稱謂")]
        public string StaffTitle { get; set; }


        //Store
        [Display(Name = "首頁圖")]
        public string BannerPath { get; set; }


        //Store
        [Display(Name = "顧客可選時間區間")]
        public string TimeInterval { get; set; }



        //Store
        [Display(Name = "平日開始營業時間")]
        public string WeekdayStartTime { get; set; }


        //Store
        [Display(Name = "平日結束營業時間")]
        public string WeekdayEndTime { get; set; }


        [Display(Name = "平日休息開始")]
        [MaxLength(50)]
        public string WeekdayBreakStart { get; set; }


        [Display(Name = "平日休息結束")]
        [MaxLength(50)]
        public string WeekdayBreakEnd { get; set; }


        //Store
        [Display(Name = "假日開始營業時間")]
        public string HolidayStartTime { get; set; }



        //Store
        [Display(Name = "假日結束營業時間")]
        public string HolidayEndTime { get; set; }


        [Display(Name = "假日休息開始")]
        [MaxLength(50)]
        public string HolidayBreakStart { get; set; }


        [Display(Name = "假日休息結束")]
        [MaxLength(50)]
        public string HolidayBreakEnd { get; set; }


        //Store
        [Display(Name = "公休日")]
        public string PublicHoliday { get; set; }



        [Display(Name = "公休日期")]
        public string HolidayDate { get; set; }



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
        [Display(Name = "自我介紹")]
        public string Introduction { get; set; }



        //Staff
        [MaxLength(20)]     //不設長度預設為nvarchar(max)
        [Display(Name = "員工職稱")]
        public string JobTitle { get; set; }


        //Staff
        [Display(Name = "員工照片")]
        public string PicturePath { get; set; }


        [Display(Name = "員工工作項目ID")]
        public string BusinessItemsId { get; set; }


        [Display(Name = "員工休假日")]
        public string StaffDaysOff { get; set; }
    }


    public class AllItems:InformationDataView
    {

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


        [Display(Name = "描述")] 
        public string Describe { get; set; }


        [Display(Name = "備註")] 
        public string Remark { get; set; }
    }

    public class ReserveInformation : ViewModel
    {
        [Display(Name = "預約項目ID")]
        public int? ReserveId { get; set; }


        [Display(Name = "顧客ID")] 
        public int? CustomerId { get; set; }



        [Display(Name = "營業項目ID")]  
        public int ItemId { get; set; }



        [Display(Name = "員工ID")] 
        public int StaffId { get; set; }


        [Display(Name = "員工名稱")] 
        public string StaffName { get; set; }



        [Display(Name = "店家ID")]
        public int StoreId { get; set; }



        [Display(Name = "項目金額")]
        [MaxLength(10)]
        public string Price { get; set; }



        [DataType(DataType.Date)]
        [DisplayName("預約日期")]
        [Required(ErrorMessage = "請選擇預約日期")]
        public DateTime ReserveDate { get; set; }



        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{HH:mm }")]
        [DataType(DataType.Date)]
        [DisplayName("預約開始時間")]
        [Required(ErrorMessage = "請選擇預約時間")]
        public DateTime ReserveStart { get; set; }



        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{HH:mm }")]
        [DataType(DataType.Date)]
        [DisplayName("預約結束時間")]
        public DateTime ReserveEnd { get; set; }



        [DisplayName("預約狀態")]
        public string ReserveState { get; set; }



        [DisplayName("手填顧客姓名")]
        public string ManualName { get; set; }



        [DisplayName("手填顧客手機")]
        public string ManualCellphoneNumber { get; set; }


        [EmailAddress(ErrorMessage = "Email格式不符")]
        [MaxLength(100)]
        [DataType(DataType.EmailAddress)]
        [DisplayName("手填顧客Email")]
        public string ManualEmail { get; set; }
    }

    public class SearchStore : ViewModel
    {
        //Store
        [Display(Name = "產業別編號")]
        public int? IndustryId { get; set; }


        //Store
        [MaxLength(50)]     //不設長度預設為nvarchar(max)
        [Display(Name = "縣市")]
        public string City { get; set; }



        //Store
        [MaxLength(50)]     //不設長度預設為nvarchar(max)
        [Display(Name = "區域")]
        public string District { get; set; }


        //Store
        [MaxLength(50)]     //不設長度預設為nvarchar(max)
        [Display(Name = "地址")]
        public string Address { get; set; }


        //Store
        [MaxLength(50)]     //不設長度預設為nvarchar(max)
        [Display(Name = "關鍵字")]
        public string Keyword { get; set; }

        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}