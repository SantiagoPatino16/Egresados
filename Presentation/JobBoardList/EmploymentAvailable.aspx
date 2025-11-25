<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmploymentAvailable.aspx.cs" 
    Inherits="Presentation.JobBoardList.EmploymentAvailable" MasterPageFile="~/MainPage.Master" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="head" runat="server">

    <link href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.11.3/font/bootstrap-icons.css" rel="stylesheet">
    <link href="css/Main.css" rel="stylesheet" />

</asp:Content>

<asp:Content ID="MainContent" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <h2 class="empav-title mb-4">Bolsa de Empleo</h2>

    <!-- FILTROS -->
    <div class="row mb-3">
        <div class="col-md-4">
            <label for="ddlCategories" class="form-label">Categoría</label>
            <asp:DropDownList ID="ddlCategories" runat="server" CssClass="empav-select form-select" />
        </div>
        <div class="col-md-4">
            <label for="ddlWorkModes" class="form-label">Modalidad de trabajo</label>
            <asp:DropDownList ID="ddlWorkModes" runat="server" CssClass="empav-select form-select" />
        </div>
        <div class="col-md-4">
            <label for="ddlContractTypes" class="form-label">Tipo de contrato</label>
            <asp:DropDownList ID="ddlContractTypes" runat="server" CssClass="empav-select form-select" />
        </div>
    </div>

    <div class="mb-4">
        <asp:Button ID="btnFiltrar" runat="server" CssClass="btn btn-primary empav-btn-filter"
            Text="Filtrar" OnClick="btnFiltrar_Click" />
    </div>

    <!-- LISTADO DE EMPLEOS -->
    <asp:Repeater ID="rptJobs" runat="server" OnItemCommand="rptJobs_ItemCommand" OnItemDataBound="rptJobs_ItemDataBound">
        <ItemTemplate>

            <div class="card empav-job-card mb-3">
                <div class="card-body">

                    <h5 class="empav-job-title"><%# Eval("Titulo") %></h5>

                    <p class="empav-job-description"><%# Eval("Descripcion") %></p>

                    <div class="mt-3 d-flex gap-2">

                        <!-- Ver detalle -->
                        <a href='EmploymentDetail.aspx?id=<%# Eval("IdOferta") %>' class="btn btn-primary btn-sm empav-btn">
                            <i class="bi bi-info-circle"></i> Ver detalle
                        </a>

                        <!-- Postularme -->
                        <asp:LinkButton
                            ID="btnPostularme"
                            runat="server"
                            CssClass="btn btn-success btn-sm empav-btn-success"
                            CommandName="Postularme"
                            CommandArgument='<%# Eval("IdOferta") %>'
                            PostBackUrl='ApplyJob.aspx?id=<%# Eval("IdOferta") %>'>
                            <i class="bi bi-send"></i> Postularme
                        </asp:LinkButton>

                        <!-- Favorito -->
                        <asp:LinkButton
                            ID="btnFavorito"
                            runat="server"
                            CssClass="btn btn-warning btn-sm empav-btn-warning"
                            CommandName="Favorito"
                            CommandArgument='<%# Eval("IdOferta") %>'>
                            <i class="bi bi-star"></i> Favorito
                        </asp:LinkButton>

                        <!-- Quitar Favorito -->
                        <asp:LinkButton
                            ID="btnQuitarFavorito"
                            runat="server"
                            CssClass="btn btn-danger btn-sm empav-btn-danger"
                            CommandName="QuitarFavorito"
                            CommandArgument='<%# Eval("IdOferta") %>'
                            Visible="false">
                            <i class="bi bi-x-circle"></i> Quitar de Favoritos
                        </asp:LinkButton>

                    </div>
                </div>
            </div>

        </ItemTemplate>
    </asp:Repeater>

</asp:Content>
