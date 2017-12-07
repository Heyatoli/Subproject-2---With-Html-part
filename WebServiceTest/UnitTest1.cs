using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xunit;

namespace WebServiceTest
{
    public class UnitTest1
    {
        private const string userAPI = "http://localhost:5001/api/users";
        private const string postAPI = "http://localhost:5001/api/posts";

        [Fact]
        public void ApiPostsGetCommentsWithInvalidPostId()
        {
            var (post, statuscode) = GetObject($"{postAPI}/comments/-1");
            Assert.Equal(HttpStatusCode.NotFound, statuscode);
        }

        [Fact]
        public void ApiUserPostMarkingWithValidValues()
        {
            var marking = new
            {
                postid = 19,
                userid = 1,
                note = "something"
            };

            var (post, statuscode) = PostData($"{userAPI}/markings/", marking);
            Assert.Equal(HttpStatusCode.Created, statuscode);
            DeleteData($"{userAPI}/markings/1/19");
        }

        [Fact]
        public void ApiUsersUpdateMarkingsWithInvalidId()
        {
            var marking = new
            {
                postid = 19,
                userid = -1,
                note = "something"
            };
            var statuscode = PutData($"{userAPI}/markings/-1", marking);
            Assert.Equal(HttpStatusCode.NotFound, statuscode);
        }



        (JObject, HttpStatusCode) PostData(string url, object content)
        {
            var client = new HttpClient();
            var requestContent = new StringContent(
                JsonConvert.SerializeObject(content),
                Encoding.UTF8,
                "application/json");
            var response = client.PostAsync(url, requestContent).Result;
            var data = response.Content.ReadAsStringAsync().Result;
            return ((JObject)JsonConvert.DeserializeObject(data), response.StatusCode);
        }

        (JObject, HttpStatusCode) GetObject(string url)
        {
            var client = new HttpClient();
            var response = client.GetAsync(url).Result;
            var data = response.Content.ReadAsStringAsync().Result;
            return ((JObject)JsonConvert.DeserializeObject(data), response.StatusCode);
        }

        HttpStatusCode DeleteData(string url)
        {
            var client = new HttpClient();
            var response = client.DeleteAsync(url).Result;
            return response.StatusCode;
        }

        HttpStatusCode PutData(string url, object content)
        {
            var client = new HttpClient();
            var response = client.PutAsync(
                url,
                new StringContent(
                    JsonConvert.SerializeObject(content),
                    Encoding.UTF8,
                    "application/json")).Result;
            return response.StatusCode;
        }
    }
}
