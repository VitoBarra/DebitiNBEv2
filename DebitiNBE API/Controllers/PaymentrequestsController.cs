using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DebitiNBE_API.Models;
using DebitiNBE_API.Models.DB;

namespace DebitiNBE_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentrequestsController : ControllerBase
    {
        private readonly DebitiContext _context;

        public PaymentrequestsController(DebitiContext context)
        {
            _context = context;
        }

        // GET: api/Paymentrequests
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Paymentrequest>>> GetPaymentrequest()
        {
            return await _context.Paymentrequest.ToListAsync();
        }

        // GET: api/Paymentrequests/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Paymentrequest>> GetPaymentrequest(int id)
        {
            var paymentrequest = await _context.Paymentrequest.FindAsync(id);

            if (paymentrequest == null)
            {
                return NotFound();
            }

            return paymentrequest;
        }

        // PUT: api/Paymentrequests/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPaymentrequest(int id, Paymentrequest paymentrequest)
        {
            if (id != paymentrequest.Id)
            {
                return BadRequest();
            }

            _context.Entry(paymentrequest).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PaymentrequestExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Paymentrequests
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Paymentrequest>> PostPaymentrequest(Paymentrequest paymentrequest)
        {
            _context.Paymentrequest.Add(paymentrequest);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPaymentrequest", new { id = paymentrequest.Id }, paymentrequest);
        }

        // DELETE: api/Paymentrequests/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Paymentrequest>> DeletePaymentrequest(int id)
        {
            var paymentrequest = await _context.Paymentrequest.FindAsync(id);
            if (paymentrequest == null)
            {
                return NotFound();
            }

            _context.Paymentrequest.Remove(paymentrequest);
            await _context.SaveChangesAsync();

            return paymentrequest;
        }

        private bool PaymentrequestExists(int id)
        {
            return _context.Paymentrequest.Any(e => e.Id == id);
        }
    }
}
