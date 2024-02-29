using Clementoni1WebAPI.Models.DB;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;

namespace Clementoni1WebAPI.Handlers.CommandHandlers
{
    public sealed record PostPersonaCommand(Person person) : IRequest;
    public sealed record PutPersonaByIdCommand(int id, Person person) : IRequest<bool>;

    public sealed record DeletePersonaByIdCommand(int id) : IRequest;

    public sealed class PersonaCommandHandler :
        IRequestHandler<PostPersonaCommand>,
        IRequestHandler<PutPersonaByIdCommand, bool>,
        IRequestHandler<DeletePersonaByIdCommand>
    {
        private readonly FormazioneDBContext _context;

        public PersonaCommandHandler(FormazioneDBContext context)
        {
            _context = context;
        }

        public async Task Handle(PostPersonaCommand request, CancellationToken cancellationToken)
        {
            _context.Person.Add(request.person);
            await _context.SaveChangesAsync();

        }

        public async Task<bool> Handle(PutPersonaByIdCommand request, CancellationToken cancellationToken)
        {
            if (request.id != request.person.Id)
            {
                return false;
            }

            _context.Entry(request.person).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonExists(request.id))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }
        }

        private bool PersonExists(int id)
        {
            throw new NotImplementedException();
        }

        public async Task Handle(DeletePersonaByIdCommand request, CancellationToken cancellationToken)
        {
            var person = await _context.Person.FindAsync(request.id);

            _context.Person.Remove(person);
            await _context.SaveChangesAsync();


        }
    }
}
