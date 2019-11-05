using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MT940Data.Entities
{
    [Table("StatementLine", Schema = "mt940")]
    public partial class StatementLine
    {
        public StatementLine()
        {
            StatementLineInformation = new HashSet<StatementLineInformation>();
        }

        [Key]
        public int Id { get; set; }
        public int StatementId { get; set; }
        [Column(TypeName = "datetime2(0)")]
        public DateTime? ValueDate { get; set; }
        [Column(TypeName = "datetime2(0)")]
        public DateTime? EntryDate { get; set; }
        public byte? Mark { get; set; }
        [StringLength(1)]
        public string FundsCode { get; set; }
        [Column(TypeName = "money")]
        public decimal? Amount { get; set; }
        [StringLength(50)]
        public string TransactionTypeIdCode { get; set; }
        [StringLength(50)]
        public string CustomerReference { get; set; }
        [StringLength(50)]
        public string SupplementaryDetails { get; set; }
        [StringLength(50)]
        public string BankReference { get; set; }

        [ForeignKey(nameof(StatementId))]
        [InverseProperty("StatementLine")]
        public virtual Statement Statement { get; set; }
        [InverseProperty("StatementLine")]
        public virtual ICollection<StatementLineInformation> StatementLineInformation { get; set; }
    }
}
