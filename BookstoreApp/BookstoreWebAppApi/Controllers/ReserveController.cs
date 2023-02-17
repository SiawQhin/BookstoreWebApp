using AutoMapper;
using BookstoreWebAppApi.Data;
using BookstoreWebAppApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookstoreWebAppApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReserveController : ControllerBase
    {
        private readonly QueryDatabaseContext _dbQuery;
        private readonly IMapper _mapper;

        public ReserveController(QueryDatabaseContext db, IMapper mapper)
        {
            _dbQuery = db;
            _mapper = mapper;
        }

        [HttpPost]
        public IActionResult Reserve([FromBody] ReserveDto reserveDto)
        {
            Console.WriteLine($"Book Id = {reserveDto.bookId}, User Id = {reserveDto.userId}");
            var booking = _mapper.Map<Booking>(reserveDto);
            _dbQuery.Bookings.Add(booking);
            _dbQuery.SaveChanges();
            return Ok("Success");
        }
    }

}
