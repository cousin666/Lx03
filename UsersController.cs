using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        [HttpGet]
        public ActionResult Get()
        {
            return Json(DAL.UserInfo.Instance.GetCount());
        }
        [HttpPut("{username}")]
        public ActionResult Put([FromBody]Model.UserInfo users)
        {
            try
            {
                var n = DAL.UserInfo.Instance.Update(users);
                if(n !=0)
                    return Json(Result.Ok("修改成功"));
                else
                    return Json(Result.Err("用户名不存在"));
            }
            catch (Exception ex)
            {
                
                if (ex.Message.ToLower().Contains("null"))
                    return Json(Result.Err("用户名，密码，身份不能为空"));
                else
                    return Json(Result.Err(ex.Message));

            }
        }
        [HttpPost("page")]
        public ActionResult getPage([FromBody]Model.Page page)
        {
            var result = DAL.UserInfo.Instance.GetPage(page);
            if(result.Count()==0)
                return Json(Result.Err("返回记录数为0"));
            else
                return Json(Result.Ok(result));



        }
        [HttpDelete("username")]
        public ActionResult Delete(string username)
        {
            try
            {
                var n = DAL.UserInfo.Instance.Delete(username);
                if (n != 0)
                    return Json(Result.Ok("删除成功"));
                else
                    return Json(Result.Err("用户名不存在"));
            }
            catch (Exception ex)
            {

                if (ex.Message.ToLower().Contains("foreign"))
                    return Json(Result.Err("发布了作品或活动的用户不能删除"));
                else
                    return Json(Result.Err(ex.Message));

            }
        }
    }
}
