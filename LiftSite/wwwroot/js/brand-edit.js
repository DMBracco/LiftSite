////$('#uploadedImg').on('change', function (e) {
////    e.preventDefault();
////    var files = document.getElementById('uploadedImg').files;
////    if (files.length > 0) {
////        if (window.FormData !== undefined) {
////            var data = new FormData();
////            for (var x = 0; x < files.length; x++) {
////                data.append("file" + x, files[x]);
////            }

////            $.ajax({
////                type: "POST",
////                url: '../AddProductPhoto',
////                contentType: false,
////                processData: false,
////                data: data,
////                success: function (result) {
////                    alert(result);
////                },
////                error: function (xhr, status, p3) {
////                    alert(xhr.responseText);
////                }
////            });
////        } else {
////            alert("Браузер не поддерживает загрузку файлов HTML5!");
////        }
////    }
////});
//нерабочее
function redirectEditProductPartial() {
    $.ajax({
        url: "/Projects/EditProductPartial",
        method: "POST",
        data: {
            productId: productId,
            projectId: projectId
        },
        dataType: "html"
    }).done(function (acceptHtml) {
        $("#productContainer").html(acceptHtml);
    }).fail(function () {
        alert("ошибка, закройте окно(");
    });
}

var btnBrandImg = document.querySelector('#uploadedImg');

async function addBrandImg(fileInput, dataBrand) {

    const formData = new FormData();
    formData.append("file", fileInput.files[0]);
    formData.append("id", dataBrand);

    try {
        const response = await fetch('../AddBrandImg', {
            method: 'POST',
            body: formData
        });
        const result = await response.json();
        console.log('Успех:', JSON.stringify(result));
    } catch (error) {
        console.error('Ошибка:', error);
    }
}

btnBrandImg.addEventListener('change', function () {
    var that = this;
    addBrandImg(that, that.dataset.brand);
});

//const ApiUrl = "/brands/";
//const inputElement = document.querySelector('input[type="file"]');
//debugger

//function process(fieldName, file, metadata, load, error, progress, abort) {
//    const formData = new FormData();
//    formData.append(fieldName, file, file.name);
//    formData.append("brandId", "@Model.Id");
//    const request = new XMLHttpRequest();
//    request.open('POST', api);
//    // Setting computable to false switches the loading indicator to infinite mode
//    request.upload.onprogress = (e) => {
//        progress(e.lengthComputable, e.loaded, e.total);
//    };
//    request.onload = function () {
//        if (request.status >= 200 && request.status < 300) {
//            load(request.responseText);// the load method accepts either a string (id) or an object
//        }
//        else {
//            error('Error during Upload!');
//        }
//    };
//    request.send(formData);
//    //expose an abort method so the request can be cancelled
//    return {
//        abort: () => {
//            // This function is entered if the user has tapped the cancel button
//            request.abort();
//            // Let FilePond know the request has been cancelled
//            abort();
//        }
//    };
//}

//function remove(source, load, error) {
//    const request = new XMLHttpRequest();
//    request.open('DELETE', api);
//    // Setting computable to false switches the loading indicator to infinite mode
//    request.upload.onprogress = (e) => {
//        progress(e.lengthComputable, e.loaded, e.total);
//    };
//    request.onload = function () {
//        if (request.status >= 200 && request.status < 300) {
//            load();// the load method accepts either a string (id) or an object
//        }
//        else {
//            error('Error while removing file!');
//        }
//    }
//    request.send(source);
//}

//const pond = FilePond.create(inputElement, {
//    server: {
//        url: ApiUrl,
//        process: process,
//        load: "/load",
//        fetch: null,
//        remove: remove,
//    },
//    //files: [
//    //    {
//    //        source: "@Model.ImageId.Guid",
//    //        options: {
//    //            type: 'local', // set type to local to indicate an already uploaded file, so it hits the load endpoint
//    //        }
//    //    },
//    //],
//});