$(function () {
    GetTableEquipment();
});


$('#btnAdd').on('click', function (e) {
    $("#workingModalTitle").text("Добавление оборудования");
    document.getElementById("btnSave").classList.add("btnAdd");
    GetModalEquipment();
    $("#btnSave").text("Добавить");
    $("#workingModal").modal("show");
});

$('#getTable').on('click', function (e) {
    let targetItem = e.target;
    if (targetItem.closest('.btnUpdate')) { ///Update
        let typeEqId = targetItem.closest('.btnUpdate').dataset.id
        $("#workingModalTitle").text("Изменение оборудования");
        document.getElementById("btnSave").classList.add("btnAdd");
        GetModalEquipment(typeEqId);
        $("#btnSave").text("Сохранить");
        $("#workingModal").modal("show");
    }

    if (targetItem.closest('.btnDelete')) {
        $("#workingModalTitle").text("Удаление оборудования");
        $("#getModal").text("Вы действительно хотите удалить запись?");

        const div = document.createElement('div');
        div.className = 'row';
        div.innerHTML = '<input id="Id" name="Id" value="' + targetItem.dataset.id + '" hidden="">'
        document.getElementById('getModal').appendChild(div);

        $("#btnSave").text("Удалить");
        document.getElementById("btnSave").classList.add("btnDelete");
        $("#workingModal").modal("show");
    }
});

$('#btnSave').on('click', function (e) {
    let targetItem = e.target;
    if (targetItem.classList.contains('btnAdd')) {

        let equipment = {}
        equipment.Id = $('#Id').val();
        equipment.Name = $('#Name').val();
        equipment.BrandId = $('#BrandId').val();
        equipment.Model = $('#Model').val();
        equipment.Description = $('#Description').val();
        //equipment.Images = $('#Images').get(0).files;
        equipment.TypeId = $('#TypeId').val();
        var data = {
            "equipment": equipment
        };
        images = $('#Images').get(0).files;

        AddEquipment(data, images)
        targetItem.classList.remove('btnAdd');
    }

    if (targetItem.classList.contains('btnDelete')) {
        let typeEqId = $('#Id').val();
        AddEquipment(typeEqId)
        targetItem.classList.remove('btnDelete');
    }
});

function DeleteEquipment(id) {
    $.ajax({
        url: '/Equipment/Delete',
        type: "DELETE",
        cache: false,
        async: true,
        dataType: "json",
        data: { "id": id }
    })
        .done(function (result) {
            $("#workingModal").modal('hide');
            GetTableEquipment();
        }).fail(function (xhr) {
            console.log('error : ' + xhr.status + ' - '
                + xhr.statusText + ' - ' + xhr.responseText);
        });
}

function AddEquipment(data, image) {
    const formData = new FormData();
    formData.append("equipment", data);
    formData.append("img", image);

    $.ajax({
        url: '/Equipment/Add',
        type: 'POST',
        cache: false,
        async: true,
        //dataType: "json",
        data: formData
    })
        .done(function (result) {
            $("#workingModal").modal('hide');
            GetTableEquipment();
        }).fail(function (xhr) {
            console.log('error : ' + xhr.status + ' - '
                + xhr.statusText + ' - ' + xhr.responseText);
        });
}

function GetTableEquipment() {
    $.ajax({
        url: '/Equipment/TableEquipmentPartial',
        type: 'GET',
        cache: false,
        async: true,
        dataType: "html"
    })
        .done(function (result) {
            $('#getTable').html(result);
        }).fail(function (xhr) {
            console.log('error : ' + xhr.status + ' - '
                + xhr.statusText + ' - ' + xhr.responseText);
        });
}

function GetModalEquipment(id) {
    $.ajax({
        url: '/Equipment/ModalEquipmentPartial',
        type: 'POST',
        cache: false,
        async: true,
        dataType: "html",
        data: { "id": id }
    })
        .done(function (result) {
            $('#getModal').html(result);
        }).fail(function (xhr) {
            console.log('error : ' + xhr.status + ' - '
                + xhr.statusText + ' - ' + xhr.responseText);
        });
}