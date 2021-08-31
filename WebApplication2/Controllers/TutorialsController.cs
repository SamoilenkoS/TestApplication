using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLayer.Models;

namespace WebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TutorialsController : ControllerBase
    {
        private static List<Tutorial> _tutorials;

        static TutorialsController()
        {
            _tutorials = new List<Tutorial>
            {
                new Tutorial
                {
                    Id = 1,
                    CreatedAt = DateTime.Now,
                    Description = "Test",
                    Published = true,
                    Title = "First",
                    UpdatedAt = DateTime.Now
                },
                new Tutorial
                {
                    Id = 2,
                    CreatedAt = DateTime.Now.Subtract(TimeSpan.FromDays(1)),
                    Description = "Test",
                    Published = true,
                    Title = "Second",
                    UpdatedAt = DateTime.Now.Subtract(TimeSpan.FromDays(1)),
                }
            };
        }

        [HttpGet]
        public async Task<IEnumerable<Tutorial>> GetAllAsync()
        {
            return _tutorials;
        }

        [HttpGet("{id}")]
        public async Task<Tutorial> GetByIdAsync(int id)
        {
            return _tutorials.FirstOrDefault(x => x.Id == id);
        }

        [HttpPost]
        public async Task AddTutorial(Tutorial tutorial)
        {
            tutorial.Id = _tutorials.Count + 1;
            tutorial.CreatedAt = DateTime.Now;
            tutorial.UpdatedAt = tutorial.CreatedAt;

            _tutorials.Add(tutorial);
        }

        [HttpPut]
        public async Task UpdateTutorial(Tutorial tutorial)
        {
            var index = _tutorials.FindIndex(x => x.Id == tutorial.Id);
            if (index != -1)
            {
                tutorial.CreatedAt = _tutorials[index].CreatedAt;
                tutorial.UpdatedAt = DateTime.Now;

                _tutorials[index] = tutorial;
            }
        }

        [HttpDelete]
        public async Task Clear()
        {
            _tutorials.Clear();
        }

        [HttpDelete("{id}")]
        public async Task Remove(int id)
        {
            var index = _tutorials.FindIndex(x => x.Id == id);
            if (index != -1)
            {
                _tutorials.RemoveAt(index);
            }
        }
    }
}
