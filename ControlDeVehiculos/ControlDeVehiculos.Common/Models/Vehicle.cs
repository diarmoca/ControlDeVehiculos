

namespace ControlDeVehiculos.Common.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using SQLite;
    using System;


    public class Vehicle
    {
        [Key]
        [AutoIncrement]
        public int Id { get; set; }

        [Required]
        public string Marca { get; set; }

        [Required]
        public string Tipo { get; set; }

        [Required]
        public string Color { get; set; }

        [Required]
        public int Modelo { get; set; }

        [Required]
        [Display(Name = "No de Placa")]
        public string NoPlacas { get; set; }

        [Required]
        [Display(Name = "No de Serie")]
        public string NoSerie { get; set; }

        [Required]
        [Display(Name = "Nombre del Resguardante")]
        public string Resguardante { get; set; }

        [Required]
        public string Cargo { get; set; }

        [Required]
        public string Adscripcion { get; set; }

        [Required]
        [Display(Name = "No de Averiguación")]
        public string NoAvPrevia { get; set; }

        [Required]
        [Display(Name = "No de Expediente")]
        public string NoExpediente { get; set; }

        [Required]
        public string Origen { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Fecha de Inicio del resguardo")]
        public DateTime FechaInicio { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Fecha que concluye el reguardo")]
        public DateTime FechaFinal { get; set; }

        // [Display(Name = "Foto")]
        // public string ImagePath { get; set; }

        //[NotMapped]
        // public byte[] ImageArray { get; set; }

        /* public String ImageFullPath
         {
             get
             {
                 if (string.IsNullOrEmpty(this.ImagePath))
                 {
                     var imgDefault = "VehiclesDBA.png";
                     return imgDefault;
                 }
                 var image = $"https://dbavehiclesapi.azurewebsites.net{this.ImagePath.Substring(1)}";
                 return image.ToString();
             }
         }*/

        public string ConverDateEnd
         {
             get
             {

                 var FechaVencimiento = FechaFinal.ToShortDateString();
                 return FechaVencimiento;

             }
         }

        public override string ToString()
        {
            return this.Marca;
        }
    }
}
