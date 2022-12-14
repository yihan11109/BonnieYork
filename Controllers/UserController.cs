using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Http;
using BonnieYork.Models;
using BonnieYork.Tool;
using BonnieYork.JWT;
using Newtonsoft.Json;
using NSwag.Annotations;

namespace BonnieYork.Controllers
{
    [OpenApiTag("User", Description = "登入註冊API")]
    [RoutePrefix("user")] //屬性路由前綴
    public class UserController : ApiController
    {
        private BonnieYorkDbContext db = new BonnieYorkDbContext();

        /// <summary>
        /// (註冊)判斷Email是否註冊過
        /// </summary>
        [HttpPost]
        [Route("SignUpIsValid")]
        public IHttpActionResult SignUpIsValid(ViewModel view) // 先判斷身分別，再判斷帳號是否註冊過
        {
            object result = new { };
            var modelErrorMessage = ModelState.Values.Select(e => e.Errors).ToList();

            if (view.Identity == "member")
            {
                var hasEmail = db.CustomerDetail.Where(c => c.Account == view.Account.ToLower()).ToList();

                //判斷Email是否註冊過
                if (hasEmail.Count > 0)
                {
                    return BadRequest("已註冊過");
                }
                else
                {
                    //判斷Email是否符合格式
                    if (ModelState.IsValid)
                    {
                        result = new
                        {
                            Message = "未註冊過"
                        };
                        return Ok(result);
                    }
                    else
                    {
                        return BadRequest(modelErrorMessage[0][0].ErrorMessage);
                    }
                }
            }
            else
            {
                var hasStoreEmail = db.StoreDetail.Where(s => s.Account == view.Account.ToLower()).ToList();
                var hasStaffEmail = db.StaffDetail.Where(e => e.Account == view.Account.ToLower()).ToList();
                if (view.Identity == "store" || view.Identity == "staff")
                {
                    //判斷Email是否註冊過
                    if (hasStoreEmail.Count > 0 || hasStaffEmail.Count > 0)
                    {
                        return BadRequest("已註冊過");

                    }
                    else
                    {
                        //判斷Email是否符合格式
                        if (ModelState.IsValid)
                        {
                            result = new
                            {
                                Message = "未註冊過"
                            };
                            return Ok(result);
                        }
                        else
                        {
                            return BadRequest(modelErrorMessage[0][0].ErrorMessage);
                        }
                    }
                }
                else
                {
                    return BadRequest("無此Identity");
                }
            }
        }

        /// <summary>
        /// (註冊)發送註冊連結
        /// </summary>
        [HttpPost]
        [Route("SignUpSendLink")]
        public IHttpActionResult SignUpSendLink(SignUpUserDataView view) //未註冊過，寄送註冊連結
        {
            string fromAddress = ConfigurationManager.AppSettings["fromAddress"];
            string toAddress = view.Account.ToLower();
            string subject = "BonnieYork註冊連結確認";
            string mailBody = "親愛的BonnieYork會員您好：" + "<br>此封信件為您在BonnieYork註冊會員時所發送之連結信件，" +
                              "<br >請點選註冊連結進入頁面以完成註冊。<br ><br>" +
                              "※提醒您，此註冊連結有效期為10分鐘，若連結失效請再次前往註冊頁面重新寄送註冊連結，謝謝您。<br><br>  http://localhost:3000/signup?token=";
            string mailBodyEnd = "<br><br>-----此為系統發出信件，請勿直接回覆，感謝您的配合。-----";
            string emailPassword = ConfigurationManager.AppSettings["emailPassword"];
            string token = "";
            if (view.Identity == "staff")
            {
                var userToken = JwtAuthFilter.GetToken(Request.Headers.Authorization.Parameter);

                if (view.BusinessItemsId != null)
                {
                    int storeId = (int)userToken["StoreId"];
                    string storeName = userToken["StoreName"].ToString();
                    userToken["Account"] = view.Account.ToLower();
                    userToken["JobTitle"] = view.JobTitle;
                    userToken["BusinessItemsId"] = view.BusinessItemsId;
                    token = JwtAuthUtil.GenerateSignUpToken(view.Account.ToLower(), storeId, storeName, "staff",
                        view.BusinessItemsId, view.JobTitle);
                }
            }
            else
            {
                token = JwtAuthUtil.GenerateSignUpToken(view.Account.ToLower(), 0, "", view.Identity, null, "");
            }

            Mail.SendGmailMail(fromAddress, toAddress, subject, mailBody + token + mailBodyEnd, emailPassword);

            object result = new
            {
                Message = $"註冊連結已寄到{view.Account.ToLower()}",
            };
            return Ok(result);
        }


        /// <summary>
        /// (註冊)解密回傳token
        /// </summary>
        [HttpGet]
        [Route("GetSignUpToken")]
        [JwtAuthFilter]
        public IHttpActionResult GetSignUpToken() //取得token解密後回傳
        {
            // 取出請求內容，解密 JwtToken 取出資料(每一個都做token檢查)
            var userToken = JwtAuthFilter.GetToken(Request.Headers.Authorization.Parameter);

            return Ok(new { Token = userToken });

        }


        /// <summary>
        /// (註冊)存入密碼及基本資料
        /// </summary>
        [HttpPost]
        [Route("SignUpUserData")]
        public IHttpActionResult SignUpUserData(SignUpUserDataView view)
        {
            object result = new { };
            CustomerDetail customerDetail = new CustomerDetail();
            StoreDetail storeDetail = new StoreDetail();
            StaffDetail staffDetail = new StaffDetail();

            if (view.Identity == "member")
            {
                if (view.Password != view.CheckPassword)
                {
                    return BadRequest("密碼不同");
                }
                else
                {
                    customerDetail.Account = view.Account.ToLower();
                    customerDetail.Password = BitConverter
                        .ToString(MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(view.Password))).Replace("-", null);
                    customerDetail.CustomerName = view.CustomerName;
                    customerDetail.CellphoneNumber = view.CellphoneNumber;
                    customerDetail.BirthDay = view.BirthDay;
                    db.CustomerDetail.Add(customerDetail);
                    db.SaveChanges();

                    var customerInformation = db.CustomerDetail.Where(c => c.Account == view.Account).ToList();

                    string token = JwtAuthUtil.GenerateToken(customerInformation[0].Id, 0, view.Account.ToLower(),
                        "", "", customerInformation[0].CustomerName, "member");

                    result = new
                    {
                        Message = "顧客註冊完成",
                        Token = token
                    };
                }

            }
            else if (view.Identity == "store")
            {
                if (view.Password != view.CheckPassword)
                {
                    return BadRequest("密碼不同");
                }
                else
                {
                    storeDetail.Account = view.Account.ToLower();
                    storeDetail.Password = BitConverter
                        .ToString(MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(view.Password))).Replace("-", null);
                    storeDetail.StoreName = view.StoreName;
                    storeDetail.City = view.City;
                    storeDetail.District = view.District;
                    storeDetail.Address = view.Address;
                    storeDetail.CellphoneNumber = view.CellphoneNumber;

                    db.StoreDetail.Add(storeDetail);
                    db.SaveChanges();

                    var storeInformation = db.StoreDetail.Where(s => s.Account == view.Account.ToLower()).ToList();

                    string token = JwtAuthUtil.GenerateToken(storeInformation[0].Id, storeInformation[0].Id, view.Account.ToLower(), storeInformation[0].StoreName, "", "", "store");

                    result = new
                    {
                        Message = "店鋪註冊完成",
                        Token = token
                    };
                }

            }
            else if (view.Identity == "staff")
            {
                if (view.Password != view.CheckPassword)
                {
                    result = new
                    {
                        message = "密碼不同"
                    };
                }
                else
                {
                    var userToken = JwtAuthFilter.GetToken(Request.Headers.Authorization.Parameter);   //staff註冊信連結的token
                    int storeId = (int)userToken["StoreId"];

                    staffDetail.StoreId = storeId;
                    staffDetail.Account = view.Account.ToLower();
                    staffDetail.Password = BitConverter
                        .ToString(MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(view.Password))).Replace("-", null);
                    staffDetail.StaffName = view.StaffName;
                    staffDetail.CellphoneNumber = view.CellphoneNumber;
                    staffDetail.JobTitle = userToken["JobTitle"].ToString();
                    db.StaffDetail.Add(staffDetail);
                    db.SaveChanges();

                    var staffInformation = db.StaffDetail.Where(e => e.Account == view.Account.ToLower()).Select(e => new
                    {
                        e.Id,
                        e.StaffName,
                        e.StoreId,
                        e.StoreDetail.StoreName
                    })
                        .ToList();

                    StaffWorkItems staffWorkItems = new StaffWorkItems();
                    foreach (var item in (IEnumerable)userToken["BusinessItemId"])
                    {
                        int workItemId = Convert.ToInt32(item);
                        staffWorkItems.BusinessItemsId = workItemId;
                        staffWorkItems.StaffId = staffInformation[0].Id;
                        staffWorkItems.StaffName = staffInformation[0].StaffName;
                        db.StaffWorkItems.Add(staffWorkItems);
                        db.SaveChanges();
                    }

                    string token = JwtAuthUtil.GenerateToken(staffInformation[0].Id, staffInformation[0].StoreId, view.Account.ToLower(), staffInformation[0].StoreName, staffInformation[0].StaffName, "", "staff");
                    result = new
                    {
                        message = "員工註冊完成",
                        Token = token
                    };
                }
            }
            return Ok(result);
        }


        /// <summary>
        /// 登入
        /// </summary>
        [HttpPost]
        [Route("Login")]
        public IHttpActionResult Login(SignUpUserDataView view)
        {
            object result = new { };
            if (view.Identity == "member")
            {
                CustomerDetail customer = new CustomerDetail();
                customer.Password = BitConverter
                    .ToString(MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(view.Password))).Replace("-", null);
                var passwordChecked = db.CustomerDetail.Where(c => c.Account == view.Account.ToLower())
                    .Where(c => c.Password == customer.Password).ToList();

                if (passwordChecked.Count > 0)
                {
                    var customerInformation = db.CustomerDetail.Where(c => c.Account == view.Account.ToLower())
                        .Select(c => new
                        {
                            c.Id,
                            c.CustomerName
                        }).ToList();

                    string token = JwtAuthUtil.GenerateToken(customerInformation[0].Id, 0, view.Account,
                        "", "", customerInformation[0].CustomerName, "member");
                    result = new
                    {
                        Identity = "member",
                        Id = customerInformation[0].Id,
                        CustomerName = customerInformation[0].CustomerName,
                        Message = "已登入",
                        Token = token
                    };
                }
                else
                {
                    return BadRequest("密碼錯誤");
                }
            }
            else if (view.Identity == "store")
            {

                StoreDetail store = new StoreDetail();
                store.Password = BitConverter.ToString(MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(view.Password)))
                    .Replace("-", null);
                //找帳號密碼是否存在
                var StorePasswordChecked = db.StoreDetail.Where(s => s.Account == view.Account.ToLower()).Where(s => s.Password == store.Password).ToList();
                var StaffPasswordChecked = db.StaffDetail.Where(s => s.Account == view.Account.ToLower()).Where(s => s.Password == store.Password).ToList();

                if (StorePasswordChecked.Count > 0)
                {
                    var storeInformation = db.StoreDetail.Where(s => s.Account == view.Account.ToLower()).Select(s =>
                        new
                        {
                            s.Id,
                            s.StoreName
                        }).ToList();
                    string token = JwtAuthUtil.GenerateToken(storeInformation[0].Id, storeInformation[0].Id,
                        view.Account.ToLower(), storeInformation[0].StoreName, "", "", "store");
                    result = new
                    {
                        Identity = "store",
                        Id = storeInformation[0].Id,
                        StoreName = storeInformation[0].StoreName,
                        Message = "已登入",
                        Token = token
                    };

                }
                else if (StaffPasswordChecked.Count > 0)
                {
                    var staffInformation = db.StaffDetail.Where(e => e.Account == view.Account.ToLower()).Select(e =>
                        new
                        {
                            e.Id,
                            e.StoreId,
                            e.StaffName,
                            e.StoreDetail.StoreName
                        }).ToList();

                    string token = JwtAuthUtil.GenerateToken(staffInformation[0].Id, staffInformation[0].StoreId,
                        view.Account.ToLower(), staffInformation[0].StoreName, staffInformation[0].StaffName, "", "staff");
                    result = new
                    {
                        Identity = "staff",
                        Id = staffInformation[0].Id,
                        StoreId = staffInformation[0].StoreId,
                        StoreName = staffInformation[0].StoreName,
                        StaffName = staffInformation[0].StaffName,
                        Message = "已登入",
                        Token = token
                    };
                }
                else
                {
                    return BadRequest("密碼錯誤");
                }
            }
            return Ok(result);
        }


        /// <summary>
        /// 重設密碼
        /// </summary>
        [HttpPost]
        [Route("ResetPassword")]
        public IHttpActionResult ResetPassword(ResetPasswordView view)
        {
            CustomerDetail customer = new CustomerDetail();
            StoreDetail store = new StoreDetail();
            StaffDetail staff = new StaffDetail();
            object result = new { };
            var userToken = JwtAuthFilter.GetToken(Request.Headers.Authorization.Parameter);

            if (userToken["Identity"].ToString() == "member")
            {
                string hashPassword = BitConverter
                    .ToString(MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(view.OriginalPassword)))
                    .Replace("-", null);

                var passwordInDb = db.CustomerDetail.Where(c => c.Password == hashPassword).ToList();

                if (passwordInDb.Count > 0)
                {
                    if (view.Password != view.CheckPassword)
                    {
                        return BadRequest("新密碼與再次輸入新密碼的內容不一致");
                    }
                    else
                    {
                        string newHashPassword = BitConverter
                            .ToString(MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(view.CheckPassword)))
                            .Replace("-", null);

                        foreach (CustomerDetail item in passwordInDb)
                        {
                            item.Password = newHashPassword;
                        }

                        db.SaveChanges();
                        result = new
                        {
                            Message = "密碼修改完成",
                        };
                    }
                }
                else
                {
                    return BadRequest("輸入的舊密碼不符");
                }
            }
            else if (userToken["Identity"].ToString() == "store")
            {
                string hashPassword = BitConverter
                    .ToString(MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(view.OriginalPassword)))
                    .Replace("-", null);
                var passwordInDb = db.StoreDetail.Where(s => s.Password == hashPassword).ToList();
                if (passwordInDb.Count > 0)
                {
                    if (view.Password != view.CheckPassword)
                    {
                        return BadRequest("新密碼與再次輸入新密碼的內容不一致");
                    }
                    else
                    {
                        string newHashPassword = BitConverter
                            .ToString(MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(view.CheckPassword)))
                            .Replace("-", null);

                        foreach (StoreDetail item in passwordInDb)
                        {
                            item.Password = newHashPassword;
                        }

                        db.SaveChanges();
                        result = new
                        {
                            Message = "密碼修改完成",
                        };
                    }
                }
            }
            else if (userToken["Identity"].ToString() == "staff")
            {
                string hashPassword = BitConverter
                    .ToString(MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(view.OriginalPassword)))
                    .Replace("-", null);
                var passwordInDb = db.StaffDetail.Where(e => e.Password == hashPassword).ToList();
                if (passwordInDb.Count > 0)
                {
                    if (view.Password != view.CheckPassword)
                    {
                        return BadRequest("新密碼與再次輸入新密碼的內容不一致");
                    }
                    else
                    {
                        string newHashPassword = BitConverter
                            .ToString(MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(view.CheckPassword)))
                            .Replace("-", null);

                        foreach (StaffDetail item in passwordInDb)
                        {
                            item.Password = newHashPassword;
                        }

                        db.SaveChanges();
                        result = new
                        {
                            Message = "密碼修改完成",
                        };
                    }
                }
            }

            return Ok(result);
        }


        /// <summary>
        /// 每頁驗證
        /// </summary>
        [HttpGet]
        [Route("VerifyUser")]
        [JwtAuthFilter]
        public IHttpActionResult VerifyUser()
        {
            object result = new { };
            var userToken = JwtAuthFilter.GetToken(Request.Headers.Authorization.Parameter);
            string identity = userToken["Identity"].ToString();
            int identityId = (int)userToken["IdentityId"];
            string headShot = null;

            if (identity == "member")
            {
                var theHeadShot = db.CustomerDetail.Where(c => c.Id == identityId).Select(c => c.HeadShot).ToList();
                if (theHeadShot[0] != null)
                {
                    headShot = "https://" + Request.RequestUri.Host + "/upload/HeadShot/" + theHeadShot[0];
                }
            }
            else if (identity == "store")
            {
                var theHeadShot = db.StoreDetail.Where(s => s.Id == identityId).Select(s => s.HeadShot).ToList();
                if (theHeadShot[0] != null)
                {
                    headShot = "https://" + Request.RequestUri.Host + "/upload/HeadShot/" + theHeadShot[0];
                }
            }
            else
            {
                var theHeadShot = db.StaffDetail.Where(s => s.Id == identityId).Select(s => s.HeadShot).ToList();
                if (theHeadShot[0] != null)
                {
                    headShot = "https://" + Request.RequestUri.Host + "/upload/HeadShot/" + theHeadShot[0];
                }
            }
            result = new
            {
                Message = "有效的JwtToken",
                IdentityId = userToken["IdentityId"],
                Identity = userToken["Identity"],
                Account = userToken["Account"],
                StoreId = userToken["StoreId"],
                StoreName = userToken["StoreName"],
                StaffName = userToken["StaffName"],
                CustomerName = userToken["CustomerName"],
                HeadShot = headShot
            };
            return Ok(result);
        }



        /// <summary>
        /// 發送忘記密碼連結
        /// </summary>
        [HttpPost]
        [Route("ForgetPasswordLink")]
        public IHttpActionResult ForgetPasswordLink(SignUpUserDataView view)
        {
            string fromAddress = ConfigurationManager.AppSettings["fromAddress"];
            string toAddress = view.Account.ToLower();
            string subject = "BonnieYork重設密碼連結";
            string mailBody = "親愛的BonnieYork會員您好：" + "<br>此封信件為您在BonnieYork點選「忘記密碼」時所發送之連結信件，" +
                              "<br >請點選下列連結進入頁面以完成重設密碼。<br ><br>" +
                              "※提醒您，此連結有效期為10分鐘，若連結失效請再次點選「忘記密碼」按鈕重新寄送連結，謝謝您。<br><br>  http://localhost:3000/signup?token=";
            string mailBodyEnd = "<br><br>-----此為系統發出信件，請勿直接回覆，感謝您的配合。-----";
            string emailPassword = ConfigurationManager.AppSettings["emailPassword"];
            string token = JwtAuthUtil.GenerateSignUpToken(view.Account.ToLower(), 0, "", "",
                null, "");
            

            Mail.SendGmailMail(fromAddress, toAddress, subject, mailBody + token + mailBodyEnd, emailPassword);

            return Ok(new { Message = $"註冊連結已寄到{view.Account.ToLower()}"});
        }



        /// <summary>
        /// 忘記密碼
        /// </summary>
        [HttpPost]
        [Route("ForgetPassword")]
        public IHttpActionResult ForgetPassword(ResetPasswordView view)
        {
            CustomerDetail customer = new CustomerDetail();
            StoreDetail store = new StoreDetail();
            StaffDetail staff = new StaffDetail();
            object result = new { };
            var userToken = JwtAuthFilter.GetToken(Request.Headers.Authorization.Parameter);
            string Account = userToken["Account"].ToString();

            var customerIdentity = db.CustomerDetail.Where(c => c.Account == Account).ToList();
            if (customerIdentity.Count > 0)
            {

            }



            //    if (userToken["Identity"].ToString() == "member")
            //{
            //    string hashPassword = BitConverter
            //        .ToString(MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(view.OriginalPassword)))
            //        .Replace("-", null);

            //    var passwordInDb = db.CustomerDetail.Where(c => c.Password == hashPassword).ToList();

            //    if (passwordInDb.Count > 0)
            //    {
            //        if (view.Password != view.CheckPassword)
            //        {
            //            return BadRequest("新密碼與再次輸入新密碼的內容不一致");
            //        }
            //        else
            //        {
            //            string newHashPassword = BitConverter
            //                .ToString(MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(view.CheckPassword)))
            //                .Replace("-", null);

            //            foreach (CustomerDetail item in passwordInDb)
            //            {
            //                item.Password = newHashPassword;
            //            }

            //            db.SaveChanges();
            //            result = new
            //            {
            //                Message = "密碼修改完成",
            //            };
            //        }
            //    }
            //    else
            //    {
            //        return BadRequest("輸入的舊密碼不符");
            //    }
            //}
            //else if (userToken["Identity"].ToString() == "store")
            //{
            //    string hashPassword = BitConverter
            //        .ToString(MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(view.OriginalPassword)))
            //        .Replace("-", null);
            //    var passwordInDb = db.StoreDetail.Where(s => s.Password == hashPassword).ToList();
            //    if (passwordInDb.Count > 0)
            //    {
            //        if (view.Password != view.CheckPassword)
            //        {
            //            return BadRequest("新密碼與再次輸入新密碼的內容不一致");
            //        }
            //        else
            //        {
            //            string newHashPassword = BitConverter
            //                .ToString(MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(view.CheckPassword)))
            //                .Replace("-", null);

            //            foreach (StoreDetail item in passwordInDb)
            //            {
            //                item.Password = newHashPassword;
            //            }

            //            db.SaveChanges();
            //            result = new
            //            {
            //                Message = "密碼修改完成",
            //            };
            //        }
            //    }
            //}
            //else if (userToken["Identity"].ToString() == "staff")
            //{
            //    string hashPassword = BitConverter
            //        .ToString(MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(view.OriginalPassword)))
            //        .Replace("-", null);
            //    var passwordInDb = db.StaffDetail.Where(e => e.Password == hashPassword).ToList();
            //    if (passwordInDb.Count > 0)
            //    {
            //        if (view.Password != view.CheckPassword)
            //        {
            //            return BadRequest("新密碼與再次輸入新密碼的內容不一致");
            //        }
            //        else
            //        {
            //            string newHashPassword = BitConverter
            //                .ToString(MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(view.CheckPassword)))
            //                .Replace("-", null);

            //            foreach (StaffDetail item in passwordInDb)
            //            {
            //                item.Password = newHashPassword;
            //            }

            //            db.SaveChanges();
            //            result = new
            //            {
            //                Message = "密碼修改完成",
            //            };
            //        }
            //    }
            //}

            return Ok(result);
        }

    }
}



