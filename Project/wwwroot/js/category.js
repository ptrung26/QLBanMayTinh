const setFilterProductsByInput = (type, input) => {
    const query = $(input).val().replace(/[\n\t]/g, '');
    const queryString = window.location.search
    const urlParams = new URLSearchParams(queryString);
    urlParams.set(type, query);
    const newUrl = window.location.pathname + '?' + urlParams.toString();
    window.location.href = newUrl;
}


const getTopProductSell = (url, id) => {
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

const handleToAddToCard = (productId) => {
    let cartPostUrl = `https://localhost:7019/api/cart?mahang=${productId}&quatity=1`;
    $.ajax({
        type: "POST",
        url: cartPostUrl,
        dataType: 'json',
        contentType: "application/json",
        success: () => {
            let cartGetUrl = `https://localhost:7019/api/cart`;
            $.ajax({
                type: "GET",
                url: cartGetUrl,
                dataType: "json",
                success: (data) => {
                    let str = "";
                    $.each(data, (key, val) => {
                        str += `<div class="product-widget">
											<div class="product-img">
												<img src="../imageBTL/${val.Anh}" alt="">
											</div>
											<div class="product-body">
												<h3 class="product-name"><a href="#">${val.tenHang}</a></h3>
												<h4 class="product-price">${formatMoney(val.donGiaBan)}<span class="qty"> ${val.soLuong}</span></h4>
											</div>
											<button class="delete"><i class="fa fa-close"></i></button>
										</div>`
                    })
                    $("#CartPdOverview").html(str);
                    alert("Thêm vào giỏ hàng thành công");
                }
            })
        }
    })
}

$(document).ready(function () {
    getTopProductSell("https://localhost:7019/api/productapi/topsellproducts", "#showTopSellCategory")

    $("#price-min").change(function () {
        setFilterProductsByInput("priceMin", $(this));
    })

    $("#price-max").change(function () {
        setFilterProductsByInput("priceMax", $(this));
    })



});