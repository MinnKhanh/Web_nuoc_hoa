// Thực thi các hàm
var Id;
$(document).ready(function() {
    getActor();
    $('body').on('click','.btn_edit' , function() {
        var id = $(this).data('id'); // lấy ra mã
        Id = id;
        GetActorById(id);
    });
})

// Thêm dữ liệu
function SaveActor() {

    var url = "/api/SP";
    var objActor = {};
    objActor.TenSP = $("#TenSP").val();
    objActor.DonGia = $("#GiaBan").val();
    objActor.SoLuong = $("#SoLuong").val();
    objActor.NongDo = $("#NongDo").val();
    objActor.NhomHuong = $("#NhomHuong").val();
    objActor.MoTaSanPham = $("#MoTaSanPham").val();
    objActor.NamPhatHanhSanPham = $("#NamPhatHanh").val();
    objActor.AnhSanPham = $("#AnhSanPham").get(0).files[0].name
    objActor.MaNhanHieu = $("#DropDwn1").val();
    objActor.MaLoai = $("#DropDwn").val();
    objActor.DungTich = $("#DungTich").val();
    if (objActor.TenSP == '') {
        alert("Yêu cầu nhập tên cho sản phẩm");
        return;
    }
    if (objActor.DonGia == '' || parseFloat(objActor.DonGia) < 0) {
        alert("Yêu cầu nhập đơn giá và đơn giá phải lớn hơn 0");
        return;
    }
    if (objActor.SoLuong == '' || parseFloat(objActor.SoLuong) < 0) {
        alert("Yêu cầu nhập số lượng và số lượng phải lớn hơn 0");
        return;
    }
    if (objActor.MaLoai == '') {
        alert("Yêu cầu chọn loại nước hoa");
        return;
    }
    if (objActor.MaNhanHieu == '') {
        alert("yêu cầu chọn thương hiệu");
        return;
    }
    if (parseFloat(objActor.NamPhatHanhSanPham) < 2000) {
        alert("Yêu cầu năm phát hành sản phẩm phải lớn hơn 2000");
        return;
    }
    if (objActor.DungTich == '' || parseFloat(objActor.DungTich) < 0) {
        alert("Yêu cầu nhập dung tích và dung tích phải lớn hơn 0");
        return;
    }
    if (objActor) {
        $.ajax({
            url: url,
            contentType: "application/json; charset=utf-8",
            dataType: "Json",
            data: JSON.stringify(objActor),
            type: "Post",
            success: function (result) {
                Clear();
                alert("Thêm thành công");
                getActor();
            },
            error:function(msg) {
               alert("Lỗi khi thêm");
            }
        });
    }
}

// Dọn sạch Input sau khi thêm
function Clear() {
    $("#TenSP").val('');
    $("#GiaBan").val('');
    $("#SoLuong").val('');
    $("#NongDo").val('');
    $("#NhomHuong").val('');
    $("#MoTaSanPham").val('');
    $("#NamPhatHanh").val('');
    $("#AnhSanPham").val('');
    $("#MaNhanHieu").val('');
    $("#MaLoai").val('');
    $("#DungTich").val('');
}


// lấy dữ liệu ra
function getActor() {
    var url = "/api/SP";
    $.ajax({
        url: url,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        type: "Get",
        success: function (result) {
            let table = '';
            for (let i = 0; i < result.length; i++) {
                    table = table + '<tr>';
                    table = table + '<td>' + result[i].TenSP + '</td>';
                    table = table + '<td>' + result[i].DonGia + '</td>';
                    table = table + '<td>' + result[i].MoTaSanPham + '</td>';
                    table = table + '<td>' + result[i].DungTich + '</td>';
                    table = table + '<td>' + `<img src="../../Resource/SanPham/${result[i].AnhSanPham}" style="display:block;width:130px;"/>` + '</td>';
                    table = table + '<td> <button type="button" class="btn btn-primary" onclick="DeleteActor(\'' + result[i].MaSP + '\')">Xóa</button> </td>';
                table = table + '<td> <button type="button" class="btn btn-primary btn_edit" data-id="' + result[i].MaSP + '" data-toggle="modal" data-target="#ModalUpdate" ">Sửa</button></td>';
                    table = table + '</tr>';
                }
            document.getElementById('AllActor').innerHTML = table;
            //let options = {
            //    numberPerPage: 2, //Cantidad de datos por pagina
            //    goBar: false, //Barra donde puedes digitar el numero de la pagina al que quiere ir
            //    pageCounter: true, //Contador de paginas, en cual estas, de cuantas paginas
            //};

            //let filterOptions = {
            //    el: '#searchBox' //Caja de texto para filtrar, puede ser una clase o un ID
            //};

            //paginate.init('.myTable', options, filterOptions);
        },
        error: function (msg) {
            alert(msg);
        }
    });
}

// Xóa 
function DeleteActor(MaSP) {
    var url = "/api/SP/";
    $.ajax({
            url: url + MaSP,
            type: "Delete",
            contentType: "application/json ; charset=utf-8",
            dataType: "json",
            success: function (result) {
                    Clear();
                    alert("Xóa thành công");
                    getActor();
                
            },
            error: function (msg) {
                alert("Xóa thất bại");
            }
        });
}

// lấy sản phẩm theo mã sản phẩm

function GetActorById(MaSP) {
    let url = "/api/SP/" + MaSP;
    $.ajax({
        url: url,
        type: "Get",
        contentType: 'json',
        dataType: 'json',
        data:{MaSP:MaSP},
        success: function(result) {
            // lấy dữ liệu của mã sản phẩm gán vào form
                $("#TenSP_Update").val(result.TenSP);
                $("#GiaBan_Update").val(result.DonGia);
                $("#SoLuong_Update").val(result.SoLuong);
                $("#NongDo_Update").val(result.NongDo);
                $("#NhomHuong_Update").val(result.NhomHuong);
                $("#MoTaSanPham_Update").val(result.MoTaSanPham);
                $("#NamPhatHanh_Update").val(result.NamPhatHanhSanPham);
                $("#AnhSanPham").val(result.AnhSanPham);
                $("#DropDwn1_Update").val(result.MaNhanHieu);
                $("#DropDwn_Update").val(result.MaLoai);
                $("#DungTich_Update").val(result.DungTich);
                // sau khi gán xong thì show ra cái modal
                $("#ModalUpdate").modal.show();
        },
        error: function(msg) {
            alert("Không lấy được mã sản phẩm");
        }
    });
}

// sửa
function UpdateActor() {
    let url = "/UpdateSP";
    var objActor = {};
    objActor.MaSP = Id;
    objActor.TenSP = $("#TenSP_Update").val();
    objActor.DonGia = $("#GiaBan_Update").val();
    objActor.SoLuong = $("#SoLuong_Update").val();
    objActor.NongDo = $("#NongDo_Update").val();
    objActor.NhomHuong = $("#NhomHuong_Update").val();
    objActor.MoTaSanPham = $("#MoTaSanPham_Update").val();
    objActor.NamPhatHanhSanPham = $("#NamPhatHanh_Update").val();
    objActor.AnhSanPham = $("#AnhSanPham_Update").get(0).files[0].name
    objActor.MaNhanHieu = $("#DropDwn1_Update").val();
    objActor.MaLoai = $("#DropDwn_Update").val();
    objActor.DungTich = $("#DungTich_Update").val();
    if (objActor.TenSP == '') {
        alert("Yêu cầu nhập tên cho sản phẩm");
        return;
    }
    if (objActor.DonGia == '' || parseFloat(objActor.DonGia) < 0) {
        alert("Yêu cầu nhập đơn giá và đơn giá phải lớn hơn 0");
        return;
    }
    if (objActor.SoLuong == '' || parseFloat(objActor.SoLuong) < 0) {
        alert("Yêu cầu nhập số lượng và số lượng phải lớn hơn 0");
        return;
    }
    if (objActor.MaLoai == '') {
        alert("Yêu cầu chọn loại nước hoa");
        return;
    }
    if (objActor.MaNhanHieu == '') {
        alert("yêu cầu chọn thương hiệu");
        return;
    }
    if (parseFloat(objActor.NamPhatHanhSanPham) < 2000) {
        alert("Yêu cầu năm phát hành sản phẩm phải lớn hơn 2000");
        return;
    }
    if (objActor.DungTich == '' || parseFloat(objActor.DungTich) < 0) {
        alert("Yêu cầu nhập dung tích và dung tích phải lớn hơn 0");
        return;
    }
    if (objActor) {
        $.ajax({
            url: url,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify(objActor),
            type: "Put",
            success: function (result) {
                alert("Sửa thành công");
                getActor();
            },
            error: function (msg) {
                alert("Sửa không thành công");
            }
        });
    }
}
