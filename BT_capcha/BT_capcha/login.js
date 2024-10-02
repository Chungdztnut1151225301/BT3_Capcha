$(document).ready(function () {
    $('#loginForm').on('submit', function (e) {
        e.preventDefault();

        var username = $('#username').val();
        var password = $('#password').val();

        $.ajax({
            url: "login.aspx", // Đường dẫn đến trang xử lý đăng nhập
            method: "POST",
            data: { username: username, password: password },
            success: function (response) {
                if (response.status === "success") {
                    // Chuyển hướng đến index.html
                    window.location.href = "index.html";
                } else {
                    $('#loginMessage').html('<div class="alert alert-danger">' + response.message + '</div>');
                }
            },
            error: function () {
                $('#loginMessage').html('<div class="alert alert-danger">Error logging in. Please try again.</div>');
            }
        });
    });
});
