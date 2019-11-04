using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CoreApi.Entities;
using CoreApi.Models;
using CoreApi.Reposirories;
using Microsoft.AspNetCore.Mvc;

namespace CoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        public AuthorsController(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        private IUnitOfWork UnitOfWork { get; }
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<Author>> Get()
        {
            var result = UnitOfWork.AuthorRepository.GetAll(x => x.Books).ToList();
            return Ok(result);
        }

        [HttpGet("{trigram}")]
        [ProducesResponseType(typeof(Author), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        public ActionResult<Author> GetByTrigram(string trigram)
        {
            if (string.IsNullOrEmpty(trigram))
                return BadRequest();

            return Ok(UnitOfWork.AuthorRepository.Get(x=>x.Trigram.ToLower().Equals(trigram.ToLower()), x => x.Books));

        }

        [HttpPut]
        [ProducesResponseType(typeof(void), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        public ActionResult Put([Required][FromBody] AddAuthorVM author)
        {
            if (string.IsNullOrEmpty(author.Trigram))
                return BadRequest();

            UnitOfWork.AuthorRepository.Add(new Author
            {
                LastName = author.LastName,
                FirstName = author.FirstName,
                Trigram = author.Trigram.ToUpper()
            });
            return Ok();
        }

        //// GET api/values/5
        //[HttpGet("{id}")]
        //public ActionResult<string> Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/values
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT api/values/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/values/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
