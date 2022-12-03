﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
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


        [HttpGet]
        [Route("GetInformation")]
        public IHttpActionResult GetInformation()
        {
            var userToken = JwtAuthFilter.GetToken(Request.Headers.Authorization.Parameter);
            int identityId = (int)userToken["IdentityId"];
            int storeId = (int)userToken["StoreId"];
            var staffInformation = db.StaffDetail.Where(e => e.Id == identityId).Select(e => new
            {
                e.Account,
                e.StoreDetail.StoreName,
                e.CellphoneNumber,
                e.Introduction,
                e.FacebookLink,
                e.InstagramLink,
                e.LineLink,
                HeadShot = "https://" + Request.RequestUri.Host + "/upload/headshot/" + e.HeadShot,
            }).ToList();

            return Ok(new { Identity = "staff", StaffInformation = staffInformation });
        }


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
                Message = "顧客資訊修改完成",
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


            string root = HttpContext.Current.Server.MapPath(@"~/upload/headshot");

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
                var customerHeadShot = db.CustomerDetail.Where(c => c.Id == identityId).ToList();
                customerHeadShot[0].HeadShot = fileName;
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
    }
}