using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Suma5834163
{
    public class LocalDbService
    {
        private const string DB_NAME = "demo_suma.db3";
        private readonly SQLiteAsyncConnection _connection;

        public LocalDbService()
        {
            _connection = new SQLiteAsyncConnection(Path.Combine(FileSystem.AppDataDirectory, DB_NAME));

            //Le indica al sistema que cree la tabla de nuestro contexto
            _connection.CreateTableAsync<Resultado>();
        }

        //Metodo para listar los registros de la tabla
        public async Task<List<Resultado>> GetResultados()
        {
            return await _connection.Table<Resultado>().ToListAsync();
        }

        //Metodo para listar los registros por id
        public async Task<Resultado> GetById(int id)
        {
            return await _connection.Table<Resultado>().Where(x=>x.Id==id).FirstOrDefaultAsync();
        }

        //Metodo para crear registros
        public async Task Create(Resultado resultado)
        {
            await _connection.InsertAsync(resultado);
        }

        //Metodo para actualizar
        public async Task Update(Resultado resultado)
        {
            await _connection.UpdateAsync(resultado);
        }

        //Metodo para eliminar
        public async Task Delete(Resultado resultado)
        {
            await _connection.DeleteAsync(resultado);
        }

        
        
    }
}
