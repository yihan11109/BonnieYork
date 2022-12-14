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
            var staffTitle = db.StoreDetail;
            var customerCellPhoneNum = db.CustomerDetail.Where(c => c.Id == identityId).Select(c => c.CellphoneNumber);
            var itemsInformation = db.BusinessItems.Where(i => i.Id == itemId).Where(i => i.StoreId == storeId).Select(
                i => new
                {
                    PicturePath = i.PicturePath == null ? i.PicturePath : "https://" + Request.RequestUri.Host + "/upload/ItemsImage/" + i.PicturePath,
                    i.ItemName,
                    i.SpendTime,
                    i.Price,
                    i.Remark,
                    StaffTitle = staffTitle.Where(s => s.Id == storeId).Select(s => s.StaffTitle),
                    CustomerName = customerName,
                    CustomerCellPhoneNum = customerCellPhoneNum,
                    TheStaffName = projectEmployees.Where(s => s.BusinessItemsId == itemId).Select(s => new
                    {
                        s.StaffId,
                        s.StaffName,
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
            List<string> theEnableDate = new List<string>();
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

                if (!storeHolidayDateArr.Contains(now.ToString("yyyy/MM/dd")) && weekDay != storeHolidayDate[0].PublicHoliday)
                {
                    theStoreWorkDate.Add(now.ToString("yyyy/MM/dd"));
                }
                else
                {
                    theEnableDate.Add(now.ToString("yyyy/MM/dd"));
                }

                now = now.AddDays(+1);
            } while (now != afterMonth);

            //從店家有營業的日期裡刪掉這個員工的休假日 = 這個員工可預約的日期
            var theStaffDaysOff = db.StaffDetail.Where(e => e.Id == staffId).Select(e => e.StaffDaysOff).ToList();
            List<string> theStaffWorkDate = new List<string>();
            //迴圈找店家工作日中有沒有員工休假日，如果沒有把店家工作日存入字串 = 員工工作日
            for (int i = 0; i < theStoreWorkDate.Count; i++)
            {
                if (!string.IsNullOrEmpty(theStaffDaysOff[0]))
                {
                    //判斷店家工作日中有沒有員工休假日
                    if (!theStaffDaysOff[0].Contains(theStoreWorkDate[i]))
                    {
                        theStaffWorkDate.Add(theStoreWorkDate[i]);
                    }
                    else
                    {
                        theEnableDate.Add(theStoreWorkDate[i]);
                    }
                }
                else
                {
                    theStaffWorkDate.Add(theStoreWorkDate[i]);
                }
            }

            DateTime nextMonth = DateTime.Now.AddMonths(1);

            //在顧客預約表撈現在日期往後推一個月範圍的預約
            var theStaffReserveDetail = db.CustomerReserve.Where(c => c.StaffId == staffId)
                .Where(c => c.ReserveState == "Undone")
                .Where(c => c.ReserveDate >= DateTime.Now && c.ReserveDate <= nextMonth).Select(c => new
                {
                    c.ReserveDate,
                    c.ReserveStart,
                    c.ReserveEnd
                }).ToList();
            Dictionary<DateTime, List<JObject>> theReserveDateDictionary = new Dictionary<DateTime, List<JObject>>();
            string theReserveDate = "";
            foreach (var date in theStaffReserveDetail)
            {
                JObject dateJObject = new JObject();
                dateJObject["ReserveStart"] = date.ReserveStart;
                dateJObject["ReserveEnd"] = date.ReserveEnd;
                List<JObject> reserveDateList = new List<JObject>();
                if (theReserveDateDictionary.ContainsKey(date.ReserveDate))
                {
                    reserveDateList = theReserveDateDictionary[date.ReserveDate];
                    reserveDateList.Add(dateJObject);
                    theReserveDateDictionary[date.ReserveDate] = reserveDateList;
                }
                else
                {
                    reserveDateList.Add(dateJObject);
                    theReserveDateDictionary.Add(date.ReserveDate, reserveDateList);
                }

                theReserveDate += date.ReserveDate;
                theReserveDate += ',';
            }

            theReserveDate = theReserveDate.TrimEnd(',');

            //撈出店家營業時間
            var theStoreBusinessInformation = db.StoreDetail.Where(s => s.Id == storeId).Select(s => new
            {
                s.BusinessInformation.HolidayStartTime,
                s.BusinessInformation.HolidayEndTime,
                s.BusinessInformation.HolidayBreakStart,
                s.BusinessInformation.HolidayBreakEnd,
                s.BusinessInformation.WeekdayStartTime,
                s.BusinessInformation.WeekdayEndTime,
                s.BusinessInformation.WeekdayBreakStart,
                s.BusinessInformation.WeekdayBreakEnd,
                s.BusinessInformation.TimeInterval
            }).ToList();
            int holidayStartTimeToInt =
                Int32.Parse(theStoreBusinessInformation[0].HolidayStartTime.Replace(":", ""));
            int holidayEndTimeToInt = Int32.Parse(theStoreBusinessInformation[0].HolidayEndTime.Replace(":", ""));
            int holidayBreakStartToInt =
                string.IsNullOrEmpty(theStoreBusinessInformation[0].HolidayBreakStart) == true
                    ? 0
                    : Int32.Parse(theStoreBusinessInformation[0].HolidayBreakStart.Replace(":", ""));
            int holidayBreakEndToInt = string.IsNullOrEmpty(theStoreBusinessInformation[0].HolidayBreakEnd) == true
                ? 0
                : Int32.Parse(theStoreBusinessInformation[0].HolidayBreakEnd.Replace(":", ""));
            int weekdayStartTimeToInt =
                Int32.Parse(theStoreBusinessInformation[0].WeekdayStartTime.Replace(":", ""));
            int weekdayEndTimeToInt = Int32.Parse(theStoreBusinessInformation[0].WeekdayEndTime.Replace(":", ""));
            int weekdayBreakStartToInt =
                string.IsNullOrEmpty(theStoreBusinessInformation[0].WeekdayBreakStart) == true
                    ? 0
                    : Int32.Parse(theStoreBusinessInformation[0].WeekdayBreakStart.Replace(":", ""));
            int weekdayBreakEndToInt =
                string.IsNullOrEmpty(theStoreBusinessInformation[0].WeekdayBreakStart) == true
                    ? 0
                    : Int32.Parse(theStoreBusinessInformation[0].WeekdayBreakEnd.Replace(":", ""));
            int timeIntervalToInt = Int32.Parse(theStoreBusinessInformation[0].TimeInterval.Replace("分鐘", "")
                .Replace("1小時", "60"));

            //撈出項目花費時間
            var theItemSpendTime = db.BusinessItems.Where(i => i.Id == itemId).Select(i => i.SpendTime).ToList();
            int hourIndex = theItemSpendTime[0].LastIndexOf("小時");
            int minIndex = theItemSpendTime[0].LastIndexOf("分鐘");
            int theItemSpendTimeMin;
            int theItemSpendTimeHour;
            if (theItemSpendTime[0].Length == 3)
            {
                theItemSpendTimeHour = Int32.Parse(theItemSpendTime[0].Remove(hourIndex));
                theItemSpendTimeMin = 0;
            }
            else if (theItemSpendTime[0].Length == 4)
            {
                theItemSpendTimeHour = 0;
                theItemSpendTimeMin = Int32.Parse(theItemSpendTime[0].Substring(0, minIndex));
            }
            else
            {
                theItemSpendTimeHour = Int32.Parse(theItemSpendTime[0].Remove(hourIndex));
                theItemSpendTimeMin = Int32.Parse(theItemSpendTime[0].Substring(3, 2));
            }


            JObject theStaffWorkDateJObject = new JObject();
            for (int i = 0; i < theStaffWorkDate.Count; i++)
            {
                int startTimeToInt;
                int endTimeToInt;
                int breakStartToInt;
                int breakEndToInt;
                var a = theStaffWorkDate[i];
                if (DateTime.Parse(theStaffWorkDate[i]).DayOfWeek == DayOfWeek.Saturday ||
                    DateTime.Parse(theStaffWorkDate[i]).DayOfWeek == DayOfWeek.Sunday)
                {
                    startTimeToInt = holidayStartTimeToInt;
                    endTimeToInt = holidayEndTimeToInt;
                    breakStartToInt = holidayBreakStartToInt;
                    breakEndToInt = holidayBreakEndToInt;
                }
                else
                {
                    startTimeToInt = weekdayStartTimeToInt;
                    endTimeToInt = weekdayEndTimeToInt;
                    breakStartToInt = weekdayBreakStartToInt;
                    breakEndToInt = weekdayBreakEndToInt;
                }

                int theStartTime = startTimeToInt;
                StringBuilder theDepositTime = new StringBuilder();
                while (theStartTime < endTimeToInt)
                {
                    int theStartTimesHour;
                    int theStartTimesMin;
                    int totalTime = theStartTime + theItemSpendTimeHour * 100 + theItemSpendTimeMin;
                    int totalTimeHour = totalTime / 100;
                    int totalTimeMin = totalTime % 100;
                    while (totalTimeMin >= 60)
                    {
                        totalTimeHour += 1;
                        totalTimeMin -= 60;
                    }
                    totalTime = totalTimeHour * 100 + totalTimeMin;
                    bool isOk = true;
                    List<JObject> staffWorkDateInReserve = new List<JObject>();
                    if (theReserveDateDictionary.ContainsKey(DateTime.Parse(theStaffWorkDate[i])))
                    {
                        staffWorkDateInReserve = theReserveDateDictionary[DateTime.Parse(theStaffWorkDate[i])];
                    }
                    foreach (var timeObject in staffWorkDateInReserve)
                    {
                        DateTime reserveStart = (DateTime)timeObject["ReserveStart"];
                        DateTime reserveEnd = (DateTime)timeObject["ReserveEnd"];
                        int reserveStartTime = reserveStart.Hour * 100 + reserveStart.Minute;
                        int reserveEndTime = reserveEnd.Hour * 100 + reserveEnd.Minute;
                        if (totalTime > reserveStartTime && theStartTime < reserveEndTime)
                        {
                            isOk = false;
                            break;
                        }
                    }

                    if (isOk && (totalTime < breakStartToInt || theStartTime > breakEndToInt))
                    {
                        theStartTimesHour = theStartTime / 100;
                        theStartTimesMin = theStartTime % 100;
                        while (theStartTimesMin >= 60)
                        {
                            theStartTimesHour += 1;
                            theStartTimesMin -= 60;
                        }

                        if (theStartTimesHour < 10)
                        {
                            if (theStartTimesMin < 10)
                            {
                                theDepositTime.Append("0" + theStartTimesHour + ":0" + theStartTimesMin +
                                                      ",");
                                theStartTime = int.Parse(theStartTimesHour + "0" + theStartTimesMin);
                            }
                            else
                            {
                                theDepositTime.Append(
                                    "0" + theStartTimesHour + ":" + theStartTimesMin + ",");
                                theStartTime = int.Parse(theStartTimesHour + "" + theStartTimesMin);
                            }
                        }
                        else
                        {
                            if (theStartTimesMin < 10)
                            {
                                theDepositTime.Append(theStartTimesHour + ":0" + theStartTimesMin + ",");
                                theStartTime = int.Parse(theStartTimesHour + "0" + theStartTimesMin);
                            }
                            else
                            {
                                theDepositTime.Append(theStartTimesHour + ":" + theStartTimesMin + ",");
                                theStartTime = int.Parse(theStartTimesHour + "" + theStartTimesMin);
                            }
                        }
                    }
                    theStartTime += timeIntervalToInt;
                    int theStartTimeHour = theStartTime / 100;
                    int theStartTimeMin = theStartTime % 100;
                    while (theStartTimeMin >= 60)
                    {
                        theStartTimeHour += 1;
                        theStartTimeMin -= 60;
                    }
                    theStartTime = theStartTimeHour * 100 + theStartTimeMin;
                    theStaffWorkDateJObject[theStaffWorkDate[i]] = theDepositTime.ToString().TrimEnd(',');
                }
            }
            return Ok(new { TheReserveDate = theStaffWorkDateJObject, TheEnableDate = theEnableDate });
        }



        /// <summary>
        /// 顧客確定預約
        /// </summary>
        [HttpPost]
        [Route("ConfirmReserve")]
        public IHttpActionResult ConfirmReserve(ReserveInformation view)
        {
            var userToken = JwtAuthFilter.GetToken(Request.Headers.Authorization.Parameter);
            int identityId = (int)userToken["IdentityId"];
            var itemPrice = db.BusinessItems.Where(i => i.Id == view.ItemId).Select(i => new
            {
                i.SpendTime,
                i.Price,
            }).ToList();

            int hourIndex = itemPrice[0].SpendTime.LastIndexOf("小時");
            int minIndex = itemPrice[0].SpendTime.LastIndexOf("分鐘");
            int theItemSpendTimeMin;
            int theItemSpendTimeHour;
            if (itemPrice[0].SpendTime.Length == 3)
            {
                theItemSpendTimeHour = Int32.Parse(itemPrice[0].SpendTime.Remove(hourIndex));
                theItemSpendTimeMin = 0;
            }
            else if (itemPrice[0].SpendTime.Length == 4)
            {
                theItemSpendTimeHour = 0;
                theItemSpendTimeMin = Int32.Parse(itemPrice[0].SpendTime.Substring(0, minIndex));
            }
            else
            {
                theItemSpendTimeHour = Int32.Parse(itemPrice[0].SpendTime.Remove(hourIndex));
                theItemSpendTimeMin = Int32.Parse(itemPrice[0].SpendTime.Substring(3, 2));
            }
            CustomerReserve customerReserve = new CustomerReserve();
            customerReserve.CustomerId = identityId;
            customerReserve.StaffId = view.StaffId;
            customerReserve.ItemId = view.ItemId;
            customerReserve.StoreId = view.StoreId;
            customerReserve.ReserveDate = view.ReserveDate;
            customerReserve.StaffName = view.StaffName;
            customerReserve.ReserveStart = view.ReserveStart;
            customerReserve.ReserveEnd = view.ReserveStart.AddMinutes(theItemSpendTimeHour * 60 + theItemSpendTimeMin);
            customerReserve.ReserveState = "undone";
            customerReserve.Price = itemPrice[0].Price;

            db.CustomerReserve.Add(customerReserve);
            db.SaveChanges();

            return Ok(new { Message = "預約完成" });
        }



        /// <summary>
        /// 顧客預約店家項目狀態
        /// </summary>
        [HttpGet]
        [Route("ReserveState")]
        public IHttpActionResult ReserveState()
        {
            var userToken = JwtAuthFilter.GetToken(Request.Headers.Authorization.Parameter);
            int identityId = (int)userToken["IdentityId"];
            var storeId = db.StoreDetail.AsQueryable();
            var customerReserve = db.CustomerReserve.AsQueryable();
            var finishReserve = customerReserve.Where(r => r.CustomerId == identityId).Where(r => r.ReserveState == "Finish").Select(r => new
            {
                r.StoreId,
                StoreInformation = storeId.Where(s => s.Id == r.StoreId).Select(s => new
                {
                    s.StoreName,
                    Address = s.City + s.District + s.Address,
                    s.StaffTitle
                }),
                r.ReserveDate,
                r.ReserveStart,
                r.StaffName,
                r.BusinessItems.ItemName,
                r.BusinessItems.SpendTime,
                r.BusinessItems.Price
            }).AsEnumerable().Select(a => new
            {
                a.StoreId,
                a.StoreInformation,
                ReserveDate = a.ReserveDate.ToString("yyyy/MM/dd"),
                ReserveStart = a.ReserveStart.ToString("HH:mm"),
                a.StaffName,
                a.ItemName,
                a.SpendTime,
                a.Price
            }).ToList();

            var undoneReserve = customerReserve.Where(r => r.CustomerId == identityId).Where(r => r.ReserveState == "Undone").Select(r => new
            {
                r.StoreId,
                StoreInformation = storeId.Where(s => s.Id == r.StoreId).Select(s => new
                {
                    s.StoreName,
                    Address = s.City + s.District + s.Address,
                    s.StaffTitle
                }),
                r.ReserveDate,
                r.ReserveStart,
                r.StaffName,
                r.BusinessItems.ItemName,
                r.BusinessItems.SpendTime,
                r.BusinessItems.Price
            }).AsEnumerable().Select(a => new
            {
                a.StoreId,
                a.StoreInformation,
                ReserveDate = a.ReserveDate.ToString("yyyy/MM/dd"),
                ReserveStart = a.ReserveStart.ToString("HH:mm"),
                a.StaffName,
                a.ItemName,
                a.SpendTime,
                a.Price
            }).ToList();
            if (finishReserve.Count < 0 && undoneReserve.Count < 0)
            {
                return Ok("無資料");
            }
            else
            {
                return Ok(new { FinishReserve = finishReserve, UndoneReserve = undoneReserve });
            }
        }



        /// <summary>
        /// 搜尋店家
        /// </summary>
        [HttpGet]
        [Route("SearchStore")]
        public IHttpActionResult SearchStore([FromBody] SearchStore view)
        {

            int page = view.Page;
            int pageSize = 6;
            var skip = (page - 1) * pageSize;
            var businessItem = db.BusinessItems.AsQueryable();
            if (!string.IsNullOrEmpty(view.City))
            {
                if (!string.IsNullOrEmpty(view.District))
                {
                    if (!string.IsNullOrEmpty(view.IndustryId.ToString()))
                    {
                        var theStoreInformation = db.StoreDetail.Where(s => s.City == view.City)
                            .Where(s => s.Description == view.District).Where(s => s.IndustryId == view.IndustryId)
                            .Select(s => new
                            {
                                s.Id,
                                s.StoreName,
                                BusinessItem = businessItem.Where(i => i.StoreId == s.Id),
                                s.BusinessInformation.HolidayStartTime,
                                s.BusinessInformation.HolidayEndTime,
                                s.BusinessInformation.WeekdayStartTime,
                                s.BusinessInformation.WeekdayEndTime,
                                Address = s.City + s.District + s.Address,
                                s.Description,
                               s.BannerPath
                            }).AsEnumerable().Select(a => new
                            {
                                a.Id,
                                a.StoreName,
                                a.BusinessItem,
                                a.HolidayStartTime,
                                a.HolidayEndTime,
                                a.WeekdayStartTime,
                                a.WeekdayEndTime,
                                a.Address,
                                a.Description,
                                BannerPath = BannerObject(a.BannerPath)
                            }).ToList();
                        var theResult = theStoreInformation.OrderBy(s => s.Id).Skip(skip).Take(pageSize);
                        return Ok(theResult);
                    }
                    else
                    {
                        var theStoreInformation = db.StoreDetail.Where(s => s.City == view.City)
                            .Where(s => s.Description == view.District)
                            .Select(s => new
                            {
                                s.Id,
                                s.StoreName,
                                BusinessItem = businessItem.Where(i => i.StoreId == s.Id),
                                s.BusinessInformation.HolidayStartTime,
                                s.BusinessInformation.HolidayEndTime,
                                s.BusinessInformation.WeekdayStartTime,
                                s.BusinessInformation.WeekdayEndTime,
                                Address = s.City + s.District + s.Address,
                                s.Description,
                                s.BannerPath
                            }).AsEnumerable().Select(a => new
                            {
                                a.Id,
                                a.StoreName,
                                a.BusinessItem,
                                a.HolidayStartTime,
                                a.HolidayEndTime,
                                a.WeekdayStartTime,
                                a.WeekdayEndTime,
                                a.Address,
                                a.Description,
                                BannerPath = BannerObject(a.BannerPath)
                            }).ToList();
                        var theResult = theStoreInformation.OrderBy(s => s.Id).Skip(skip).Take(pageSize);
                        return Ok(theResult);
                    }
                }
                else
                {
                    var theStoreInformation = db.StoreDetail.Where(s => s.City == view.City).Select(s => new
                    {
                        s.Id,
                        s.StoreName,
                        BusinessItem = businessItem.Where(i => i.StoreId == s.Id),
                        s.BusinessInformation.HolidayStartTime,
                        s.BusinessInformation.HolidayEndTime,
                        s.BusinessInformation.WeekdayStartTime,
                        s.BusinessInformation.WeekdayEndTime,
                        Address = s.City + s.District + s.Address,
                        s.Description,
                        s.BannerPath
                    }).AsEnumerable().Select(a => new
                    {
                        a.Id,
                        a.StoreName,
                        a.BusinessItem,
                        a.HolidayStartTime,
                        a.HolidayEndTime,
                        a.WeekdayStartTime,
                        a.WeekdayEndTime,
                        a.Address,
                        a.Description,
                        BannerPath = BannerObject(a.BannerPath)
                    }).ToList();
                    var theResult = theStoreInformation.OrderBy(s => s.Id).Skip(skip).Take(pageSize);
                    return Ok(theResult);
                }
            }
            else if (!string.IsNullOrEmpty(view.Keyword))
            {
                //var theStoreInformation = db.StoreDetail.Where(s => s.StoreName.Contains(view.Keyword)).Select(s => new
                //{
                //    s.StoreDetail.Id,
                //    s.StoreDetail.StoreName,
                //    s.BusinessItems
                //    s.BusinessInformation.HolidayStartTime,
                //    s.BusinessInformation.HolidayEndTime,
                //    s.BusinessInformation.WeekdayStartTime,
                //    s.BusinessInformation.WeekdayEndTime,
                //    Address = i.StoreDetail.City + i.StoreDetail.District + i.StoreDetail.Address,
                //    i.StoreDetail.Description,
                //    i.StoreDetail.BannerPath
                //}).AsEnumerable().Select(a => new
                //{
                //    a.Id,
                //    a.StoreName,
                //    a.HolidayStartTime,
                //    a.HolidayEndTime,
                //    a.WeekdayStartTime,
                //    a.WeekdayEndTime,
                //    a.Address,
                //    a.Description,
                //    BannerPath = BannerObject(a.BannerPath)

                //}).ToList();
                //var theResult = theStoreInformation.OrderBy(s => s.Id).Skip(skip).Take(pageSize);
                return Ok("theResult");
            }
            else
            {
                var theStoreInformation = db.StoreDetail.Select(s => new
                {
                    s.Id,
                    s.StoreName,
                    BusinessItem = businessItem.Where(i => i.StoreId == s.Id),
                    s.BusinessInformation.HolidayStartTime,
                    s.BusinessInformation.HolidayEndTime,
                    s.BusinessInformation.WeekdayStartTime,
                    s.BusinessInformation.WeekdayEndTime,
                    Address = s.City + s.District + s.Address,
                    s.Description,
                    s.BannerPath
                }).AsEnumerable().Select(a => new
                {
                    a.Id,
                    a.StoreName,
                    a.BusinessItem,
                    a.HolidayStartTime,
                    a.HolidayEndTime,
                    a.WeekdayStartTime,
                    a.WeekdayEndTime,
                    a.Address,
                    a.Description,
                    BannerPath = BannerObject(a.BannerPath)
                }).ToList();
                var theResult = theStoreInformation.OrderBy(s => s.Id).Skip(skip).Take(pageSize);
                return Ok(theResult);
            }
        }
        JObject BannerObject(string bannerPath)
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
