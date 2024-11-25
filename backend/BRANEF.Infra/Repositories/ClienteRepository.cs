using BRANEF.Domain.Entities;
using BRANEF.Domain.Interfaces.IRepository;
using BRANEF.Infra.EntityFramework;
using BRANEF.Infra.Repositories.Base;

namespace BRANEF.Infra.Repositories
{
    public class ClienteRepository : RepositoryBase<Cliente>, IClienteRepository
    {
        public ClienteRepository(BranefContext context) : base(context)
        {

        }
    }
}
