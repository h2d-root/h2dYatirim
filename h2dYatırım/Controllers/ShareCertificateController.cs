using h2dYatırım.DataAccess;
using h2dYatırım.Entities;
using h2dYatırım.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace h2dYatırım.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShareCertificateController : ControllerBase
    {
        ShareCertificateDal _shareDal = new ShareCertificateDal();

        [HttpGet]
        public IActionResult Get()
        {
            List<List<string>> list = new List<List<string>>();
            for (int i = 1; i < 6; i++)
            {
                var item = ShareCertificatesService.Hisse(i.ToString());
                ShareCertificate entity = new ShareCertificate();
                entity.Code = item[0];
                entity.DailyPrice = Convert.ToDouble(item[2]);
                entity.Difference = item[1];
                entity.BasePrice = Convert.ToDouble(item[3]);
                entity.CeilingPrice = Convert.ToDouble(item[4]);
                _shareDal.Add(entity);
                Console.WriteLine(item[0] + " eklendi!");
            }
            return Ok(list);
        }


        [HttpGet("Refresh")]
        public IActionResult Refresh()
        {
            

            for (int i = 1; i < 6; i++)
            {
                var result = _shareDal.Get(x=>x.Id == i);
                var item = ShareCertificatesService.Hisse(i.ToString());
                result.DailyPrice = Convert.ToDouble(item[1]);
                result.Difference = item[0];
                result.BasePrice = Convert.ToDouble(item[2]);
                result.CeilingPrice = Convert.ToDouble(item[3]);
                _shareDal.Update(result);
            }
            return Ok("işlem tamamlandı");
        }
        
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            //var client = new RestClient("https://api.collectapi.com/economy/hisseSenedi");
            //var request = new RestRequest(Method.GET);
            //request.AddHeader("authorization", "apikey 3JUCcNJqG6lJaVxsn5SXbB:0bKwqw7x95LKAZuFtlFtGi");
            //request.AddHeader("content-type", "application/json");
            //IRestResponse response = client.Execute(request);
            var result = _shareDal.GetAll().OrderBy(s => s.Id);
            //var result = response.Result;
            return Ok(result);
        }
        [HttpGet("GetShareCertificateCODE")]
        public IActionResult GetShareCertificateCode(string code)
        {
            var result = _shareDal.Get(x => x.Code == code.ToUpper());
            return Ok(result);
        }
        [HttpGet("GetShareCertificateID")]
        public IActionResult GetShareCertificateId(int id)
        {
            var result = _shareDal.Get(x => x.Id == id);
            return Ok(result);
        }
        
    }
}
