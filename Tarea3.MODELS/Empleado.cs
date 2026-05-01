using System.ComponentModel.DataAnnotations;

namespace Tarea3.MODELS
{
    public class Empleado
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Es obligatorio llenar este campo.")]
        [StringLength(80, ErrorMessage = "El nombre no puede superar 80 caracteres.")]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "Es obligatorio llenar este campo.")]
        [StringLength(100, ErrorMessage = "Los apellidos no pueden superar 100 caracteres.")]
        public string Apellidos { get; set; } = string.Empty;

        [Required(ErrorMessage = "Es obligatorio llenar este campo.")]
        public string Departamento { get; set; } = string.Empty;

        [Required(ErrorMessage = "Es obligatorio llenar este campo.")]
        [Range(400000, 10000000, ErrorMessage = "El salario debe estar entre 400000 y 10000000.")]
        public decimal Salario { get; set; }

        public DateTime FechaIngreso { get; set; } = DateTime.Now;

        public bool Activo { get; set; }

        public string NombreCompleto => $"{Nombre} {Apellidos}";
    }
}
