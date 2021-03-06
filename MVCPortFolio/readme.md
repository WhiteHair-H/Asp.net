# ๊ธฐ๋ฅ ์๊ฐ

## ๐ณContents
- [๊ฒ์ํ](#๊ฒ์ํ)
- [์ฐ๋ฝ์ฒ](#์ฐ๋ฝ์ฒ)
- [ํ์๊ฐ์](#ํ์๊ฐ์)
- [๋ก๊ทธ์ธ](#๋ก๊ทธ์ธ)

## ๊ฒ์ํ
  
<p align='center'>
  <img width="70%" height="50%" src="https://github.com/WhiteHair-H/Asp.net/blob/main/MVCPortFolio/IntroFile/GIF/%EA%B2%8C%EC%8B%9C%ED%8C%90.gif">
</p>

<br>

### Controller

<p align='center'>์ค์บํ๋ฉ์ ํตํด์ Create, Detail, Delete , Edit ๊ธฐ๋ฅ ์ถ๊ฐ</p>

```C#
public async Task<IActionResult> Create([Bind("id,Subject,Contents,Writer,RegDate,ReadCount")] Board board)
public async Task<IActionResult> Details(int? id)
public async Task<IActionResult> Delete(int? id)
public async Task<IActionResult> Edit(int id, [Bind("id,Subject,Contents,Writer,RegDate,ReadCount")] Board board)
```

<p align='center'>ํด๋ฆญ์ ์กฐํ์ UP!</p>


```C#
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

            // Readcount ์ฆ๊ฐ
            board.ReadCount += 1; // 1์ฉ ์ฆ๊ฐ
            _context.Board.Update(board); // DB board์ ์๋ฐ์ดํธ
            _context.SaveChanges(); // ๋ฐ๋๋ถ๋ถ์ ์ ์ฅ

            return View(board);
        }
```

<br>

### DB์ฐ๊ฒฐ

<p align='center'>ConnectionStrings ์ฌ์ฉ</p>

```JSON
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

<p align='center'>DB์ ์ฐ๋์์์ ์ํด์ ๋ชจ๋ธ์์ฑ</p>

```C#
public class Board
    {
        [Key]
        public int id { get; set; }

        [Required(ErrorMessage = "์ ๋ชฉ์ ์๋ ฅํ์ธ์")]
        [DataType(DataType.Text)]
        [StringLength(250)]
        public string Subject { get; set; }

        [DataType(DataType.Text)]
        public string Contents { get; set; }

        [Required(ErrorMessage = "์์ฑ์๋ฅผ ์๋ ฅํ์ธ์")]
        [DataType(DataType.Text)]
        [StringLength(25)]
        public string Writer { get; set; }
        public DateTime RegDate { get; set; }
        public int ReadCount { get; set; }
    }
```

<br>

### Views

<p align='center'>View๋ฅผ ํตํ์ฌ Detail, Create, Edit, Delete ์์ฑ</p>

```HTML
<tbody>
    @foreach (var item in Model)
    {
        <tr>
            <td>
                @Html.DisplayFor(modelItem => item.id)
            </td>
            <td>
                @*์ ๋ชฉ์ ๋๋ ์ ๋ Details์นธ์ผ๋ก ์ด๋*@
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
                <a asp-action="Edit" asp-route-id="@item.id">์์ </a> /
                <a asp-action="Details" asp-route-id="@item.id">์์ธํ</a> /
                <a asp-action="Delete" asp-route-id="@item.id">์ญ์ </a>
            </td>
        </tr>
    }
</tbody>
```

<br>

## ์ฐ๋ฝ์ฒ
<p align='center'>
  <img width="70%" height="50%" src="https://github.com/WhiteHair-H/Asp.net/blob/main/MVCPortFolio/IntroFile/GIF/%EC%97%B0%EB%9D%BD%EC%B2%98.gif">
</p>

### Controller
<p align='center'>์ค์บํ๋ฉ์ ํตํด์ Index ๊ธฐ๋ฅ ์ถ๊ฐ</p>

```C#
public async Task<IActionResult> Index([Bind("Id,Name,Email,Contents")] Contact contact)
```

<p align='center'>์ด๋ฆ, ์ด๋ฉ์ผ, ๋ด์ฉ ์ ์ฅ ๊ธฐ๋ฅ ์ถ๊ฐ</p>

```C#
public async Task<IActionResult> Index([Bind("Id,Name,Email,Contents")] Contact contact)
    {
        if (ModelState.IsValid)
        {
            try
            {
                contact.Regdate = DateTime.Now;
                _context.Add(contact); // ๋ฉ๋ชจ๋ฆฌ์์ ๋ฐ์ดํฐ๊ฐ ์ฌ๋ผ๊ฐ
                await _context.SaveChangesAsync(); // DB์ ์ฅ , ์ปค๋ฐ

                ViewBag.Message = "๊ฐ์ฌํฉ๋๋ค. ์ฐ๋ฝ๋๋ฆฌ๊ฒ ์ต๋๋ค.";
                ModelState.Clear();
            }
            catch (Exception ex)
            {
                ViewBag.Message = $"์์ธ๊ฐ ๋ฐ์ํ์ต๋๋ค. {ex.InnerException}";
                ModelState.Clear();
            }
        }
        return View();
    }
```

<br>

### Models
<p align='center'>DB์ ์ฐ๋์์์ ์ํด์ ๋ชจ๋ธ์์ฑ</p>

```C#
public class Contact
  {
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "์ฑํจ์ ํ์์๋๋ค")]
    [DataType(DataType.Text)]
    [StringLength(50)]
    public string Name { get; set; }

    [Required(ErrorMessage = "์ด๋ฉ์ผ์ ํ์์๋๋ค")]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }

    [Required(ErrorMessage = "๋ด์ฉ์ ํ์์๋๋ค")]
    [DataType(DataType.Text)]
    public string Contents { get; set; }

    [DataType(DataType.DateTime)]
    public DateTime Regdate { get; set; }
  }
```

<br>

### Views
<p align='center'>์ฐ๋ฝ์ฒ ๋ทฐ</p>

```C#
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

## ํ์๊ฐ์
<p align='center'>
  <img width="70%" height="50%" src="https://github.com/WhiteHair-H/Asp.net/blob/main/MVCPortFolio/IntroFile/GIF/%ED%9A%8C%EC%9B%90%EA%B0%80%EC%9E%85.gif">
</p>

<br>

### Controller

<p align='center'>์ค์บํ๋ฉ์ ํตํด์ SignUp ์์ฑ</p>

```C#
public async Task<IActionResult> SignUp([Bind("UserName,Email,Password")] User user)
```

<br>

### Models

<p align='center'>DB์ ์ฐ๋์์์ ์ํด์ ๋ชจ๋ธ์์ฑ</p>

```C#
public class User
    {
        // ์ ์ ๋ฒํธ
        [Key]
        public int UserNo { get; set; }
        // ์ ์ ์ด๋ฆ
        [Required(ErrorMessage = "์ด๋ฆ์ ์๋ ฅํ์ธ์!")]
        [DataType(DataType.Text)]
        [StringLength(200)]
        public string UserName { get; set; }
        // ์ ์ ์ด๋ฉ์ผ
        [Required(ErrorMessage = "์ด๋ฉ์ผ์ ์๋ ฅํ์ธ์!")]
        [DataType(DataType.EmailAddress)]
        [StringLength(200)]
        public string Email { get; set; }
        // ์ ์ ํจ์ค์๋
        [Required(ErrorMessage = "๋น๋ฐ๋ฒํธ๋ฅผ ์๋ ฅํ์ธ์!")]
        [DataType(DataType.Password)]
        [StringLength(200)]
        public string Password { get; set; }
    }
```

<br>

### Views

<p align='center'>ํ์๊ฐ์ ๋ทฐ ์์ฑ</p>

```HTML
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

## ๋ก๊ทธ์ธ 
<p align='center'>
  <img width="70%" height="50%" src="https://github.com/WhiteHair-H/Asp.net/blob/main/MVCPortFolio/IntroFile/GIF/%EB%A1%9C%EA%B7%B8%EC%9D%B8.gif">
</p>

### Controller

<p align='center'>์ค์บํ๋ฉ์ ํตํด์ Login ์์ฑ</p>

```C#
public async Task<IActionResult> Login([Bind("Email,Password")] User user)
```

<br>

### Models

<p align='center'>DB์ ์ฐ๋์์์ ์ํด์ ๋ชจ๋ธ์์ฑ</p>

```C#
public async Task<IActionResult> Login([Bind("Email,Password")] User user)
        {

            if (!ModelState.IsValid)
            {
                var result = checkAccount(user.Email, user.Password);
                if (result == null)
                {
                    // ๊ณ์ ์ด ์์ ๊ฒฝ์ฐ ํ๋ฉด์ Login ์ ์๋ฆฌ
                    ViewBag.Message = "ํด๋น๊ณ์ ์ด ์์ต๋๋ค";
                    return View("Login");
                }
                else
                {
                    // ๋ก๊ทธ์ธํ  ๊ฒฝ์ฐ Home/Home์ผ๋ก ์ด๋
                    HttpContext.Session.SetString("UserEmail", result.Email);
                    return RedirectToAction("Home", "Home");
                }
            }

            return View("Login");

        }
```

<p align='center'>DB์ ์ ์ฅ๋์ด ์๋ Email๊ณผ Password๊ฐ ์ผ์น์ฌ๋ถ</p>

```C#
private User checkAccount(string email, string password)
{
    return _context.User.SingleOrDefault(a => a.Email.Equals(email) && a.Password.Equals(password));
}
```

<p align='center'>๋ก๊ทธ์์ ์์ฑ</p>

```C#
public IActionResult Logout()
{
    HttpContext.Session.Clear();
    return RedirectToAction("Home", "Home"); // Home/Home์ผ๋ก ์ด๋
}
```

<br>

### Views

<p align='center'>๋ก๊ทธ์ธ ๋ทฐ ์์ฑ</p>

```HTML
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

