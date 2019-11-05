using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MT940Data.Entities
{
    [Table("StatementLineInformation", Schema = "mt940")]
    public partial class StatementLineInformation
    {
        [Key]
        public int Id { get; set; }
        public int StatementLineId { get; set; }
        public int? TransactionCode { get; set; }
        [StringLength(50)]
        public string PostingText { get; set; }
        [StringLength(50)]
        public string JournalNumber { get; set; }
        [StringLength(50)]
        public string BankCodeOfPayer { get; set; }
        [StringLength(50)]
        public string AccountNumberOfPayer { get; set; }
        [StringLength(50)]
        public string NameOfPayer { get; set; }
        public int? TextKeyAddition { get; set; }
        [StringLength(50)]
        public string EndToEndReference { get; set; }
        [StringLength(50)]
        public string CustomerReference { get; set; }
        [StringLength(50)]
        public string MandateReference { get; set; }
        [StringLength(50)]
        public string CreditorReference { get; set; }
        [StringLength(50)]
        public string OriginatorsIdentificationCode { get; set; }
        [StringLength(50)]
        public string CompensationAmount { get; set; }
        [StringLength(50)]
        public string OriginalAmount { get; set; }
        [StringLength(50)]
        public string SepaRemittanceInformation { get; set; }
        [StringLength(50)]
        public string PayersReferenceParty { get; set; }
        [StringLength(50)]
        public string CreditorsReferenceParty { get; set; }
        [StringLength(50)]
        public string UnstructuredRemittanceInformation { get; set; }
        public bool? IsUnstructuredData { get; set; }
        public string UnstructuredData { get; set; }

        [ForeignKey(nameof(StatementLineId))]
        [InverseProperty("StatementLineInformation")]
        public virtual StatementLine StatementLine { get; set; }
    }
}
