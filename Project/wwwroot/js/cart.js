const showProductsOfCart = () => {
    let cartItems = JSON.parse(sessionStorage.getItem("cartItems")) || [];
    if (cartItems.length > 0) {
        let str = ``;
        $.each(cartItems, (key, val) => {
            str +=
                `<div style="display:flex; align-items:center;padding: 10px 18px;" class="pCart-item">
                    <div style="width: 12%;">
                            <input type="checkbox" class="cb-pcart" style="width: 20px; height: 20px;" value="${parseInt(val.productPrice)}" data-tenhang="${val.productName}" data-mahang="${val.productId}" data-dongia="${val.productPrice}" data-soluong="${val.quantity}" onchange="handleToCalTotalPrice()" />
                    </div>
                    <div style="width: 25%;">
                        <div style="display: flex; align-items:center; gap:0 20px;">
                                <img src="../template/img/${val.productImage}" style="width: 80px; height: 80px;" />
                                <p style="margin: 0; font-size: 16px;font-weight:bold">${val.productName}</p>
                        </div>
                    </div>
                    <div style="width: 20%;">
                            <p style="margin: 0; font-size: 16px;">${formatMoney(parseInt(val.productPrice))}</p>
                    </div>
                    <div style="width: 20%;">
                        <div class="input-number" style="width: 120px">
                                    <input type="number" value="${parseInt(val.quantity)}" class="pcart-quatity" onchange="handleToCalTotalPrice()">
                            <span class="qty-up">+</span>
                            <span class="qty-down">-</span>
                        </div>
                    </div>
                    <div style="width: 15%;">
                            <p style="margin: 0; font-size: 16px;" class="item-total-price">${parseInt(val.quantity) * parseInt(val.productPrice)}</p>
                    </div>
                    <div style="flex:1;">
                        <p
                            style="margin: 0; text-align:right; font-size:16px; width:80px;cursor:pointer;"
                            onmouseover="this.style.color='#ee4d2d';" onmouseout="this.style.color='#333333';"
                            onclick="deleteProductItemCart('${val.productId}')">Xóa</p>
                        </div>
                 </div>`
        })
        $("#showPcart").html(str);
        handleToCalTotalPrice();
        $("#has-show-checkout").css("display", "block");
        $("#empty-cart").css("display", "none");
    } else {
        $("#has-show-checkout").css("display", "none");
        $("#empty-cart").css("display", "block");
    }
}

const handleToCalTotalPrice = () => {
    let total = 0;
    // Lặp qua từng hàng trong bảng giỏ hàng để tính lại tổng giá tiền
    $('.pCart-item').each(function () {
        let price = parseInt($(this).find('.cb-pcart').val());
        let quantity = parseInt($(this).find('.pcart-quatity').val());

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
        let maHang = $(this).data("mahang");
        let soLuong = $(this).data("soluong");
        let donGia = $(this).val();
        let pd = {
            tenHang,
            donGia,
            maHang,
            soLuong,
        }
        console.log(pd);
        lst.push(pd);
    })
    sessionStorage.setItem('selectedProducts', JSON.stringify(lst));
    sessionStorage.setItem('totalPrice', $('.cart-total-price').text());
    window.location.href = "https://localhost:7019/checkout";

}

const deleteProductItemCart = (mahang) => {
    let cartItems = JSON.parse(sessionStorage.getItem("cartItems")) || [];
    cartItems = cartItems.filter(item => item.maHang !== mahang);
    sessionStorage.setItem("cartItems", JSON.stringify(cartItems));
    showProductsOfCart();
}

$(document).ready(function () {
    showProductsOfCart();
})