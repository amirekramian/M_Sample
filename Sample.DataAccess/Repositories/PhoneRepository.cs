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

public class PhoneRepository : GenericRepository<Phone>
{
        #region [Field(s)]

        private readonly SampleContext _context;

        #endregion

        #region [Constructor]

        public PhoneRepository(SampleContext context, ISieveProcessor sieveProcessor) : base(context,
                sieveProcessor) =>
                _context = context;

        #endregion

        #region [Method(s)]

        public async Task<Phone?> LoadByNationalCodeAsync(string nationalCode, CancellationToken cancellationToken = new()) =>
                await _context.Phones!
                        .Include(x => x.Person)
                        .Where(x => x.Person != null && x.Person.NationalCode == nationalCode)
                        .SingleOrDefaultAsync(cancellationToken);

        public async Task<bool> CheckPhoneExistAsync(string phone, CancellationToken cancellationToken = new()) =>
                await _context.Phones!.AnyAsync(x => x.Content == phone, cancellationToken);

        #endregion
}
