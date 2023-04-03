using Sample.DataAccess.Contexts;
using Sample.DataAccess.Contracts;
using Sample.DataAccess.Repositories;
using Sieve.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.DataAccess;

public class UnitOfWork : IUnitOfWork, IDisposable
{

        #region [Field(s)]

        private PersonRepository? _personRepository;

        private PhoneRepository? _phoneRepository;

        private readonly SampleContext _context;

        private readonly ISieveProcessor _sieveProcessor;

        #endregion

        public UnitOfWork(SampleContext context, ISieveProcessor sieveProcessor)
        {
                _context = context;
                _sieveProcessor = sieveProcessor;
        }

        public PersonRepository PersonRepository =>
                _personRepository ??= new PersonRepository(_context, _sieveProcessor);

        public PhoneRepository PhoneRepository =>
                _phoneRepository ??= new PhoneRepository(_context, _sieveProcessor);

        public int Commit() =>
                _context.SaveChanges();

        public async Task<int> CommitAsync(CancellationToken cancellationToken) =>
                await _context.SaveChangesAsync(cancellationToken);

        public void Dispose()
        {
                GC.SuppressFinalize(this);
                _context.Dispose();
        }
}
