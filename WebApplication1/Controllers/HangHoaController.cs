using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HangHoaController : ControllerBase
    {
        public static List<Hanghoa> hanghoas = new List<Hanghoa>();

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(hanghoas);
        }

        [HttpPost]
        public IActionResult Create(HanghoaVM hanghoaVM)
        {
            var hanghoa = new Hanghoa()
            {
                Mahanghoa = Guid.NewGuid(),
                TenHangHoa = hanghoaVM.TenHangHoa,
                Dongia = hanghoaVM.Dongia
            };
            hanghoas.Add(hanghoa);
            return Ok(hanghoas);

        }

        [HttpGet("id")]
        public IActionResult GetByID(string id)
        {
            try
            {
                var hh = hanghoas.FirstOrDefault(hh => hh.Mahanghoa == Guid.Parse(id));
                if (hh == null)
                {
                    return NotFound();
                }
                return Ok(hh);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(string id, Hanghoa hanghoa)
        {
            try
            {
                var hh = hanghoas.FirstOrDefault(hh => hh.Mahanghoa == Guid.Parse(id));
                if (hh == null)
                {
                    return NotFound();
                }
                if (id != hanghoa.Mahanghoa.ToString())
                {
                    return BadRequest();
                }
                hh.TenHangHoa = hanghoa.TenHangHoa;
                hh.Dongia = hanghoa.Dongia;
                return Ok();
            }
            catch
            {
                return BadRequest();
            }

        }

        [HttpDelete("id")]
        public IActionResult Delete(string id)
        {
            try
            {
                var hh = hanghoas.FirstOrDefault(hh => hh.Mahanghoa == Guid.Parse(id));
                if (hh == null)
                {
                    return NotFound();
                }
                else
                {
                    hanghoas.Remove(hh);
                }
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }


    }
}
