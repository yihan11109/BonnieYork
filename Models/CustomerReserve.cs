using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BonnieYork.Models
{
    public class CustomerReserve
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisplayName(displayName: "編號")]
        public int Id { get; set; }



        [Display(Name = "顧客ID")]  //ForeignKey類別名稱
        public int? CustomerId { get; set; }
        [ForeignKey("CustomerId")]  //綁關聯
        public virtual CustomerDetail CustomerDetail { get; set; }



        [Display(Name = "營業項目ID")]  //ForeignKey類別名稱
        public int ItemId { get; set; }
        [ForeignKey("ItemId")]  //綁關聯
        public virtual BusinessItems BusinessItems { get; set; }



        [Display(Name = "員工ID")] //ForeignKey類別名稱
        public int StaffId { get; set; }


        [Display(Name = "員工名稱")] //ForeignKey類別名稱
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
}