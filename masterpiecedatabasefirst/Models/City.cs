using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace masterpiecedatabasefirst.Models;

public partial class City
{
    [Key]
    [Column("CityID")]
    public int CityId { get; set; }

    public string CityName { get; set; } = null!;

    public string CityNameAr { get; set; } = null!;

    [InverseProperty("City")]
    public virtual ICollection<Student> Students { get; set; } = new List<Student>();

    [InverseProperty("City")]
    public virtual ICollection<University> Universities { get; set; } = new List<University>();
}
