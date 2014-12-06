using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using CoverMap.Models;
using CoverMap.dtos;

namespace CoverMap.Controllers
{
    public class CoversController : ApiController
    {
        private CoverContext db = new CoverContext();

        // GET: api/Covers
        public IQueryable<Cover> GetCovers()
        {
            return db.Covers;
        }

        // GET: api/Covers/5
        [ResponseType(typeof(Cover))]
        public IHttpActionResult GetCover(int id)
        {
            Cover cover = db.Covers.Find(id);
            if (cover == null)
            {
                return NotFound();
            }

            return Ok(cover);
        }

        // PUT: api/Covers/5
        [Authorize]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCover(int id, Cover cover)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != cover.CoverID)
            {
                return BadRequest();
            }

            db.Entry(cover).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CoverExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        private void DbLogger(string s)
        {

        }

        [HttpPost]
        public IQueryable<Cover> GetDatapointsWithinBounds([FromBody]CoversBound pos)
        {
            var covers = from c in db.Covers
                         where (c.Lattitude > pos.P1Lat && c.Lattitude < pos.P2Lat) &&
                               (c.Longitude > pos.P1Lng && c.Longitude < pos.P2Lng)
                         select c;
            foreach (var c in covers)
            {
                if (c.Technology == "") {
                    c.Technology = "Tale";
                }
            }
            return covers;
        }

        [HttpPost]
        public IQueryable<CoverInfoDTO> GetCoverageInformationWithinBounds([FromBody] CoversBound pos)
        {
            var covers = from c in db.Covers
                         where (c.Lattitude > pos.P1Lat && c.Lattitude < pos.P2Lat) &&
                               (c.Longitude > pos.P1Lng && c.Longitude < pos.P2Lng)
                         group c by new { c.NetworkName, c.Technology } into avgCoverage
                         select new CoverInfoDTO
                         {
                             NetworkName = avgCoverage.Key.NetworkName,
                             Technology = (avgCoverage.Key.Technology == "" ? "Tale" : avgCoverage.Key.Technology),
                             SignalStrength = (int)avgCoverage.Average(x => x.SignalStrength)
                         };
            return covers;
        }

        // POST: api/Covers
        [ResponseType(typeof(Cover))]
        public IHttpActionResult PostCover(Cover cover)
        {
            cover.Created = DateTime.Now;
            cover.Updated = DateTime.Now;
            ModelState.Clear();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Covers.Add(cover);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = cover.CoverID }, cover);
        }

        // DELETE: api/Covers/5
        [Authorize]
        [ResponseType(typeof(Cover))]
        public IHttpActionResult DeleteCover(int id)
        {
            Cover cover = db.Covers.Find(id);
            if (cover == null)
            {
                return NotFound();
            }

            db.Covers.Remove(cover);
            db.SaveChanges();

            return Ok(cover);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CoverExists(int id)
        {
            return db.Covers.Count(e => e.CoverID == id) > 0;
        }
    }
}