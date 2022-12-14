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
    public class BusinessInformation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisplayName(displayName: "編號")]
        public int Id { get; set; }
        

        [Display(Name = "顧客可選時間區間")]
        [MaxLength(50)]     //不設長度預設為nvarchar(max)
        public string TimeInterval { get; set; }


        [Display(Name = "平日開始營業時間")]
        [MaxLength(50)]
        public string WeekdayStartTime { get; set; }


        [Display(Name = "平日結束營業時間")]
        [MaxLength(50)]
        public string WeekdayEndTime { get; set; }


        [Display(Name = "平日休息開始")]
        [MaxLength(50)]
        public string WeekdayBreakStart { get; set; }


        [Display(Name = "平日休息結束")]
        [MaxLength(50)]
        public string WeekdayBreakEnd { get; set; }


        [Display(Name = "假日開始營業時間")]
        [MaxLength(50)]
        public string HolidayStartTime { get; set; }


        [Display(Name = "假日結束營業時間")]
        [MaxLength(50)]
        public string HolidayEndTime { get; set; }


        [Display(Name = "假日休息開始")]
        [MaxLength(50)]
        public string HolidayBreakStart { get; set; }


        [Display(Name = "假日休息結束")]
        [MaxLength(50)]
        public string HolidayBreakEnd { get; set; }


        [Display(Name = "公休日")]
        [MaxLength(20)]
        public string PublicHoliday { get; set; }

        [JsonIgnore]
        public virtual ICollection<StoreDetail> StoreDetail { get; set; }
    }
}