<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="MantenedorTipoEmpresa.aspx.cs" Inherits="MantenedorTipoEmpresa" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table style="width:100%;">
        <tr>
            <td class="auto-style2">ID Tipo Empresa: </td>
            <td class="auto-style3">
                <asp:TextBox ID="txtIdTipoEmpresa" runat="server"></asp:TextBox>
&nbsp;&nbsp;
                </td>
            <td>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtIdTipoEmpresa" ErrorMessage="*Obligatorio"></asp:RequiredFieldValidator>
&nbsp;<asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="txtIdTipoEmpresa" ErrorMessage="*Fuera de Rango" MaximumValue="1000" MinimumValue="10" Type="Integer"></asp:RangeValidator>
&nbsp;<asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtIdTipoEmpresa" ErrorMessage="*Numérico" Operator="DataTypeCheck" Type="Integer"></asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td class="auto-style2">Descripcion: </td>
            <td class="auto-style3">
                <asp:TextBox ID="txtDescripcion" runat="server" Width="250px"></asp:TextBox>
            </td>
            <td>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtDescripcion" ErrorMessage="*Obligatorio"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="auto-style2">
                <asp:Button ID="btnGrabar" runat="server" Text="Guardar Nuevo" Width="150px" OnClick="btnGrabar_Click" />
            </td>
            <td class="auto-style3">
                <asp:Button ID="btnActualizar" runat="server" Text="Actualizar" Width="167px" OnClick="btnActualizar_Click" />
            </td>
            <td>
                <asp:Button ID="btnBorrar" runat="server" Text="Eliminar" Width="131px" OnClick="btnBorrar_Click" />
            &nbsp;
                <asp:Button ID="btnLimpiar" runat="server" OnClick="btnLimpiar_Click" Text="Limpiar" />
            </td>
        </tr>
        <tr>
            <td class="auto-style2">&nbsp;</td>
            <td class="auto-style3">
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style2">&nbsp;</td>
            <td colspan="2">
                <asp:GridView ID="gdTipoEmpresa" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" Width="419px" OnSelectedIndexChanged="gdTipoEmpresa_SelectedIndexChanged" AllowPaging="True"
                    OnPageIndexChanging="gdTipoEmpresa_PageIndexChanging">
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