using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Common.Constants;

public class QueriesConstants
{

        #region [Field(s)]

        public const string PersonWithNationalCodeAndBirthDate = @"Select 
                                                                        Id,Name,Family,BirthDate From Persons 
						                        Where NationalCode = @NationalCode And                                                  BirthDatePersian = @BirthDate";

        public const string PersonWithNationalCode = @"Select Id,Name,Family,BirthDate From Persons 
						                Where NationalCode = @NationalCode";

        public const string PersonPhoneWithNationalCode = @"Select
                                                                p.NationalCode, ph.Content
						                From Persons p
						                Join Phones ph
                                                                On p.Id = ph.PersonId And ph.IsDeleted = 0
						                Where p.NationalCode = @NationalCode 
                                                                And p.IsDeleted = 0";

        public const string CheckPersonMobileExistWithNationalCode = @"Select Count(*)
						                                From Persons p
						                                Join Phones ph 
                                                                                On p.Id = ph.PersonId 
                                                                                And ph.IsDeleted = 0
						                                Where p.NationalCode = @NationalCode                                                    And ph.Content = @Content 
                                                                                And p.IsDeleted = 0";

        public const string ExistPersonWithNationalCode = "Select Id From Persons Where NationalCode = @NationalCode";

        public const string InsertPerson = @"Insert Into Persons
                                                ([NationalCode],[Name],[Family],[IsMale],[BirthDate],                                   [FatherName],[IsDead],[IsDeleted],[CreationDate],[LastUpdated])
					        Output Inserted.Id
					        Values (@NationalCode,@Name,@Family,@IsMale,@BirthDate,
                                                @FatherName,@IsDead,@IsDeleted,@CreationDate,@LastUpdated)";

        public const string UpdatePerson = @"Update Persons Set 
                                                IsDead = @IsDead,LastUpdated = @LastUpdated,
                                                BirthDate = @BirthDate = Where Id = @Id";

        public const string DeletePerson = "Update Persons Set IsDeleted = 1 Where Id = @Id";

        #endregion
}
