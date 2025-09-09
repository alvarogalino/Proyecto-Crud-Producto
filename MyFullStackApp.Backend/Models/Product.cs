using System.ComponentModel.DataAnnotations;

namespace MyFullStackApp.Backend.Models
{
    public class Product
    {
        [Key] // Indica que Id es la clave primaria
        public int Id { get; set; }

        [Required] // Indica que el nombre es un campo requerido
        [StringLength(100)] // Limita la longitud del nombre
        public required string Name { get; set; }

        [StringLength(500)] // Limita la longitud de la descripción
        public string? Description { get; set; }

        [Range(0.01, double.MaxValue)] // El precio debe ser mayor que 0
        public decimal Price { get; set; }
    }
}
