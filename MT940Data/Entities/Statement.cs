using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MT940Data.Entities
{
    [Table("Statement", Schema = "mt940")]
    public partial class Statement
    {
        public Statement()
        {
            StatementBalance = new HashSet<StatementBalance>();
            StatementInformation = new HashSet<StatementInformation>();
            StatementLine = new HashSet<StatementLine>();
        }

        [Key]
        public int Id { get; set; }
        [StringLength(50)]
        public string AccountIdentification { get; set; }
        [StringLength(50)]
        public string RelatedReference { get; set; }
        public int? SequenceNumber { get; set; }
        public int? StatementNumber { get; set; }
        [StringLength(50)]
        public string TransactionReferenceNumber { get; set; }
        [StringLength(150)]
        public string FileName { get; set; }
        public int? FileId { get; set; }
        [Required]
        [StringLength(128)]
        public string ChangedBy { get; set; }

        [InverseProperty("Statement")]
        public virtual ICollection<StatementBalance> StatementBalance { get; set; }
        [InverseProperty("Statement")]
        public virtual ICollection<StatementInformation> StatementInformation { get; set; }
        [InverseProperty("Statement")]
        public virtual ICollection<StatementLine> StatementLine { get; set; }
    }
}
