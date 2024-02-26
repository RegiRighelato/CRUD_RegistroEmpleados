using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CRUD.EntityLayer;
using CRUD.BusinessLayer;
using System.Globalization;


namespace CRUD.WebForm
{
    public partial class Contact : Page
    {
        private static int IdEmpleado = 0;
        DepartamentoBL departamentoBL = new DepartamentoBL();
        EmpleadoBL empleadoBL = new EmpleadoBL();

            protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            {
                if (Request.QueryString["IdEmpleado"] != null)
                {
                    IdEmpleado = Convert.ToInt32(Request.QueryString["IdEmpleado"].ToString());

                    if (IdEmpleado != 0) {
                        lblTitulo.Text ="Editar Empleado";
                        btnSubmit.Text ="Actualizar";

                        Empleado empleado = empleadoBL.Obtener(IdEmpleado);
                        txtNombreCompleto.Text = empleado.NombreCompleto;
                        CargarDepartamentos(empleado.Departamento.IdDepartamento.ToString());
                        txtSueldo.Text = empleado.Sueldo.ToString();
                        txtFechaContrato.Text = Convert.ToDateTime(empleado.FechaContrato, new CultureInfo("es-AR")).ToString("yyyy-MM-dd");
                    }
                    else
                    {
                        lblTitulo.Text = "Nuevo Empleado";
                        btnSubmit.Text = "Guardar";
                    }
                }
                else
                    Response.Redirect("~/Default.aspx");
                    CargarDepartamentos();

            }
        }

        private void CargarDepartamentos(string IdDepartamento = "")
        {
            List<Departamento> lista = departamentoBL.Lista();

            ddlDepartamento.DataTextField = "Nombre";
            ddlDepartamento.DataValueField = "IdDepartamento";
            
            ddlDepartamento.DataSource = lista;
            ddlDepartamento.DataBind();

            if (IdDepartamento != "")
                ddlDepartamento.SelectedValue = IdDepartamento;


        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            Empleado entidad = new Empleado()
            {
                IdEmpleado = IdEmpleado,
                NombreCompleto = txtNombreCompleto.Text,
                Departamento = new Departamento() { IdDepartamento = Convert.ToInt32(ddlDepartamento.SelectedValue) },
                Sueldo = Convert.ToDecimal(txtSueldo.Text, new CultureInfo("es-AR")),
                FechaContrato = txtFechaContrato.Text
            };

            bool respuesta;
            if (IdEmpleado != 0)
                respuesta = empleadoBL.Editar(entidad);
            else 
                respuesta = empleadoBL.Crear(entidad);

            if (respuesta)
                Response.Redirect("~/Default.aspx");
            else
                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('No se pudo realizar la operación')", true);
        }
    }
}