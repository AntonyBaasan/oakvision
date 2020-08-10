using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Emgu.CV;
using Emgu.CV.OCR;
using Emgu.CV.Structure;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        // GET: api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            var text = this.RecognizeText();
            return new string[] { "value1", "value2" };
        }

        private string RecognizeText()
        {
            using (var image = new Image<Bgr, byte>(Path.GetFullPath("test_image.png")))
            {
                using (var tesseractOcrProvider = new Tesseract("", "eng", OcrEngineMode.Default))
                {
                    tesseractOcrProvider.SetImage(image);
                    var result = tesseractOcrProvider.Recognize();
                    if (result == 0) {
                        return tesseractOcrProvider.GetUTF8Text().Trim();
		    
		            }
                    return $"Failed. Result={result}";
                }
            }
        }


        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
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
