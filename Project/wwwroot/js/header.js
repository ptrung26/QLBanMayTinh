const handleSearchInput = () => {
    let query = $("#search-input").val().trim();
    let url = "";
    if (query == "") {
        url = `https://localhost:7019/danhmucsanpham`;
    } else {
        url = `https://localhost:7019/danhmucsanpham?query=${query}`;
    }
    window.location.href = url;
}

const getTotalOfCart = () => {
    let cartItems = JSON.parse(sessionStorage.getItem('cartItems')) || [];
    if (cartItems.length > 0) {
        $(".qty").css("display", "inline-block");
        $(".qty").text(cartItems.length.toString());
    } else {
        $(".qty").css("display", "none");
        $(".qty").text("");

    }
}


$(document).ready(function () {
    handleGetUserInfo();
    getTotalOfCart();
    $(".search-btn").click(function () {
        handleSearchInput();
    })
    $("#btn-logout").click(function () {
        localStorage.removeItem("token");
        localStorage.removeItem("username");
        window.location.href = `https://localhost:7019/`;
    });

})

function handleGetUserInfo() {
    let token = localStorage.getItem("token");
    if (!token) {
        $("#sign-in").css("display", "inline-block");
        $("#sign-out").css("display", "none");
        return;
    }
    let url = "https://localhost:7019/api/access/getInfo";
    $.ajax({
        type: "POST",
        headers: {
            'Authorization': `Bearer ${token}`
        },
        url: url,
        success: function (res) {

            $("#sign-out").css("display", "inline-block");
            let username = $("#sign-out").find(".username")[0];
            $(username).text(res.data.usernameLogin);
            localStorage.setItem("username", res.data.usernameLogin);
            $("#sign-in").css("display", "none");
        },
        error: function (err) {
            console.log(err);
            localStorage.removeItem("username");
            $("#sign-in").css("display", "inline-block");
            $("#sign-out").css("display", "none");
        }
    });
}