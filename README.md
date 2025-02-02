# Tourkit Test: https://tourkit.somee.com/
# Công nghệ sử dụng ASP.NET Core, EF Core và Npgsql

**ASP.NET Core** là một framework mã nguồn mở của Microsoft, cho phép phát triển các ứng dụng web và API hiệu suất cao. Nó có thể chạy trên nhiều hệ điều hành (Windows, macOS, Linux) và được tối ưu hóa cho các ứng dụng web đám mây.

**Entity Framework Core (EF Core)** là một ORM (Object-Relational Mapper) nhẹ và nhanh, giúp phát triển ứng dụng bằng cách tương tác với cơ sở dữ liệu mà không cần viết nhiều câu lệnh SQL thủ công. EF Core hỗ trợ nhiều cơ sở dữ liệu khác nhau và có thể dễ dàng tích hợp với các ứng dụng ASP.NET Core.

**Npgsql** là một provider của .NET cho PostgreSQL, cho phép sử dụng EF Core để giao tiếp với PostgreSQL. Nó cung cấp khả năng tương thích cao và hiệu suất tốt, là sự lựa chọn lý tưởng khi làm việc với PostgreSQL trong các ứng dụng .NET.

## Tại sao sử dụng ASP.NET Core, EF Core và Npgsql?

1. **Hiệu suất cao**: ASP.NET Core được thiết kế để tối ưu hóa hiệu suất, giúp xử lý các yêu cầu web nhanh chóng và hiệu quả. Cùng với EF Core, việc truy vấn và thao tác với cơ sở dữ liệu cũng được tối ưu để giảm thiểu độ trễ.

2. **Tính linh hoạt và dễ dàng mở rộng**: ASP.NET Core và EF Core cho phép xây dựng các ứng dụng có khả năng mở rộng linh hoạt, dễ dàng thay đổi cấu trúc hoặc thay đổi nhà cung cấp cơ sở dữ liệu mà không gặp phải khó khăn lớn

3. **Tính di động**: Với ASP.NET Core, ứng dụng có thể chạy trên nhiều hệ điều hành khác nhau (Windows, Linux, macOS), giúp bạn xây dựng ứng dụng dễ dàng triển khai và vận hành trên môi trường đám mây hoặc máy chủ không giới hạn.

4. **Dễ dàng kết nối với PostgreSQL**: Việc sử dụng Npgsql kết hợp với EF Core giúp dễ dàng giao tiếp với PostgreSQL mà không cần phải viết SQL thủ công. Điều này giúp giảm thiểu các lỗi phát sinh từ việc viết SQL sai và tăng hiệu suất phát triển.

# API .NET Core

## Kiến Trúc và Các Kỹ Thuật Sử Dụng

### 1. **Repository Pattern**

**Repository Pattern** là một mẫu thiết kế giúp tách biệt logic truy xuất dữ liệu khỏi phần còn lại của ứng dụng. Mẫu thiết kế này tạo ra một lớp `Repository` để xử lý các thao tác với dữ liệu, giúp dễ dàng bảo trì, kiểm thử, và thay đổi nguồn dữ liệu.

#### Lợi ích của Repository Pattern:
- **Tách biệt mối quan tâm**: Tách biệt logic nghiệp vụ và truy vấn dữ liệu, giúp mã nguồn dễ bảo trì và kiểm thử.
- **Dễ dàng thay đổi nguồn dữ liệu**: Repository giúp thay đổi cách thức truy cập dữ liệu mà không ảnh hưởng đến các phần khác trong ứng dụng.
- **Quản lý truy vấn đơn giản**: Tạo các phương thức để xử lý các truy vấn dữ liệu phổ biến, giúp giảm sự trùng lặp mã nguồn.

### 2. **Unit of Work Pattern**

**Unit of Work Pattern** giúp quản lý các giao dịch trong ứng dụng. Mẫu thiết kế này đồng bộ hóa các thao tác CRUD với cơ sở dữ liệu, đảm bảo tất cả các thay đổi trong một giao dịch đều được thực hiện hoặc bị hủy bỏ (rollback) khi có lỗi xảy ra, giữ cho dữ liệu luôn nhất quán.

#### Lợi ích của Unit of Work Pattern:
- **Quản lý giao dịch (Transaction Management)**: Đảm bảo rằng các thay đổi đối với cơ sở dữ liệu chỉ được thực hiện khi tất cả các thao tác trong một đơn vị công việc đều thành công.
- **Tối ưu hóa hiệu suất**: Giảm thiểu số lần gọi đến cơ sở dữ liệu bằng cách gom nhóm các thao tác vào một giao dịch duy nhất.
- **Dễ dàng quản lý và kiểm thử**: Đơn giản hóa việc kiểm thử và quản lý các giao dịch trong ứng dụng.

### 3. **Tại Sao Sử Dụng Repository và Unit of Work?**

- **Tính tổ chức**: Repository giúp tổ chức mã nguồn khi làm việc với cơ sở dữ liệu, giữ cho các thao tác truy vấn trở nên rõ ràng và dễ bảo trì. Unit of Work giúp quản lý các giao dịch của nhiều repository, đảm bảo tính toàn vẹn của dữ liệu.
- **Kiểm thử dễ dàng**: Repository và Unit of Work giúp kiểm thử dễ dàng hơn, vì chúng tách biệt các thao tác dữ liệu và nghiệp vụ. 
- **Tối ưu hóa hiệu suất**: Việc gom nhóm các thao tác dữ liệu vào một giao dịch giúp giảm số lần kết nối đến cơ sở dữ liệu và đảm bảo hiệu suất tối ưu, đặc biệt là trong các ứng dụng có khối lượng dữ liệu lớn.

### 4. **Cấu Hình và Giải Thích Code**
#### Cấu Hình Dự Án
- **appsettings.json**: Cấu hình thông tin connection nếu trên local không sử dụng PostgreSQL: <br/> 
"DefaultConnection": "Host=tourkit-truongnx.l.aivencloud.com;Port=27334;Database=TourkitTest;User Id=avnadmin;Password=****;Pooling=true;Timeout=300;CommandTimeout=300".
![image](https://github.com/user-attachments/assets/46e20fda-9766-476c-941a-66cfa3d9833a)
- **Dependency Injection (DI)**: Các service và repository được đăng ký trong `Program.cs`.
- ![image](https://github.com/user-attachments/assets/470e1978-4a28-4690-b65a-9a74412c6aac)
- **DbContext**: `ApplicationDbContext` là lớp kế thừa `DbContext` của EF Core, giúp quản lý các entity và cấu hình mối quan hệ giữa các bảng trong cơ sở dữ liệu.

#### 2. Danh sách API
![image](https://github.com/user-attachments/assets/a99bc29f-13ab-4365-9345-2162efc4a388)
- truy cập `[/swagger/index.html](https://tourkit.somee.com/swagger/index.html)` và import `[/swagger/v1/swagger.json](https://tourkit.somee.com/swagger/v1/swagger.json)` postman cho mục đích kiểm thử API
#### 3. Seed Data (Code thể hiện quá trình khởi tạo dữ liệu trong cơ sở dữ liệu khi ứng dụng bắt đầu chạy. )
![image](https://github.com/user-attachments/assets/2673e4cd-cc4f-445f-b16c-ba604b82aea7)
![image](https://github.com/user-attachments/assets/4c6abab8-75c0-4fb4-99e0-2aae12b47300)
