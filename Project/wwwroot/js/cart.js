const showProductCart = () => {
    let url = `https://localhost:7019/api/cart`;
    $.ajax({
        type: "GET",
        url: url,
        dataType: 'json',
        success: (data) => {
            let str = ``;
            $.each(data, (key, val) => {
                str += `<div style="display:flex; align-items:center;padding: 10px 18px;" class="pCart-item">
                <div style="width: 12%;">
                        <input type="checkbox" class="cb-pcart" style="width: 20px; height: 20px;" value="${val.donGiaBan}" data-tenhang="${val.tenHang}" onchange="handleToCalTotalPrice()" />
                </div>
                <div style="width: 25%;">
                    <div style="display: flex; align-items:center; gap:0 20px;">
                            <img src="../imageBTL/${val.anh}" style="width: 80px; height: 80px;" />
                            <p style="margin: 0; font-size: 16px;font-weight:bold">${val.tenHang}</p>
                    </div>
                </div>
                <div style="width: 20%;">
                        <p style="margin: 0; font-size: 16px;">${formatMoney(val.donGiaBan)}</p>
                </div>
                <div style="width: 20%;">
                    <div class="input-number" style="width: 120px">
                                <input type="number" value="${val.soLuong}" class="pcart-quatity" onchange="handleToCalTotalPrice()">
                        <span class="qty-up">+</span>
                        <span class="qty-down">-</span>
                    </div>
                </div>
                <div style="width: 15%;">
                        <p style="margin: 0; font-size: 16px;" class="item-total-price">${val.soLuong * val.donGiaBan}</p>
                </div>
                <div style="flex:1;">
                    <p
                        style="margin: 0; text-align:right; font-size:16px; width:80px;cursor:pointer;"
                        onmouseover="this.style.color='#ee4d2d';" onmouseout="this.style.color='#333333';"
                        onclick="deleteProductItemCart('${val.maHang}')">Xóa</p>
                    </div>

            </div>`
            })
            $("#showPcart").html(str);
            handleToCalTotalPrice();
        },
        error: () => {
            alert("ERROR");
        }
    })
}

const handleToCalTotalPrice = () => {
    var total = 0;
    // Lặp qua từng hàng trong bảng giỏ hàng để tính lại tổng giá tiền
    $('.pCart-item').each(function () {
        var price = parseInt($(this).find('.cb-pcart').val());
        var quantity = parseInt($(this).find('.pcart-quatity').val());

        if ($(this).find('.cb-pcart').is(':checked')) {
            total += price * quantity;
        }

        $(this).find('.item-total-price').text(formatMoney(price * quantity));
    });

    // Cập nhật tổng giá tiền
    $('.cart-total-price').text(formatMoney(total));

}

const addProductToCheckout = () => {
    let lst = [];
    $(".cb-pcart:checked").each(function () {
        let tenHang = $(this).data("tenhang");
        let donGia = $(this).val();
        let pd = {
            tenHang,
            donGia
        }
        lst.push(pd);
    })
    sessionStorage.setItem('selectedProducts', JSON.stringify(lst));
    sessionStorage.setItem('totalPrice', $('.cart-total-price').text());
    window.location.href = "https://localhost:7019/checkout";

}

const deleteProductItemCart = (mahang) => {
    let url = `https://localhost:7019/api/cart/${mahang}`;
    $.ajax({
        type: 'GET',
        url: url,
        dataType: 'json',
        success: () => {
            alert("Xóa sản phẩm thành công");
            showProductCart();

        },
        error: () => {
            alert("Xóa sản phẩm không thành công");

        }
    })
}

$(document).ready(function () {
    showProductCart();

})