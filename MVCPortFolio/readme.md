# 기능 소개

## 🏳Contents
- [게시판](#게시판)
- [연락처](#연락처)
- [회원가입](#회원가입)
- [로그인](#로그인)

## 게시판
  
<p align='center'>
  <img width="70%" height="50%" src="https://github.com/WhiteHair-H/Asp.net/blob/main/MVCPortFolio/IntroFile/GIF/%EA%B2%8C%EC%8B%9C%ED%8C%90.gif">
</p>

<br>

### Controller
- 스캐풀딩을 통해서 Create, Detail, Delete , Edit 기능 추가
```
public async Task<IActionResult> Create([Bind("id,Subject,Contents,Writer,RegDate,ReadCount")] Board board)
public async Task<IActionResult> Details(int? id)
public async Task<IActionResult> Delete(int? id)
public async Task<IActionResult> Edit(int id, [Bind("id,Subject,Contents,Writer,RegDate,ReadCount")] Board board)
```

- 클릭시 조회수 UP!
```
public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var board = await _context.Board
                .FirstOrDefaultAsync(m => m.id == id);
            if (board == null)
            {
                return NotFound();
            }

            // Readcount 증가
            board.ReadCount += 1; // 1씩 증가
            _context.Board.Update(board); // DB board에 업데이트
            _context.SaveChanges(); // 바뀐부분을 저장

            return View(board);
        }
```

<br>

### DB연결
```
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=127.0.0.1;Initial Catalog=MVCPortFolio;User ID=sa;Password=*********;"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft": "Warning",
      "Microsoft.Hosting.Lifetime": "Information"
    }
  },
  "AllowedHosts": "*"
}
```


<br>

### Models
- DB와 연동작업을 위해서 모델생성
```
public class Board
    {
        [Key]
        public int id { get; set; }

        [Required(ErrorMessage = "제목을 입력하세요")]
        [DataType(DataType.Text)]
        [StringLength(250)]
        public string Subject { get; set; }

        [DataType(DataType.Text)]
        public string Contents { get; set; }

        [Required(ErrorMessage = "작성자를 입력하세요")]
        [DataType(DataType.Text)]
        [StringLength(25)]
        public string Writer { get; set; }
        public DateTime RegDate { get; set; }
        public int ReadCount { get; set; }
    }
```

<br>

### Views
- View를 통하여 Detail, Create, Edit, Delete 생성


```
<tbody>
    @foreach (var item in Model)
    {
        <tr>
            <td>
                @Html.DisplayFor(modelItem => item.id)
            </td>
            <td>
                @*제목을 눌렀을 때 Details칸으로 이동*@
                <a asp-action="Details" asp-route-id="@item.id">
                    @Html.DisplayFor(modelItem => item.Subject)
                </a>
            </td>
            <td>
                @Html.DisplayFor(modelItem => item.Writer)
            </td>
            <td>
                @Html.DisplayFor(modelItem => item.RegDate)
            </td>
            <td>
                @Html.DisplayFor(modelItem => item.ReadCount)
            </td>
            <td>
                <a asp-action="Edit" asp-route-id="@item.id">수정</a> /
                <a asp-action="Details" asp-route-id="@item.id">자세히</a> /
                <a asp-action="Delete" asp-route-id="@item.id">삭제</a>
            </td>
        </tr>
    }
</tbody>
```

<br>

## 연락처
<p align='center'>
  <img width="70%" height="50%" src="https://github.com/WhiteHair-H/Asp.net/blob/main/MVCPortFolio/IntroFile/GIF/%EC%97%B0%EB%9D%BD%EC%B2%98.gif">
</p>

### Controller
- 스캐풀딩을 통해서 Index 기능 추가
```
public async Task<IActionResult> Index([Bind("Id,Name,Email,Contents")] Contact contact)
```
- 이름, 이메일, 내용 저장 기능 추가
```
public async Task<IActionResult> Index([Bind("Id,Name,Email,Contents")] Contact contact)
    {
        if (ModelState.IsValid)
        {
            try
            {
                contact.Regdate = DateTime.Now;
                _context.Add(contact); // 메모리상에 데이터가 올라감
                await _context.SaveChangesAsync(); // DB저장 , 커밋

                ViewBag.Message = "감사합니다. 연락드리겠습니다.";
                ModelState.Clear();
            }
            catch (Exception ex)
            {
                ViewBag.Message = $"예외가 발생했습니다. {ex.InnerException}";
                ModelState.Clear();
            }
        }
        return View();
    }
```

<br>

### Models
- DB와 연동작업을 위해서 모델생성
```
public class Contact
  {
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "성함은 필수입니다")]
    [DataType(DataType.Text)]
    [StringLength(50)]
    public string Name { get; set; }

    [Required(ErrorMessage = "이메일은 필수입니다")]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }

    [Required(ErrorMessage = "내용은 필수입니다")]
    [DataType(DataType.Text)]
    public string Contents { get; set; }

    [DataType(DataType.DateTime)]
    public DateTime Regdate { get; set; }
  }
```

<br>

### Views
- 연락처 뷰 
```
<div class="input-field message">
    <span class="input-icon"><i class="tf-pencil2"></i></span>
    <textarea name="Contents" id="message" asp-for="Contents" class="form-control" placeholder="Write your message"></textarea>
    <span asp-validation-for="Contents" class="text-danger"></span>
</div>
<div class="input-field">
    <span class="btn-border">
        <button type="submit" class="btn btn-primary btn-custom-border text-uppercase">Send Message now</button>
    </span>
</div>
<div class="col">
    @if (ViewBag.Message != null)
    {
        <span>@ViewBag.Message</span>
    }
</div>
```

<br>

## 회원가입
<p align='center'>
  <img width="70%" height="50%" src="https://github.com/WhiteHair-H/Asp.net/blob/main/MVCPortFolio/IntroFile/GIF/%ED%9A%8C%EC%9B%90%EA%B0%80%EC%9E%85.gif">
</p>

<br>

### Controller
- 스캐풀딩을 통해서 SignUp 기능 추가
```
public async Task<IActionResult> SignUp([Bind("UserName,Email,Password")] User user)
```

<br>

### Models
- DB와 연동작업을 위해서 모델생성
```
public class User
    {
        // 유저번호
        [Key]
        public int UserNo { get; set; }
        // 유저이름
        [Required(ErrorMessage = "이름을 입력하세요!")]
        [DataType(DataType.Text)]
        [StringLength(200)]
        public string UserName { get; set; }
        // 유저이메일
        [Required(ErrorMessage = "이메일을 입력하세요!")]
        [DataType(DataType.EmailAddress)]
        [StringLength(200)]
        public string Email { get; set; }
        // 유저패스워드
        [Required(ErrorMessage = "비밀번호를 입력하세요!")]
        [DataType(DataType.Password)]
        [StringLength(200)]
        public string Password { get; set; }
    }
```

<br>
### Views
- 회원가입 뷰 생성
```
<form asp-action="SignUp" class="form-signup">
    <h2 class="form-signin-heading">SignUp</h2>

    <input asp-for="UserName" type="text" class="form-control" name="UserName"
           placeholder="UserName" autofocus="" />

    <input asp-for="Email" type="email" class="form-control" name="Email"
           placeholder="Email" style="margin-bottom:0" />

    <input asp-for="Password" type="password" class="form-control" name="Password" placeholder="Password" />

    <div>
        <button asp-controller="Account" asp-action="SignUp" class="btn btn-lg btn-primary btn-block" type="submit">Sign-Up</button>
    </div>

</form>
```

<br>

## 로그인 
<p align='center'>
  <img width="70%" height="50%" src="https://github.com/WhiteHair-H/Asp.net/blob/main/MVCPortFolio/IntroFile/GIF/%EB%A1%9C%EA%B7%B8%EC%9D%B8.gif">
</p>

### Controller
- 스캐풀딩을 통해서 Create, Detail, Delete , Edit 기능 추가
```
public async Task<IActionResult> Create([Bind("id,Subject,Contents,Writer,RegDate,ReadCount")] Board board)
public async Task<IActionResult> Details(int? id)
public async Task<IActionResult> Delete(int? id)
public async Task<IActionResult> Edit(int id, [Bind("id,Subject,Contents,Writer,RegDate,ReadCount")] Board board)
```

<br>

### Models
- DB와 연동작업을 위해서 모델생성
```
public async Task<IActionResult> Login([Bind("Email,Password")] User user)
        {

            if (!ModelState.IsValid)
            {
                var result = checkAccount(user.Email, user.Password);
                if (result == null)
                {
                    // 계정이 없을 경우 화면을 Login 제자리
                    ViewBag.Message = "해당계정이 없습니다";
                    return View("Login");
                }
                else
                {
                    // 로그인할 경우 Home/Home으로 이동
                    HttpContext.Session.SetString("UserEmail", result.Email);
                    return RedirectToAction("Home", "Home");
                }
            }

            return View("Login");

        }
```
- DB에 저장되어 있는 Email과 Password가 일치여부
```
private User checkAccount(string email, string password)
{
    return _context.User.SingleOrDefault(a => a.Email.Equals(email) && a.Password.Equals(password));
}
```
- 로그아웃
```
public IActionResult Logout()
{
    HttpContext.Session.Clear();
    return RedirectToAction("Home", "Home"); // Home/Home으로 이동
}
```

<br>

### Views
- 로그인 뷰 생성
```
<h2 class="form-signin-heading">Login</h2>
<input asp-for="Email" type="email" class="form-control" name="email" placeholder="Email Address" autofocus="" />
<span asp-validation-for="Email" class="text-danger"></span>

<input asp-for="Password" type="password" class="form-control" name="password" placeholder="Password" style="margin-bottom:0"/>
<span asp-validation-for="Password" class="text-danger"></span>

<button asp-action="Login" class="btn btn-lg btn-primary btn-block" type="submit"
        style="margin-top:10px">Login</button>
<div class="label-primary"
     style="text-align:center; color:white;
            margin-top:10px; border-radius:5px;
            margin-bottom:10px;
            font-weight:bolder;">
    @if (ViewBag.Message != null)
    {
        <span>@ViewBag.Message</span>
    }
</div>
```

