// Cấu trúc dữ liệu chứa thông tin chi tiết của người dùng
public struct UserInfo
{
	// Địa chỉ email của người dùng
	public string Email { get; set; }
	// Căn cước công dân
	public string CCCD { get; set; }
	// Số điện thoại liên lạc
	public string PhoneNumber { get; set; }
	// Họ và tên đầy đủ của người dùng
	public string FullName { get; set; }
	// Ngày sinh của người dùng (định dạng chuỗi)
	public string BirthDay { get; set; }
	// Địa chỉ nơi ở của người dùng
	public string Address { get; set; }
	// Giới tính của người dùng (M/F hoặc Nam/Nữ)
	public char Sex { get; set; }
}
// Giao diện định nghĩa các phương thức cơ bản cho tài khoản
public interface IAccount
{
	// Thiết lập tên đăng nhập cho tài khoản
	void SetUsername(string username);
	// Thiết lập mật khẩu cho tài khoản
	void SetPassword(string password);
	// Thiết lập địa chỉ email cho tài khoản
	void SetEmail(string email);
	// Gán thông tin chi tiết người dùng vào tài khoản
	void SetAccountInfo(UserInfo info);
	// Các phương thức dành cho tài khoản khách (GuestAccount)

	// Đặt phòng theo ID, trả về mã kết quả
	int BookRoom(int roomID);
	// Hủy đặt phòng theo ID, trả về mã kết quả
	int CancelRoom(int roomID);
	// Liệt kê danh sách các phòng đã đặt, trả về mã kết quả
	int ListBooking();
	// Các phương thức dành cho tài khoản chủ nhà (HostAccount)

	// Chỉnh sửa danh sách phòng có sẵn, trả về mã kết quả
	int EditRoomList();
	// Chỉnh sửa lịch phòng và thời gian, trả về mã kết quả
	int EditCalendar();
}
