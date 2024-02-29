using Clementoni1.Interfaces;

namespace Clementoni1.Services
{
    public class PersonaItaliaService : IPersonaService<PersonaItaliaService>
    {
        public string AggiungiPrefisso(string numero)
        {
            return "+39" + numero;
        }
    }

}
