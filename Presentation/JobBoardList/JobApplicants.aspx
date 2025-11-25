<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="JobApplicants.aspx.cs" Inherits="Presentation.JobBoardList.JobApplicants" MasterPageFile="~/MainPage.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <h2>Postulantes de Mis Ofertas</h2>

    <!-- Alerta Bootstrap -->
    <div id="alertEstado" runat="server" visible="false"
         class="alert alert-success alert-dismissible fade show" role="alert">
        Estado actualizado correctamente.
        <button type="button" class="btn-close" data-bs-dismiss="alert"></button>
    </div>

    <asp:GridView ID="gvPostulantes" runat="server" AutoGenerateColumns="False"
        CssClass="table table-bordered" OnRowCommand="gvPostulantes_RowCommand" OnRowDataBound="gvPostulantes_RowDataBound">

        <Columns>

            <asp:BoundField DataField="IdPostulacion" HeaderText="ID" />

            <asp:BoundField DataField="Egresado.NombreCompleto" HeaderText="Egresado" />

            <asp:BoundField DataField="OfertaEmpleo.Titulo" HeaderText="Oferta" />

            <asp:BoundField DataField="FechaPostulacion" HeaderText="Fecha"
                DataFormatString="{0:dd/MM/yyyy}" />

            <asp:BoundField DataField="EstadoFinal" HeaderText="Estado" />

            <asp:TemplateField HeaderText="CV">
                <ItemTemplate>
                    <asp:HyperLink ID="hlCV" runat="server"
                        NavigateUrl='<%# "data:application/pdf;base64," + Eval("CvUrl") %>'
                        Text="Ver CV" Target="_blank" />
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Acciones">
                <ItemTemplate>
                    <asp:Button ID="btnAceptar" runat="server"
                        Text="Aceptar"
                        CommandName="CambiarEstado"
                        CommandArgument='<%# Eval("IdPostulacion") + ";Aceptada" %>'
                        CssClass="btn btn-success btn-sm" />

                    <asp:Button ID="btnRechazar" runat="server"
                        Text="Rechazar"
                        CommandName="CambiarEstado"
                        CommandArgument='<%# Eval("IdPostulacion") + ";Rechazada" %>'
                        CssClass="btn btn-danger btn-sm" />
                </ItemTemplate>
            </asp:TemplateField>

        </Columns>

    </asp:GridView>

</asp:Content>
