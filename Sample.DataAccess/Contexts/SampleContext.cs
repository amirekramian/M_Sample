using Microsoft.EntityFrameworkCore;
using Sample.Model;
using Sample.Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.DataAccess.Contexts;

public class SampleContext : DbContext
{

        #region [Constructure]

        public SampleContext(DbContextOptions options) : base(options) { }

        #endregion

        #region [Properties]

        public DbSet<Person>? Persons { get; set; }

        public DbSet<Phone>? Phones { get; set; }

        #endregion

        #region [Method(s)]

        protected override void OnModelCreating(ModelBuilder builder)
        {
                base.OnModelCreating(builder);

                builder.Entity<Person>().HasData(new List<Person>{

                        new()
                        {
                                Id = 1,
                                BirthDate = new DateTime(1998,1,18),
                                Family = "Ddshi",
                                FatherName = "Ahmad",
                                IsMale = false,
                                Name = "Mina",
                                NationalCode = "0020788312"
                        },

                        new()
                        {
                                Id = 2,
                                BirthDate = new DateTime(1998,1,18),
                                Family = "Ddshi",
                                FatherName = "Ahmad",
                                IsMale = false,
                                Name = "Mina",
                                NationalCode = "0020788312"
                        }
                });

                builder.Entity<Phone>().HasData(new List<Phone>
                {
                        new()
                        {
                                Id = 1,
                                PersonId = 1,
                                Content = "09367636359",
                                TypeNumber = TypeNumberEnum.MobileNumber,
                        },

                        new()
                        {
                                Id = 2,
                                PersonId = 2,
                                Content = "02166353280",
                                TypeNumber = TypeNumberEnum.TelephoneNumber,
                        }
                });
        }

        #endregion
}
