using System;

// Lớp tài khoản khách (Guest Account), kế thừa từ lớp Account
public class GuestAccount : Account
{
    // Constructor mặc định, gọi constructor của lớp cha (Account)
    public GuestAccount() { }

    // Ghi đè phương thức thiết lập tên đăng nhập, gọi phương thức của lớp cha
    public override void SetUsername(string username) => base.SetUsername(username);

    // Ghi đè phương thức thiết lập mật khẩu, gọi phương thức của lớp cha
    public override void SetPassword(string password) => base.SetPassword(password);

    // Ghi đè phương thức thiết lập email, gọi phương thức của lớp cha
    public override void SetEmail(string email) => base.SetEmail(email);

    // Ghi đè phương thức gán thông tin tài khoản, gọi phương thức của lớp cha
    public override void SetAccountInfo(UserInfo info) => base.SetAccountInfo(info);

    // Ghi đè phương thức đặt phòng - trả về chính ID phòng (thành công)
    public override int BookRoom(int roomID) => roomID;

    // Ghi đè phương thức hủy đặt phòng - trả về ID phòng âm (xác nhận hủy)
    public override int CancelRoom(int roomID) => -roomID;

    // Ghi đè phương thức liệt kê đặt phòng - trả về 1 (có danh sách)
    public override int ListBooking() => 1;
}
