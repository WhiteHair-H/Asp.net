using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCPortFolio.Data;
using MVCPortFolio.Models;
using X.PagedList;

namespace MVCPortFolio.Controllers
{
    public class BoardController : Controller
    {
        private readonly MVCPortFolioContext _context;

        public BoardController(MVCPortFolioContext context)
        {
            _context = context;
        }

        /// <summary>
        /// 첫번째 작업은 인덱스 창 - 페이지 수 및 사이즈 조정
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>

        // GET: Board
        public async Task<IActionResult> Index(int? page)
        {
            var pageNumber = page ?? 1; /*page값이 널이면 1*/
            var pageSize = 10; // 페이지 사이즈

            var boards = await _context.Board.ToPagedListAsync(pageNumber, pageSize);

            return View(boards);
        }

        // GET: Board/Details/5
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

        // GET: Board/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Board/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,Subject,Contents,Writer,RegDate,ReadCount")] Board board)
        {
            if (ModelState.IsValid)
            {
                DateTime now = DateTime.Now;
                board.RegDate = now;
                board.ReadCount = 0;
                _context.Add(board);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(board);
        }

        // GET: Board/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var board = await _context.Board.FindAsync(id);
            if (board == null)
            {
                return NotFound();
            }
            return View(board);
        }

        // POST: Board/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,Subject,Contents,Writer,RegDate,ReadCount")] Board board)
        {
            if (id != board.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(board);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BoardExists(board.id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(board);
        }

        // GET: Board/Delete/5
        public async Task<IActionResult> Delete(int? id)
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

            return View(board);
        }

        // POST: Board/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var board = await _context.Board.FindAsync(id);
            _context.Board.Remove(board);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BoardExists(int id)
        {
            return _context.Board.Any(e => e.id == id);
        }
    }
}
