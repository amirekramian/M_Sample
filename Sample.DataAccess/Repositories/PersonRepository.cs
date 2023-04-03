using Microsoft.EntityFrameworkCore;
using Sample.DataAccess.Base;
using Sample.DataAccess.Contexts;
using Sample.Model;
using Sieve.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.DataAccess.Repositories;

public class PersonRepository : GenericRepository<Person>
{
        #region [Field(s)]

        private readonly SampleContext _context;

        #endregion

        #region [Constructor]

        public PersonRepository(SampleContext context, ISieveProcessor sieveProcessor) : base(context, sieveProcessor) => _context = context;

        #endregion

        #region [Method(s)]

        public async Task<Person?> LoadByNationalCodeAsync(string nationalCode,
                CancellationToken cancellationToken = new()) =>
                await _context.Persons!
                        .SingleOrDefaultAsync(x => x.NationalCode == nationalCode, cancellationToken);

        #endregion
}
