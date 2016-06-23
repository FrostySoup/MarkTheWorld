using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.PhotoUpload
{
    public class CustomMultipartFormDataStreamProvider : MultipartFormDataStreamProvider
    {
        public CustomMultipartFormDataStreamProvider(string path) : base(path)
        {
        }

        public override string GetLocalFileName(System.Net.Http.Headers.HttpContentHeaders headers)
        {

            Random rnd = new Random();
            int card = rnd.Next(100);
            var test = "OriginalPhoto" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + "_" + card;
            var name = !string.IsNullOrWhiteSpace(headers.ContentDisposition.FileName)
                ? test + headers.ContentDisposition.FileName
                : "NoName";
            return name.Replace("\"", string.Empty);
            //this is here because Chrome submits files in quotation marks which get treated as part of the filename and get escaped
        }
    }
}
