﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class VwcBuscaCodMagnu
    {
        [StringLength(20)]
        public string CodMagnus { get; set; }
        [StringLength(80)]
        public string NomeEquip { get; set; }
    }
}
