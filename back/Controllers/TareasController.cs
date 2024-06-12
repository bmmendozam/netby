using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace back.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TareasController : ControllerBase
    {
        private readonly TareasDAL _tareasDAL;

        public TareasController(TareasDAL tareasDAL)
        {
            _tareasDAL = tareasDAL;
        }

        [HttpGet]
        public async Task<IActionResult> GetTareas()
        {
            var tareas = await _tareasDAL.GetTareasAsync();
            return Ok(tareas);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTareaById(int id)
        {
            var tarea = await _tareasDAL.GetTareaByIdAsync(id);
            if (tarea == null)
            {
                return NotFound();
            }
            return Ok(tarea);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTarea(TareaPendiente tarea)
        {
            var id = await _tareasDAL.Insertar(tarea);
            return Ok(id );
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTarea(int id, TareaPendiente tarea)
        {
            if (id != tarea.Id)
            {
                return BadRequest();
            }

            var updated = await _tareasDAL.Update(tarea);
            if (!updated)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTarea(int id)
        {
            var deleted = await _tareasDAL.Delete(id);
            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
