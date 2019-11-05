using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MT940Data.Entities
{
    [Table("StatementBalance", Schema = "mt940")]
    public partial class StatementBalance
    {
        [Key]
        public int Id { get; set; }
        public int StatementId { get; set; }
        [Column(TypeName = "money")]
        public decimal? Amount { get; set; }
        [StringLength(3)]
        public string Currency { get; set; }
        [Column(TypeName = "datetime2(0)")]
        public DateTime? Date { get; set; }
        public byte? Mark { get; set; }
        public byte? Type { get; set; }

        [ForeignKey(nameof(Mark))]
        [InverseProperty(nameof(MarkType.StatementBalance))]
        public virtual MarkType MarkNavigation { get; set; }
        [ForeignKey(nameof(StatementId))]
        [InverseProperty("StatementBalance")]
        public virtual Statement Statement { get; set; }
        [ForeignKey(nameof(Type))]
        [InverseProperty(nameof(BalanceType.StatementBalance))]
        public virtual BalanceType TypeNavigation { get; set; }
    }
}
