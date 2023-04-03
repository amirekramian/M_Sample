using Sample.Model.Base;
using Sieve.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Model;

public class Person : BaseEntityWithKey
{

        #region [Constructore]

        public Person() 
        {

                IsDead = false;

        }

        #endregion

        #region [Properties]

        [Sieve(CanSort = true, CanFilter = true)]
        [StringLength(10)]
        public string? NationalCode { get; set; }

        [MaxLength(100)]
        public string? Name { get; set; }

        [MaxLength(100)]
        public string? Family { get; set; }

        public string FullName => $"{Name} {Family}";
        //or : => "Name + " " + Family"

        [Sieve(CanFilter = true)]
        public bool IsMale { get; set; }

        [Sieve(CanSort = true, CanFilter = true)]
        public DateTime BirthDate { get; set; }

        [MaxLength(100)]
        public string? FatherName { get; set; }

        [Sieve(CanFilter = true)]
        public bool IsDead { get; set; }

        public virtual Phone? Phone { get; set; }

        #endregion
}
