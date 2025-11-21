<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ApplyJob.aspx.cs"
    Inherits="Presentation.JobBoardList.ApplyJob" MasterPageFile="~/MainPage.Master" %>

<asp:Content ID="MainContent" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <h2 class="mb-4">Postulación a la Oferta</h2>

    <!-- ALERTA DE ÉXITO -->
    <div id="alertExito" runat="server" visible="false"
         class="alert alert-success alert-dismissible fade show" role="alert">
        ¡Tu postulación ha sido enviada exitosamente!
    </div>

    <asp:Panel ID="pnlFormulario" runat="server">

        <p><strong>Sube tu hoja de vida (PDF):</strong></p>

        <asp:FileUpload ID="fuCV" runat="server" CssClass="form-control" />

        <br />

        <asp:Button ID="btnPostular" runat="server"
            Text="Postularme"
            CssClass="btn btn-success"
            OnClick="btnPostular_Click" />
    </asp:Panel>

</asp:Content>
