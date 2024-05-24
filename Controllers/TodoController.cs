using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApi.Models;

namespace todoapi_v00.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TodoController : ControllerBase
    {
        private readonly TodoContext _context;
        private readonly CategoryContext _contextCategories;
        private readonly ListContext _contextList;

        public TodoController(TodoContext context, CategoryContext contextCategories, ListContext contextList)
        {
            _context = context;
            _contextCategories = contextCategories;
            _contextList = contextList;
        }

        // GET: api/todo
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItem>>> GetTodoItems()
        {
            return await _context.TodoItems.ToListAsync();
        }

        // GET: api/todo/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItem>> GetTodoItem(int id)
        {
            var todoItem = await _context.TodoItems.FindAsync(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            return todoItem;
        }

        // GET: api/todo/{id}/Category
        [HttpGet("{id}/Category")]
        public async Task<ActionResult<Category>> GetListMembershipCategory(int id)
        {
            var list = await _context.TodoItems.FindAsync(id);

            Category? cat;
            if (list is not null)
            {
                cat = await _contextCategories.Categories.FindAsync(list.CategoryId);
            }
            else
            {
                return NotFound();
            }

            if (cat == null)
            {
                return NotFound();
            }

            return cat;
        }

        // GET: api/todo/{id}/List
        [HttpGet("{id}/List")]
        public async Task<ActionResult<List>> GetListMembershipList(int id)
        {
            var list = await _context.TodoItems.FindAsync(id);

            List? ls;
            if (list is not null)
            {
                ls = await _contextList.Lists.FindAsync(list.ListId);
            }
            else
            {
                return NotFound();
            }

            if (ls == null)
            {
                return NotFound();
            }

            return ls;
        }

        // POST: api/Todo
        [HttpPost]
        public async Task<ActionResult<TodoItem>> PostTodoItem(TodoItem item)
        {
            _context.TodoItems.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTodoItem), new { id = item.Id }, item);
        }

        // PUT: api/Todo/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodoItem(int id, TodoItem item)
        {
            if (id != item.Id)
            {
                return BadRequest();
            }

            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Todo/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(int id)
        {
            var todoItem = await _context.TodoItems.FindAsync(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            _context.TodoItems.Remove(todoItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}