using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MT940Data.Entities
{
    [Table("MarkType", Schema = "mt940")]
    public partial class MarkType
    {
        public MarkType()
        {
            StatementBalance = new HashSet<StatementBalance>();
        }

        [Key]
        public byte Id { get; set; }
        [StringLength(15)]
        public string Name { get; set; }

        [InverseProperty("MarkNavigation")]
        public virtual ICollection<StatementBalance> StatementBalance { get; set; }
    }
}
