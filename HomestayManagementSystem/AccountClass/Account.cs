// Account.cs - Phiên bản đã sửa lỗi
using System;

// Lớp cơ sở đại diện cho tài khoản, triển khai giao diện IAccount
public class Account : IAccount
{
    // Mã định danh duy nhất của tài khoản
    protected string Id { get; set; }

    // Tên đăng nhập của người dùng
    protected string Username { get; set; }

    // Mật khẩu của người dùng
    protected string Password { get; set; }

    // Thông tin bổ sung của người dùng (struct UserInfo)
    protected UserInfo AccountInfo { get; set; } = new UserInfo();

    // Thiết lập tên đăng nhập (đã thêm virtual để cho phép ghi đè trong lớp con)
    public virtual void SetUsername(string username) => Username = username;

    // Thiết lập mật khẩu (đã thêm virtual để cho phép ghi đè trong lớp con)
    public virtual void SetPassword(string password) => Password = password;
    
    // Thiết lập email - đã sửa lỗi khi làm việc với struct
    public virtual void SetEmail(string email) 
    {
        // Tạo bản sao của struct UserInfo hiện tại
        var info = AccountInfo;
        // Thay đổi giá trị email trong bản sao
        info.Email = email;
        // Gán lại toàn bộ struct đã được cập nhật
        AccountInfo = info;
    }
    
    // Gán toàn bộ đối tượng UserInfo (đã thêm virtual để cho phép ghi đè)
    public virtual void SetAccountInfo(UserInfo info) => AccountInfo = info;

    // Đặt phòng theo ID - phương thức ảo có thể được ghi đè
    public virtual int BookRoom(int roomID) => -1;

    // Hủy đặt phòng theo ID - phương thức ảo có thể được ghi đè
    public virtual int CancelRoom(int roomID) => -1;

    // Liệt kê danh sách đặt phòng - phương thức ảo có thể được ghi đè
    public virtual int ListBooking() => -1;

    // Chỉnh sửa danh sách phòng - phương thức ảo có thể được ghi đè
    public virtual int EditRoomList() => -1;

    // Chỉnh sửa lịch đặt phòng - phương thức ảo có thể được ghi đè
    public virtual int EditCalendar() => -1;
}
