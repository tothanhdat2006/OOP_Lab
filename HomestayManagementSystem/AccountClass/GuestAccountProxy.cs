using System;

// Lớp Proxy cho tài khoản khách, triển khai giao diện IAccount
// Sử dụng design pattern Proxy để kiểm soát quyền truy cập vào đối tượng GuestAccount thực
public sealed class GuestAccountProxy : IAccount
{
    // Đối tượng tài khoản khách thực sự được bao bọc bởi proxy
    private readonly GuestAccount realAccount;

    // Constructor nhận vào đối tượng GuestAccount để tạo proxy
    public GuestAccountProxy(GuestAccount account) => realAccount = account;

    // Ủy quyền việc thiết lập tên đăng nhập cho đối tượng thực
    public void SetUsername(string username) => realAccount.SetUsername(username);

    // Ủy quyền việc thiết lập mật khẩu cho đối tượng thực
    public void SetPassword(string password) => realAccount.SetPassword(password);

    // Ủy quyền việc thiết lập email cho đối tượng thực
    public void SetEmail(string email) => realAccount.SetEmail(email);

    // Ủy quyền việc gán thông tin tài khoản cho đối tượng thực
    public void SetAccountInfo(UserInfo info) => realAccount.SetAccountInfo(info);

    // Ủy quyền việc đặt phòng cho đối tượng thực
    public int BookRoom(int roomID) => realAccount.BookRoom(roomID);

    // Ủy quyền việc hủy đặt phòng cho đối tượng thực
    public int CancelRoom(int roomID) => realAccount.CancelRoom(roomID);

    // Ủy quyền việc liệt kê đặt phòng cho đối tượng thực
    public int ListBooking() => realAccount.ListBooking();

    // Từ chối quyền chỉnh sửa danh sách phòng (tài khoản khách không có quyền này)
    public int EditRoomList() => -1;

    // Từ chối quyền chỉnh sửa lịch (tài khoản khách không có quyền này)
    public int EditCalendar() => -1;
}
