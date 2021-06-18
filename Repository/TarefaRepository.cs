using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using TestandoDapper.Data;

namespace TestandoDapper.Repository
{
    public sealed class TarefaRepository : ITarefaRepository
    {
        private readonly DbSession _db;

        public TarefaRepository(DbSession dbSession)
        {
            _db = dbSession;
        }
        public async Task<IEnumerable<Tarefa>> GetTarefasAsync()
        {
            using (var connection = _db.Connection)
            {
                var query = "SELECT * FROM Tarefas";
                var tarefas = await connection.QueryAsync<Tarefa>(sql: query);

                return tarefas;
            }
        }

        public async Task<Tarefa> GetTarefaByIdAsync(int id)
        {
            using (var connetion = _db.Connection)
            {
                var query = "SELECT * FROM Tarefas WHERE Id = @id";
                var tarefa = await connetion.QueryFirstAsync<Tarefa>(sql: query, param: new { id });

                return tarefa;
            }
        }

        public Task<IEnumerable<Tarefa>> GetTarefasEContadorByIdAsync(out int count)
        {
            count = default(int);
            using (var connection = _db.Connection)
            {
                var query =
                    @"SELECT * FROM Tarefas
                    SELECT COUNT(*) FROM Tarefas";

                var reader = connection.QueryMultipleAsync(sql: query).Result;

                var tarefas = reader.ReadAsync<Tarefa>();
                count = reader.ReadAsync<int>().Result.FirstOrDefault();

                return tarefas;
            }
        }

        public async Task<int> SaveAsync(Tarefa tarefa)
        {
            using (var connetion = _db.Connection)
            {
                var query =
                    @"INSERT INTO Tarefas(Id, Descricao, IsCompleta)
                    VALUES(@Id, @Descricao, @IsCompleta)";
                var result = await connetion.ExecuteAsync(sql: query, param: tarefa);

                return result;
            }
        }

        public async Task<int> UpdateTarefaStatusAsync(Tarefa tarefa)
        {
            using (var connetion = _db.Connection)
            {
                var command =
                    @"UPDATE Tarefas 
                    SET IsCompleta = @IsCompleta
                    WHERE Id = @Id";
                var result = await connetion.ExecuteAsync(sql: command, param: tarefa);

                return result;
            }
        }

        public async Task<int> DeleteAsync(int id)
        {
            using (var connetion = _db.Connection)
            {
                var command =
                    @"DELETE FROM Tarefas 
                    WHERE Id = @Id";
                var result = await connetion.ExecuteAsync(sql: command, param: new { id });

                return result;
            }
        }

        public async Task<Tarefa> GetTarefaByIdProcedureAsync(int id)
        {
            using (var connetion = _db.Connection)
            {
                var parameter = new DynamicParameters();
                parameter.Add("@Id", id, DbType.Int32, ParameterDirection.Input);
                parameter.Add("@Descricao", dbType: DbType.String, direction: ParameterDirection.ReturnValue);
                parameter.Add("@IsCompleta", dbType: DbType.Boolean, direction: ParameterDirection.ReturnValue);
                parameter.Add("@RowCount", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

                var storageProcedure = "SelectTarefaById";
                var tarefa = await connetion.QueryFirstOrDefaultAsync<Tarefa>(sql: storageProcedure, param: parameter, commandType: CommandType.StoredProcedure);

                return tarefa;
            }
        }

        public async Task<IEnumerable<Tarefa>> GetTarefasConcluidasAsync()
        {
            using (var connetion = _db.Connection)
            {
                var parameter = new DynamicParameters();
                parameter.Add("@Id", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);
                parameter.Add("@Descricao", dbType: DbType.String, direction: ParameterDirection.ReturnValue);
                parameter.Add("@IsCompleta", dbType: DbType.Boolean, direction: ParameterDirection.ReturnValue);
                parameter.Add("@RowCount", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

                var storageProcedure = "SelectTarefasConcluidas";
                var tarefas = await connetion.QueryAsync<Tarefa>(sql: storageProcedure, param: parameter, commandType: CommandType.StoredProcedure);

                return tarefas;
            }
        }
    }
}
