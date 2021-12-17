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