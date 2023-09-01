using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class ItemXContract
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int ContractId { get; set; }

        [ForeignKey("ContractId")]
        public virtual Contract Contract { get; set; }

        [Required]
        public int ItemId { get; set; }

        [ForeignKey("ItemId")]
        public virtual Item Item { get; set; }

        [Required]
        public int Quantity { get; set; }

        public DateTime CreateDate { get; set; } = DateTime.Now;

        public DateTime UpdateDate { get; set; }

        public bool Enabled { get; set; } = true;

        public bool Deleted { get; set; } = false;

        [StringLength(100)]
        public string CreatedBy { get; set; } = string.Empty;
    }
}
