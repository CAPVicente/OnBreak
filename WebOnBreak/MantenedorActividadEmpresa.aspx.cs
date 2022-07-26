using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OnBreak.BC;

public partial class MantenedorActividadEmpresa : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DataGridSources();
            btn_Eliminar.Enabled = false;
            btn_Actualizar.Enabled = false;
        }

    }

    private void DataGridSources()
    {
        ActividadEmpresa actividadEmpresa = new ActividadEmpresa();
        dg_ActividadEmpresa.DataSource = actividadEmpresa.ReadAll();
        dg_ActividadEmpresa.DataBind();
    }

    private void LimpiarControles()
    {
        tb_idActEmp.Text = string.Empty;
        tb_nombreActEmp.Text = string.Empty;

        btn_Actualizar.Enabled = false;
        btn_Eliminar.Enabled = false;

        tb_idActEmp.Enabled = true;
        btnGrabar.Enabled = true;
    }

    protected void dg_ActividadEmpresa_SelectedIndexChanged(object sender, EventArgs e)
    {
        btn_Eliminar.Enabled = true;
        btn_Actualizar.Enabled = true;
        tb_idActEmp.Enabled = false;
        btnGrabar.Enabled = false;

        tb_idActEmp.Text = dg_ActividadEmpresa.SelectedRow.Cells[1].Text;
        tb_nombreActEmp.Text = Server.HtmlDecode(dg_ActividadEmpresa.SelectedRow.Cells[2].Text);
    }

    protected void btnGrabar_Click(object sender, EventArgs e)
    {
        ActividadEmpresa actividadEmpresa = new ActividadEmpresa()
        {
            IdActividadEmpresa = int.Parse(tb_idActEmp.Text),
            Descripcion = tb_nombreActEmp.Text
        };

        if (actividadEmpresa.Create())
        {
            lbl_Msg.Text = "Actividad registrada correctamente.";
            LimpiarControles();
        }
        else
        {
            lbl_Msg.Text = "Hubo un error al registrar la actividad.";
        }
        Response.AppendHeader("Refresh", "3");
    }

    protected void btn_Actualizar_Click(object sender, EventArgs e)
    {
        ActividadEmpresa actividadEmpresa = new ActividadEmpresa()
        {
            IdActividadEmpresa = int.Parse(tb_idActEmp.Text),
            Descripcion = tb_nombreActEmp.Text
        };

        if (actividadEmpresa.Update())
        {
            lbl_Msg.Text = "Actividad actualizada correctamente.";
            LimpiarControles();
        }
        else
        {
            lbl_Msg.Text = "Hubo un error al actualizar la actividad.";
        }
        Response.AppendHeader("Refresh", "3");
    }

    protected void btn_Eliminar_Click(object sender, EventArgs e)
    {
        ActividadEmpresa actividadEmpresa = new ActividadEmpresa()
        {
            IdActividadEmpresa = int.Parse(tb_idActEmp.Text)
        };

        if (actividadEmpresa.Delete())
        {
            lbl_Msg.Text = "Actividad eliminada correctamente.";
            LimpiarControles();
        }
        else
        {
            lbl_Msg.Text = "Hubo un error al eliminar la actividad.";
        }
        Response.AppendHeader("Refresh", "3");
    }

    protected void btn_Limpiar_Click(object sender, EventArgs e)
    {
        LimpiarControles();
    }

    protected void dg_ActividadEmpresa_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        dg_ActividadEmpresa.PageIndex = e.NewPageIndex;
        DataGridSources();
    }
}