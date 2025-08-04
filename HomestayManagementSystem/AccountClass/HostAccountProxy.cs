using System;

// Lớp Proxy cho tài khoản chủ nhà (Host Account), triển khai giao diện IAccount
// Sử dụng mẫu thiết kế Proxy để kiểm soát quyền truy cập vào đối tượng HostAccount thực
public sealed class HostAccountProxy : IAccount
{
    // Đối tượng HostAccount thật được bao bọc bởi proxy
    private readonly HostAccount realAccount;
    // Constructor nhận vào một HostAccount để khởi tạo proxy
    public HostAccountProxy(HostAccount account) => realAccount = account;

    // Ủy quyền thiết lập tên đăng nhập cho đối tượng thật
    public void SetUsername(string username) => realAccount.SetUsername(username);

    // Ủy quyền thiết lập mật khẩu cho đối tượng thật
    public void SetPassword(string password) => realAccount.SetPassword(password);

    // Ủy quyền thiết lập email cho đối tượng thật
    public void SetEmail(string email) => realAccount.SetEmail(email);

    // Ủy quyền gán thông tin tài khoản cho đối tượng thật
    public void SetAccountInfo(UserInfo info) => realAccount.SetAccountInfo(info);

    // Khách không được phép đặt phòng qua proxy của HostAccount => trả về -1 (không thành công)
    public int BookRoom(int roomID) => -1;

    // Khách không được phép hủy phòng qua proxy của HostAccount => trả về -1
    public int CancelRoom(int roomID) => -1;

    // Khách không được phép xem danh sách đặt phòng qua proxy của HostAccount => trả về -1
    public int ListBooking() => -1;

    // Ủy quyền chỉnh sửa danh sách phòng cho đối tượng HostAccount thật
    public int EditRoomList() => realAccount.EditRoomList();

    // Ủy quyền chỉnh sửa lịch cho đối tượng HostAccount thật
    public int EditCalendar() => realAccount.EditCalendar();
}
