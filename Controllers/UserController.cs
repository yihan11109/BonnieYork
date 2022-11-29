using System;
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
    [RoutePrefix("user")]   //屬性路由前綴
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
                var hasEmail = db.CustomerDetail.Where(c => c.Account == view.Account).ToList();

                //判斷Email是否註冊過
                if (hasEmail.Count > 0)
                {
                    result = new
                    {
                        message = "已註冊過"
                    };
                }
                else
                {
                    //判斷Email是否符合格式
                    if (ModelState.IsValid)
                    {
                        result = new
                        {
                            message = "未註冊過"
                        };
                    }
                    else
                    {
                        result = new
                        {
                            message = modelErrorMessage[0][0].ErrorMessage
                        };
                    }
                }
            }
            else
            {
                var hasStoreEmail = db.StoreDetail.Where(s => s.Account == view.Account).ToList();
                var hasStaffEmail = db.StaffDetail.Where(e => e.Account == view.Account).ToList();
                if (view.Identity == "store" || view.Identity == "staff")
                {
                    //判斷Email是否註冊過
                    if (hasStoreEmail.Count > 0 || hasStaffEmail.Count > 0)
                    {
                        result = new
                        {
                            message = "已註冊過"
                        };
                    }
                    else
                    {
                        //判斷Email是否符合格式
                        if (ModelState.IsValid)
                        {
                            result = new
                            {
                                message = "未註冊過"
                            };
                        }
                        else
                        {
                            result = new
                            {
                                message = modelErrorMessage[0][0].ErrorMessage
                            };
                        }
                    }

                }
                else
                {
                    result = new
                    {
                        message = "無此Identity"
                    };
                }
            }

            return Ok(result);
        }


        [HttpPost]
        [Route("SignUpSendLink")]
        public IHttpActionResult SignUpSendLink(SignUpUserDataView view) //未註冊過，寄送註冊連結
        {
            string fromAddress = ConfigurationManager.AppSettings["fromAddress"];
            string toAddress = view.Account;
            string subject = "BonnieYork註冊連結確認";
            string mailBody = "http://localhost:3000/signup?token=";
            string emailPassword = ConfigurationManager.AppSettings["emailPassword"];
            string token = "";
            if (view.Identity == "staff")
            {
                var userToken = JwtAuthFilter.GetToken(Request.Headers.Authorization.Parameter);

                if (view.BusinessItemsId != 0)
                {
                    userToken["Account"] = view.Account;
                    userToken["JobTitle"] = view.JobTitle;
                    userToken["BusinessItemsId"] = view.BusinessItemsId;
                    token = JwtAuthUtil.StaffSignUpToken(userToken);
                }
            }
            else
            {
                token = JwtAuthUtil.GenerateSignUpToken(view.Account, 0, view.Identity, 0, "");
            }

            Mail.SendGmailMail(fromAddress, toAddress, subject, mailBody + token, emailPassword);

            object result = new
            {
                message = $"註冊連結已寄到{view.Account}",
            };
            return Ok(result);
        }


        [HttpGet]
        [Route("GetSignUpToken")]
        [JwtAuthFilter]
        public IHttpActionResult GetSignUpToken() //*********取得token解密後回傳 **********
        {
            // 取出請求內容，解密 JwtToken 取出資料(每一個都做token檢查)
            var userToken = JwtAuthFilter.GetToken(Request.Headers.Authorization.Parameter);
            // ExpRefreshToken() 生成刷新效期 JwtToken 用法
            JwtAuthUtil jwtAuthUtil = new JwtAuthUtil();
            //string jwtToken = jwtAuthUtil.ExpRefreshToken(userToken);
            object result = new
            {
                Token = userToken
            };
            return Ok(result);
        }


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
                    result = new
                    {
                        message = "密碼不同"
                    };
                }
                else
                {
                    customerDetail.Account = view.Account;
                    customerDetail.Password = BitConverter
                        .ToString(MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(view.Password))).Replace("-", null);
                    customerDetail.CustomerName = view.CustomerName;
                    customerDetail.CellphoneNumber = view.CellphoneNumber;
                    customerDetail.BirthDay = view.BirthDay;
                    db.CustomerDetail.Add(customerDetail);
                    db.SaveChanges();
                    result = new
                    {
                        message = "顧客註冊完成"
                    };
                }

            }
            else if (view.Identity == "store")
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
                    storeDetail.Account = view.Account;
                    storeDetail.Password = BitConverter
                        .ToString(MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(view.Password))).Replace("-", null);
                    storeDetail.StoreName = view.StoreName;
                    storeDetail.City = view.City;
                    storeDetail.District = view.District;
                    storeDetail.Address = view.Address;
                    storeDetail.CellphoneNumber = view.CellphoneNumber;

                    var storeDetailResult = db.StoreDetail.Add(storeDetail);
                    db.SaveChanges();

                    result = new
                    {
                        message = "店鋪註冊完成"
                    };
                }

            }
            //else if (view.Identity == "staff")
            //{
            //    if (view.Password != view.CheckPassword)
            //    {
            //        result = new
            //        {
            //            message = "密碼不同"
            //        };
            //    }
            //    else
            //    {
            //        var storeId = db.StaffDetail.Where(e => e.Account == view.Account).Select(e => new
            //        {
            //            e.StoreId,
            //            e.JobTitle
            //        }).ToList();
            //        staffDetail.Account = view.Account;
            //        staffDetail.Password = BitConverter.ToString(MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(view.Password))).Replace("-", null);
            //        staffDetail.StaffName = view.StaffName;
            //        staffDetail.CellphoneNumber = view.CellphoneNumber;

            //        db.StaffDetail.Add(staffDetail);
            //        db.SaveChanges();

            //        result = new
            //        {
            //            message = "員工註冊完成"
            //        };
            //    }

            //}
            return Ok(result);
        }


        [HttpPost]
        [Route("Login")]
        public IHttpActionResult Login(SignUpUserDataView view) // 還沒加staff
        {
            object result = new { };
            if (view.Identity == "member")
            {
                CustomerDetail customer = new CustomerDetail();
                customer.Password = BitConverter
                    .ToString(MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(view.Password))).Replace("-", null);
                var passwordChecked = db.CustomerDetail.Where(c => c.Account == view.Account)
                    .Where(c => c.Password == customer.Password).ToList();

                if (passwordChecked.Count > 0)
                {
                    var customerInformation = db.CustomerDetail
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
                        CostomerName = customerInformation[0].CustomerName,
                        Message = "已登入",
                        Token = token
                    };
                }
                else
                {
                    result = new
                    {
                        message = "密碼錯誤",
                    };
                }
            }
            else if (view.Identity == "store")
            {

                StoreDetail store = new StoreDetail();
                store.Password = BitConverter.ToString(MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(view.Password)))
                    .Replace("-", null);
                //找帳號密碼是否存在
                var StorePasswordChecked = db.StoreDetail.Where(s => s.Account == view.Account).Where(s => s.Password == store.Password).ToList();
                var StaffPasswordChecked = db.StaffDetail.Where(s => s.Account == view.Account).Where(s => s.Password == store.Password).ToList();

                if (StorePasswordChecked.Count > 0)
                {
                    var storeInformation = db.StoreDetail.Select(s => new
                    {
                        s.Id,
                        s.StoreName
                    }).ToList();
                    string token = JwtAuthUtil.GenerateToken(0, storeInformation[0].Id, view.Account, storeInformation[0].StoreName, "", "", "store");
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
                    var staffInformation = db.StaffDetail.Select(e => new
                    {
                        e.Id,
                        e.StaffName,
                        e.StoreId
                    }).ToList();
                    var storeName = db.StoreDetail.Where(s => s.Id == staffInformation[0].StoreId).ToList();

                    string token = JwtAuthUtil.GenerateToken(staffInformation[0].Id, staffInformation[0].StoreId, view.Account, storeName[0].StoreName, staffInformation[0].StaffName, "", "staff");
                    result = new
                    {
                        Identity = "staff",
                        Id = staffInformation[0].Id,
                        StoreId = staffInformation[0].StoreId,
                        StoreName = storeName[0].StoreName,
                        StaffName = staffInformation[0].StaffName,
                        Message = "已登入",
                        Token = token
                    };
                }
                else
                {
                    result = new
                    {
                        message = "密碼錯誤",
                    };
                }
            }
            return Ok(result);
        }


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
                string hashPassword = BitConverter.ToString(MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(view.OriginalPassword)))
                    .Replace("-", null);

                var passwordInDb = db.CustomerDetail.Where(c => c.Password == hashPassword).ToList();

                if (passwordInDb.Count > 0)
                {
                    if (view.Password != view.CheckPassword)
                    {
                        result = new
                        {
                            message = "新密碼與再次新密碼輸入的內容不一致",
                        };
                    }
                    else
                    {
                        string newHashPassword = BitConverter.ToString(MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(view.CheckPassword))).Replace("-", null);

                        foreach (CustomerDetail item in passwordInDb)
                        {
                            item.Password = newHashPassword;
                        }

                        db.SaveChanges();
                        result = new
                        {
                            message = "密碼修改完成",
                        };
                    }
                }
                else
                {
                    result = new
                    {
                        message = "輸入的舊密碼不符",
                    };
                }
            }
            else if (userToken["Identity"].ToString() == "store")
            {
                string hashPassword = BitConverter.ToString(MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(view.OriginalPassword)))
                    .Replace("-", null);
                var passwordInDb = db.StoreDetail.Where(s => s.Password == hashPassword).ToList();
                if (passwordInDb.Count > 0)
                {
                    if (view.Password != view.CheckPassword)
                    {
                        result = new
                        {
                            message = "新密碼與再次新密碼輸入的內容不一致",
                        };
                    }
                    else
                    {
                        string newHashPassword = BitConverter.ToString(MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(view.CheckPassword))).Replace("-", null);

                        foreach (StoreDetail item in passwordInDb)
                        {
                            item.Password = newHashPassword;
                        }

                        db.SaveChanges();
                        result = new
                        {
                            message = "密碼修改完成",
                        };
                    }
                }
            }
            else if (userToken["Identity"].ToString() == "staff")
            {
                string hashPassword = BitConverter.ToString(MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(view.OriginalPassword)))
                    .Replace("-", null);
                var passwordInDb = db.StaffDetail.Where(e => e.Password == hashPassword).ToList();
                if (passwordInDb.Count > 0)
                {
                    if (view.Password != view.CheckPassword)
                    {
                        result = new
                        {
                            message = "新密碼與再次新密碼輸入的內容不一致",
                        };
                    }
                    else
                    {
                        string newHashPassword = BitConverter.ToString(MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(view.CheckPassword))).Replace("-", null);

                        foreach (StaffDetail item in passwordInDb)
                        {
                            item.Password = newHashPassword;
                        }

                        db.SaveChanges();
                        result = new
                        {
                            message = "密碼修改完成",
                        };
                    }
                }
            }

            return Ok(result);
        }

        [HttpGet]
        [Route("VerifyUser")]
        [JwtAuthFilter]
        public IHttpActionResult VerifyUser()
        {
            object result = new { };
            var userToken = JwtAuthFilter.GetToken(Request.Headers.Authorization.Parameter);
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

            };
            return Ok(result);
        }

    }
}
