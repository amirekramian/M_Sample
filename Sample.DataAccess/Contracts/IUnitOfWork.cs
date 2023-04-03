using Sample.DataAccess.Contexts;
using Sample.DataAccess.Repositories;
using Sieve.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.DataAccess.Contracts;

public interface IUnitOfWork
{
        PersonRepository? PersonRepository { get; }

        PhoneRepository? PhoneRepository { get; }

        int Commit();

        Task<int> CommitAsync(CancellationToken cancellationToken);
}
