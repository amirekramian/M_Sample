using Sieve.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Model.Base;

public class BaseEntityWithKey : BaseEntity
{
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Sieve(CanFilter = true, CanSort = true)]
        public int Id { get; set; }
}
