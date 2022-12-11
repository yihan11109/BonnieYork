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
    [OpenApiTag("Staff", Description = "員工資訊API")]
    [RoutePrefix("staff")]   //屬性路由前綴
    public class StaffController : ApiController
    {
        private BonnieYorkDbContext db = new BonnieYorkDbContext();
        object result = new { };


        /// <summary>
        /// 員工資訊顯示
        /// </summary>
        [HttpGet]
        [Route("GetInformation")]
        public IHttpActionResult GetInformation()
        {
            var userToken = JwtAuthFilter.GetToken(Request.Headers.Authorization.Parameter);
            int identityId = (int)userToken["IdentityId"];
            var staffInformation = db.StaffDetail.Where(e => e.Id == identityId).Select(e => new
            {
                e.Account,
                e.StoreDetail.StoreName,
                e.StaffName,
                e.CellphoneNumber,
                e.Introduction,
                e.FacebookLink,
                e.InstagramLink,
                e.LineLink,
                HeadShot = e.HeadShot == null ? null : "https://" + Request.RequestUri.Host + "/upload/headshot/" + e.HeadShot,
            }).ToList();
            return Ok(new { Identity = "staff", StaffInformation = staffInformation });
        }



        /// <summary>
        /// 員工資訊存取
        /// </summary>
        [HttpPost]
        [Route("EditInformation")]
        public IHttpActionResult EditInformation(InformationDataView view)
        {
            var userToken = JwtAuthFilter.GetToken(Request.Headers.Authorization.Parameter);
            int identityId = (int)userToken["IdentityId"];
            int storeId = (int)userToken["StoreId"];
            var staffInformation = db.StaffDetail.Where(s => s.Id == identityId).ToList();
            var belongs = db.StoreDetail.Where(e => e.Id == storeId).Select(e => e.StoreName).ToList();

            foreach (StaffDetail item in staffInformation)
            {
                item.StaffName = view.StaffName;
                item.CellphoneNumber = view.CellphoneNumber;
                item.Introduction = view.Introduction;
                item.FacebookLink = view.FacebookLink;
                item.InstagramLink = view.InstagramLink;
                item.LineLink = view.LineLink;
            }
            db.SaveChanges();
            string token = JwtAuthUtil.GenerateToken(staffInformation[0].Id, staffInformation[0].StoreId, staffInformation[0].Account, belongs[0], staffInformation[0].StaffName, "", "staff");

            result = new
            {
                Message = "員工資訊修改完成",
                Token = token,
            };
            return Ok(result);
        }


        /// <summary>
        /// 員工大頭照存取
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
                var staffHeadShot = db.StaffDetail.Where(c => c.Id == identityId).ToList();
                staffHeadShot[0].HeadShot = fileName;
                image.Save(outputPath);
                db.SaveChanges();

                return Ok(new
                {
                    Message = "照片上傳成功",
                });
            }
            catch (Exception e)
            {
                return BadRequest("照片上傳失敗或未上傳"); // 400
            }
        }



        /// <summary>
        /// 員工休假日顯示
        /// </summary>
        [HttpGet]
        [JwtAuthFilter]
        [Route("GetStaffDaysOff")]
        public IHttpActionResult GetHolidayDate()
        {
            var userToken = JwtAuthFilter.GetToken(Request.Headers.Authorization.Parameter);
            int identityId = (int)userToken["IdentityId"];
            var theHoliday = db.StaffDetail.Where(e => e.Id == identityId).Select(e => e.StaffDaysOff).ToList();

            if (theHoliday[0] != null)
            {
                //把所有公休日轉成string陣列
                string[] stringHolidayArr = theHoliday[0].Split(',');
                StringBuilder showStaffDaysOff = new StringBuilder();
                StringBuilder pastStaffDaysOff = new StringBuilder();
                DateTime[] dateHolidayArr = new DateTime[stringHolidayArr.Length];

                for (int i = 0; i < stringHolidayArr.Length; i++)
                {
                    //把string陣列的公休日轉成DateTime陣列
                    dateHolidayArr[i] = DateTime.Parse(stringHolidayArr[i]);
                    //字串存入這個月跟下個月的公休日
                    if (dateHolidayArr[i].Month == DateTime.Now.Month || dateHolidayArr[i].Month == DateTime.Now.AddMonths(1).Month)
                    {
                        showStaffDaysOff.Append(dateHolidayArr[i].ToString("yyyy/MM/dd"));
                        showStaffDaysOff.Append(',');
                    }
                    //字串存入這個月之前及下個月之後的日期
                    else if (dateHolidayArr[i].Month != DateTime.Now.Month || dateHolidayArr[i].Month > DateTime.Now.AddMonths(1).Month)
                    {
                        pastStaffDaysOff.Append(dateHolidayArr[i].ToString("yyyy/MM/dd"));
                        pastStaffDaysOff.Append(',');
                    }
                }
                if (pastStaffDaysOff.ToString() == "")
                {
                    return Ok(new { PastStaffDaysOff = "", ShowStaffDaysOff = showStaffDaysOff.ToString().TrimEnd(',') });
                }
                else if (showStaffDaysOff.ToString() == "")
                {
                    return Ok(new { PastStaffDaysOff = pastStaffDaysOff.ToString().TrimEnd(','), ShowStaffDaysOff = "" });
                }
                else
                {
                    return Ok(new { PastStaffDaysOff = pastStaffDaysOff.ToString().TrimEnd(','), ShowStaffDaysOff = showStaffDaysOff.ToString().TrimEnd(',') });
                }
            }
            else
            {
                return Ok(new { PastStaffDaysOff = "", ShowStaffDaysOff = "" });
            }


        }



        /// <summary>
        /// 員工修改休假日
        /// </summary>
        [HttpPost]
        [JwtAuthFilter]
        [Route("EditStaffDaysOff")]
        public IHttpActionResult EditHolidayDate(InformationDataView view)
        {
            var userToken = JwtAuthFilter.GetToken(Request.Headers.Authorization.Parameter);
            int identityId = (int)userToken["IdentityId"];
            var theHoliday = db.StaffDetail.Where(s => s.Id == identityId).ToList();
            StaffDetail staffDaysOff = new StaffDetail();

            if (theHoliday.Count != 0)
            {
                theHoliday[0].StaffDaysOff = view.StaffDaysOff;
            }
            else
            {
                staffDaysOff.StaffDaysOff = view.StaffDaysOff;
                db.StaffDetail.Add(staffDaysOff);
            }
            db.SaveChanges();

            db.SaveChanges();
            return Ok(new { Message = "休假日修改成功" });
        }
    }
}
