# **ĐỀ KIỂM TRA: ENTITY FRAMEWORK CORE & CRUD**

**Môn học:** Lập Trình .NET Advanced  
**Thời gian:** 120 phút  
**Điểm:** 10 điểm

---

# **ĐỀ BÀI 1: HỆ THỐNG QUẢN LÝ BLOG**

## **Yêu cầu chung:**

Xây dựng ứng dụng ASP.NET Core Web API quản lý Blog với các chức năng CRUD. Ứng dụng phải có 2 bảng dữ liệu liên kết với nhau: **Category** (Danh mục) và **BlogPost** (Bài viết).

**Quan hệ:** 1 Danh mục có thể chứa nhiều bài viết, 1 bài viết thuộc về 1 danh mục.

---

## **PHẦN 1: THIẾT KẾ DATABASE (2 điểm)**

### **Yêu cầu:**

1. **Entity Category** phải có các thuộc tính:
   - `Id` (int, Primary Key, Auto-increment)
   - `Name` (string, bắt buộc, 3-100 ký tự)
   - `Description` (string, tùy chọn, tối đa 500 ký tự)
   - `CreatedAt` (DateTime)
   - `UpdatedAt` (DateTime, nullable)
   - Navigation Property: `ICollection<BlogPost>`

2. **Entity BlogPost** phải có các thuộc tính:
   - `Id` (int, Primary Key, Auto-increment)
   - `Title` (string, bắt buộc, 5-200 ký tự)
   - `Content` (string, bắt buộc, 50-5000 ký tự)
   - `ThumbnailUrl` (string, tùy chọn, phải là URL hợp lệ)
   - `Author` (string, bắt buộc, tối đa 100 ký tự)
   - `ViewCount` (int, mặc định = 0)
   - `IsPublished` (bool, mặc định = true)
   - `CreatedAt` (DateTime)
   - `UpdatedAt` (DateTime, nullable)
   - `PublishedAt` (DateTime, nullable)
   - `CategoryId` (int, Foreign Key)
   - Navigation Property: `Category`

3. **DbContext** phải:
   - Kế thừa từ `DbContext`
   - Có `DbSet<Category>` và `DbSet<BlogPost>`
   - Cấu hình Fluent API cho cả 2 entity
   - Seed dữ liệu ban đầu: **10 categories** và **30 blog posts**
   - Thiết lập relationship One-to-Many với `OnDelete(DeleteBehavior.Restrict)`

4. **Dữ liệu Seed:**
   - **Categories (10):** Công Nghệ, Lập Trình, Web Dev, Mobile, DevOps, AI, Database, Cloud, Bảo Mật, Khác
   - **BlogPosts (30):** Chia đều cho các categories, mỗi bài viết có đầy đủ thông tin (tiêu đề, nội dung, tác giả, etc.)

---

## **PHẦN 2: BUSINESS LOGIC - SERVICE LAYER (3 điểm)**

### **Yêu cầu: Tạo Service cho Category**

Tạo interface `ICategoryService` và class `CategoryService` với các method:

1. **`GetAllAsync()`**
   - Trả về tất cả categories
   - Bao gồm BlogPosts của mỗi category (Include)
   - Sắp xếp theo tên (OrderBy)

2. **`GetByIdAsync(int id)`**
   - Trả về category theo ID
   - Bao gồm BlogPosts

3. **`CreateAsync(Category category)`**
   - Validate: Tên không trống, 3-100 ký tự
   - Validate: Không cho phép trùng tên (case-insensitive)
   - Throw `ArgumentException` nếu validation fail
   - Throw `ArgumentException` nếu tên đã tồn tại
   - Set `CreatedAt = DateTime.UtcNow`
   - Return category đã tạo

4. **`UpdateAsync(int id, Category category)`**
   - Validate: Category phải tồn tại (throw `KeyNotFoundException`)
   - Validate: Tên không trống, 3-100 ký tự
   - Validate: Không cho phép trùng tên (except self)
   - Set `UpdatedAt = DateTime.UtcNow`
   - Return category đã cập nhật

5. **`DeleteAsync(int id)`**
   - Validate: Category phải tồn tại
   - **Business Logic:** Nếu category có blog posts, throw `InvalidOperationException` với message: `"Không thể xóa danh mục '{name}' vì nó chứa {count} bài viết"`
   - Xóa category và return `true`

### **Yêu cầu: Tạo Service cho BlogPost**

Tạo interface `IBlogPostService` và class `BlogPostService` với các method:

1. **`GetAllAsync()`**
   - Trả về tất cả blog posts (published)
   - Include Category
   - Sắp xếp theo CreatedAt descending

2. **`GetByIdAsync(int id)`**
   - Trả về blog post theo ID
   - Include Category
   - **Business Logic:** Tăng ViewCount lên 1 mỗi lần gọi
   - SaveChanges sau khi tăng

3. **`GetByCategoryAsync(int categoryId)`**
   - Validate: Category phải tồn tại (throw `KeyNotFoundException`)
   - Trả về tất cả blog posts của category (published)
   - Include Category
   - Sắp xếp theo CreatedAt descending

4. **`CreateAsync(BlogPost blogPost)`**
   - Validate: Title (required, 5-200 ký tự)
   - Validate: Content (required, 50-5000 ký tự)
   - Validate: Author (required, tối đa 100 ký tự)
   - Validate: CategoryId phải tồn tại
   - Validate: ThumbnailUrl nếu có phải là URL hợp lệ
   - Set `CreatedAt = DateTime.UtcNow`
   - Nếu `IsPublished = true`, set `PublishedAt = DateTime.UtcNow`
   - Set `ViewCount = 0`
   - Return blog post đã tạo

5. **`UpdateAsync(int id, BlogPost blogPost)`**
   - Validate: BlogPost phải tồn tại
   - Validate: Title (5-200 ký tự)
   - Validate: Content (50-5000 ký tự)
   - Validate: Author (required, tối đa 100 ký tự)
   - Validate: CategoryId phải tồn tại
   - Set `UpdatedAt = DateTime.UtcNow`
   - Nếu `IsPublished = true` và `PublishedAt = null`, set `PublishedAt = DateTime.UtcNow`
   - Return blog post đã cập nhật

6. **`DeleteAsync(int id)`**
   - Validate: BlogPost phải tồn tại
   - Xóa blog post
   - Return `true`

---

## **PHẦN 3: API CONTROLLERS (3 điểm)**

### **Yêu cầu: CategoriesController**

Tạo `CategoriesController` với các endpoint:

1. **`[HttpGet]` GetAll()**
   - Call `CategoryService.GetAllAsync()`
   - Response format: `{ success: bool, data: Category[], message?: string }`

2. **`[HttpGet("{id}")]` GetById(int id)**
   - Call `CategoryService.GetByIdAsync(id)`
   - Return 404 nếu không tìm thấy
   - Return 200 với category nếu found

3. **`[HttpPost]` Create([FromBody] Category category)**
   - Call `CategoryService.CreateAsync(category)`
   - Return 201 (CreatedAtAction)
   - Catch `ArgumentException` → Return 400 BadRequest
   - Catch `Exception` → Return 500 Internal Server Error

4. **`[HttpPut("{id}")]` Update(int id, [FromBody] Category category)**
   - Call `CategoryService.UpdateAsync(id, category)`
   - Return 200 nếu success
   - Catch `ArgumentException` → Return 400
   - Catch `KeyNotFoundException` → Return 404
   - Catch `Exception` → Return 500

5. **`[HttpDelete("{id}")]` Delete(int id)**
   - Call `CategoryService.DeleteAsync(id)`
   - Return 200 với message: `"Xóa danh mục thành công"`
   - Catch `KeyNotFoundException` → Return 404
   - Catch `InvalidOperationException` → Return 400 (không xóa vì có bài viết)

### **Yêu cầu: BlogPostsController**

Tạo `BlogPostsController` với các endpoint:

1. **`[HttpGet]` GetAll()**
   - Call `BlogPostService.GetAllAsync()`
   - Return 200 với array of BlogPost

2. **`[HttpGet("{id}")]` GetById(int id)**
   - Call `BlogPostService.GetByIdAsync(id)`
   - Return 404 nếu không tìm thấy
   - Return 200 với BlogPost

3. **`[HttpGet("category/{categoryId}")]` GetByCategory(int categoryId)**
   - Call `BlogPostService.GetByCategoryAsync(categoryId)`
   - Return 200 với array of BlogPost
   - Catch `KeyNotFoundException` → Return 404 (category không tồn tại)

4. **`[HttpPost]` Create([FromBody] BlogPost blogPost)**
   - Call `BlogPostService.CreateAsync(blogPost)`
   - Return 201 (CreatedAtAction)
   - Catch `ArgumentException` → Return 400
   - Catch `KeyNotFoundException` → Return 404 (category không tồn tại)

5. **`[HttpPut("{id}")]` Update(int id, [FromBody] BlogPost blogPost)**
   - Call `BlogPostService.UpdateAsync(id, blogPost)`
   - Return 200 nếu success
   - Proper error handling với status codes 400, 404, 500

6. **`[HttpDelete("{id}")]` Delete(int id)**
   - Call `BlogPostService.DeleteAsync(id)`
   - Return 200 với message
   - Proper error handling

---

## **PHẦN 4: CONFIGURATION & MIGRATIONS (2 điểm)**

### **Yêu cầu:**

1. **Program.cs** phải:
   - Add DbContext với SQL Server
   - Đăng ký `ICategoryService` và `IBlogPostService` vào DI Container
   - Add CORS (AllowAnyOrigin, AllowAnyMethod, AllowAnyHeader)
   - Add Swagger/Swashbuckle

2. **appsettings.json** phải:
   - Có ConnectionString cho `BlogDb` database
   - Cấu hình Logging

3. **Migrations:**
   - Tạo migration `InitialCreate`
   - Apply migration để tạo database tự động
   - Database phải có 10 categories + 30 blog posts

4. **Database Constraints:**
   - CategoryId là Foreign Key
   - Không cho phép xóa Category nếu có BlogPosts (Restrict)

---

## **PHẦN 5: KIỂM TRA CHỨC NĂNG (2 điểm)**

### **Yêu cầu:**

Chạy ứng dụng và kiểm tra các chức năng sau bằng Swagger UI hoặc Postman:

#### **Test Cases for Categories:**

1. ✓ GET `/api/categories` → Trả về 10 categories
2. ✓ GET `/api/categories/1` → Trả về category "Công Nghệ" với tất cả blog posts
3. ✓ POST `/api/categories` → Tạo category mới thành công
4. ✓ POST `/api/categories` (duplicate name) → Return 400 "Danh mục đã tồn tại"
5. ✓ POST `/api/categories` (invalid length) → Return 400 "Tên phải từ 3-100 ký tự"
6. ✓ PUT `/api/categories/1` → Cập nhật category thành công
7. ✓ DELETE `/api/categories/1` → Return 400 "Không thể xóa vì có bài viết"
8. ✓ DELETE `/api/categories/10` (category có 0 posts) → Xóa thành công

#### **Test Cases for BlogPosts:**

1. ✓ GET `/api/blogposts` → Trả về 30 blog posts
2. ✓ GET `/api/blogposts/1` → Trả về bài viết với ViewCount tăng lên
3. ✓ GET `/api/blogposts/category/1` → Trả về tất cả posts của "Công Nghệ"
4. ✓ GET `/api/blogposts/category/999` → Return 404 "Category không tồn tại"
5. ✓ POST `/api/blogposts` (valid) → Tạo blog post thành công
6. ✓ POST `/api/blogposts` (invalid title) → Return 400 "Tiêu đề phải từ 5-200 ký tự"
7. ✓ POST `/api/blogposts` (invalid content) → Return 400 "Nội dung phải từ 50-5000 ký tự"
8. ✓ POST `/api/blogposts` (invalid categoryId) → Return 404 "Category không tồn tại"
9. ✓ PUT `/api/blogposts/1` → Cập nhật thành công, UpdatedAt được set
10. ✓ DELETE `/api/blogposts/1` → Xóa thành công

---

---

# **ĐỀ BÀI 2: HỆ THỐNG QUẢN LÝ KHÓA HỌC**

## **Yêu cầu chung:**

Xây dựng ứng dụng ASP.NET Core Web API quản lý Khóa Học với các chức năng CRUD. Ứng dụng phải có 2 bảng dữ liệu liên kết: **Course** (Khóa học) và **Lesson** (Bài học).

**Quan hệ:** 1 Khóa học có thể chứa nhiều bài học, 1 bài học thuộc về 1 khóa học.

---

## **PHẦN 1: THIẾT KẾ DATABASE (2 điểm)**

### **Yêu cầu:**

1. **Entity Course** phải có các thuộc tính:
   - `Id` (int, Primary Key)
   - `Title` (string, bắt buộc, 5-150 ký tự)
   - `Description` (string, bắt buộc, 20-1000 ký tự)
   - `CoverImageUrl` (string, tùy chọn, URL hợp lệ)
   - `Level` (string, bắt buộc: "Beginner", "Intermediate", "Advanced")
   - `Price` (decimal, 0-100000)
   - `Duration` (int, tính bằng phút, > 0)
   - `Instructor` (string, bắt buộc, tối đa 100 ký tự)
   - `Rating` (decimal, 0-5)
   - `EnrolledCount` (int, mặc định = 0)
   - `IsActive` (bool, mặc định = true)
   - `CreatedAt` (DateTime)
   - `UpdatedAt` (DateTime, nullable)
   - Navigation Property: `ICollection<Lesson>`

2. **Entity Lesson** phải có các thuộc tính:
   - `Id` (int, Primary Key)
   - `Title` (string, bắt buộc, 3-200 ký tự)
   - `Content` (string, bắt buộc, 30-10000 ký tự)
   - `VideoUrl` (string, tùy chọn, URL hợp lệ)
   - `Duration` (int, tính bằng phút, > 0)
   - `LessonOrder` (int, thứ tự bài học, 0-1000)
   - `Resources` (string, tùy chọn, tối đa 5000 ký tự)
   - `CompletionPercentage` (int, 0-100)
   - `IsPublished` (bool, mặc định = true)
   - `CreatedAt` (DateTime)
   - `UpdatedAt` (DateTime, nullable)
   - `CourseId` (int, Foreign Key, bắt buộc)
   - Navigation Property: `Course`

3. **DbContext** phải:
   - Có `DbSet<Course>` và `DbSet<Lesson>`
   - Cấu hình Fluent API cho cả 2 entity
   - Seed dữ liệu: **10 courses** và **30 lessons**
   - Relationship: One-to-Many với `OnDelete(DeleteBehavior.Cascade)`

4. **Dữ liệu Seed:**
   - **Courses (10):** C# Basics, ASP.NET Core, EF Core, JavaScript, React, SQL Server, Docker & K8s, Python, Git, Web Security
   - **Lessons (30):** Chia đều cho các khóa học

---

## **PHẦN 2: BUSINESS LOGIC - SERVICE LAYER (3 điểm)**

### **Yêu cầu: Tạo Service cho Course**

Interface `ICourseService` và class `CourseService` với các method:

1. **`GetAllAsync()`**
   - Trả về tất cả active courses
   - Include Lessons
   - Sắp xếp theo CreatedAt descending

2. **`GetByLevelAsync(string level)`**
   - Validate: Level phải là "Beginner", "Intermediate", hoặc "Advanced"
   - Throw `ArgumentException` nếu level không hợp lệ
   - Trả về courses theo level
   - Sắp xếp theo Rating descending

3. **`GetByIdAsync(int id)`**
   - Trả về course theo ID
   - Include Lessons sắp xếp theo LessonOrder

4. **`CreateAsync(Course course)`**
   - Validate: Title (5-150 ký tự)
   - Validate: Description (20-1000 ký tự)
   - Validate: Level (Beginner, Intermediate, Advanced)
   - Validate: Instructor (không trống)
   - Validate: Price (0-100000)
   - Validate: Duration > 0
   - Set CreatedAt
   - Throw `ArgumentException` nếu validation fail
   - Return course đã tạo

5. **`UpdateAsync(int id, Course course)`**
   - Validate: Course tồn tại (throw `KeyNotFoundException`)
   - Validate tất cả fields như Create
   - Set UpdatedAt
   - Return course đã cập nhật

6. **`DeleteAsync(int id)`**
   - Validate: Course tồn tại
   - Xóa course
   - Return true

7. **`GetLessonCountAsync(int courseId)`**
   - Validate: Course tồn tại
   - Return số lessons của course

8. **`GetAveragePriceAsync()`**
   - Return giá trung bình của tất cả active courses

### **Yêu cầu: Tạo Service cho Lesson**

Interface `ILessonService` và class `LessonService` với các method:

1. **`GetAllByCourseAsync(int courseId)`**
   - Validate: Course phải tồn tại
   - Trả về tất cả published lessons của course
   - Sắp xếp theo LessonOrder

2. **`GetByIdAsync(int id)`**
   - Trả về lesson theo ID
   - Include Course

3. **`CreateAsync(Lesson lesson)`**
   - Validate: Title (3-200 ký tự)
   - Validate: Content (30-10000 ký tự)
   - Validate: Duration > 0
   - Validate: CourseId phải tồn tại
   - Validate: LessonOrder >= 0
   - Set CreatedAt
   - Throw exception nếu validation fail
   - Return lesson đã tạo

4. **`UpdateAsync(int id, Lesson lesson)`**
   - Validate: Lesson tồn tại
   - Validate tất cả fields như Create
   - Set UpdatedAt
   - Return lesson đã cập nhật

5. **`DeleteAsync(int id)`**
   - Validate: Lesson tồn tại
   - Xóa lesson
   - Return true

6. **`GetTotalDurationAsync(int courseId)`**
   - Validate: Course tồn tại
   - Return tổng Duration của tất cả lessons
   - Return 0 nếu không có lesson

---

## **PHẦN 3: API CONTROLLERS (3 điểm)**

### **Yêu cầu: CoursesController**

Tạo `CoursesController` với các endpoint:

1. **`[HttpGet]` GetAll()**
   - Call `CourseService.GetAllAsync()`
   - Return 200 với format: `{ success: bool, count: int, data: Course[] }`

2. **`[HttpGet("level/{level}")]` GetByLevel(string level)**
   - Call `CourseService.GetByLevelAsync(level)`
   - Return 200 nếu success
   - Catch `ArgumentException` → Return 400

3. **`[HttpGet("{id}")]` GetById(int id)**
   - Call `CourseService.GetByIdAsync(id)`
   - Return 404 nếu không tìm thấy
   - Return 200 nếu found

4. **`[HttpPost]` Create([FromBody] Course course)**
   - Call `CourseService.CreateAsync(course)`
   - Return 201 (CreatedAtAction)
   - Error handling: 400, 500

5. **`[HttpPut("{id}")]` Update(int id, [FromBody] Course course)**
   - Call `CourseService.UpdateAsync(id, course)`
   - Return 200 nếu success
   - Error handling: 400, 404, 500

6. **`[HttpDelete("{id}")]` Delete(int id)**
   - Call `CourseService.DeleteAsync(id)`
   - Return 200
   - Error handling: 404, 500

7. **`[HttpGet("{id}/lesson-count")]` GetLessonCount(int id)**
   - Call `CourseService.GetLessonCountAsync(id)`
   - Return 200 với format: `{ success: bool, lessonCount: int }`
   - Return 404 nếu course không tồn tại

8. **`[HttpGet("statistics/average-price")]` GetAveragePrice()**
   - Call `CourseService.GetAveragePriceAsync()`
   - Return 200 với format: `{ success: bool, averagePrice: decimal }`

### **Yêu cầu: LessonsController**

Tạo `LessonsController` với các endpoint:

1. **`[HttpGet("course/{courseId}")]` GetByCourse(int courseId)**
   - Call `LessonService.GetAllByCourseAsync(courseId)`
   - Return 200 với format: `{ success: bool, count: int, data: Lesson[] }`
   - Return 404 nếu course không tồn tại

2. **`[HttpGet("{id}")]` GetById(int id)**
   - Call `LessonService.GetByIdAsync(id)`
   - Return 404 nếu không tìm thấy
   - Return 200 nếu found

3. **`[HttpPost]` Create([FromBody] Lesson lesson)**
   - Call `LessonService.CreateAsync(lesson)`
   - Return 201
   - Error handling: 400, 404, 500

4. **`[HttpPut("{id}")]` Update(int id, [FromBody] Lesson lesson)**
   - Call `LessonService.UpdateAsync(id, lesson)`
   - Return 200
   - Error handling: 400, 404, 500

5. **`[HttpDelete("{id}")]` Delete(int id)**
   - Call `LessonService.DeleteAsync(id)`
   - Return 200
   - Error handling: 404, 500

6. **`[HttpGet("course/{courseId}/total-duration")]` GetTotalDuration(int courseId)**
   - Call `LessonService.GetTotalDurationAsync(courseId)`
   - Return 200 với format: `{ success: bool, totalDurationMinutes: int }`
   - Return 404 nếu course không tồn tại

---

## **PHẦN 4: CONFIGURATION & MIGRATIONS (2 điểm)**

### **Yêu cầu:**

1. **Program.cs:**
   - Add DbContext (CourseDbContext) với SQL Server
   - Đăng ký Services vào DI Container
   - Add CORS
   - Add Swagger

2. **appsettings.json:**
   - ConnectionString cho `CourseDb`
   - Cấu hình Logging

3. **Migrations:**
   - Tạo migration `InitialCreate`
   - Database phải có 10 courses + 30 lessons
   - Cascade delete được thiết lập đúng

---

## **PHẦN 5: KIỂM TRA CHỨC NĂNG (2 điểm)**

### **Yêu cầu:**

Test các chức năng sau bằng Swagger UI hoặc Postman:

#### **Test Cases for Courses:**

1. ✓ GET `/api/courses` → Trả về 10 courses
2. ✓ GET `/api/courses/level/Beginner` → Trả về courses cấp "Beginner"
3. ✓ GET `/api/courses/level/Invalid` → Return 400 "Mức độ không hợp lệ"
4. ✓ GET `/api/courses/1` → Trả về course với lessons
5. ✓ POST `/api/courses` (valid) → Tạo course thành công
6. ✓ POST `/api/courses` (invalid level) → Return 400 "Mức độ không hợp lệ"
7. ✓ POST `/api/courses` (price out of range) → Return 400 "Giá phải 0-100000"
8. ✓ PUT `/api/courses/1` → Cập nhật thành công
9. ✓ DELETE `/api/courses/1` → Xóa thành công (cả lessons)
10. ✓ GET `/api/courses/1/lesson-count` → Trả về số lessons = 3
11. ✓ GET `/api/courses/statistics/average-price` → Trả về giá trung bình

#### **Test Cases for Lessons:**

1. ✓ GET `/api/lessons/course/1` → Trả về 3 lessons của course 1
2. ✓ GET `/api/lessons/course/999` → Return 404 "Course không tồn tại"
3. ✓ GET `/api/lessons/1` → Trả về lesson 1 với course info
4. ✓ POST `/api/lessons` (valid) → Tạo lesson thành công
5. ✓ POST `/api/lessons` (invalid title) → Return 400 "Tiêu đề phải 3-200 ký tự"
6. ✓ POST `/api/lessons` (invalid courseId) → Return 404 "Course không tồn tại"
7. ✓ PUT `/api/lessons/1` → Cập nhật thành công
8. ✓ DELETE `/api/lessons/1` → Xóa thành công
9. ✓ GET `/api/lessons/course/1/total-duration` → Trả về tổng thời lượng (phút)

---

---

# **HƯỚNG DẪN CHẤM ĐIỂM**

## **Tiêu chí chấm:**

### **PHẦN 1: DATABASE (2 điểm)**
- ✓ Entity models đầy đủ, có đúng properties: 0.5 điểm
- ✓ Data Annotations validation: 0.5 điểm
- ✓ DbContext & Fluent API: 0.5 điểm
- ✓ Seed data (10 + 30 records): 0.5 điểm

### **PHẦN 2: SERVICE LAYER (3 điểm)**
- ✓ Interfaces định nghĩa đúng: 0.5 điểm
- ✓ Validate input đầy đủ: 1 điểm
- ✓ Business logic đúng (N+1 fix, relationships): 1 điểm
- ✓ Exception handling: 0.5 điểm

### **PHẦN 3: CONTROLLERS (3 điểm)**
- ✓ Tất cả endpoints được tạo: 1 điểm
- ✓ Response format đúng (success, data): 1 điểm
- ✓ Error handling (400, 404, 500): 1 điểm

### **PHẦN 4: CONFIGURATION (2 điểm)**
- ✓ Program.cs setup đúng: 1 điểm
- ✓ appsettings.json & Migrations: 1 điểm

### **PHẦN 5: KIỂM TRA CHỨC NĂNG (2 điểm)**
- ✓ Chạy được ứng dụng & seed data: 0.5 điểm
- ✓ Tất cả test cases pass: 1.5 điểm

---

## **Điểm cộng thêm (+1 điểm):**
- ✓ Thêm pagination cho GetAll
- ✓ Thêm search/filter functionality
- ✓ Thêm unit tests (ít nhất 5 test cases)
- ✓ Thêm logging (Serilog hoặc built-in)
- ✓ Clean code, proper naming conventions

---

## **Yêu cầu nộp bài:**

1. Source code đầy đủ (Models, Services, Controllers, DbContext)
2. File appsettings.json
3. Screenshot kết quả test (Swagger UI hoặc Postman)
4. Database schema (SQL Server)
5. Tệp báo cáo (nếu có) mô tả cách giải quyết

**Thời gian nộp:** Trước ngày _________ lúc _________ (GMT+7)

---

**Chúc các em làm bài tốt!** 💪
