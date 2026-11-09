using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace masterpiecedatabasefirst.Models;

[Index("StudentId", Name = "IX_TawjihiResults_StudentID", IsUnique = true)]
public partial class TawjihiResult
{
    [Key]
    [Column("TawjihiResultID")]
    public int TawjihiResultId { get; set; }

    [Column("StudentID")]
    public int StudentId { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal Average { get; set; }

    public string StreamName { get; set; } = null!;

    public DateTime SavedAt { get; set; }

    [ForeignKey("StudentId")]
    [InverseProperty("TawjihiResult")]
    public virtual Student Student { get; set; } = null!;
}
