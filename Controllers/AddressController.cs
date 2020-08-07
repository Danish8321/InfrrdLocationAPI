using LocationFilter.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LocationFilter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly DataContext _ctx;

        public AddressController(DataContext context)
        {
            _ctx = context;
        }

        [HttpPost]
        public async Task<IActionResult> Address(Location location)
        {
            try
            {
                _ctx.Locations.Add(location);
                await _ctx.SaveChangesAsync();
                return CreatedAtRoute("GetLocation", new { location.Id }, location);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpGet("{keyword}")]
        public async Task<IActionResult> Address(string keyword, string sort)
        {
            // number of characters matched in address) * 1 + (number of characters matched in city ) * 2 + (number of characters matched in state) * 3
            try
            {
                if (keyword.Length < 3)
                {
                    return BadRequest("Minimum 3 character is required");
                }

                var data = _ctx.Locations.Where(v => v.Address.Contains(keyword)
                || v.City.Contains(keyword)
                || v.State.Contains(keyword)
                || v.Zip.Contains(keyword)).ToList();

                if (sort == "Address" || sort == null || string.IsNullOrEmpty(sort))
                {
                    data = data.OrderBy(p => p.Address).ToList();
                }
                else
                {
                    data = data.OrderBy(p => Frequency(p, keyword)).ToList();
                }

                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpGet("GetLocation/{id}", Name = "GetLocation")]
        public async Task<IActionResult> GetLocation(int id)
        {
            try
            {
                var data = await _ctx.Locations.FirstOrDefaultAsync(p => p.Id == id);

                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        private int Frequency(Location data, string keyword)
        {
            var count = (MatchingCharacterLength(data.Address, keyword) * 1) + (MatchingCharacterLength(data.City, keyword) * 2) + (MatchingCharacterLength(data.State, keyword) * 3);
            return count;
        }

        private int MatchingCharacterLength(string word, string keyword)
        {
            int count = 0;
            for (int i = 0; i < keyword.Length; i++)
            {
                for (int j = 0; j < word.Length; j++)
                {
                    if (keyword[i] == word[j])
                    {
                        count++;
                    }
                }
            }
            return count;
        }

    }
}
