<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmploymentCreate.aspx.cs"
    Inherits="Presentation.CreateOffer.EmploymentCreate" MasterPageFile="~/MainPage.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <h2 class="mt-4">Crear Oferta de Empleo</h2>
    <hr />

    <div class="row">

        <!-- TITULO -->
        <div class="col-md-6 mb-3">
            <label>Título</label>
            <asp:TextBox ID="txtTitulo" CssClass="form-control" runat="server" />
        </div>

        <!-- EMPRESA (NO EDITABLE) -->
        <div class="col-md-6 mb-3">
            <label>Empresa</label>
            <asp:TextBox ID="txtEmpresa" CssClass="form-control" runat="server" Enabled="false" />
        </div>

        <!-- CATEGORÍA -->
        <div class="col-md-12 mb-3">
            <label>Categoría</label>
            <asp:TextBox ID="txtCategoria" CssClass="form-control" runat="server" />
        </div>

        <!-- MODALIDAD -->
        <div class="col-md-6 mb-3">
            <label>Modalidad</label>
            <asp:DropDownList ID="ddlModalidades" CssClass="form-select" runat="server" />
        </div>

        <!-- TIPO DE CONTRATO -->
        <div class="col-md-6 mb-3">
            <label>Tipo de Contrato</label>
            <asp:DropDownList ID="ddlContratos" CssClass="form-select" runat="server" />
        </div>

        <!-- SALARIO -->
        <div class="col-md-6 mb-3">
            <label>Salario</label>
            <asp:TextBox ID="txtSalario" CssClass="form-control" TextMode="Number" runat="server" />
        </div>

        <!-- CIUDAD -->
        <div class="col-md-6 mb-3">
            <label>Ciudad</label>
            <asp:TextBox ID="txtCiudad" CssClass="form-control" runat="server" />
        </div>

        <!-- REQUISITOS -->
        <div class="col-md-12 mb-3">
            <label>Requisitos</label>
            <asp:TextBox ID="txtRequisitos" CssClass="form-control" TextMode="MultiLine" Rows="4" runat="server" />
        </div>

        <!-- DESCRIPCIÓN -->
        <div class="col-md-12 mb-3">
            <label>Descripción</label>
            <asp:TextBox ID="txtDescripcion" CssClass="form-control" TextMode="MultiLine" Rows="6" runat="server" />
        </div>

        <!-- FECHA DE CIERRE -->
        <div class="col-md-4 mb-3">
            <label>Fecha de Cierre</label>
            <asp:TextBox ID="txtFechaCierre" CssClass="form-control" TextMode="Date" runat="server" />
        </div>

        <!-- BOTÓN GUARDAR -->
        <div class="col-md-12 text-end">
            <asp:Button ID="btnGuardar" CssClass="btn btn-primary" Text="Publicar Oferta" runat="server"
                OnClick="btnGuardar_Click" />
        </div>
        <asp:Label ID="lblMensaje" runat="server" Text="" CssClass="text-success"></asp:Label>

    </div>

</asp:Content>
