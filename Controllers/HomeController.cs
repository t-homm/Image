using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Image.Models;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace Image.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            string connectionString = "BlobEndpoint=https://thonmargdiag.blob.core.windows.net/;QueueEndpoint=https://thonmargdiag.queue.core.windows.net/;FileEndpoint=https://thonmargdiag.file.core.windows.net/;TableEndpoint=https://thonmargdiag.table.core.windows.net/;SharedAccessSignature=sv=2019-12-12&ss=bfqt&srt=co&sp=rwdlacupx&se=2021-01-05T14:47:37Z&st=2021-01-05T06:47:37Z&spr=https,http&sig=nGR2PRHxmnNyW8pNQx%2BH4PI2p2KBh0Qbas9%2Bi%2Fvgko4%3D";
            string containerName = "sample";
            string blobUrl = "https://thonmargdiag.blob.core.windows.net/";
            string sas = "?sv=2019-12-12&ss=bfqt&srt=co&sp=rwdlacupx&se=2021-01-05T14:47:37Z&st=2021-01-05T06:47:37Z&spr=https,http&sig=nGR2PRHxmnNyW8pNQx%2BH4PI2p2KBh0Qbas9%2Bi%2Fvgko4%3D";


            BlobContainerClient container = new BlobContainerClient(connectionString, containerName);

            List<string> list = new List<string>();

            foreach (BlobItem blob in container.GetBlobs())
            {
                // Console.WriteLine(blob.Name);
                // list.Add(blob.Name);
                string name = blobUrl+containerName+"/"+blob.Name+sas;
                list.Add(name);
            }
            ViewData["list"] = list;

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
