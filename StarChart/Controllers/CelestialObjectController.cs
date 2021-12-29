﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StarChart.Data;
using StarChart.Models;

namespace StarChart.Controllers
{
    [Route("")]
    [ApiController]
    public class CelestialObjectController: ControllerBase
    {
        private readonly ApplicationDbContext _context;



        public CelestialObjectController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("{id:int}", Name = "GetById")]
      
        public IActionResult GetById(int id)
        {
            var celestialObject = _context.CelestialObjects.Find(id);
            if(celestialObject == null)
            {
                return NotFound();
            }

            celestialObject.Satellites = _context.CelestialObjects.Where(c => c.OrbitedObjectId == id).ToList();
            return Ok(celestialObject);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var celestialObject = _context.CelestialObjects.ToList();
            foreach(var celestialObjects in celestialObject)
            {
                celestialObjects.Satellites = _context.CelestialObjects.Where(c => c.OrbitedObjectId == id).ToList();
            }
            return Ok(celestialObject);
        }


    }
}
