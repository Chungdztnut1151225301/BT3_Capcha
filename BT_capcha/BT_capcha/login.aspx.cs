using System;
using System.Drawing;
using System.IO;
using CaptchaLibrary; // Tham chiếu đến thư viện CAPTCHA của bạn

namespace CaptchaLoginApp
{
    public partial class Login : System.Web.UI.Page
    {
        private static int failedLoginAttempts = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Xử lý nếu trang được tải lần đầu
            }
        }

        // Xử lý sự kiện khi bấm nút Login
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;

            if (ValidateUser(username, password))
            {
                Response.Redirect("index.aspx"); // Đăng nhập thành công chuyển hướng đến trang index
            }
            else
            {
                failedLoginAttempts++;
                if (failedLoginAttempts >= 3)
                {
                    ShowCaptcha(); // Hiển thị CAPTCHA sau 3 lần login thất bại
                }
            }
        }

        // Xử lý sự kiện khi bấm nút Submit Captcha
        protected void btnSubmitCaptcha_Click(object sender, EventArgs e)
        {
            string captchaInput = txtCaptcha.Text;
            string sessionCaptcha = Session["captchaText"].ToString();

            if (captchaInput == sessionCaptcha && ValidateUser(txtUsername.Text, txtPassword.Text))
            {
                Response.Redirect("index.aspx");
            }
            else
            {
                failedLoginAttempts++;
                ShowCaptcha(); // Nếu CAPTCHA sai, yêu cầu nhập lại
            }
        }

        // Hàm kiểm tra tài khoản người dùng (giả lập từ database)
        private bool ValidateUser(string username, string password)
        {
            // Kiểm tra từ database, ở đây giả lập bằng username và password cố định
            return (username == "test" && password == "password");
        }

        // Hàm hiển thị CAPTCHA
        private void ShowCaptcha()
        {
            CaptchaGenerator captcha = new CaptchaGenerator(); // Sinh CAPTCHA từ thư viện CaptchaLibrary
            string captchaText = GenerateRandomText(6); // Sinh chuỗi ngẫu nhiên 6 ký tự
            Session["captchaText"] = captchaText;
            Bitmap captchaImage = captcha.GenerateCaptcha(captchaText); // Sinh ảnh CAPTCHA

            // Chuyển ảnh thành chuỗi base64 và hiển thị
            using (MemoryStream ms = new MemoryStream())
            {
                captchaImage.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                string base64String = Convert.ToBase64String(ms.ToArray());
                this.captchaImage.ImageUrl = "data:image/png;base64," + base64String;
            }

            // Hiển thị các điều khiển CAPTCHA
            captchaImage.Visible = true;
            txtCaptcha.Visible = true;
            btnSubmitCaptcha.Visible = true;
        }

        // Hàm sinh chuỗi ký tự ngẫu nhiên
        private string GenerateRandomText(int length)
        {
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            Random rand = new Random();
            char[] textArray = new char[length];
            for (int i = 0; i < length; i++)
            {
                textArray[i] = chars[rand.Next(chars.Length)];
            }
            return new string(textArray);
        }
    }
}
