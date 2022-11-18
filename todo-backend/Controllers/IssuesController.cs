using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using todo_backend.Models;
using todo_backend;

namespace test.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IssuesController : ControllerBase
    {
        private readonly DataContext _context;

        public IssuesController(DataContext context)
        {
            _context = context;
        }


        [HttpPost]
        public async Task<IActionResult> Create(IssueRequest req)
        {
            try
            {
                var _issue = new Issue()
                {
                    Subject = req.Subject,
                    Description = req.Message,
                    Mail = req.Mail
                };
                _context.Add(_issue);
                await _context.SaveChangesAsync();

                var issue = await _context.Issues.Include(x => x.Status).FirstOrDefaultAsync(x => x.Id == _issue.Id);
                var response = new IssueResponse
                {
                    Id = issue.Id,
                    Created = issue.Created,
                    Subject = issue.Subject,
                    Message = issue.Description,
                    Status = issue.Status.Name,
                    Mail = issue.Mail
                };

                return new OkObjectResult(response);
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
            return new BadRequestResult();
        }



        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var issues = new List<IssueResponse>();
            foreach (var issue in await _context.Issues.Include(x => x.Status).ToListAsync())
                issues.Add(new IssueResponse
                {
                    Id = issue.Id,
                    Created = issue.Created,
                    Subject = issue.Subject,
                    Message = issue.Description,
                    Status = issue.Status.Name,
                    Mail = issue.Mail
                });

            return new OkObjectResult(issues);
        }



        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var issue = await _context.Issues.Include(x => x.Status).FirstOrDefaultAsync(x => x.Id == id);
                if (issue != null)
                    return new OkObjectResult(new IssueResponse
                    {
                        Id = issue.Id,
                        Created = issue.Created,
                        Subject = issue.Subject,
                        Message = issue.Description,
                        Status = issue.Status.Name,
                        Mail = issue.Mail
                    });
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
            return new NotFoundResult();
        }



        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, IssueUpdateRequest req)
        {
            try
            {
                var _issue = await _context.Issues.FindAsync(id);
                _issue.StatusId = req.StatusId;
                _issue.Subject = req.Subject;
                _issue.Description = req.Message;
                _issue.Mail = req.Mail;

                _context.Entry(_issue).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                var issue = await _context.Issues.Include(x => x.Status).FirstOrDefaultAsync(x => x.Id == _issue.Id);
                return new OkObjectResult(new IssueResponse
                {
                    Id = issue.Id,
                    Created = issue.Created,
                    Subject = issue.Subject,
                    Message = issue.Description,
                    Status = issue.Status.Name,
                    Mail = issue.Mail
                });
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
            return new NotFoundResult();
        }

    }
}
