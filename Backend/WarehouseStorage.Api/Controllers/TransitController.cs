using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WarehouseStorage.DTOs.DataTransferObjects;
using WarehouseStorage.Services.Repositories.Interfaces;

namespace WarehouseStorage.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransitController : ControllerBase
    {
        public ITransitRepository _transitRepository { get; set; }
        public TransitController(ITransitRepository transitRepository)
        {
            _transitRepository = transitRepository;
        }

        [Authorize]
        [HttpPost]
        public IActionResult<TransitDTO> StartTransit(TransitDTO request)
        {
            
        }
    }
}