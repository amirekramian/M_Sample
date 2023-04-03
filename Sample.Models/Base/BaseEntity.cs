using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Model.Base;

public class BaseEntity
{
        #region [Constructore]

        public BaseEntity() 
        {
                IsDeleted = false;
                CreationDate = (LastUpdated = DateTime.Now);
        }

        #endregion

        #region [Properties]

        public bool IsDeleted { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime LastUpdated { get; set; }

        #endregion
}
