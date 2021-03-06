﻿using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private readonly IValueRepository _valueRepository;
        private readonly ISettings _settings;

        public ValuesController(IValueRepository valueRepository, ISettings settings)
        {
            if (valueRepository == null) throw new ArgumentNullException(nameof(valueRepository));

            _valueRepository = valueRepository;
            _settings = settings;
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            string val1 = _valueRepository.Get("1");
            string val2 = _valueRepository.Get("2");

            return new string[] { val1, val2 };
        }
        ///api/values/settings/sqlserver/datasource
        ///
        [HttpGet]
        [Route("settings/sqlserver/datasource")]
        public string GetSqlServerDataSource()
        {
            return _settings.DataSource;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
