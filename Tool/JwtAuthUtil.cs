using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.Http;
using System.Web.Http.Controllers;
using Jose;
using Newtonsoft.Json;

namespace BonnieYork.JWT
{
    public class JwtAuthUtil
    {
        public static string GenerateSignUpToken(string account,int storeId, string identity,int businessItemId,string userToken)   //註冊連結Token
        {
            // 自訂字串，驗證用，用來加密送出的 key 
            string secretKey = WebConfigurationManager.AppSettings["TokenKey"];
            //var user = db.User.Find(id); // 進 DB 取出想要夾帶的基本資料

            // payload 需透過 token 傳遞的資料 (可夾帶常用且不重要的資料)
            var payload = new Dictionary<string, object>
            {
                { "Account", account},
                { "StoreId", storeId },
                { "Identity", identity },
                { "BusinessItemId", businessItemId },
                { "Token", userToken },
                { "Exp", DateTime.Now.AddMinutes(60).ToString() } // JwtToken 時效設定 1 0 分
            };

            // 產生 JwtToken
            var token = Jose.JWT.Encode(payload, Encoding.UTF8.GetBytes(secretKey), JwsAlgorithm.HS512);
            return token;
        }


        public static string GenerateToken(int identityId, int storeId, string account, string storeName,string staffName,string customerName, string identity)   //判斷身分Token
        {
            // 自訂字串，驗證用，用來加密送出的 key 
            string secretKey = WebConfigurationManager.AppSettings["TokenKey"];
            //var user = db.User.Find(id); // 進 DB 取出想要夾帶的基本資料

            // payload 需透過 token 傳遞的資料 (可夾帶常用且不重要的資料)
            var payload = new Dictionary<string, object>
            {
                {"IdentityId",identityId},
                {"StoreId",storeId},
                { "Account", account},
                { "StoreName", storeName },
                { "StaffName", staffName },
                { "CustomerName", customerName },
                { "Identity", identity },
                { "Exp", DateTime.Now.AddDays(1).ToString() } // JwtToken 時效設定1天
            };

            // 產生 JwtToken
            var token = Jose.JWT.Encode(payload, Encoding.UTF8.GetBytes(secretKey), JwsAlgorithm.HS512);
            return token;
        }


        public string ExpRefreshToken(Dictionary<string, object> tokenData)       //刷新Token
        {
            string secretKey = WebConfigurationManager.AppSettings["TokenKey"];
            // payload 從原本 token 傳遞的資料沿用，並刷新效期
            var payload = new Dictionary<string, object>
            {
                { "Id", (int)tokenData["Id"] },
                { "Account", tokenData["Account"].ToString() },
                { "NickName", tokenData["NickName"].ToString() },
                { "Image", tokenData["Image"].ToString() },
                { "Exp", DateTime.Now.AddMinutes(30).ToString() } // JwtToken 時效刷新設定 30 分
            };

            //產生刷新時效的 JwtToken
            var token = Jose.JWT.Encode(payload, Encoding.UTF8.GetBytes(secretKey), JwsAlgorithm.HS512);
            return token;
        }


        public string RevokeToken()      //清除Token
        {
            string secretKey = "RevokeToken"; // 故意用不同的 key 生成
            var payload = new Dictionary<string, object>
            {
                { "Id", 0 },
                { "Account", "None" },
                { "NickName", "None" },
                { "Image", "None" },
                { "Exp", DateTime.Now.AddDays(-15).ToString() } // 使 JwtToken 過期 失效
            };

            // 產生失效的 JwtToken
            var token = Jose.JWT.Encode(payload, Encoding.UTF8.GetBytes(secretKey), JwsAlgorithm.HS512);
            return token;
        }

        public static string StaffSignUpToken(Dictionary<string, object> staffSignUpData)  //生成Staff註冊時傳送連結的token
        {
            // 自訂字串，驗證用，用來加密送出的 key 
            string secretKey = WebConfigurationManager.AppSettings["TokenKey"];

            // 產生 JwtToken
            var token = Jose.JWT.Encode(staffSignUpData, Encoding.UTF8.GetBytes(secretKey), JwsAlgorithm.HS512);
            return token;
        }     




    }
}