using System.Collections.Generic;
using System.Threading.Tasks;
using TestandoDapper.Data;

namespace TestandoDapper.Repository
{
    public interface ITarefaRepository
    {
        Task<IEnumerable<Tarefa>> GetTarefasAsync();
        Task<Tarefa> GetTarefaByIdAsync(int id);
        Task<Tarefa> GetTarefaByIdProcedureAsync(int id);
        Task<IEnumerable<Tarefa>> GetTarefasConcluidasAsync();
        Task<IEnumerable<Tarefa>> GetTarefasEContadorByIdAsync(out int count);
        Task<int> SaveAsync(Tarefa tarefa);
        Task<int> UpdateTarefaStatusAsync(Tarefa tarefa);
        Task<int> DeleteAsync(int id);
    }
}
