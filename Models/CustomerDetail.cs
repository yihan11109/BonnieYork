using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BonnieYork.Models
{
    public class CustomerDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisplayName(displayName: "編號")]
        public int Id { get; set; }



        [Required(ErrorMessage = "{0}必填")]
        [Display(Name = "帳號")]
        [EmailAddress(ErrorMessage = "Email格式不符")]
        [RegularExpression(@"^[A-Za-z0-9_\-\.\+]*\@[a-z]*[.][a-z]*(.[a-z]*)$", ErrorMessage = "請輸入正確的Email")]
        [MaxLength(100)]
        [DataType(DataType.EmailAddress)]
        public string Account { get; set; }


        [Required(ErrorMessage = "{0}必填")]
        [Display(Name = "密碼")]
        [StringLength(100, ErrorMessage = "{0} 長度至少必須為 {2} 個字元。", MinimumLength = 6)]  //最多100個字，最少6個字
        [DataType(DataType.Password)]
        public string Password { get; set; }


        [Required(ErrorMessage = "{0}必填")]
        [MaxLength(50)]     //不設長度預設為nvarchar(max)
        [Display(Name = "名稱")]
        public string CustomerName { get; set; } 


        [Required(ErrorMessage = "{0}必填")]
        [Display(Name = "手機號碼")]
        public string CellphoneNumber { get; set; }


        [Display(Name = "生日")]
        [MaxLength(50)]     
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "yyyy-MM-dd")]
        [DataType(DataType.Date)]
        public string BirthDay { get; set; }


        [Display(Name = "大頭照")]
        public string HeadShot { get; set; }
    }
}