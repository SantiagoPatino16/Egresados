<%@ Page Language="C#" AutoEventWireup="true"
    CodeBehind="EmploymentDetail.aspx.cs"
    Inherits="Presentation.JobBoardList.EmploymentDetail"
    MasterPageFile="~/MainPage.Master" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="head" runat="server">
    <link href="css/Main.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="MainContent" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="empdet-container">

        <h2 class="empdet-title">Detalle de la Oferta</h2>

        <!-- Panel con los datos -->
        <asp:Panel ID="pnlDetalle" runat="server" Visible="false">

            <h3 class="empdet-subtitle"><asp:Literal ID="litTitulo" runat="server"></asp:Literal></h3>

            <p class="empdet-text"><strong class="empdet-strong">Descripción:</strong></p>
            <p class="empdet-text"><asp:Literal ID="litDescripcion" runat="server"></asp:Literal></p>

            <p class="empdet-text"><strong class="empdet-strong">Requisitos:</strong></p>
            <p class="empdet-text"><asp:Literal ID="litRequisitos" runat="server"></asp:Literal></p>

            <p class="empdet-text"><strong class="empdet-strong">Ciudad:</strong>
                <asp:Literal ID="litCiudad" runat="server"></asp:Literal>
            </p>

            <p class="empdet-text"><strong class="empdet-strong">Salario:</strong> $
                <asp:Literal ID="litSalario" runat="server"></asp:Literal>
            </p>

            <p class="empdet-text"><strong class="empdet-strong">Fecha Publicación:</strong>
                <asp:Literal ID="litFechaPublicacion" runat="server"></asp:Literal>
            </p>

            <p class="empdet-text"><strong class="empdet-strong">Fecha Cierre:</strong>
                <asp:Literal ID="litFechaCierre" runat="server"></asp:Literal>
            </p>

            <p class="empdet-text"><strong class="empdet-strong">Estado:</strong>
                <asp:Literal ID="litEstado" runat="server"></asp:Literal>
            </p>

            <!-- Botón de volver -->
            <a href="EmploymentAvailable.aspx" class="empdet-btn-back">
                ← Volver a la bolsa de empleo
            </a>

        </asp:Panel>

        <!-- Panel de error -->
        <asp:Panel ID="pnlError" runat="server" Visible="false" CssClass="alert empdet-alert">
            No se encontró la oferta de empleo.
        </asp:Panel>

    </div>

</asp:Content>
