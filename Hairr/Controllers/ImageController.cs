using Azure;
using Hairr.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.Extensions.Options;
using Microsoft.SqlServer.Server;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Buffers.Text;
using System.Drawing;
using System;

namespace Hairr.Controllers
{
    public class ImageController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
//'0a950a7270msh2e9b070d9ba062bp1c6142jsn58d3d11b44e8',

//@{
//    ViewData["Title"] = "Index";
//    Layout = "~/Views/Shared/Test.cshtml";
//}


//< div class= "container mt-4" >
//    < div class= "text-center mb-4" >
//        < h2 class= "text-center" style = "font-weight: bold; color: #d4ac0d; text-shadow: 2px 2px #aaa;" > Merhaba, @User.Identity.Name! </ h2 >
//        < p class= "text-muted" > Saç modeli önerisi alabilirsiniz</p>
//    </div>
//</div>



//<div class= "mt-4 d-flex align-items-center" >
//    < input type = "file" id = "imageInput" accept = "image/*" class= "form-control me-2" style = "width: auto;" />
//    < button id = "uploadButton" class= "btn btn-black" > Fotoğraf Yükle </ button >
//</ div >
//< div id = "uploadedImage" style = "max-width:200px;" class= "mt-4" ></ div >
//< style >
//    .btn - black {
//    background - color: black;
//color: white;
//border: none;
//}

//        .btn - black:hover {
//            color: yellow;
//        }

//    .form - control {
//display: inline - block;
//}
//</ style >
//< !--jQuery Kütüphanesi-- >
//< script src = "https://cdnjs.cloudflare.com/ajax/libs/jquery/3.6.0/jquery.min.js" ></ script >

//< script >
//    // jQuery burda
//    $(document).ready(function() {
//        $("#uploadButton").click(function() {
//        const fileInput = $("#imageInput")[0];
//        const file = fileInput.files[0];
//        if (!file)
//        {
//            alert("Lütfen bir fotoğraf seçin!");
//            return;
//        }
//        ""

//            const options = ["101", "201", "301", "401", "501", "601", "701", "801", "901", "1001", "1101", "1201", "1301"];
//        const randomOption = options[Math.floor(Math.random() * options.length)];

//        const data = new FormData();
//        data.append("image_target", file);
//        data.append("hair_type", randomOption);

//            $.ajax({
//        url: "https://hairstyle-changer.p.rapidapi.com/huoshan/facebody/hairstyle",
//                method: "POST",
//                headers:
//            {
//                'x-rapidapi-key': '0a950a7270msh2e9b070d9ba062bp1c6142jsn58d3d11b44e8',
//                    'x-rapidapi-host': 'hairstyle-changer.p.rapidapi.com',
//                },
//                processData: false,
//                contentType: false,
//                data: data,
//                success: function(response) {
//                const base64Image = response.data.image;
//                const imgElement = document.createElement("img");
//                imgElement.src = `data: image / jpeg; base64,${ base64Image}`;
//                imgElement.className = "img-thumbnail";
//                imgElement.style.width = "100%";
//                imgElement.style.height = "auto";
//                imgElement.style.marginTop = "20px";
//                    $("#uploadedImage").html(imgElement);
//            },
//                error: function(xhr, status, error) {
//                alert("Bir hata oluştu. Lütfen tekrar deneyin.");
//                console.error("Error:", status, error);
//            },
//            });
//    });
//});
//</ script >
//@* bu sayfa şifre Se542671% *@