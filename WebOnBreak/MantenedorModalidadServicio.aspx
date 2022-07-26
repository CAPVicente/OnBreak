<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="MantenedorModalidadServicio.aspx.cs" Inherits="MantenedorModalidadServicio" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .auto-style2 {
            height: 30px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table style="width:100%;">
        <tr>
            <td class="auto-style2">ID Modalidad: </td>
            <td class="auto-style2">
                <asp:TextBox ID="tb_idModalidad" runat="server"></asp:TextBox>
&nbsp;&nbsp;
                </td>
            <td class="auto-style2">
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="tb_idModalidad" ErrorMessage="*Obligatorio"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="auto-style2">Tipo de Evento: </td>
            <td class="auto-style3">
                <asp:DropDownList ID="cb_tipoEvento" runat="server" AutoPostBack="True" Height="18px" Width="169px">
                </asp:DropDownList>
            </td>
            <td>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="cb_tipoEvento" ErrorMessage="*Obligatorio"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="auto-style2">Nombre: </td>
            <td class="auto-style3">
                <asp:TextBox ID="tb_nombreModalidad" runat="server" Width="121px"></asp:TextBox>
            </td>
            <td>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="tb_nombreModalidad" ErrorMessage="*Obligatorio"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="auto-style2">Valor Base (UF):</td>
            <td class="auto-style3">
                <asp:TextBox ID="tb_valorBase" runat="server" Width="121px" style="margin-left: 0px"></asp:TextBox>
            </td>
            <td>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="tb_valorBase" ErrorMessage="*Obligatorio"></asp:RequiredFieldValidator>
                &nbsp;
                <asp:RangeValidator ID="RangeValidator2" runat="server" ControlToValidate="tb_valorBase" ErrorMessage="*Fuera de Rango" MaximumValue="99" MinimumValue="1" Type="Integer"></asp:RangeValidator>
                &nbsp;
                <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="tb_valorBase" ErrorMessage="*Numérico" Operator="DataTypeCheck" Type="Integer"></asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td class="auto-style2">Personal Base:</td>
            <td class="auto-style3">
                <asp:TextBox ID="tb_personalBase" runat="server" Width="121px" style="margin-left: 0px"></asp:TextBox>
            </td>
            <td>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="tb_personalBase" ErrorMessage="*Obligatorio"></asp:RequiredFieldValidator>
                &nbsp;
                <asp:RangeValidator ID="RangeValidator3" runat="server" ControlToValidate="tb_personalBase" ErrorMessage="*Fuera de Rango" MaximumValue="50" MinimumValue="1" Type="Integer"></asp:RangeValidator>
                &nbsp;
                <asp:CompareValidator ID="CompareValidator3" runat="server" ControlToValidate="tb_personalBase" ErrorMessage="*Numérico" Operator="DataTypeCheck" Type="Integer"></asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td class="auto-style2">
                <asp:Button ID="btn_GuardarModalidad" runat="server" Text="Registrar" Width="150px" OnClick="btn_GuardarModalidad_Click" />
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
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style2">&nbsp;</td>
            <td colspan="2">
                <asp:GridView ID="dg_ModalidadServicio" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" Width="419px" OnSelectedIndexChanged="dg_ModalidadServicio_SelectedIndexChanged" AllowPaging="True" 
                    OnPageIndexChanging="dg_ModalidadServicio_PageIndexChanging">
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

