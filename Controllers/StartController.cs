using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;

using StocksCoreApi.Data;
using StocksCoreApi.Domain.LoadSession;
using StocksCoreApi.Models;

namespace StocksCoreApi.Controllers
{
    [Route("api/[controller]")]
    public class StartController : Controller
    {
        private readonly IMediator _application;

        public StartController(IMediator application)
        {
            _application = application;
        }

        // POST api/start
        [HttpPost]
        public void Post([FromBody]StockSizeRequest request)
        {
            var loadSessionRequest = new LoadSessionRequest{
                NumberOfStocks = request.NumberOfStocks
            };

            _application.Send(loadSessionRequest);
        }
    }
}
