using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BonnieYork.JWT;
using BonnieYork.Models;
using BonnieYork.Tool;
using NSwag.Annotations;

namespace BonnieYork.Controllers
{
    [OpenApiTag("Store", Description = "店家資訊API")]
    [RoutePrefix("store")]   //屬性路由前綴
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
            var storeInformation = db.StoreDetail.Where(s => s.Id == storeId).ToList();
            var industryId = storeInformation[0].IndustryId;
            var storeIndustry = db.Industry.Where(i => i.Id == industryId).Select(i => i.IndustryName).ToList();

            result = new
            {
                Identity = "store",
                StoreName = storeInformation[0].StoreName, 
                Industry = new
                {
                    Id = storeInformation[0].IndustryId,
                    Name = storeIndustry[0]
                },
                City = storeInformation[0].City,
                District = storeInformation[0].District,
                Address = storeInformation[0].Address,
                CellphoneNumber = storeInformation[0].CellphoneNumber,
                PhoneNumber = storeInformation[0].PhoneNumber,
                StaffTitle = storeInformation[0].StaffTitle,
                Description = storeInformation[0].Description,
                BannerPath = storeInformation[0].BannerPath,
                FacebookLink = storeInformation[0].FacebookLink,
                InstagramLink = storeInformation[0].InstagramLink,
                LineLink = storeInformation[0].LineLink,
            };
            return Ok(result);
        }


        [HttpPost]
        [Route("Information")]
        public IHttpActionResult Information([FromBody]InformationDataView view)
        {
            var userToken = JwtAuthFilter.GetToken(Request.Headers.Authorization.Parameter);
            int identityId = (int)userToken["IdentityId"];
            var storeDetailInDb = db.StoreDetail.Where(s => s.Id == identityId).ToList();

            foreach (StoreDetail item in storeDetailInDb)
            {
                item.StoreName = view.StoreName;
                item.IndustryId = view.IndustryId;
                item.City = view.City;
                item.District = view.District;
                item.Address = view.Address;
                item.CellphoneNumber = view.CellphoneNumber;
                item.PhoneNumber = view.PhoneNumber;
                item.StaffTitle = view.StaffTitle;
                item.Description = view.Description;
                item.FacebookLink = view.FacebookLink;
                item.InstagramLink = view.InstagramLink;
                item.LineLink = view.LineLink;
            }
            db.SaveChanges();
            string token = JwtAuthUtil.GenerateToken(storeDetailInDb[0].Id, storeDetailInDb[0].Id, storeDetailInDb[0].Account, storeDetailInDb[0].StoreName, "", "", "store");

            result = new
            {
                Message = "店鋪資訊修改完成",
                Token = token,
            };
            return Ok(result);
        }
    }
}
