using System;

// Lớp tài khoản chủ nhà/quản lý (Host Account), kế thừa từ lớp Account
public class HostAccount : Account
{
    // Constructor mặc định, gọi constructor của lớp cha (Account)
    public HostAccount() { }

    // Ghi đè phương thức thiết lập tên đăng nhập, gọi phương thức của lớp cha
    public override void SetUsername(string username) => base.SetUsername(username);

    // Ghi đè phương thức thiết lập mật khẩu, gọi phương thức của lớp cha
    public override void SetPassword(string password) => base.SetPassword(password);

    // Ghi đè phương thức thiết lập email, gọi phương thức của lớp cha
    public override void SetEmail(string email) => base.SetEmail(email);

    // Ghi đè phương thức gán thông tin tài khoản, gọi phương thức của lớp cha
    public override void SetAccountInfo(UserInfo info) => base.SetAccountInfo(info);

    // Ghi đè phương thức chỉnh sửa danh sách phòng - trả về 2 (có quyền thực hiện)
    public override int EditRoomList() => 2;

    // Ghi đè phương thức chỉnh sửa lịch - trả về 3 (có quyền thực hiện)
    public override int EditCalendar() => 3;
}
