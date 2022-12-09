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
using System.Web.UI.WebControls;
using System.Web.WebSockets;
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


        /// <summary>
        /// 店家資訊顯示
        /// </summary>
        [HttpGet]
        [JwtAuthFilter]
        [Route("GetInformation")]
        public IHttpActionResult GetInformation()
        {
            var userToken = JwtAuthFilter.GetToken(Request.Headers.Authorization.Parameter);
            int storeId = (int)userToken["IdentityId"];
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

            var businessInformation =
                db.StoreDetail.Where(s => s.Id == storeId).Select(s => s.BusinessInformationId).ToList();

            var storeInformation = db.StoreDetail.Where(s => s.Id == storeId).Select((s) => new
            {
                s.StoreName,
                IndustryId = s.Industry == null ? 0 : s.Industry.Id,
                IndustryName = s.Industry == null ? null : s.Industry.IndustryName,
                s.City,
                s.District,
                s.Address,
                JobTitle = s.StaffTitle,
                s.Description,
                HeadShot = s.HeadShot == null ? null : "https://" + Request.RequestUri.Host + "/upload/HeadShot/" + s.HeadShot,
                s.CellphoneNumber,
                s.PhoneNumber,
                TimeInterval = s.BusinessInformation == null ? "" : s.BusinessInformation.TimeInterval,
                WeekdayStartTime = s.BusinessInformation == null ? "" : s.BusinessInformation.WeekdayStartTime,
                WeekdayEndTime = s.BusinessInformation == null ? "" : s.BusinessInformation.WeekdayEndTime,
                WeekdayBreakStart = s.BusinessInformation == null ? "" : s.BusinessInformation.WeekdayBreakStart,
                WeekdayBreakEnd = s.BusinessInformation == null ? "" : s.BusinessInformation.WeekdayBreakEnd,
                HolidayStartTime = s.BusinessInformation == null ? "" : s.BusinessInformation.HolidayStartTime,
                HolidayEndTime = s.BusinessInformation == null ? "" : s.BusinessInformation.HolidayEndTime,
                HolidayBreakStart = s.BusinessInformation == null ? "" : s.BusinessInformation.HolidayBreakStart,
                HolidayBreakEnd = s.BusinessInformation == null ? "" : s.BusinessInformation.HolidayBreakEnd,
                PublicHoliday = s.BusinessInformation == null ? "" : s.BusinessInformation.PublicHoliday,
                s.FacebookLink,
                s.InstagramLink,
                s.LineLink
            }).ToList();
            return Ok(new { Identity = "store", StoreInformation = storeInformation, BannerPath = bannerJObject });
        }



        /// <summary>
        /// 店家資訊存取
        /// </summary>
        [HttpPost]
        [JwtAuthFilter]
        [Route("EditInformation")]
        public IHttpActionResult EditInformation(InformationDataView view)
        {
            var userToken = JwtAuthFilter.GetToken(Request.Headers.Authorization.Parameter);
            int identityId = (int)userToken["IdentityId"];
            var storeDetailInDb = db.StoreDetail.Where(s => s.Id == identityId).ToList();

            if (view.IndustryId == null || view.IndustryId == 0)
            {
                return BadRequest("IndustryId欄位必填");
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


            foreach (StoreDetail item in storeDetailInDb)
            {
                if (item.BusinessInformation == null)
                {
                    BusinessInformation businessInformation = new BusinessInformation();
                    businessInformation.TimeInterval = view.TimeInterval;
                    businessInformation.WeekdayStartTime = view.WeekdayStartTime;
                    businessInformation.WeekdayEndTime = view.WeekdayEndTime;
                    businessInformation.WeekdayBreakStart = view.WeekdayBreakStart;
                    businessInformation.WeekdayBreakEnd = view.WeekdayBreakEnd;
                    businessInformation.HolidayStartTime = view.HolidayStartTime;
                    businessInformation.HolidayEndTime = view.HolidayEndTime;
                    businessInformation.HolidayBreakStart = view.HolidayBreakStart;
                    businessInformation.HolidayBreakEnd = view.HolidayBreakEnd;
                    businessInformation.PublicHoliday = view.PublicHoliday;
                    item.BusinessInformation = businessInformation;

                }
                else
                {
                    item.BusinessInformation.TimeInterval = view.TimeInterval;
                    item.BusinessInformation.WeekdayStartTime = view.WeekdayStartTime;
                    item.BusinessInformation.WeekdayEndTime = view.WeekdayEndTime;
                    item.BusinessInformation.WeekdayBreakStart = view.WeekdayBreakStart;
                    item.BusinessInformation.WeekdayBreakEnd = view.WeekdayBreakEnd;
                    item.BusinessInformation.HolidayStartTime = view.HolidayStartTime;
                    item.BusinessInformation.HolidayEndTime = view.HolidayEndTime;
                    item.BusinessInformation.HolidayBreakStart = view.HolidayBreakStart;
                    item.BusinessInformation.HolidayBreakEnd = view.HolidayBreakEnd;
                    item.BusinessInformation.PublicHoliday = view.PublicHoliday;
                }

                item.StoreName = view.StoreName;
                item.IndustryId = view.IndustryId;
                item.City = view.City;
                item.District = view.District;
                item.Address = view.Address;
                item.StaffTitle = view.JobTitle;
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

            return Ok(new { Message = "店鋪資訊修改完成" });
        }



        /// <summary>
        /// 店家資訊banner及大頭貼圖片修改
        /// </summary>
        [HttpPost]
        [JwtAuthFilter]
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
                string imageType = provider.Contents.FirstOrDefault().Headers.ContentDisposition.Name.Trim('\"');
                string fileType = fileNameData.Remove(0, fileNameData.LastIndexOf('.')); // .jpg

                // 定義檔案名稱
                string fileName = "UserName" + "Profile" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + fileType;


                // 儲存圖片，單檔用.FirstOrDefault()直接取出，多檔需用迴圈
                var fileBytes = new byte[] { };
                fileBytes = await provider.Contents.FirstOrDefault().ReadAsByteArrayAsync();
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
                var size = image.Size();


                var userToken = JwtAuthFilter.GetToken(Request.Headers.Authorization.Parameter);
                int identityId = (int)userToken["IdentityId"];

                if (imageType == "HeadShot")
                {                //要設定超過一個大小就限制大小
                    if (size.Width > 1280 && size.Height > 1280)
                    {
                        image.Mutate(x => x.Resize(0, 640)); // 輸入(120, 0)會保持比例出現黑邊

                    }
                    var storeHeadShot = db.StoreDetail.Where(s => s.Id == identityId).ToList();
                    storeHeadShot[0].HeadShot = fileName;
                    image.Save(outputPath);
                    db.SaveChanges();
                    return Ok(new
                    {
                        Message = "照片上傳成功",
                    });
                }
                else if (imageType == "Banner1" || imageType == "Banner2" || imageType == "Banner3" || imageType == "Banner4" || imageType == "Banner5")
                {
                    //要設定超過一個大小就限制大小
                    if (size.Width > 1280 && size.Height > 1280)
                    {
                        image.Mutate(x => x.Resize(0, 900)); // 輸入(120, 0)會保持比例出現黑邊

                    }
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


        /// <summary>
        /// 店家所有項目顯示
        /// </summary>
        [HttpGet]
        [JwtAuthFilter]
        [Route("GetAllItems")]
        public IHttpActionResult GetAllItems()
        {
            var userToken = JwtAuthFilter.GetToken(Request.Headers.Authorization.Parameter);
            int identityId = (int)userToken["IdentityId"];
            var allItems = db.BusinessItems.Where(i => i.StoreId == identityId).Select(i => new
            {
                ItemId = i.Id,
                i.ItemName,
                i.SpendTime,
                i.Price,
                i.Describe,
                PicturePath = i.PicturePath == null ? i.PicturePath : "https://" + Request.RequestUri.Host + "/upload/ItemsImage/" + i.PicturePath,

            }).ToList();

            return Ok(allItems);
        }



        /// <summary>
        /// 店家新增營業項目
        /// </summary>
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

            var theItemId = db.BusinessItems.Add(addItems);
            db.SaveChanges();


            return Ok(new
            {
                Message = "項目新增成功",
                TheItemId = theItemId.Id
            });
        }



        /// <summary>
        /// 店家新增項目圖片
        /// </summary>
        [HttpPost]
        [Route("UploadItemsImage")]
        public async Task<IHttpActionResult> UploadItemsImage([FromUri] int theItemId)
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
                var itemsImagePath = db.BusinessItems.Where(b => b.Id == theItemId).ToList();
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



        /// <summary>
        /// 店家所有員工資訊
        /// </summary>
        [HttpGet]
        [JwtAuthFilter]
        [Route("AllStaff")]
        public IHttpActionResult AllStaff()
        {
            var userToken = JwtAuthFilter.GetToken(Request.Headers.Authorization.Parameter);
            int identityId = (int)userToken["IdentityId"];

            var staffWorkItems = db.StaffWorkItems.AsQueryable();   //對資料庫下指令

            var AllStaffItem = db.StaffDetail.Where(e => e.StoreId == identityId).Select(e => new
            {
                e.Id,
                e.Account,
                StaffWorkItems = staffWorkItems.Where(s => s.StaffId == e.Id).Select(b => new
                {
                    b.BusinessItemsId,
                    b.BusinessItems.ItemName
                }),
                e.StaffName,
                e.JobTitle,
                HeadShot = e.HeadShot == null ? e.HeadShot : "https://" + Request.RequestUri.Host + "/upload/headshot/" + e.HeadShot,
            }).ToList();


            return Ok(new { AllStaffItem });
        }



        /// <summary>
        /// 店家編輯員工
        /// </summary>
        [HttpPost]
        [JwtAuthFilter]
        [Route("EditStaff")]
        public IHttpActionResult EditStaff(InformationDataView view)
        {
            var userToken = JwtAuthFilter.GetToken(Request.Headers.Authorization.Parameter);
            int identityId = (int)userToken["IdentityId"];
            var staffInformaiton = db.StaffDetail.Where(e => e.StoreId == identityId).Where(e => e.Id == view.StaffId).ToList();

            staffInformaiton[0].JobTitle = view.JobTitle;


            StaffWorkItems staffWorkItems = new StaffWorkItems();
            //刪除這個員工原本儲存的工作項目資料
            var deleteStaffId = db.StaffWorkItems.Where(w => w.StaffId == view.StaffId).ToList();

            if (deleteStaffId != null)
            {
                foreach (StaffWorkItems deleteItems in deleteStaffId)
                {
                    db.StaffWorkItems.Remove(deleteItems);
                    db.SaveChanges();
                }
            }

            string[] businessItemsIdst = view.BusinessItemsId.Split(',');
            //新增這個員工更新的工作項目資料
            foreach (string item in businessItemsIdst)
            {

                int businessItemsId = Convert.ToInt32(item);
                staffWorkItems.BusinessItemsId = businessItemsId;
                staffWorkItems.StaffId = view.StaffId;
                staffWorkItems.StaffName = staffInformaiton[0].StaffName;
                db.StaffWorkItems.Add(staffWorkItems);
                db.SaveChanges();
            }

            return Ok(new { Message = "員工資料編輯完成" });
        }


        /// <summary>
        /// 店家刪除員工
        /// </summary>
        [HttpDelete]
        [JwtAuthFilter]
        [Route("DeleteStaff")]
        public IHttpActionResult DeleteStaff([FromUri] int staffId)
        {
            var userToken = JwtAuthFilter.GetToken(Request.Headers.Authorization.Parameter);
            int identityId = (int)userToken["IdentityId"];
            var staffInformaiton = db.StaffDetail.Where(e => e.StoreId == identityId).Where(e => e.Id == staffId).ToList();
            if (staffInformaiton.Count > 0)
            {
                db.StaffDetail.Remove(staffInformaiton[0]);
                db.SaveChanges();
                return Ok(new { Message = "刪除成功" });
            }
            else
            {
                return BadRequest("無此項目id");
            }
        }



        /// <summary>
        /// 店家項目編輯
        /// </summary>
        [HttpPost]
        [JwtAuthFilter]
        [Route("EditItems")]
        public IHttpActionResult EditItems([FromUri] int itemId, [FromBody] AllItems view)
        {
            var userToken = JwtAuthFilter.GetToken(Request.Headers.Authorization.Parameter);
            int identityId = (int)userToken["IdentityId"];
            var theItem = db.BusinessItems.Where(i => i.StoreId == identityId).Where(i => i.Id == itemId).ToList();

            foreach (BusinessItems item in theItem)
            {
                item.ItemName = view.ItemName;
                item.SpendTime = view.SpendTime;
                item.Price = view.Price;
                item.Describe = view.Describe;
                item.Remark = view.Remark;
            }

            db.SaveChanges();
            return Ok(new { Message = "修改成功" });
        }


        /// <summary>
        /// 店家項目刪除
        /// </summary>
        [HttpDelete]
        [JwtAuthFilter]
        [Route("DeleteItems")]
        public IHttpActionResult DeleteItems([FromUri] int itemId)
        {
            var userToken = JwtAuthFilter.GetToken(Request.Headers.Authorization.Parameter);
            int identityId = (int)userToken["IdentityId"];
            var theItem = db.BusinessItems.Where(i => i.StoreId == identityId).Where(i => i.Id == itemId).ToList();
            if (theItem.Count > 0)
            {
                db.BusinessItems.Remove(theItem[0]);
                db.SaveChanges();
                return Ok(new { Message = "刪除成功" });
            }
            else
            {
                return BadRequest("無此項目id");
            }
        }


        /// <summary>
        /// 店家公休日顯示
        /// </summary>
        [HttpGet]
        [JwtAuthFilter]
        [Route("GetHolidayDate")]
        public IHttpActionResult GetHolidayDate()
        {
            var userToken = JwtAuthFilter.GetToken(Request.Headers.Authorization.Parameter);
            int identityId = (int)userToken["IdentityId"];
            var theHoliday = db.StoreDetail.Where(s => s.Id == identityId).Select(s => s.HolidayDate).ToList();
            //把所有公休日轉成string陣列
            string[] stringHolidayArr = theHoliday[0].Split(',');
            StringBuilder showHolidayDate = new StringBuilder();
            StringBuilder pastHolidayDate = new StringBuilder();
            DateTime[] dateHolidayArr = new DateTime[stringHolidayArr.Length];
           
            for (int i = 0; i < stringHolidayArr.Length; i++)
            {
                //把string陣列的公休日轉成DateTime陣列
                dateHolidayArr[i] = DateTime.Parse(stringHolidayArr[i]);
                //字串存入這個月跟下個月的公休日
                if (dateHolidayArr[i].Month == DateTime.Now.Month || dateHolidayArr[i].Month == DateTime.Now.AddMonths(1).Month)
                {
                    showHolidayDate.Append(dateHolidayArr[i].ToShortDateString());
                    showHolidayDate.Append(',');
                }
                //字串存入這個月之前及下個月之後的日期
                else if (dateHolidayArr[i].Month != DateTime.Now.Month || dateHolidayArr[i].Month > DateTime.Now.AddMonths(1).Month)
                {
                    pastHolidayDate.Append(dateHolidayArr[i].ToShortDateString());
                    pastHolidayDate.Append(',');
                }
            }
            return Ok(new { PastHolidayDate = pastHolidayDate.ToString().TrimEnd(','), ShowHolidayDate = showHolidayDate.ToString().TrimEnd(',') });
        }


        /// <summary>
        /// 店家修改公休日
        /// </summary>
        [HttpPost]
        [JwtAuthFilter]
        [Route("EditHolidayDate")]
        public IHttpActionResult EditHolidayDate(InformationDataView view)
        {
            var userToken = JwtAuthFilter.GetToken(Request.Headers.Authorization.Parameter);
            int identityId = (int)userToken["IdentityId"];
            var theHoliday = db.StoreDetail.Where(s => s.Id == identityId).ToList();
            StoreDetail storeHoliday = new StoreDetail();

            if (theHoliday.Count != 0)
            {
                theHoliday[0].HolidayDate = view.HolidayDate;
            }
            else
            {
                storeHoliday.HolidayDate = view.HolidayDate;
                db.StoreDetail.Add(storeHoliday);
            }
            db.SaveChanges();
            return Ok(new { Message = "公休日修改成功" });
        }



        /// <summary>
        /// 店家行事曆顯示
        /// </summary>
        [HttpGet]
        [JwtAuthFilter]
        [Route("Calendar")]
        public IHttpActionResult Calendar()
        {
            var userToken = JwtAuthFilter.GetToken(Request.Headers.Authorization.Parameter);
            int identityId = (int)userToken["IdentityId"];

            if (DateTime.Now.Month == 1)
            {
                var calendar = db.CustomerReserve.Where(r => r.StoreId == identityId)
                    .Where(r =>
                        (r.ReserveDate.Year == (DateTime.Now.Year - 1) & r.ReserveDate.Month == 12) ||
                        (r.ReserveDate.Year == DateTime.Now.Year & r.ReserveDate.Month == DateTime.Now.Month) ||
                        (r.ReserveDate.Year == DateTime.Now.Year & r.ReserveDate.Month == (DateTime.Now.Month + 1)))
                    .OrderBy(r => r.ReserveDate)
                    .Select(r => new
                    {
                        ReserveId = r.Id,
                        ReserveDate = r.ReserveDate.Year + "/" + r.ReserveDate.Month + "/" + r.ReserveDate.Day,
                        ReserveStart = r.ReserveStart.Hour + ":" + (r.ReserveEnd.Minute < 10 ? "0" + r.ReserveEnd.Minute : r.ReserveEnd.Minute.ToString()),
                        ReserveEnd = r.ReserveEnd.Hour + ":" + (r.ReserveEnd.Minute < 10 ? "0" + r.ReserveEnd.Minute : r.ReserveEnd.Minute.ToString()),
                        r.StaffName,
                        r.CustomerDetail.CustomerName,
                        r.BusinessItems.ItemName
                    }).ToList();

                return Ok(calendar);
            }
            if (DateTime.Now.Month == 12)
            {
                var calendar = db.CustomerReserve.Where(r => r.StoreId == identityId)
                    .Where(r =>
                    (r.ReserveDate.Year == (DateTime.Now.Year + 1) & r.ReserveDate.Month == 1) ||
                    (r.ReserveDate.Year == DateTime.Now.Year & r.ReserveDate.Month == DateTime.Now.Month) ||
                    (r.ReserveDate.Year == DateTime.Now.Year & r.ReserveDate.Month == 11))
                    .OrderBy(r => r.ReserveDate)
                    .Select(r => new
                    {
                        ReserveId = r.Id,
                        ReserveDate = r.ReserveDate.Year + "/" + r.ReserveDate.Month + "/" + r.ReserveDate.Day,
                        ReserveStart = r.ReserveStart.Hour + ":" + (r.ReserveEnd.Minute < 10 ? "0" + r.ReserveEnd.Minute : r.ReserveEnd.Minute.ToString()),
                        ReserveEnd = r.ReserveEnd.Hour + ":" + (r.ReserveEnd.Minute < 10 ? "0" + r.ReserveEnd.Minute : r.ReserveEnd.Minute.ToString()),
                        r.StaffName,
                        r.CustomerDetail.CustomerName,
                        r.BusinessItems.ItemName,
                    }).ToList();
                return Ok(calendar);
            }
            else
            {
                var calendar = db.CustomerReserve.Where(r => r.StoreId == identityId)
                    .Where(r =>
                    (r.ReserveDate.Year == DateTime.Now.Year & r.ReserveDate.Month == (DateTime.Now.Month - 1)) ||
                    (r.ReserveDate.Year == DateTime.Now.Year & r.ReserveDate.Month == DateTime.Now.Month) ||
                    (r.ReserveDate.Year == DateTime.Now.Year & r.ReserveDate.Month == (DateTime.Now.Month + 1)))
                    .OrderBy(r => r.ReserveDate)
                    .Select(r => new
                    {
                        ReserveId = r.Id,
                        ReserveDate = r.ReserveDate.Year + "/" + r.ReserveDate.Month + "/" + r.ReserveDate.Day,
                        ReserveStart = r.ReserveStart.Hour + ":" + (r.ReserveEnd.Minute < 10 ? "0" + r.ReserveEnd.Minute : r.ReserveEnd.Minute.ToString()),
                        ReserveEnd = r.ReserveEnd.Hour + ":" + (r.ReserveEnd.Minute < 10 ? "0" + r.ReserveEnd.Minute : r.ReserveEnd.Minute.ToString()),
                        r.StaffName,
                        r.CustomerDetail.CustomerName,
                        r.BusinessItems.ItemName
                    }).ToList();

                return Ok(calendar);
            }
        }




        /// <summary>
        /// 店家預約項目顯示
        /// </summary>
        [HttpGet]
        [JwtAuthFilter]
        [Route("GetReserve")]
        public IHttpActionResult GetReserve([FromUri] int reserveId)
        {
            var userToken = JwtAuthFilter.GetToken(Request.Headers.Authorization.Parameter);
            int identityId = (int)userToken["IdentityId"];
            var theReserver = db.CustomerReserve.Where(r => r.Id == reserveId).Select(r => new
            {
                ReserveId = r.Id,
                r.StaffName,
                CustomerName = r.CustomerDetail == null ? r.ManualName : r.CustomerDetail.CustomerName,
                r.BusinessItems.ItemName,
                r.BusinessItems.Price,
                CellphoneNumber = r.CustomerDetail == null ? r.ManualCellphoneNumber : r.CustomerDetail.CellphoneNumber,
                Email = r.CustomerDetail == null ? r.ManualEmail : r.CustomerDetail.Account,
                ReserveDate = r.ReserveDate.Year + "/" + r.ReserveDate.Month + "/" + r.ReserveDate.Day,
                ReserveStart = r.ReserveStart.Hour + ":" + (r.ReserveEnd.Minute < 10 ? "0" + r.ReserveEnd.Minute : r.ReserveEnd.Minute.ToString()),

            }).ToList();

            if (theReserver.Count > 0)
            {
                return Ok(theReserver);
            }
            else
            {
                return BadRequest("無此預約id");
            }

        }



        /// <summary>
        /// 店家刪除預約項目
        /// </summary>
        [HttpDelete]
        [JwtAuthFilter]
        [Route("DeleteReserve")]
        public IHttpActionResult DeleteReserve([FromUri] int reserveId)
        {
            var userToken = JwtAuthFilter.GetToken(Request.Headers.Authorization.Parameter);
            int identityId = (int)userToken["IdentityId"];
            var theReserver = db.CustomerReserve.Where(r => r.Id == reserveId).ToList();

            if (theReserver.Count > 0)
            {
                db.CustomerReserve.Remove(theReserver[0]);
                db.SaveChanges();
                return Ok(new { Message = "刪除成功" });
            }
            else
            {
                return BadRequest("無此預約id");
            }

        }



        /// <summary>
        /// 所有店家顯示
        /// </summary>
        [HttpGet]
        [Route("GetAllStore")]
        public IHttpActionResult GetAllStore()
        {
            var businessItem = db.BusinessItems.AsQueryable();   //對資料庫下指令
            var staffInformation = db.StaffDetail.AsQueryable();
            var staffWorkItem = db.StaffWorkItems.AsQueryable();

            var allStore = db.StoreDetail.Select(s => new
            {
                StoredId = s.Id,
                s.BannerPath,
                s.BusinessInformation.HolidayStartTime,
                s.BusinessInformation.HolidayEndTime,
                s.BusinessInformation.WeekdayStartTime,
                s.BusinessInformation.WeekdayEndTime,
                Address = s.City + s.District + s.Address,
                s.LineLink,
                s.FacebookLink,
                s.InstagramLink,
                BusinessItem = businessItem.Where(b => b.StoreId == s.Id).Select(b => new
                {
                    b.Id,
                    b.ItemName,
                    b.SpendTime,
                    b.Price,
                    b.Describe,
                    PicturePath = b.PicturePath == null ? null : "https://" + Request.RequestUri.Host + "/upload/headshot/" + b.PicturePath,
                }),
                AllStaffInformation = staffInformation.Where(e => e.StoreId == s.Id).Select(e => new
                {
                    e.Id,
                    e.StaffName,
                    e.JobTitle,
                    e.FacebookLink,
                    e.InstagramLink,
                    e.LineLink,
                    StaffWorkItems = staffWorkItem.Where(w => w.StaffId == e.Id).Select(w => new
                    {
                        w.BusinessItemsId,
                        w.BusinessItems.ItemName
                    }),
                    HeadShot = e.HeadShot == null ? null : "https://" + Request.RequestUri.Host + "/upload/headshot/" + e.HeadShot,
                })
            }).AsEnumerable().Select(a => new
            {
                a.StoredId,
                BannerPath = BannerObject(a.BannerPath),
                a.HolidayStartTime,
                a.HolidayEndTime,
                a.WeekdayStartTime,
                a.WeekdayEndTime,
                a.Address,
                a.LineLink,
                a.FacebookLink,
                a.InstagramLink,
                a.BusinessItem,
                a.AllStaffInformation,

            }).ToList();
            JObject bannerJObject = new JObject(); //宣告空物件
            return Ok(allStore);
        }
        object BannerObject(string bannerPath)
        {
            if (bannerPath == null)
            {
                return null;
            }
            else
            {
                JObject bannerObject = JObject.Parse(bannerPath); //把bannerPath(string) 轉成物件
                foreach (var item in bannerObject)
                {
                    bannerObject[item.Key] = "https://" + Request.RequestUri.Host + "/upload/Banner/" + item.Value;
                }
                return bannerObject;
            }
        }
    }
}
