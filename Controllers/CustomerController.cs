using System;
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
            var storeHolidayDate = db.StoreDetail.Where(s => s.Id == storeId).Select(s => new
            {
                s.HolidayDate,
                s.BusinessInformation.PublicHoliday,
            }).ToList();
            string[] storeHolidayDateArr = storeHolidayDate[0].HolidayDate.Split(',');
            string weekDay = "";

            StringBuilder theStoreWorkDate = new StringBuilder();
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
                    theStoreWorkDate.Append(now.ToShortDateString());  // 找出所有扣除公休日後的可預約日期
                    theStoreWorkDate.Append(',');
                }
                now = now.AddDays(+1);

            } while (now != afterMonth);
            return Ok(theStoreWorkDate.ToString().TrimEnd(','));
        }

    }
}
