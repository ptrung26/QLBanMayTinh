
$(document).ready(function () {

    $("#form-login").on("submit", function (event) {
        event.preventDefault();
        let url = "https://localhost:7019/api/access/login";
        let usernameLogin = $(this).find("input[name='usernameLogin']").val();
        let passwordLogin = $(this).find("input[name='passwordLogin']").val();
        let data = {
            usernameLogin, passwordLogin
        };
        $.ajax({
            type: "POST",
            url: url,
            data: JSON.stringify(data),
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            dataType: 'json',
            success: function (res) {
                localStorage.setItem("token", res.data.jwt);
                window.location.href = "https://localhost:7019/";
            },
            error: function (err) {
                console.log(err);
                alert(err.responseJSON.Message);
            }
        }
        );
    }
    )
    $('#registerForm').on("submit", function (event) {
        event.preventDefault();
        if (!handleValidate($(this))) {
            return;
        }
        let url = "https://localhost:7019/api/access/register";
        let tenTaiKhoan = $(this).find("input[name='username']").val();
        let matKhau = $(this).find("input[name='password']").val();
        let data = {
            tenTaiKhoan, matKhau
        };
        $.ajax({
            type: "POST",
            url: url,
            data: JSON.stringify(data),
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            dataType: 'json',
            success: function (res) {
                console.log(res);
                localStorage.setItem("token", res.data.jwt);
                window.location.href = "https://localhost:7019/";
            },
            error: function (err) {
                console.log(err);
                alert(err.responseJSON.Message);
            }
        })
    });
    $("#form-forgot").on("submit", function (event) {
        event.preventDefault();
        let username = $(this).find("input[name='username']").val();
        let url = `https://localhost:7019/api/access/forgot?username=${username}`;
        $.ajax({
            type: "POST",
            url: url,
            success: function (res) {
                alert("Check email để đổi mật khẩu nhá");
                $(this).find("input[name='username']").val("");
            },
            error: function (err) {
                console.log(err);
            }
        });
    });
    $("#form-reset").on("submit", function (event) {
        event.preventDefault();
        const urlParams = new URLSearchParams(window.location.search);
        const usernameLogin = urlParams.get('username');
        const passwordLogin = $(this).find("input[name='confirm-password']").val();
        let data = {
            usernameLogin,
            passwordLogin,
        }
        $.ajax({
            type: "POST",
            url: `https://localhost:7019/api/access/resetpassword`,
            data: JSON.stringify(data),
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            dataType: 'json',
            success: function (res) {
                alert("Đổi mật khẩu thành công");
                window.location.href = "https://localhost:7019/";
            },
            error: function (err) {
                console.log(err);
                alert(err.responseJSON.Message);
            }
        });
    })
});


function handleValidate(form) {
    let name = form.find("input[name='username']").val();
    let check_error_name = $('#error-name');

    if (name === '') {
        check_error_name.html('* Họ tên không được để trống!');
        return false;
    }

    check_error_name.html('');

    let pass = form.find("input[name='password']").val();
    let check_error_pass = $('#error-password');
    let regexPass = /\s/;

    if (pass === '') {
        check_error_pass.html('* Mật khẩu không được để trống!');
        return false;
    }
    if (regexPass.test(pass)) {
        check_error_pass.html('* Mật khẩu không được chứa dấu cách!');
        return false;
    }

    if (pass.length < 3) {
        check_error_pass.html('Mật khẩu phải lớn hơn 3 kí tự!');
        return false;
    }

    check_error_pass.html('');

    let confirmPass = form.find("input[name='confirm-password']").val();
    console.log(confirmPass, pass);
    let check_error_cfrm_pass = $('#error-password');
    if (confirmPass !== pass) {
        check_error_cfrm_pass.html('Mật khẩu không trùng nhau');
        return false;
    }
    check_error_cfrm_pass.html('');

    return true;
}

