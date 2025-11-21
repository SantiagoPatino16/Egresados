using Common.Attributes;
using DataAccess;
using DataAccess.ConnectionDB;
using LogicBusiness.Service;
using System;
using System.Linq;

namespace Presentation.CreateOffer
{
    public partial class EmploymentCreate : System.Web.UI.Page
    {
        private readonly JobOffertsService _jobService = new JobOffertsService();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarEnums();
                txtEmpresa.Text = Session["NombreEmpresa"]?.ToString() ?? "Empresa";
            }
        }

        private void CargarEnums()
        {
            ddlModalidades.DataSource = Enum.GetValues(typeof(AttributesWorkModalities.ModalidadTrabajoEnum));
            ddlModalidades.DataBind();

            ddlContratos.DataSource = Enum.GetValues(typeof(AttributesContractType.TipoContratoEnum));
            ddlContratos.DataBind();
        }
        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            using (var context = new RSContext())
            {
                // ---------------------------
                // 1. Empresa
                // ---------------------------
                int idEmpresa = Convert.ToInt32(Session["IdUsuario"]);

                // ---------------------------
                // 2. Categoría
                // ---------------------------
                string nombreCategoria = txtCategoria.Text.Trim();
                int idCategoria;

                var categoriaExistente = context.CategoriasEmpleo
                                                .FirstOrDefault(c => c.Categoria == nombreCategoria);

                if (categoriaExistente != null)
                {
                    idCategoria = categoriaExistente.IdCategoria;
                }
                else
                {
                    var nuevaCategoria = new AttributesCategoriesJobs
                    {
                        Categoria = nombreCategoria
                    };
                    context.CategoriasEmpleo.Add(nuevaCategoria);
                    context.SaveChanges();
                    idCategoria = nuevaCategoria.IdCategoria;
                }

                // ---------------------------
                // 3. Modalidad de trabajo
                // ---------------------------
                string modalidadNombre = ddlModalidades.SelectedItem.Text;
                int idModalidad;

                var modalidadExistente = context.ModalidadesTrabajo
                                                .FirstOrDefault(m => m.Modalidad.ToString() == modalidadNombre);

                if (modalidadExistente != null)
                {
                    idModalidad = modalidadExistente.IdModalidadTrabajo;
                }
                else
                {
                    var nuevaModalidad = new AttributesWorkModalities
                    {
                        Modalidad = (AttributesWorkModalities.ModalidadTrabajoEnum)
                                    Enum.Parse(typeof(AttributesWorkModalities.ModalidadTrabajoEnum), modalidadNombre)
                    };
                    context.ModalidadesTrabajo.Add(nuevaModalidad);
                    context.SaveChanges();
                    idModalidad = nuevaModalidad.IdModalidadTrabajo;
                }

                // ---------------------------
                // 4. Tipo de contrato
                // ---------------------------
                string contratoNombre = ddlContratos.SelectedItem.Text;
                int idTipoContrato;

                var contratoExistente = context.TiposContrato
                                               .FirstOrDefault(t => t.TipoContrato.ToString() == contratoNombre);

                if (contratoExistente != null)
                {
                    idTipoContrato = contratoExistente.IdTipoContrato;
                }
                else
                {
                    var nuevoContrato = new AttributesContractType
                    {
                        TipoContrato = (AttributesContractType.TipoContratoEnum)
                                       Enum.Parse(typeof(AttributesContractType.TipoContratoEnum), contratoNombre)
                    };
                    context.TiposContrato.Add(nuevoContrato);
                    context.SaveChanges();
                    idTipoContrato = nuevoContrato.IdTipoContrato;
                }

                // ---------------------------
                // 5. Crear la oferta de empleo
                // ---------------------------
                var oferta = new AttributesJobOfferts
                {
                    Titulo = txtTitulo.Text,
                    Descripcion = txtDescripcion.Text,
                    Requisitos = txtRequisitos.Text,
                    Salario = decimal.Parse(txtSalario.Text),
                    Ciudad = txtCiudad.Text,
                    FechaPublicacion = DateTime.Now,
                    FechaCierre = DateTime.Parse(txtFechaCierre.Text),
                    IdEmpresa = idEmpresa,
                    IdCategoria = idCategoria,
                    IdModalidadTrabajo = idModalidad,
                    IdTipoContrato = idTipoContrato,
                    Estado = AttributesJobOfferts.EstadoOferta.PorVerificar
                };

                // ---------------------------
                // 6. Guardar usando el Service
                // ---------------------------
                _jobService.Agregar(oferta);

                // Opcional: mostrar un mensaje de éxito al usuario
                lblMensaje.Text = "¡Oferta publicada correctamente!";
                lblMensaje.CssClass = "alert alert-success";
            }
        }


    }
}
