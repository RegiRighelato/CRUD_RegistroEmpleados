using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CRUD.EntityLayer;
using CRUD.BusinessLayer;

namespace CRUD.WebForm
{
    public partial class _Default : Page
    {

        EmpleadoBL empleadoBL = new EmpleadoBL();

        protected void Page_Load(object sender, EventArgs e)
        {
            MostrarEmpleados();
        }

        private void MostrarEmpleados()
        {
            List<Empleado> lista = empleadoBL.Lista();

            GVEmpleado.DataSource = lista;
            GVEmpleado.DataBind();

        }

        protected void Nuevo_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Contact.aspx?IdEmpleado=0");
        }

        protected void Editar_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            string IdEmpleado = btn.CommandArgument;
            Response.Redirect($"~/Contact.aspx?IdEmpleado={IdEmpleado}");
        }

        protected void Eliminar_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            string IdEmpleado = btn.CommandArgument;

            bool respuesta = empleadoBL.Eliminar(Convert.ToInt32(IdEmpleado));

            if (respuesta)
                MostrarEmpleados();

        }
    }
}