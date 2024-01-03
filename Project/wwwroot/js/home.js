
/**
 * Add product to cart 
 */
const handleAddProductToCart = (event, parentId) => {
    let parent = $(event.target).parents(`#product-detail-${parentId}`)[0];
    let quantity = $(parent).find("input[type='number']").val();
    let productId = $(parent).attr("data-id");
    let productImage = $(parent).attr("data-image");
    let productPrice = $(parent).attr("data-price");
    let productName = $(parent).attr("data-name");

    let cartItem = {
        productId,
        quantity,
        productImage,
        productPrice,
        productName
    };
    let cartItems = JSON.parse(sessionStorage.getItem('cartItems')) || [];
    cartItems.push(cartItem);
    sessionStorage.setItem('cartItems', JSON.stringify(cartItems));
    alert("Thêm vào giỏ hàng thành công");
};

/**
 * Show all related products
 */
const showProductsRelated = () => {
    let location = window.location.href;
    let productId = location.split("/").pop();
    let url = `https://localhost:7019/api/productapi/related/${productId}`;
    $.ajax({
        type: "GET",
        url: url,
        dataType: "json",
        success: (data) => {
            let str = `<div class="col-md-12">
									<div class="section-title text-center">
										<h3 class="title">Related Products</h3>
									</div>
								</div>`
            $.each(data, (key, val) => {
                str += `<div class="col-md-3 col-xs-6">
								<div class="product" id="product-detail-${val.maHang}" data-id='${val.maHang}' data-image='${val.anhDaiDien}' data-price='${val.donGiaBan}' data-name='${val.tenHang}'>
									<div class="product-img">
											<img src="../template/img/${val.anhDaiDien}" alt="">
										<div class="product-label">
											<span class="sale">-30%</span>
										</div>
									</div>
									<div class="product-body">
										<h3 class="product-name"><a href="product/${val.maHang}">${val.tenHang}</a></h3>
												<h4 class="product-price">${formatMoney(0.7 * val.donGiaBan)} <del class="product-old-price">${formatMoney(val.donGiaBan)}</del></h4>
										<div class="product-rating">
										</div>
										<div class="product-btns">
											<button class="add-to-wishlist"><i class="fa fa-heart-o"></i><span class="tooltipp">add to wishlist</span></button>
											<button class="add-to-compare"><i class="fa fa-exchange"></i><span class="tooltipp">add to compare</span></button>
											<button class="quick-view"><i class="fa fa-eye"></i><span class="tooltipp">quick view</span></button>
										</div>
									</div>
									<div class="add-to-cart">
										<button class="add-to-cart-btn" onclick="handleAddProductToCart(event, '${val.MaHang}')><i class="fa fa-shopping-cart"></i> add to cart</button>
									</div>
								</div>
							</div>`

            })
            $("#productRelated").html(str);
        }
    })
}


$(document).ready(function () {
    showProductsRelated();

});
