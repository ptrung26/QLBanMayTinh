/**
 * Handle toogle filter param when checkbox change
 * @param {any} type
 * @param {any} input
 */
const setFilterProductsByInput = (input) => {
    const priceMin = $("#price-min").val().trim() || 0;
    const priceMax = $("#price-max").val().trim() || 0;
    const queryString = window.location.search
    const urlParams = new URLSearchParams(queryString);
    if (parseInt(priceMax) > 0) {
        urlParams.set("priceMin", priceMin);
        urlParams.set("priceMax", priceMax);
    } else {
        urlParams.delete("priceMin");
        urlParams.delete("priceMax");
    }
    const newUrl = window.location.pathname + '?' + urlParams.toString();
    window.location.href = newUrl;
}


/**
 * Get all top product sell 
 * */
const getTopProductSell = () => {
    let url = "https://localhost:7019/api/productapi/topsellproducts";
    let id = "#showTopSellCategory"
    $.ajax({
        type: "GET",
        url: url,
        dataType: "json",
        success: (data) => {
            let str = `<h3 class="aside-title">Top selling</h3>`;
            $.each(data, (key, value) => {
                str += `<div class="product-widget">
								<div class="product-img">
									<img src="../imageBTL/${value.anhDaiDien}" alt="">
								</div>
								<div class="product-body">
										<h3 class="product-name"><a href="product/${value.maHang}">${value.tenHang}</a></h3 >
												<h4 class="product-price">${formatMoney(0.7 * value.donGiaBan)} <del class="product-old-price">${formatMoney(value.donGiaBan)}</del></h4>
								</div>
							</div>`
            });
            $(`${id}`).html(str);

            $('.products-slick').each(function () {
                var $this = $(this),
                    $nav = $this.attr('data-nav');

                $this.slick({
                    slidesToShow: 4,
                    slidesToScroll: 1,
                    autoplay: true,
                    infinite: true,
                    speed: 300,
                    dots: false,
                    arrows: true,
                    appendArrows: $nav ? $nav : false,
                    responsive: [{
                        breakpoint: 991,
                        settings: {
                            slidesToShow: 2,
                            slidesToScroll: 1,
                        }
                    },
                    {
                        breakpoint: 480,
                        settings: {
                            slidesToShow: 1,
                            slidesToScroll: 1,
                        }
                    },
                    ]
                });
            });
        }

    })
}

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
 * Handle filter product 
 * */
const handleFilterProduct = () => {
    let url = `https://localhost:7019/api/productapi/filters${window.location.search}`;
    let urlParams = new URLSearchParams(window.location.search);
    if (urlParams.get("priceMin")) {
        $("#price-min").val(urlParams.get("priceMin"))
    }
    if (urlParams.get("priceMax")) {
        $("#price-max").val(urlParams.get("priceMax"))
    }
    let page = urlParams.get("page") || 1;
    $.ajax({
        type: "GET",
        url: url,
        success: (res) => {
            let html = ``;
            $.each(res.lstSP, (key, val) => {
                html += `
             
                <div class="col-md-4 col-xs-6">
                    <div class="product" id="product-detail-${val.maHang}" data-id='${val.maHang}' data-image='${val.anhDaiDien}' data-price='${val.donGiaBan}' data-name='${val.tenHang}'>
                        <div class="product-img">
                            <img src="../../template/img/${val.anhDaiDien}" alt="">
                                <div class="product-label">
                                    <span class="sale">-30%</span>
                                    <span class="new">NEW</span>
                                </div>
                        </div>
                        <div class="product-body">
                            <h3 class="product-name"><a href="product/${val.maHang}">${val.tenHang}</a></h3>
                            <h4 class="product-price">${formatMoney(0.7 * val.donGiaBan)}<del class="product-old-price">${formatMoney(val.donGiaBan)}</del></h4>
                            <div class="product-rating">
                                <i class="fa fa-star"></i>
                                <i class="fa fa-star"></i>
                                <i class="fa fa-star"></i>
                                <i class="fa fa-star"></i>
                                <i class="fa fa-star"></i>
                            </div>
                        </div>
                        <div class="add-to-cart">
										<button class="add-to-cart-btn" onclick="handleAddProductToCart(event, '${val.MaHang}')><i class="fa fa-shopping-cart"></i> add to cart</button>
									</div>
                    </div>
                </div>`
            })
            $("#product-list").html(html);
            let paginationHTMl = ``;
            let pageSize = 10;
            let totalPage = Math.ceil(res.total / pageSize);
            for (let i = 0; i < totalPage; ++i) {
                if (i + 1 === parseInt(page)) {
                    paginationHTMl += ` <li class="active"><a onclick="handleChangePage(${i + 1})">${i + 1}</a></li>`
                } else {
                    paginationHTMl += `<li><a onclick="handleChangePage(${i + 1})">${i + 1}</a></li>`
                }
            }
            $("#category-pagination").html(paginationHTMl);
        },
        error: (err) => {
            console.log(err);
        }
    });
}

/**
 * Handle filter data when page change 
 * @param {int} page
 */
const handleChangePage = (page) => {
    console.log(page);
    const queryString = window.location.search
    const urlParams = new URLSearchParams(queryString);
    urlParams.set("page", page);
    const newUrl = window.location.pathname + '?' + urlParams.toString();
    window.location.href = newUrl;
};

$(document).ready(function () {
    getTopProductSell();
    handleFilterProduct();
    $("#price-min").change(function () {
        setFilterProductsByInput();
    })

    $("#price-max").change(function () {
        setFilterProductsByInput();
    })



});