using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using CCLRAbogadosWeb.Helpers;
using System.Web.Mvc;

namespace CCLRAbogadosWeb.Core.DTO
{
    public class PostulacionDTO
    {
        //Datos Personales
        public IDictionary<int, string> Sexos { get; set; }
        public IDictionary<int, string> EstadosCivil { get; set; }
        public IDictionary<int, string> TiposDocumentoIdentidad { get; set; }
        public IDictionary<int, string> Nacionalidades { get; set; }
        public IDictionary<int, string> TiposEntidadProcedencia { get; set; }
        public IDictionary<int, string> GradosInstruccion { get; set; }

        [Required]
        public string Nombres { get; set; }
        [Required]
        public string Apellidos { get; set; }
        [Required]
        public string FechaNacimiento { get; set; }
        [Required]
        public int Sexo { get; set; }
        [Required, Range(1, Int32.MaxValue)]
        public int EstadoCivil { get; set; }
        [Required, Range(1, Int32.MaxValue)]
        public int Nacionalidad { get; set; }
        [Required]
        public int TipoDocumentoIdentidad { get; set; }
        [Required]
        public string NumeroDocumentoIdentidad { get; set; }
        [Required]
        public string PaisNacimiento { get; set; }
        [Required]
        public string Departamento { get; set; }
        [Required]
        public string Provincia { get; set; }
        [Required]
        public string Distrito { get; set; }
        [Required]
        public string TelefonoFijo { get; set; }
        [Required]
        public string Celular { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public int TipoEntidadProcedencia { get; set; }
        [Required]
        public string NombreEntidadProcedencia { get; set; }
        [Required]
        public string AnioEgreso { get; set; }
        [Required]
        public string PaisEstudio { get; set; }
        [Required]
        public string DepartmentoEstudio { get; set; }
        [Required]
        public string ProvinciaEstudio { get; set; }
        [Required]
        public string DistritoEstudio { get; set; }
        [Required, Range(1, Int32.MaxValue)]
        public int GradoInstruccion { get; set; }

        //Datos del Padre/Madre/Apoderado

        public IDictionary<int, string> Relaciones { get; set; }
        public IDictionary<int, string> SexosTitular { get; set; }
        public IDictionary<int, string> NacionalidadesTitular { get; set; }

        [Required]
        public int Relacion { get; set; }
        [Required]
        public string NombresTitular { get; set; }
        [Required]
        public string ApellidosTitular { get; set; }
        [Required]
        public int SexoTitular { get; set; }
        [Required, Range(1, Int32.MaxValue)]
        public int NacionalidadTitular { get; set; }
        [Required]
        public string TelefonoTitular { get; set; }
        public string EmailTitular { get; set; }
        public string OcupacionTitular { get; set; }
        public string CentroLaboralTitular { get; set; }
        
        //Religiosos
        public string CongregacionDiocesis { get; set; }
        public string NombreFormador { get; set; }

        //Datos de la postulacion
        public IDictionary<int, string> Modalidades { get; set; }
        public IDictionary<int, string> Carreras { get; set; }

        [Required, Range(1, Int32.MaxValue)]
        public int Modalidad { get; set; }
        [Required, Range(1, Int32.MaxValue)]
        public int Carrera { get; set; }

        //Adicionales
        public string Asunto { get; set; }
        public string Mensaje { get; set; }
        
        [MustBeTrue(ErrorMessage = "Por favor, lea y acepte las Condiciones de Uso y Políticas de Privacidad para poder enviar su consulta")]
        public bool privacidad { get; set; }

        public PostulacionDTO()
        {
            Sexos = new Dictionary<int, string>();
            EstadosCivil = new Dictionary<int, string>();
            Nacionalidades = new Dictionary<int, string>();
            TiposDocumentoIdentidad = new Dictionary<int, string>();
            TiposEntidadProcedencia = new Dictionary<int, string>();
            GradosInstruccion = new Dictionary<int, string>();
            Relaciones = new Dictionary<int, string>();
            SexosTitular = new Dictionary<int, string>();
            NacionalidadesTitular = new Dictionary<int, string>();
            Modalidades = new Dictionary<int, string>();
            Carreras = new Dictionary<int, string>();

            fillSexo();
            fillEstadoCivil();
            fillNacionalidad();
            fillTipoDocumentoIdentidad();
            fillTipoEntidadProcedencia();
            fillGradoInstruccion();
            fillRelacion();
            fillSexoTitular();
            fillNacionalidadTitular();
            fillModalidad();
            fillCarrera();
        }

        public void fillAsunto(){
            Asunto = "Nuevo Postulante - " + Nombres + " " + Apellidos;
        }

        public void fillSexo()
        {
            if (Sexos == null) Sexos = new Dictionary<int, string>();
            Sexos.Clear();
            Sexos.Add(1, "Masculino");
            Sexos.Add(2, "Femenino");
        }

        public void fillEstadoCivil()
        {
            if (EstadosCivil == null) EstadosCivil = new Dictionary<int, string>();
            EstadosCivil.Clear();
            EstadosCivil.Add(0, "Seleccione");
            EstadosCivil.Add(1, "Soltero");
            EstadosCivil.Add(2, "Casado");
        }

        public void fillNacionalidad()
        {
            if (Nacionalidades == null) Nacionalidades = new Dictionary<int, string>();
            Nacionalidades.Clear();
            Nacionalidades.Add(0, "Seleccione Nacionalidad");
            Nacionalidades.Add(1, "Peruano");
            Nacionalidades.Add(2, "Otro");
        }

        public void fillTipoDocumentoIdentidad()
        {
            if (TiposDocumentoIdentidad == null) TiposDocumentoIdentidad = new Dictionary<int, string>();
            TiposDocumentoIdentidad.Clear();
            TiposDocumentoIdentidad.Add(1, "DNI");
            TiposDocumentoIdentidad.Add(2, "C.E.");
            TiposDocumentoIdentidad.Add(3, "B.M.");
            TiposDocumentoIdentidad.Add(4, "Pasaporte");
            TiposDocumentoIdentidad.Add(5, "Partida Nacimiento");
        }

        public void fillTipoEntidadProcedencia()
        {
            if (TiposEntidadProcedencia == null) TiposEntidadProcedencia = new Dictionary<int, string>();
            TiposEntidadProcedencia.Clear();
            TiposEntidadProcedencia.Add(1, "Particular");
            TiposEntidadProcedencia.Add(2, "Estatal");
            TiposEntidadProcedencia.Add(3, "Parroquial");
        }

        public void fillGradoInstruccion()
        {
            if (GradosInstruccion == null) GradosInstruccion = new Dictionary<int, string>();
            GradosInstruccion.Clear();
            GradosInstruccion.Add(0, "Seleccione Grado de Instrucción");
            GradosInstruccion.Add(1, "Superior");
            GradosInstruccion.Add(2, "Universitaria");
            
        }

        public void fillRelacion()
        {
            if (Relaciones == null) Relaciones = new Dictionary<int, string>();
            Relaciones.Clear();
            Relaciones.Add(1, "Padre");
            Relaciones.Add(2, "Madre");
            Relaciones.Add(3, "Apoderado");
        }

        public void fillSexoTitular()
        {
            if (SexosTitular == null) SexosTitular = new Dictionary<int, string>();
            SexosTitular.Clear();
            SexosTitular.Add(1, "Masculino");
            SexosTitular.Add(2, "Femenino");
        }

        public void fillNacionalidadTitular()
        {
            if (NacionalidadesTitular == null) NacionalidadesTitular = new Dictionary<int, string>();
            NacionalidadesTitular.Clear();
            NacionalidadesTitular.Add(0, "Seleccione Nacionalidad");
            NacionalidadesTitular.Add(1, "Peruano");
            NacionalidadesTitular.Add(2, "Otro");
        }

        public void fillModalidad()
        {
            if (Modalidades == null) Modalidades = new Dictionary<int, string>();
            Modalidades.Clear();
            Modalidades.Add(0, "Seleccione Modalidad de Admisión");
            Modalidades.Add(1, "Admisión General");
            Modalidades.Add(2, "Evaluación para el desempeño universitario");
            Modalidades.Add(3, "Exoneración por primeros puestos");
            Modalidades.Add(4, "Tercio superior");
            Modalidades.Add(5, "Admisión por colegio seleccionado");
            Modalidades.Add(6, "Admisión por colegio seleccionado (Tercio Superior)");
            Modalidades.Add(7, "Pre de la Ruiz (Para quienes aprobaron todos los cursos)");
            Modalidades.Add(8, "Pre de la Ruiz (Quienes NO alcanzaron el ingreso directo)");
            Modalidades.Add(9, "Concurso Beca Fe y Alegría");
            Modalidades.Add(10, "Egresados de Bachillerato (colegios)");
            Modalidades.Add(11, "Religiosos y miembros del Clero");
            Modalidades.Add(12, "Cuerpo Diplomático acreditado en el Perú y familiares directos");
            Modalidades.Add(13, "Deportistas Calificados");
            Modalidades.Add(14, "Personas con discapacidad");
            Modalidades.Add(15, "Traslados externos");
            Modalidades.Add(16, "Titulados y graduados");
        }

        public void fillCarrera()
        {
            if (Carreras == null) Carreras = new Dictionary<int, string>();
            Carreras.Clear();
            Carreras.Add(0, "Seleccione carrera a la que postula");
            Carreras.Add(1, "Filosofía");
            Carreras.Add(2, "Educación");
            Carreras.Add(3, "Periodismo");
            Carreras.Add(4, "Psicología");
            Carreras.Add(5, "Ciencia Política");
            Carreras.Add(6, "Turismo Sostenible");
            Carreras.Add(7, "Derecho");
            Carreras.Add(8, "Economía y Gestión Ambiental");
            Carreras.Add(9, "Administración");
            Carreras.Add(10, "Ingeniería Industrial");
            
        }

    }

}
