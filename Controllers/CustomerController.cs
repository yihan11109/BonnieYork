﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using BonnieYork.JWT;
using BonnieYork.Models;
using BonnieYork.Tool;
using Newtonsoft.Json.Linq;
using NSwag.Annotations;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace BonnieYork.Controllers
{
    [OpenApiTag("Customer", Description = "顧客資訊API")]
    [RoutePrefix("customer")]   //屬性路由前綴
    public class CustomerController : ApiController
    {
        private BonnieYorkDbContext db = new BonnieYorkDbContext();


        /// <summary>
        /// 顧客資訊顯示
        /// </summary>
        [HttpGet]
        [Route("GetInformation")]
        public IHttpActionResult GetInformation()
        {
            var userToken = JwtAuthFilter.GetToken(Request.Headers.Authorization.Parameter);
            int identityId = (int)userToken["IdentityId"];
            var customerInformation = db.CustomerDetail.Where(c => c.Id == identityId).Select(c => new
            {
                c.Account,
                c.CustomerName,
                c.CellphoneNumber,
                c.BirthDay,
                HeadShot = c.HeadShot == null ? null : "https://" + Request.RequestUri.Host + "/upload/headshot/" + c.HeadShot,
            }).ToList();

            return Ok(new { Identity = "member", CustomerInformation = customerInformation });
        }



        /// <summary>
        /// 顧客資訊存取
        /// </summary>
        [HttpPost]
        [Route("EditInformation")]
        public IHttpActionResult EditInformation(InformationDataView view)
        {
            var userToken = JwtAuthFilter.GetToken(Request.Headers.Authorization.Parameter);
            int identityId = (int)userToken["IdentityId"];
            var customerInformation = db.CustomerDetail.Where(c => c.Id == identityId).ToList();

            foreach (CustomerDetail item in customerInformation)
            {
                item.CustomerName = view.CustomerName;
                item.CellphoneNumber = view.CellphoneNumber;
                item.BirthDay = view.BirthDay;
            }
            db.SaveChanges();
            string token = JwtAuthUtil.GenerateToken(customerInformation[0].Id, 0, customerInformation[0].Account, "", "", customerInformation[0].CustomerName, "member");

            return Ok(new { Message = "顧客資訊修改完成", Token = token });
        }


        /// <summary>
        /// 顧客大頭照存取
        /// </summary>
        [HttpPost]
        [Route("UploadProfile")]
        public async Task<IHttpActionResult> UploadProfile()
        {
            // 檢查請求是否包含 multipart/form-data.
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }


            string root = HttpContext.Current.Server.MapPath(@"~/upload/headshot");

            try
            {
                // 讀取 MIME 資料
                var provider = new MultipartMemoryStreamProvider();
                try
                {
                    await Request.Content.ReadAsMultipartAsync(provider);
                }
                catch
                {
                    return BadRequest("檔案超過限制大小");
                }

                // 取得檔案副檔名，單檔用.FirstOrDefault()直接取出，多檔需用迴圈
                string fileNameData = provider.Contents.FirstOrDefault().Headers.ContentDisposition.FileName.Trim('\"');
                string fileType = fileNameData.Remove(0, fileNameData.LastIndexOf('.')); // .jpg

                // 定義檔案名稱
                string fileName = "UserName" + "Profile" + DateTime.Now.ToString("yyyyMMddHHmmss") + fileType;

                // 儲存圖片，單檔用.FirstOrDefault()直接取出，多檔需用迴圈
                var fileBytes = await provider.Contents.FirstOrDefault().ReadAsByteArrayAsync();
                if (fileBytes.Length > 5000000)
                {
                    return BadRequest("檔案超過限制大小");
                }
                var outputPath = Path.Combine(root, fileName);
                using (var output = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                {
                    await output.WriteAsync(fileBytes, 0, fileBytes.Length);
                }

                // 使用 SixLabors.ImageSharp 調整圖片尺寸 (正方形大頭貼)
                var image = SixLabors.ImageSharp.Image.Load<Rgba32>(outputPath);
                //要設定超過一個大小就限制大小
                var size = image.Size();
                if (size.Width > 1280 && size.Height > 1280)
                {
                    image.Mutate(x => x.Resize(0, 640)); // 輸入(120, 0)會保持比例出現黑邊

                }
                image.Save(outputPath);
                var userToken = JwtAuthFilter.GetToken(Request.Headers.Authorization.Parameter);
                int identityId = (int)userToken["IdentityId"];
                var customerHeadShot = db.CustomerDetail.Where(c => c.Id == identityId).ToList();
                customerHeadShot[0].HeadShot = fileName;
                db.SaveChanges();

                return Ok(new
                {
                    Message = "照片上傳成功",
                });
            }
            catch (Exception)
            {
                return BadRequest("照片上傳失敗或未上傳"); // 400
            }
        }


        /// <summary>
        /// 預約項目顯示
        /// </summary>
        [HttpGet]
        [Route("Reserve")]
        public IHttpActionResult Reserve([FromUri] int storeId, [FromUri] int itemId)
        {
            var userToken = JwtAuthFilter.GetToken(Request.Headers.Authorization.Parameter);
            int identityId = (int)userToken["IdentityId"];
            string customerName = (string)userToken["CustomerName"];
            var projectEmployees = db.StaffWorkItems;  //會這個項目的所有員工
            var customerCellPhoneNum = db.CustomerDetail.Where(c => c.Id == identityId).Select(c => c.CellphoneNumber);
            var itemsInformation = db.BusinessItems.Where(i => i.Id == storeId).Where(i => i.StoreId == storeId).Select(
                i => new
                {
                    PicturePath = i.PicturePath == null ? i.PicturePath : "https://" + Request.RequestUri.Host + "/upload/ItemsImage/" + i.PicturePath,
                    i.ItemName,
                    i.SpendTime,
                    i.Price,
                    CustomerName = customerName,
                    CustomerCellPhoneNum = customerCellPhoneNum,
                    TheStaffName = projectEmployees.Where(s => s.BusinessItemsId == itemId).Select(s => new
                    {
                        StaffId = s.Id,
                        s.StaffName
                    }),

                });
            return Ok(itemsInformation);
        }




        /// <summary>
        /// 預約日期及時間
        /// </summary>
        [HttpGet]
        [Route("SelectReserveTime")]
        public IHttpActionResult SelectReserveTime([FromUri] int storeId, [FromUri] int itemId, [FromUri] int staffId)
        {
            var userToken = JwtAuthFilter.GetToken(Request.Headers.Authorization.Parameter);
            int identityId = (int)userToken["IdentityId"];
            DateTime now = DateTime.Now;
            DateTime afterMonth = DateTime.Now.AddMonths(+1);

            //找出店家公休日
            var storeHolidayDate = db.StoreDetail.Where(s => s.Id == storeId).Select(s => new
            {
                s.HolidayDate,
                s.BusinessInformation.PublicHoliday,
            }).ToList();

            //店家設定的不定期公休日
            string[] storeHolidayDateArr = storeHolidayDate[0].HolidayDate.Split(',');
            string weekDay = "";
            //店家設定的每個禮拜的公休日
            List<string> theStoreWorkDate = new List<string>();
            do
            {
                switch (now.DayOfWeek)
                {
                    case DayOfWeek.Monday:
                        weekDay = "星期一";
                        break;
                    case DayOfWeek.Tuesday:
                        weekDay = "星期二";
                        break;
                    case DayOfWeek.Wednesday:
                        weekDay = "星期三";
                        break;
                    case DayOfWeek.Thursday:
                        weekDay = "星期四";
                        break;
                    case DayOfWeek.Friday:
                        weekDay = "星期五";
                        break;
                    case DayOfWeek.Saturday:
                        weekDay = "星期六";
                        break;
                    case DayOfWeek.Sunday:
                        weekDay = "星期日";
                        break;
                    default:
                        break;
                }
                if (!storeHolidayDateArr.Contains(now.ToShortDateString()) && weekDay != storeHolidayDate[0].PublicHoliday)
                {
                    theStoreWorkDate.Add(now.ToShortDateString());
                }
                now = now.AddDays(+1);
            } while (now != afterMonth);

            //從店家有營業的日期裡刪掉這個員工的休假日 = 這個員工可預約的日期
            var theStaffDaysOff = db.StaffDetail.Where(e => e.Id == staffId).Select(e => e.StaffDaysOff).ToList();
            List<string> theStaffWorkDate = new List<string>();
            //迴圈找店家工作日中有沒有員工休假日，如果沒有把店家工作日存入字串 = 員工工作日
            for (int i = 0; i < theStoreWorkDate.Count; i++)
            {
                //判斷店家工作日中有沒有員工休假日
                if (!theStaffDaysOff[0].Contains(theStoreWorkDate[i]))
                {
                    theStaffWorkDate.Add(theStoreWorkDate[i]);
                }
            }
            
            DateTime nextMonth = DateTime.Now.AddMonths(1);
            //在顧客預約表撈出現在日期往後推一個月範圍的預約
            var theStaffReserveDetail = db.CustomerReserve.Where(c => c.StaffId == staffId)
                .Where(c => c.ReserveState == "undone").Where(c => c.ItemId == itemId).Where(c =>
                    c.ReserveDate > DateTime.Now && c.ReserveDate <= nextMonth).ToList();

            JObject theStaffWorkDateJObject = new JObject();
            for (int i = 0; i < theStaffWorkDate.Count; i++)
            {
                //if (!theStaffWorkDate[i].Contains(theStaffReserveDetail[0].ReserveDate.ToShortDateString()))
                //{
                //    theStaffWorkDateJObject["TheDate"] = theStaffReserveDetail[0].ReserveDate.ToShortDateString();
                //    theStaffWorkDateJObject["TheReserveTime"] = 
                //}
            }
            
            return Ok(theStaffReserveDetail);
        }

    }
}
