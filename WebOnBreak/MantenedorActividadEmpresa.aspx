<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="MantenedorActividadEmpresa.aspx.cs" Inherits="MantenedorActividadEmpresa" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table style="width:100%;">
        <tr>
            <td class="auto-style2">ID Actividad Empresa</td>
            <td class="auto-style2">
                <asp:TextBox ID="tb_idActEmp" runat="server" Width="247px"></asp:TextBox>
&nbsp;&nbsp;
                </td>
            <td class="auto-style2">
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="tb_idActEmp" ErrorMessage="*Obligatorio"></asp:RequiredFieldValidator>
&nbsp;<asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="tb_idActEmp" ErrorMessage="*Fuera de Rango" MaximumValue="100" MinimumValue="1" Type="Integer"></asp:RangeValidator>
&nbsp;<asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="tb_idActEmp" ErrorMessage="*Numérico" Operator="DataTypeCheck" Type="Integer"></asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td class="auto-style2">Nombre Actividad Empresa: </td>
            <td class="auto-style3">
                <asp:TextBox ID="tb_nombreActEmp" runat="server" Width="250px"></asp:TextBox>
            </td>
            <td>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="tb_nombreActEmp" ErrorMessage="*Obligatorio"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="auto-style2">
                <asp:Button ID="btnGrabar" runat="server" Text="Guardar" Width="150px" OnClick="btnGrabar_Click"/>
            </td>
            <td class="auto-style3">
                <asp:Button ID="btn_Actualizar" runat="server" Text="Actualizar" Width="167px" OnClick="btn_Actualizar_Click" />
            </td>
            <td>
                <asp:Button ID="btn_Eliminar" runat="server" Text="Eliminar" Width="131px" OnClick="btn_Eliminar_Click" />
            &nbsp;
                <asp:Button ID="btn_Limpiar" runat="server" Text="Limpiar" OnClick="btn_Limpiar_Click" />
            </td>
        </tr>
        <tr>
            <td class="auto-style2">&nbsp;</td>
            <td class="auto-style3">
                <asp:Label ID="lbl_Msg" runat="server" Text=""></asp:Label>
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style2">&nbsp;</td>
            <td colspan="2">
                <asp:GridView ID="dg_ActividadEmpresa" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" Width="419px" OnSelectedIndexChanged="dg_ActividadEmpresa_SelectedIndexChanged" AllowPaging="True"
                    OnPageIndexChanging="dg_ActividadEmpresa_PageIndexChanging">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:CommandField ButtonType="Image" SelectImageUrl="~/img/lapizParaEditar.png" SelectText="" ShowSelectButton="True">
                            <ControlStyle Height="25px" Width="25px" />
                        </asp:CommandField>
                    </Columns>
                    <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                    <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
                    <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                    <SortedAscendingCellStyle BackColor="#FDF5AC" />
                    <SortedAscendingHeaderStyle BackColor="#4D0000" />
                    <SortedDescendingCellStyle BackColor="#FCF6C0" />
                    <SortedDescendingHeaderStyle BackColor="#820000" />
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td class="auto-style2">&nbsp;</td>
            <td class="auto-style3">&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
    </table>
</asp:Content>
