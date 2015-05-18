using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using CCLRAbogados.Helpers;

namespace CCLRAbogados.Core.DTO
{
    public class ContactoDTO
    {
        public IDictionary<int, string> Areas { get; set; }
        public IDictionary<int, string> Referencias { get; set; }
        public IDictionary<int, string> Destinatarios { get; set; }
        [Required]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [StringLength(100, MinimumLength = 4)]
        public string Nombres { get; set; }
        [Required]
        public string Apellidos { get; set; }
        public string Direccion { get; set; }
        [Required]
        public string Correo { get; set; }
        [Required]
        public string Telefono { get; set; }
        [Required]
        public int Area { get; set; }
        [Required]
        public int Referencia { get; set; }
        [Required]
        public string Asunto { get; set; }
        [Required]
        public string Mensaje { get; set; }
        [MustBeTrue(ErrorMessage = "Por favor, lea y acepte las Condiciones de Uso y Políticas de Privacidad para poder enviar su consulta")]
        public bool privacidad { get; set; }

        public ContactoDTO()
        {
            Areas = new Dictionary<int, string>();
            Referencias = new Dictionary<int, string>();
            Destinatarios = new Dictionary<int, string>();
            fillAreas();
            fillReferencia();
        }

        public void fillAreas()
        {
            if (Areas == null) Areas = new Dictionary<int, string>();
            Areas.Clear();
            Areas.Add(1, "Informes");
            Areas.Add(2, "Tele Marketing");
            Areas.Add(3, "Admisión");
            Areas.Add(4, "La Pre Ruiz");
            Areas.Add(5, "Ciclo de Avance Académico");
            Areas.Add(6, "Posgrado");
            Areas.Add(7, "Educación Continua");
            Areas.Add(8, "Secretaría Académica");
            Areas.Add(9, "Biblioteca");
            Areas.Add(10, "Titulación en Educación");
            Areas.Add(11, "Relaciones Institucionales");
            Areas.Add(12, "Web Master");
        }
        public void fillReferencia()
        {
            if (Referencias == null) Referencias = new Dictionary<int, string>();
            Referencias.Clear();
            Referencias.Add(1, "Página Web");
            Referencias.Add(2, "Radio");
            Referencias.Add(3, "Redes Sociales");
            Referencias.Add(4, "Aviso en Diario / Aviso en Revista");
            Referencias.Add(5, "Panel Publicitario");
            Referencias.Add(6, "E-mailing");
        }
        public void fillDestinatarios()
        {
            if (Destinatarios == null) Destinatarios = new Dictionary<int, string>();
            Destinatarios.Clear();
            Destinatarios.Add(1, "informes@uarm.edu.pe");
            Destinatarios.Add(2, "informes@uarm.edu.pe");
            Destinatarios.Add(3, "admision@uarm.edu.pe");
            Destinatarios.Add(4, "cepreuarm@uarm.edu.pe");
            Destinatarios.Add(5, "admision@uarm.edu.pe");
            Destinatarios.Add(6, "posgrado@uarm.edu.pe");
            Destinatarios.Add(7, "educacion.continua@uarm.edu.pe");
            Destinatarios.Add(8, "sacademica@uarm.edu.pe");
            Destinatarios.Add(9, "biblioteca@uarm.edu.pe");
            Destinatarios.Add(10, "informes@uarm.edu.pe");
            Destinatarios.Add(11, "ccordova@uarm.edu.pe");
            Destinatarios.Add(12, "kevin.rebosio@uarm.pe");
        }
    }
}
