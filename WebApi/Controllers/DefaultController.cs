using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FaApi.Comon;
using FaApi.Model;
using IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Models;
using MongoDB.Driver;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FaApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DefaultController : ControllerBase
    {
        IConfiguration config;
        IUserRepository user;
        public DefaultController(IConfiguration _config,IUserRepository _user){
            config=_config;
            user=_user;
        }
        
        [HttpGet]
        public string Index()
        {
            User u=new User();
            u.UserName="asfda";
            //2.要修改的字段内容
            // List<User> userList=new List<User>();
            
            // userList.Add(new User(){UserName="asdf",Password="sfasdf"});
            // MongodbHost host=new MongodbHost();
            // host.Connection="mongodb://fa:fa@45.32.134.176/fa";
            // host.DataBase="fa";
            // host.Table="user";

            // //3.批量修改
            // var kk = TMongodbHelper<User>.InsertMany(host, userList);
            // return kk.ToString();
            
            //根据条件查询集合
            // var time = DateTime.Now;
            // var list = new List<FilterDefinition<User>>();
            // var filter = Builders<User>.Filter.Empty;
            // var field = new[] { "UserName", "Password"};
            // var res = TMongodbHelper<User>.FindList(filter,field);
            // return res.Count().ToString();
            return user.Test();
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            JArray array = new JArray();
            array.Add("Manual text");
            array.Add(new DateTime(2000, 5, 23));

            JObject o = new JObject();
            o["MyArray"] = array;

            string json = o.ToString();

            return json;
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        [HttpPost,HttpGet]
        public string Error()
        {
            return "error:";
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
