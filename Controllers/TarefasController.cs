using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TestandoDapper.Data;
using TestandoDapper.Repository;

namespace TestandoDapper.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TarefasController : Controller
    {
        private readonly ITarefaRepository _tarefaRepository;

        public TarefasController(ITarefaRepository tarefaRepository)
        {
            _tarefaRepository = tarefaRepository;
        }

        [HttpGet]
        [Route("tarefas")]
        public async Task<IActionResult> GetTarefasAsync()
        {
            var tarefas = await _tarefaRepository.GetTarefasAsync();
            return Ok(tarefas);
        }

        [HttpGet]
        [Route("tarefa")]
        public async Task<IActionResult> GetTarefaByIdAsync(int id)
        {
            var tarefa = await _tarefaRepository.GetTarefaByIdAsync(id);
            return Ok(tarefa);
        }

        [HttpGet]
        [Route("tarefacontador")]
        public async Task<IActionResult> GetTarefasEContadorByIdAsync()
        {
            var tarefa = await _tarefaRepository.GetTarefasEContadorByIdAsync(out int count);
            return Ok(new { count, tarefa });
        }

        [HttpPost]
        [Route("criartarefa")]
        public async Task<IActionResult> SaveAsync(Tarefa tarefa)
        {
            var result = await _tarefaRepository.SaveAsync(tarefa);
            return Ok(result);
        }

        [HttpPut]
        [Route("atualizartarefa")]
        public async Task<IActionResult> UpdateTarefaStatusAsync(Tarefa tarefa)
        {
            var result = await _tarefaRepository.UpdateTarefaStatusAsync(tarefa);
            return Ok(result);
        }

        [HttpDelete]
        [Route("deletartarefa")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _tarefaRepository.DeleteAsync(id);
            return Ok(result);
        }
    }
}
