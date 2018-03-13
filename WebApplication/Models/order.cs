namespace WebApplication.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class order
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id { get; set; }

        [Required]
        [StringLength(100)]
        public string name { get; set; }

        public double sum { get; set; }

        public int number_products { get; set; }

        public int customer_id { get; set; }

        public Boolean discontinued { get; set; }

        public int products_in_stock { get; set; }

        public virtual customer customer { get; set; }

    }
}
