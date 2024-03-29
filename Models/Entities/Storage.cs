﻿using System.ComponentModel.DataAnnotations.Schema;

namespace rendszerfejlesztes_beadando.Models.Entities
{
    public class Storage
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int Row { get; set; }
        public int Columnn { get; set; }
        public int Level { get; set; }
        public int? ComponentId { get; set; }
        public Component Component { get; set; }
        public int? Quantity { get; set; }
    }
}
