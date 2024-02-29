using MediatR;
using Microsoft.EntityFrameworkCore;
using Clementoni1WebAPI.Models.DB;
using Microsoft.Data.SqlClient;
using Dapper;



namespace Clementoni1WebAPI.Handlers.QueryHandlers
{
   public sealed record GetPersonaQuery() : IRequest<List<Person>>;
   public sealed record GetPersonaByIdQuery(int id) : IRequest<Person>;

    public sealed record GetPersonaConDapperQuery() : IRequest<List<Person>>;
    public sealed record GetPersonaByIdConDapperQuery(int id) : IRequest<Person>;
    public sealed class PersonaQueryHandler : 
        IRequestHandler<GetPersonaConDapperQuery, List<Person>>,
        IRequestHandler<GetPersonaByIdConDapperQuery, Person>
    {
        private readonly FormazioneDBContext _context;
        private readonly string _connectionString;

        public PersonaQueryHandler(FormazioneDBContext context)
        {
            _context = context;
            _connectionString = context.Database.GetConnectionString()!;
        }

        public async Task<List<Person>> Handle(GetPersonaConDapperQuery request, CancellationToken cancellationToken)
        {
            var query = "SELECT * FROM Person";
            using var connection = new SqlConnection(_connectionString);
            var risultato = (await connection.QueryAsync<Person>(query, new { param = (int?)1 })).ToList();
            return risultato;
        }

        public async Task<Person> Handle(GetPersonaByIdConDapperQuery request, CancellationToken cancellationToken)
        {
            var query = "SELECT * FROM Person WHERE Person.Id = @param";
            using var connection = new SqlConnection(_connectionString);
            var risultato = (await connection.QueryAsync<Person>(query, new { param = request.id })).SingleOrDefault();
            return await _context.Person.FindAsync(request.id);
        }
    }
}
