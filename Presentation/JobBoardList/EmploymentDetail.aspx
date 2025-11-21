<%@ Page Language="C#" AutoEventWireup="true"
    CodeBehind="EmploymentDetail.aspx.cs"
    Inherits="Presentation.JobBoardList.EmploymentDetail"
    MasterPageFile="~/MainPage.Master" %>

<asp:Content ID="MainContent" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <h2 class="mb-4">Detalle de la Oferta</h2>

    <asp:Panel ID="pnlDetalle" runat="server" Visible="false">

        <h3><asp:Literal ID="litTitulo" runat="server"></asp:Literal></h3>

        <p><strong>Descripción:</strong></p>
        <p><asp:Literal ID="litDescripcion" runat="server"></asp:Literal></p>

        <p><strong>Requisitos:</strong></p>
        <p><asp:Literal ID="litRequisitos" runat="server"></asp:Literal></p>

        <p><strong>Ciudad:</strong> 
            <asp:Literal ID="litCiudad" runat="server"></asp:Literal>
        </p>

        <p><strong>Salario:</strong> $
            <asp:Literal ID="litSalario" runat="server"></asp:Literal>
        </p>

        <!-- NUEVA INFORMACIÓN QUE YA EXISTE EN TU TABLA -->

        <p><strong>Fecha Publicación:</strong>
            <asp:Literal ID="litFechaPublicacion" runat="server"></asp:Literal>
        </p>

        <p><strong>Fecha Cierre:</strong>
            <asp:Literal ID="litFechaCierre" runat="server"></asp:Literal>
        </p>

        <p><strong>Estado:</strong>
            <asp:Literal ID="litEstado" runat="server"></asp:Literal>
        </p>

        <div class="mt-3">
            <a href='ApplyJob.aspx?id=<%= Request.QueryString["id"] %>' class="btn btn-success">
                Postularme
            </a>
        </div>

    </asp:Panel>

    <asp:Panel ID="pnlError" runat="server" Visible="false" CssClass="alert alert-danger mt-4">
        No se encontró la oferta de empleo.
    </asp:Panel>

</asp:Content>
