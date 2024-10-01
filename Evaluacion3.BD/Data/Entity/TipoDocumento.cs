﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evaluacion3.BD.Data.Entity
{
    [Index(nameof(Codigo), Name = "TipoDocumento_UQ", IsUnique = true)]

    
    public class TipoDocumento : EntityBase
    {
        [Required(ErrorMessage = "El Cdigo de Tipo de Documento es obligatorio ")]
        [MaxLength(4, ErrorMessage = "Maximo numero de caracteres {1}.")]
        public string Codigo { get; set; }

        [Required(ErrorMessage = "El Nombre de Tipo de Documento es obligatorio ")]
        [MaxLength(100, ErrorMessage = "Maximo numero de caracteres {1}.")]
        public string Nombre { get; set; }
    }
}