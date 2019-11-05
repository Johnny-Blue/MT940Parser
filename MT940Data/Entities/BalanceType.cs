using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MT940Data.Entities
{
    [Table("BalanceType", Schema = "mt940")]
    public partial class BalanceType
    {
        public BalanceType()
        {
            StatementBalance = new HashSet<StatementBalance>();
        }

        [Key]
        public byte Id { get; set; }
        [StringLength(15)]
        public string Name { get; set; }

        [InverseProperty("TypeNavigation")]
        public virtual ICollection<StatementBalance> StatementBalance { get; set; }
    }
}
