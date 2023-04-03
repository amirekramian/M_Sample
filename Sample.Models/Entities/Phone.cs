using Sample.Model.Base;
using Sample.Model.Enums;
using Sieve.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Model;

public class Phone : BaseEntityWithKey
{

        #region [Properties]

        [StringLength(11)]
        public string? Content { get; set; }

        [Sieve(CanSort = true, CanFilter = true)]
        public TypeNumberEnum TypeNumber { get; set; }

        
        [ForeignKey("PersonId")]
        public Person? Person { get; set; }

        [Sieve(CanSort = true, CanFilter = true)]
        public int PersonId { get; set; }

        #endregion
}
