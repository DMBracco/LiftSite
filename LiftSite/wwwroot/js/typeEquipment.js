$(function () {
    GetTableTypeEqu();
    GetModalTypeEqu();
});


$('#btnAdd').on('click', function (e) {
    $("#workingModalTitle").text("Добавление типа оборудования");
    document.getElementById("btnSave").classList.add("btnAdd");
    GetModalTypeEqu();
    $("#btnSave").text("Добавить");
    $("#workingModal").modal("show");
});

$('#getTable').on('click', function (e) {
    let targetItem = e.target;
    if (targetItem.closest('.btnUpdate')) { ///Update
        let typeEqId = targetItem.closest('.btnUpdate').dataset.id
        $("#workingModalTitle").text("Изменение типа оборудования");
        document.getElementById("btnSave").classList.add("btnAdd");
        GetModalTypeEqu(typeEqId);
        $("#btnSave").text("Сохранить");
        $("#workingModal").modal("show");
    }

    if (targetItem.closest('.btnDelete')) {
        $("#workingModalTitle").text("Удаление типа оборудования");
        $("#getModal").text("Вы действительно хотите удалить запись?");

        const div = document.createElement('div');
        div.className = 'row';
        div.innerHTML = '<input id="Id" name="Id" value="' + targetItem.dataset.id +'" hidden="">'
        document.getElementById('getModal').appendChild(div);

        $("#btnSave").text("Удалить");
        document.getElementById("btnSave").classList.add("btnDelete");
        $("#workingModal").modal("show");
    }
});

$('#btnSave').on('click', function (e) {
    let targetItem = e.target;
    if (targetItem.classList.contains('btnAdd')) {
        let typeEq = {}
        typeEq.Id = $('#Id').val();
        typeEq.Name = $('#Name').val();
        var data = {
            "typeEq": typeEq
        };
        AddTypeEqu(data)
        targetItem.classList.remove('btnAdd');
    }

    if (targetItem.classList.contains('btnDelete')) {
        let typeEqId = $('#Id').val();
        DeleteTypeEqu(typeEqId)
        targetItem.classList.remove('btnDelete');
    }
});
//$('#btnSave.btnAdd').on('click', function (e) {
//    let typeEq = {}
//    typeEq.Id = $('#Id').val();
//    typeEq.Name = $('#Name').val();
//    var data = {
//        "typeEq": typeEq
//    };
//    AddTypeEqu(data)
//});

//$('#btnSave.btnDelete').on('click', function (e) {
//    let typeEqId = $('#Id').val();
//    DeleteTypeEqu(typeEqId)
//});

function DeleteTypeEqu(id) {
    $.ajax({
        url: '/TypeEquipment/Delete',
        type: "DELETE",
        cache: false,
        async: true,
        dataType: "json",
        data: { "id": id }
    })
        .done(function (result) {
            $("#workingModal").modal('hide');
            GetTableTypeEqu();
        }).fail(function (xhr) {
            console.log('error : ' + xhr.status + ' - '
                + xhr.statusText + ' - ' + xhr.responseText);
        });
}

function AddTypeEqu(data) {
    $.ajax({
        url: '/TypeEquipment/Add',
        type: 'POST',
        cache: false,
        async: true,
        dataType: "json",
        data: data
    })
        .done(function (result) {
            $("#workingModal").modal('hide');
            GetTableTypeEqu();
        }).fail(function (xhr) {
            console.log('error : ' + xhr.status + ' - '
                + xhr.statusText + ' - ' + xhr.responseText);
        });
}

function GetTableTypeEqu() {
    $.ajax({
        url: '/TypeEquipment/TableTypeEquPartial',
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

function GetModalTypeEqu(id) {
    $.ajax({
        url: '/TypeEquipment/ModalTypeEquPartial',
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