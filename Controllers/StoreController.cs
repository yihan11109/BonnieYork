using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
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


        //[HttpPut]
        //[Route("PutInformation")]
        //public IHttpActionResult PutInformation()
        //{


        //}
    }
}
