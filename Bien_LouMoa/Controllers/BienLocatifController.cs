using Bien_LouMoa.Models;
using Bien_LouMoa.Services.middlewares;
using BienLocatif_LouMoa.Models;
using BienLocatif_LouMoa.Services;
using Microsoft.AspNetCore.Mvc;

namespace BienLocatif_LouMoa.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BienLocatifController : ControllerBase
    {

        private readonly ILogger<BienLocatifController> _logger;
        private readonly BienLocatifService _bienLocatifService;

        public BienLocatifController(ILogger<BienLocatifController> logger,
            BienLocatifService bienLocatifService)
        {
            _logger = logger;
            _bienLocatifService = bienLocatifService;
        }

        [HttpDelete("user/{idUser}")]
        public ActionResult DeleteAll(int idUser)
        {
            _bienLocatifService.DeleteAllAsync(idUser);
            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult> PostOne(BienLocatif body)
        {
            var idUtilisateur = HttpContext.Items[UserHeaderMiddleware.ID_KEY] as int?;

            if (idUtilisateur == null)
                return Unauthorized();

            if (body.IdProprietaire != idUtilisateur.Value)
                return Unauthorized();

            var bienLocatif = await _bienLocatifService.AddAsync(body, idUtilisateur.Value);

            if (bienLocatif == null)
                return BadRequest();
            
            return Ok();
        }

        [HttpPut("{id}")]
        public ActionResult UpdateOne(int id, BienLocatif body)
        {
            var idUtilisateur = HttpContext.Items[UserHeaderMiddleware.ID_KEY] as int?;

            if (idUtilisateur == null)
                return Unauthorized();

            if (body.IdProprietaire != idUtilisateur.Value)
                return Unauthorized();

            if (body.IdBien != id)
                return BadRequest();

            var bienLocatif = _bienLocatifService.UpdateAsync(body, id);

            if (bienLocatif == null)
                return BadRequest();

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteOne(int id)
        {
            var idUtilisateur = HttpContext.Items[UserHeaderMiddleware.ID_KEY] as int?;

            if (idUtilisateur == null)
                return Unauthorized();
            
            var bienSupprime = await _bienLocatifService.DeleteAsync(id, idUtilisateur.Value);

            if (bienSupprime == null)
                return NotFound();

            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BienLocatif>> GetOne(int id)
        {
            var idUtilisateur = HttpContext.Items[UserHeaderMiddleware.ID_KEY] as int?;
            
            if (idUtilisateur == null)
                return Unauthorized();

            var bienLocatif = await _bienLocatifService.GetByIdAsync(id, idUtilisateur.Value);

            if (bienLocatif == null)
                return NotFound();

            return bienLocatif;

        }

        [HttpGet("user/{idUser}")]
        public async Task<ActionResult<List<BienLocatif>>> GetAll(int idUser)
        {
            var roleUtilisateur = HttpContext.Items[UserHeaderMiddleware.ROLE_KEY] as UserHeaderMiddleware.Role?;

            if (roleUtilisateur != UserHeaderMiddleware.Role.PROPRIETAIRE)
                return BadRequest();

            // CHECK NECESSAIRE DE L EXISTANCE DU USER DANS LE SERVICE USER
            // ICI SI PAS DE USER -> LISTE VIDE

            var biensLocatifs = await _bienLocatifService.GetAllAsync(idUser);

            return biensLocatifs;
        }

        [HttpPost("requestMatchingLogement")]
        public async Task<ActionResult<List<BienLocatif>>> GetMatchingLogement(RequestMatchingLogement request)
            => await _bienLocatifService.GetMatchingLogementAsync(request);
        

        [HttpGet("{id}/belongsTo/{idUser}")]
        public async Task<ActionResult<bool>> BelongsTo(int id, int idUser)
            => await _bienLocatifService.BelongsToAsync(id, idUser);
    }
}