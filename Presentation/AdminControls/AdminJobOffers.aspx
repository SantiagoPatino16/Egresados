<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminJobOffers.aspx.cs" Inherits="Presentation.AdminJobOffers" MasterPageFile="~/MainPage.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-4">
        <h2>Ofertas Pendientes de Verificación</h2>

        <asp:Label ID="lblMensaje" runat="server" CssClass="mt-2 mb-2"></asp:Label>

        <asp:GridView ID="gvOfertasPendientes" runat="server" AutoGenerateColumns="False" CssClass="table table-striped">
            <Columns>
                <asp:BoundField DataField="Titulo" HeaderText="Título" />
                <asp:BoundField DataField="Ciudad" HeaderText="Ciudad" />
                <asp:BoundField DataField="FechaPublicacion" HeaderText="Fecha" DataFormatString="{0:dd/MM/yyyy}" />

                <asp:TemplateField HeaderText="Acciones">
                    <ItemTemplate>
                        <asp:Button ID="btnAceptar" runat="server" Text="Aceptar" CssClass="btn btn-success btn-sm me-2"
                            CommandArgument='<%# Eval("IdOferta") %>' OnClick="btnAceptar_Click" />
                        <asp:Button ID="btnRechazar" runat="server" Text="Rechazar" CssClass="btn btn-danger btn-sm"
                            CommandArgument='<%# Eval("IdOferta") %>' OnClick="btnRechazar_Click" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
