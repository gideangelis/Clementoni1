using Clementoni1.Interfaces;

namespace Clementoni1.Services
{
    public class PersonaFranciaService : IPersonaService<PersonaFranciaService>
    {
        public string AggiungiPrefisso(string numero)
        {
            return "+33" + numero;
        }
    }
}
