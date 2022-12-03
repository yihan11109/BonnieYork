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
using System.Web.UI.WebControls;
using BonnieYork.JWT;
using BonnieYork.Models;
using BonnieYork.Tool;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NSwag.Annotations;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace BonnieYork.Controllers
{
    [OpenApiTag("Store", Description = "店家資訊API")]
    [RoutePrefix("store")] //屬性路由前綴
    public class StoreController : ApiController
    {
        private BonnieYorkDbContext db = new BonnieYorkDbContext();
        object result = new { };


        [HttpGet]
        [Route("GetInformation")]
        public IHttpActionResult GetInformation()
        {
            var userToken = JwtAuthFilter.GetToken(Request.Headers.Authorization.Parameter);
            int storeId = (int)userToken["StoreId"];
            var bannerPath = db.StoreDetail.Where(s => s.Id == storeId).Select(s => s.BannerPath).ToList();
            JObject bannerJObject = new JObject(); //宣告空物件
            if (bannerPath[0] != null)
            {
                bannerJObject = JObject.Parse(bannerPath[0]); //把bannerPath(string) 轉成物件
            }

            foreach (var item in bannerJObject)
            {
                bannerJObject[item.Key] = "https://" + Request.RequestUri.Host + "/upload/Banner/" + item.Value;
            }

            var storeInformation = db.StoreDetail.Where(s => s.Id == storeId).Select(s => new
            {
                s.StoreName,
                s.Industry.Id,
                s.City,
                s.District,
                s.Address,
                s.StaffTitle,
                s.BusinessInformation.TimeInterval,
                s.BusinessInformation.WeekdayStartTime,
                s.BusinessInformation.WeekdayEndTime,
                s.BusinessInformation.WeekdayBreakTime,
                s.BusinessInformation.HolidayStartTime,
                s.BusinessInformation.HolidayEndTime,
                s.BusinessInformation.HolidayBreakTime,
                s.BusinessInformation.PublicHoliday,
                s.Description,
                HeadShot = "https://" + Request.RequestUri.Host + "/upload/HeadShot/" + s.HeadShot,
                s.CellphoneNumber,
                s.PhoneNumber,
                s.FacebookLink,
                s.InstagramLink,
                s.LineLink
            }).ToList();

            return Ok(new { Identity = "store", StoreInformation = storeInformation, BannerPath = bannerJObject });
        }


        [HttpPost]
        [Route("EditInformation")]
        public IHttpActionResult EditInformation(InformationDataView view)
        {
            var userToken = JwtAuthFilter.GetToken(Request.Headers.Authorization.Parameter);
            int identityId = (int)userToken["IdentityId"];
            var storeDetailInDb = db.StoreDetail.Where(s => s.Id == identityId).ToList();

            if (view.IndustryId == null)
            {
                return BadRequest($"{view.IndustryId}欄位必填");
            }

            if (view.City == null)
            {
                return BadRequest("City欄位必填");
            }

            if (view.District == null)
            {
                return BadRequest("District欄位必填");
            }

            if (view.Address == null)
            {
                return BadRequest("{Address欄位必填");
            }

            if (view.JobTitle == null)
            {
                return BadRequest("JobTitle欄位必填");
            }

            if (view.TimeInterval == null)
            {
                return BadRequest("TimeInterval欄位必填");
            }

            if (view.WeekdayStartTime == null)
            {
                return BadRequest("WeekdayStartTime欄位必填");
            }

            if (view.WeekdayEndTime == null)
            {
                return BadRequest("WeekdayEndTime欄位必填");
            }

            if (view.HolidayStartTime == null)
            {
                return BadRequest("HolidayStartTime欄位必填");
            }

            if (view.HolidayEndTime == null)
            {
                return BadRequest("HolidayEndTime欄位必填");
            }

            if (view.Description == null)
            {
                return BadRequest("Description欄位必填");
            }

            if (view.BannerPath == null)
            {
                return BadRequest("BannerPath欄位必填");
            }

            if (view.HeadShot == null)
            {
                return BadRequest("HeadShot欄位必填");
            }


            foreach (StoreDetail item in storeDetailInDb)
            {
                if (item.BusinessInformation == null)
                {
                    BusinessInformation businessInformation = new BusinessInformation();
                    businessInformation.TimeInterval = view.TimeInterval;
                    businessInformation.WeekdayStartTime = view.WeekdayStartTime;
                    businessInformation.WeekdayEndTime = view.WeekdayEndTime;
                    businessInformation.WeekdayBreakTime = view.WeekdayBreakTime;
                    businessInformation.HolidayStartTime = view.HolidayStartTime;
                    businessInformation.HolidayEndTime = view.HolidayEndTime;
                    businessInformation.HolidayBreakTime = view.HolidayBreakTime;
                    businessInformation.PublicHoliday = view.PublicHoliday;
                    item.BusinessInformation = businessInformation;

                }
                else
                {
                    item.BusinessInformation.TimeInterval = view.TimeInterval;
                    item.BusinessInformation.WeekdayStartTime = view.WeekdayStartTime;
                    item.BusinessInformation.WeekdayEndTime = view.WeekdayEndTime;
                    item.BusinessInformation.WeekdayBreakTime = view.WeekdayBreakTime;
                    item.BusinessInformation.HolidayStartTime = view.HolidayStartTime;
                    item.BusinessInformation.HolidayEndTime = view.HolidayEndTime;
                    item.BusinessInformation.HolidayBreakTime = view.HolidayBreakTime;
                    item.BusinessInformation.PublicHoliday = view.PublicHoliday;
                }

                item.StoreName = view.StoreName;
                item.IndustryId = view.IndustryId;
                item.City = view.City;
                item.District = view.District;
                item.Address = view.Address;
                item.StaffTitle = view.StaffTitle;
                item.Description = view.Description;
                item.FacebookLink = view.FacebookLink;
                item.InstagramLink = view.InstagramLink;
                item.LineLink = view.LineLink;
                item.CellphoneNumber = view.CellphoneNumber;
                item.PhoneNumber = view.PhoneNumber;

            }

            db.SaveChanges();
            string token = JwtAuthUtil.GenerateToken(storeDetailInDb[0].Id, storeDetailInDb[0].Id,
                storeDetailInDb[0].Account, storeDetailInDb[0].StoreName, "", "", "store");

            result = new
            {
                Message = "店鋪資訊修改完成",
                Token = token,
            };
            return Ok(result);
        }




        [HttpPost]
        [Route("UploadProfile")]
        public async Task<IHttpActionResult> UploadProfile()
        {
            // 檢查請求是否包含 multipart/form-data.
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            string root = HttpContext.Current.Server.MapPath(@"~/upload/HeadShot");
            try
            {
                // 讀取 MIME 資料
                var provider = new MultipartMemoryStreamProvider();
                await Request.Content.ReadAsMultipartAsync(provider);

                // 取得檔案副檔名，單檔用.FirstOrDefault()直接取出，多檔需用迴圈
                string fileNameData = provider.Contents.FirstOrDefault().Headers.ContentDisposition.FileName.Trim('\"');
                string imageType = provider.Contents.FirstOrDefault().Headers.ContentDisposition.Name.Trim('\"');
                string fileType = fileNameData.Remove(0, fileNameData.LastIndexOf('.')); // .jpg

                // 定義檔案名稱
                string fileName = "UserName" + "Profile" + DateTime.Now.ToString("yyyyMMddHHmmss") + fileType;


                // 儲存圖片，單檔用.FirstOrDefault()直接取出，多檔需用迴圈
                var fileBytes = await provider.Contents.FirstOrDefault().ReadAsByteArrayAsync();
                if (imageType.Contains("Banner"))
                {
                    root = HttpContext.Current.Server.MapPath(@"~/upload/Banner");
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
                if (size.Width > 600 && size.Height > 600)
                {
                    image.Mutate(x => x.Resize(150, 120)); // 輸入(120, 0)會保持比例出現黑邊

                }

                var userToken = JwtAuthFilter.GetToken(Request.Headers.Authorization.Parameter);
                int identityId = (int)userToken["IdentityId"];

                if (imageType == "HeadShot")
                {
                    var storeHeadShot = db.StoreDetail.Where(s => s.Id == identityId).ToList();
                    storeHeadShot[0].HeadShot = fileName;
                    image.Save(outputPath);
                    db.SaveChanges();
                    return Ok(new
                    {
                        Message = "照片上傳成功",
                    });
                }
                else if (imageType.Contains("Banner"))
                {
                    var storeBannerDb = db.StoreDetail.Where(s => s.Id == identityId).ToList();
                    //string storeBanner = storeBannerDb[0];
                    JObject bannerJObject = new JObject();
                    if (storeBannerDb[0].BannerPath != null)
                    {
                        bannerJObject = JObject.Parse(storeBannerDb[0].BannerPath); //json
                    }

                    bannerJObject[imageType] = fileName;
                    storeBannerDb[0].BannerPath = bannerJObject.ToString();
                    image.Save(outputPath);
                    db.SaveChanges();
                    return Ok(new
                    {
                        Message = "照片上傳成功",
                    });
                }
                else
                {
                    return BadRequest("ImageType錯誤"); // 400
                }


            }
            catch (Exception e)
            {
                return BadRequest("照片上傳失敗或未上傳"); // 400
            }
        }

        [HttpGet]
        [JwtAuthFilter]
        [Route("GetAllItems")]
        public IHttpActionResult GetAllItems()
        {
            var userToken = JwtAuthFilter.GetToken(Request.Headers.Authorization.Parameter);
            int identityId = (int)userToken["IdentityId"];
            var allItems = db.BusinessItems.Where(i => i.StoreId == identityId).Select(i => new
            {
                i.Id,
                i.ItemName,
                i.SpendTime,
                i.Price,
                i.Describe,
                PicturePath = "https://" + Request.RequestUri.Host + "/upload/ItemsImage/" + i.PicturePath,

            }).ToList();

            return Ok(allItems);
        }


        [HttpPost]
        [JwtAuthFilter]
        [Route("AddItems")]
        public IHttpActionResult AddItems(AllItems view)
        {
            var modelErrorMessage = ModelState.Values.Select(e => e.Errors).ToList();
            var userToken = JwtAuthFilter.GetToken(Request.Headers.Authorization.Parameter);
            int identityId = (int)userToken["IdentityId"];
            BusinessItems addItems = new BusinessItems();
            if (string.IsNullOrEmpty(view.ItemName))
            {
                return BadRequest(modelErrorMessage[0][0].ErrorMessage);
            }

            if (string.IsNullOrEmpty(view.SpendTime))
            {
                return BadRequest(modelErrorMessage[0][0].ErrorMessage);
            }

            if (string.IsNullOrEmpty(view.Price))
            {
                return BadRequest(modelErrorMessage[0][0].ErrorMessage);
            }

            addItems.StoreId = identityId;
            addItems.ItemName = view.ItemName;
            addItems.SpendTime = view.SpendTime;
            addItems.Price = view.Price;
            addItems.Describe = view.Describe;
            addItems.Remark = view.Remark;

            db.BusinessItems.Add(addItems);
            db.SaveChanges();


            return Ok(new
            {
                Message = "項目新增成功",
            });
        }




        [HttpPost]
        [Route("UploadItemsImage")]
        public async Task<IHttpActionResult> UploadItemsImage()
        {
            // 檢查請求是否包含 multipart/form-data.
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }


            string root = HttpContext.Current.Server.MapPath(@"~/upload/ItemsImage");

            try
            {
                // 讀取 MIME 資料
                var provider = new MultipartMemoryStreamProvider();
                await Request.Content.ReadAsMultipartAsync(provider);

                // 取得檔案副檔名，單檔用.FirstOrDefault()直接取出，多檔需用迴圈
                string fileNameData = provider.Contents.FirstOrDefault().Headers.ContentDisposition.FileName.Trim('\"');
                string fileType = fileNameData.Remove(0, fileNameData.LastIndexOf('.')); // .jpg

                // 定義檔案名稱
                string fileName = "UserName" + "Profile" + DateTime.Now.ToString("yyyyMMddHHmmss") + fileType;

                // 儲存圖片，單檔用.FirstOrDefault()直接取出，多檔需用迴圈
                var fileBytes = await provider.Contents.FirstOrDefault().ReadAsByteArrayAsync();
                var outputPath = Path.Combine(root, fileName);
                using (var output = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                {
                    await output.WriteAsync(fileBytes, 0, fileBytes.Length);
                }

                // 使用 SixLabors.ImageSharp 調整圖片尺寸 (正方形大頭貼)
                var image = SixLabors.ImageSharp.Image.Load<Rgba32>(outputPath);
                //要設定超過一個大小就限制大小
                var size = image.Size();
                if (size.Width > 600 && size.Height > 600)
                {
                    image.Mutate(x => x.Resize(150, 120)); // 輸入(120, 0)會保持比例出現黑邊

                }

                image.Save(outputPath);
                var userToken = JwtAuthFilter.GetToken(Request.Headers.Authorization.Parameter);
                int identityId = (int)userToken["IdentityId"];
                var itemsImagePath = db.BusinessItems.Where(b => b.StoreId == identityId).ToList();
                itemsImagePath[0].PicturePath = fileName;
                image.Save(outputPath);
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
    }
}
