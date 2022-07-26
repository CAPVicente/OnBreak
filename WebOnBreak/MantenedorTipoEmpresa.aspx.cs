using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OnBreak.BC;

public partial class MantenedorTipoEmpresa : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) //La primera vez que se carga la página viene en FALSE
        {
            llenarGrilla();
            btnBorrar.Enabled = false;
            btnActualizar.Enabled = false;
        }
    }

    private void llenarGrilla()
    {
        /* Carga todas las empresas */
        TipoEmpresa tipoEmpresa = new TipoEmpresa();
        gdTipoEmpresa.DataSource = tipoEmpresa.ReadAll();
        gdTipoEmpresa.DataBind();
    }
    private void LimpiarControles()
    {
        /* Limpia los controles de texto */
        txtIdTipoEmpresa.Text = string.Empty;
        txtDescripcion.Text = string.Empty;
        btnBorrar.Enabled = false;
        btnActualizar.Enabled = false;
        btnGrabar.Enabled = true;
        txtIdTipoEmpresa.Enabled = true;
    }

    protected void btnGrabar_Click(object sender, EventArgs e)
    {
        TipoEmpresa tipoEmpresa = new TipoEmpresa()
        {
            IdTipoEmpresa = int.Parse(txtIdTipoEmpresa.Text),
            Descripcion = txtDescripcion.Text,
        };

        if (tipoEmpresa.Create())
        {
            lblMsg.Text = "Tipo de Empresa registrado";
            LimpiarControles();
        }
        else
        {
            lblMsg.Text = "Tipo de Empresa no pudo ser registrado debido a que este ya existe.";
        }
        Response.AppendHeader("Refresh", "3");
    }

    protected void btnActualizar_Click(object sender, EventArgs e)
    {
        TipoEmpresa tipoEmpresa = new TipoEmpresa()
        {
            IdTipoEmpresa = int.Parse(txtIdTipoEmpresa.Text),
            Descripcion = txtDescripcion.Text,
        };

        if (tipoEmpresa.Update())
        {
            lblMsg.Text = "Modificado con Exito!";
            LimpiarControles();
        }
        else
        {
            lblMsg.Text = "Tipo de Empresa no pudo ser modificado";
        }
        Response.AppendHeader("Refresh", "3");
    }

    protected void btnBorrar_Click(object sender, EventArgs e)
    {
        TipoEmpresa tipoEmpresa = new TipoEmpresa()
        {
            IdTipoEmpresa = int.Parse(txtIdTipoEmpresa.Text)
        };

        if (tipoEmpresa.Delete())
        {
            lblMsg.Text = "Eliminado con Exito!";
            LimpiarControles();
        }
        else
        {
            lblMsg.Text = "Tipo de Empresa no pudo ser eliminado debido a que esta está asociada a un cliente registrado.";
        }
        Response.AppendHeader("Refresh", "3");
    }

    protected void btnLimpiar_Click(object sender, EventArgs e)
    {
        LimpiarControles();
    }

    protected void gdTipoEmpresa_SelectedIndexChanged(object sender, EventArgs e)
    {
        btnBorrar.Enabled = true;
        btnActualizar.Enabled = true;
        btnGrabar.Enabled = false;
        txtIdTipoEmpresa.Enabled = false;          
        txtIdTipoEmpresa.Text = gdTipoEmpresa.SelectedRow.Cells[1].Text;
        txtDescripcion.Text = Server.HtmlDecode(gdTipoEmpresa.SelectedRow.Cells[2].Text);
    }

    protected void gdTipoEmpresa_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gdTipoEmpresa.PageIndex = e.NewPageIndex;
        llenarGrilla();
    }
}