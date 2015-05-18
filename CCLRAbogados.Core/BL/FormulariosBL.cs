using CCLRAbogados.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCLRAbogados.Core.BL
{
    public class FormulariosBL
    {
        public string getContactoBodyMessage(ContactoDTO contacto) {
            contacto.fillAreas();
            contacto.fillReferencia();

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<b>Area:</b> {0}<br>", contacto.Areas[contacto.Area]).AppendLine();
            sb.AppendFormat("<b>Nombres:</b> {0}<br>", contacto.Nombres).AppendLine();
            sb.AppendFormat("<b>Apellidos:</b> {0}<br>", contacto.Apellidos).AppendLine();
            sb.AppendFormat("<b>Direccion:</b> {0}<br>", contacto.Direccion).AppendLine();
            sb.AppendFormat("<b>Correo:</b> {0}<br>", contacto.Correo).AppendLine();
            sb.AppendFormat("<b>Telefono:</b> {0}<br>", contacto.Telefono).AppendLine();
            sb.AppendFormat("<b>Referencia:</b> {0}<br>", contacto.Referencias[contacto.Referencia]).AppendLine();
            sb.AppendFormat("<b>Asunto:</b> {0}<br>", contacto.Asunto).AppendLine();
            sb.AppendFormat("<b>Mensaje:</b> {0}<br>", contacto.Mensaje).AppendLine();
            
            return sb.ToString();
        }

        public string getPostulacionBodyMessage(PostulacionDTO postulacion)
        {
            postulacion.fillSexo();
            postulacion.fillEstadoCivil();
            postulacion.fillNacionalidad();
            postulacion.fillTipoDocumentoIdentidad();
            postulacion.fillTipoEntidadProcedencia();
            postulacion.fillGradoInstruccion();
            postulacion.fillRelacion();
            postulacion.fillSexoTitular();
            postulacion.fillNacionalidadTitular();
            postulacion.fillModalidad();
            postulacion.fillCarrera();

            StringBuilder sb = new StringBuilder();
            sb.Append("<b>DATOS PERSONALES</b><br><br>");
            sb.AppendFormat("<b>Nombres:</b> {0}<br>", postulacion.Nombres);
            sb.AppendFormat("<b>Apellidos:</b> {0}<br>", postulacion.Apellidos);
            sb.AppendFormat("<b>Fecha de Nacimiento:</b> {0}<br>", postulacion.FechaNacimiento);
            sb.AppendFormat("<b>Sexo:</b> {0}<br>", postulacion.Sexos[postulacion.Sexo]);
            sb.AppendFormat("<b>EstadoCivil:</b> {0}<br>", postulacion.EstadosCivil[postulacion.EstadoCivil]);
            sb.AppendFormat("<b>Nacionalidad:</b> {0}<br>", postulacion.Nacionalidades[postulacion.Nacionalidad]);
            sb.AppendFormat("<b>Tipo de Documento de Identidad:</b> {0}<br>", postulacion.TiposDocumentoIdentidad[postulacion.TipoDocumentoIdentidad]);
            sb.AppendFormat("<b>Numero de Documento de Identidad:</b> {0}<br>", postulacion.NumeroDocumentoIdentidad);
            sb.AppendFormat("<b>Pais de Nacimiento:</b> {0}<br>", postulacion.PaisNacimiento);
            sb.AppendFormat("<b>Departamento:</b> {0}<br>", postulacion.Departamento);
            sb.AppendFormat("<b>Provincia:</b> {0}<br>", postulacion.Provincia);
            sb.AppendFormat("<b>Distrito:</b> {0}<br>", postulacion.Distrito);
            sb.AppendFormat("<b>Telefono Fijo:</b> {0}<br>", postulacion.TelefonoFijo);
            sb.AppendFormat("<b>Celular:</b> {0}<br>", postulacion.Celular);
            sb.AppendFormat("<b>E-mail:</b> {0}<br>", postulacion.Email);
            sb.AppendFormat("<b>Nombre de Entidad de Procedencia:</b> {0}<br>", postulacion.NombreEntidadProcedencia);
            sb.AppendFormat("<b>Tipo de Entidad de Procedencia:</b> {0}<br>", postulacion.TiposEntidadProcedencia[postulacion.TipoEntidadProcedencia]);
            sb.AppendFormat("<b>Anio de Egreso:</b> {0}<br>", postulacion.AnioEgreso);
            sb.AppendFormat("<b>Pais de Estudio:</b> {0}<br>", postulacion.PaisEstudio);
            sb.AppendFormat("<b>Departmento:</b> {0}<br>", postulacion.DepartmentoEstudio);
            sb.AppendFormat("<b>Provincia:</b> {0}<br>", postulacion.ProvinciaEstudio);
            sb.AppendFormat("<b>Distrito:</b> {0}<br>", postulacion.DistritoEstudio);
            sb.AppendFormat("<b>Grado de Instruccion:</b> {0}<br>", postulacion.GradosInstruccion[postulacion.GradoInstruccion]);
            sb.Append("<br><hr><br>");
            sb.Append("<b>DATOS DEL PADRE/MADRE O APODERADO</b><br><br>");
            sb.AppendFormat("<b>Relación:</b> {0}<br>", postulacion.Relaciones[postulacion.Relacion]);
            sb.AppendFormat("<b>Nombres:</b> {0}<br>", postulacion.NombresTitular);
            sb.AppendFormat("<b>Apellidos:</b> {0}<br>", postulacion.ApellidosTitular);
            sb.AppendFormat("<b>Sexo:</b> {0}<br>", postulacion.SexosTitular[postulacion.SexoTitular]);
            sb.AppendFormat("<b>Nacionalidad:</b> {0}<br>", postulacion.NacionalidadesTitular[postulacion.NacionalidadTitular]);
            sb.AppendFormat("<b>Telefono:</b> {0}<br>", postulacion.TelefonoTitular);
            sb.AppendFormat("<b>Email:</b> {0}<br>", postulacion.EmailTitular);
            sb.AppendFormat("<b>Ocupacion:</b> {0}<br>", postulacion.OcupacionTitular);
            sb.AppendFormat("<b>Centro Laboral:</b> {0}<br>", postulacion.CentroLaboralTitular);
            sb.Append("<br><hr><br><br>");
            sb.Append("<b>PARA RELIGIOSOS</b><br>");
            sb.AppendFormat("<b>Congregación o Diócesis a la que pertenece:</b> {0}<br>", postulacion.CongregacionDiocesis);
            sb.AppendFormat("<b>Nombre del formador:</b> {0}<br>", postulacion.NombreFormador);
            sb.Append("<br><hr><br>");
            sb.Append("<b>DATOS DE LA POSTULACIÓN</b><br><br>");
            sb.AppendFormat("<b>Modalidad de admisión:</b> {0}<br>", postulacion.Modalidades[postulacion.Modalidad]);
            sb.AppendFormat("<b>Carrera a la que postula:</b> {0}<br>", postulacion.Carreras[postulacion.Carrera]);


            return sb.ToString();
        }
    }
}
