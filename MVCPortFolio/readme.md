# 기능 소개



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
- View를 통하여 Detail, Create, Edit, Delete 
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



## 연락처
<p align='center'>
  <img width="70%" height="50%" src="https://github.com/WhiteHair-H/Asp.net/blob/main/MVCPortFolio/IntroFile/GIF/%EC%97%B0%EB%9D%BD%EC%B2%98.gif">
</p>


## 회원가입
<p align='center'>
  <img width="70%" height="50%" src="https://github.com/WhiteHair-H/Asp.net/blob/main/MVCPortFolio/IntroFile/GIF/%ED%9A%8C%EC%9B%90%EA%B0%80%EC%9E%85.gif">
</p>


## 로그인 
<p align='center'>
  <img width="70%" height="50%" src="https://github.com/WhiteHair-H/Asp.net/blob/main/MVCPortFolio/IntroFile/GIF/%EB%A1%9C%EA%B7%B8%EC%9D%B8.gif">
</p>
