<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ApplyJob.aspx.cs"
    Inherits="Presentation.JobBoardList.ApplyJob" MasterPageFile="~/MainPage.Master" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="head" runat="server">

    <link href="css/Main.css" rel="stylesheet" />

</asp:Content>

<asp:Content ID="MainContent" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="applyjb-container">

        <h2 class="applyjb-title">Postulación a la Oferta</h2>

        <!-- ALERTA DE ÉXITO -->
        <div id="alertExito" runat="server" visible="false"
             class="alert alert-success alert-dismissible fade show applyjb-alert" role="alert">
            ¡Tu postulación ha sido enviada exitosamente!
        </div>

        <!-- FORMULARIO -->
        <asp:Panel ID="pnlFormulario" runat="server">

            <p class="applyjb-label"><strong class="applyjb-strong">Sube tu hoja de vida (PDF):</strong></p>

            <asp:FileUpload ID="fuCV" runat="server" CssClass="applyjb-input form-control" />

            <br />

            <asp:Button ID="btnPostular" runat="server"
                Text="Postularme"
                CssClass="applyjb-btn-success btn btn-success"
                OnClick="btnPostular_Click" />

        </asp:Panel>

    </div>

</asp:Content>
