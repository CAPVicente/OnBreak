using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OnBreak.BC;

public partial class MantenedorModalidadServicio : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CargarTipoEvento();
            DataGridSources();
            btn_Eliminar.Enabled = false;
            btn_Actualizar.Enabled = false;
        }
    }

    private void CargarTipoEvento()
    {
        TipoEvento tipoEvento = new TipoEvento();
        cb_tipoEvento.DataSource = tipoEvento.ReadAll();

        cb_tipoEvento.DataTextField = "Descripcion";
        cb_tipoEvento.DataValueField = "IdTipoEvento";
        cb_tipoEvento.DataBind();

        cb_tipoEvento.SelectedIndex = 0;
    }

    private void DataGridSources()
    {
        ModalidadServicio modalidad = new ModalidadServicio();
        dg_ModalidadServicio.DataSource = modalidad.ReadAll();
        dg_ModalidadServicio.DataBind();
    }

    private void LimpiarControles()
    {
        tb_idModalidad.Text = string.Empty;
        tb_nombreModalidad.Text = string.Empty;
        tb_personalBase.Text = string.Empty;
        tb_valorBase.Text = string.Empty;
        btn_Eliminar.Enabled = false;
        btn_GuardarModalidad.Enabled = true;
        btn_Actualizar.Enabled = false;
        tb_idModalidad.Enabled = true;
    }

    protected void dg_ModalidadServicio_SelectedIndexChanged(object sender, EventArgs e)
    {
        btn_Eliminar.Enabled = true;
        btn_Actualizar.Enabled = true;
        tb_idModalidad.Enabled = false;
        btn_GuardarModalidad.Enabled = false;

        tb_idModalidad.Text = dg_ModalidadServicio.SelectedRow.Cells[1].Text;
        cb_tipoEvento.SelectedValue = dg_ModalidadServicio.SelectedRow.Cells[2].Text;
        tb_nombreModalidad.Text = Server.HtmlDecode(dg_ModalidadServicio.SelectedRow.Cells[3].Text);
        tb_valorBase.Text = dg_ModalidadServicio.SelectedRow.Cells[4].Text;
        tb_personalBase.Text = dg_ModalidadServicio.SelectedRow.Cells[5].Text;
    }

    protected void btn_GuardarModalidad_Click(object sender, EventArgs e)
    {
        ModalidadServicio modalidad = new ModalidadServicio()
        {
            IdModalidad = tb_idModalidad.Text,
            IdTipoEvento = int.Parse(cb_tipoEvento.SelectedValue),
            Nombre = tb_nombreModalidad.Text,
            ValorBase = int.Parse(tb_valorBase.Text),
            PersonalBase = int.Parse(tb_personalBase.Text)
        };

        if (modalidad.Create())
        {
            lblMsg.Text = "Modalidad creada exitosamente";
            LimpiarControles();
        }
        else
        {
            lblMsg.Text = "Modalidad no ha sido creada debido a que ya existe en la base de datos. Inténtelo nuevamente";
        }
        Response.AppendHeader("Refresh", "3");
    }

    protected void btn_Actualizar_Click(object sender, EventArgs e)
    {
        ModalidadServicio modalidad = new ModalidadServicio()
        {
            IdModalidad = tb_idModalidad.Text,
            IdTipoEvento = int.Parse(cb_tipoEvento.SelectedValue),
            Nombre = tb_nombreModalidad.Text,
            ValorBase = int.Parse(tb_valorBase.Text),
            PersonalBase = int.Parse(tb_personalBase.Text)
        };

        if (modalidad.Update())
        {
            lblMsg.Text = "Modalidad actualizada exitosamente";
            LimpiarControles();
        }
        else
        {
            lblMsg.Text = "Modalidad no ha sido actualizada. Asegúrese de rellenar todos los campos.";
        }
        Response.AppendHeader("Refresh", "3");
    }

    protected void btn_Eliminar_Click(object sender, EventArgs e)
    {
        ModalidadServicio modalidad = new ModalidadServicio()
        {
            IdModalidad = tb_idModalidad.Text
        };

        if (modalidad.Delete())
        {
            lblMsg.Text = "Modalidad eliminada exitosamente";
            LimpiarControles();
        }
        else
        {
            lblMsg.Text = "Modalidad no ha sido eliminada debido a que esta se encuentra asociada a un Contrato.";
        }
        Response.AppendHeader("Refresh", "3");
    }

    protected void btn_Limpiar_Click(object sender, EventArgs e)
    {
        LimpiarControles();
    }

    protected void dg_ModalidadServicio_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        dg_ModalidadServicio.PageIndex = e.NewPageIndex;
        DataGridSources();
    }
}