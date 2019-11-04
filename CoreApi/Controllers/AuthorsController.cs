using CoreApi.Entities;
using CoreApi.Models;
using CoreApi.Reposirories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

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
        public ActionResult Add([Required][FromBody] AddAuthorVM author)
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

        [HttpPost]
        [ProducesResponseType(typeof(void), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        public ActionResult Update([Required][FromBody] AddAuthorVM author)
        {
            if (string.IsNullOrEmpty(author.Trigram))
                return BadRequest();

            var existingAuthor = UnitOfWork.AuthorRepository.Get(x => x.Trigram.ToLower().Equals(author.Trigram.ToLower()), null);
            if(existingAuthor == null)
                return BadRequest();

            existingAuthor.FirstName = author.FirstName;
            existingAuthor.LastName = author.LastName;            

            UnitOfWork.AuthorRepository.Update(existingAuthor);
            return Ok();

        }

        [HttpDelete("{trigram}")]
        [ProducesResponseType(typeof(void), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult Delete(string trigram)
        {
            if (string.IsNullOrEmpty(trigram))
                return BadRequest();

            var existingAuthor = UnitOfWork.AuthorRepository.Get(x => x.Trigram.ToLower().Equals(trigram.ToLower()), null);
            if (existingAuthor == null)
                return NoContent();

            UnitOfWork.AuthorRepository.Delete(existingAuthor) ;
            return Ok();
        }
    }
}
