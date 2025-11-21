<%@ Page Language="C#" AutoEventWireup="true"
    CodeBehind="EmploymentAvailable.aspx.cs"
    Inherits="Presentation.JobBoardList.EmploymentAvailable"
    MasterPageFile="~/MainPage.Master" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="MainContent" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <h2 class="mb-4">Bolsa de Empleo</h2>

    <div class="row mb-3">
        <div class="col-md-4">
            <asp:DropDownList ID="ddlCategories" runat="server" CssClass="form-select" />
        </div>
        <div class="col-md-4">
            <asp:DropDownList ID="ddlWorkModes" runat="server" CssClass="form-select" />
        </div>
        <div class="col-md-4">
            <asp:DropDownList ID="ddlContractTypes" runat="server" CssClass="form-select" />
        </div>
    </div>

    <div class="mb-4">
        <asp:Button ID="btnFiltrar" runat="server" CssClass="btn btn-primary"
            Text="Filtrar" OnClick="btnFiltrar_Click" />
    </div>

    <asp:Repeater ID="rptJobs" runat="server"
        OnItemCommand="rptJobs_ItemCommand"
        OnItemDataBound="rptJobs_ItemDataBound">

        <ItemTemplate>

            <div class="card mb-3">
                <div class="card-body">

                    <h5><%# Eval("Titulo") %></h5>
                    <p><%# Eval("Descripcion") %></p>

                    <div class="mt-3 d-flex gap-2">

                        <a href='EmploymentDetail.aspx?id=<%# Eval("IdOferta") %>'
                           class="btn btn-primary btn-sm">
                            Ver detalle
                        </a>

                        <a href='ApplyJob.aspx?id=<%# Eval("IdOferta") %>'
                           class="btn btn-success btn-sm">
                            Postularme
                        </a>
                        <asp:LinkButton 
                            ID="btnFavorito"
                            runat="server"
                            CssClass="btn btn-warning btn-sm"
                            CommandName="Favorito"
                            CommandArgument='<%# Eval("IdOferta") %>'>
                            Favorito
                        </asp:LinkButton>
                        <asp:LinkButton 
                            ID="btnQuitarFavorito"
                            runat="server"
                            CssClass="btn btn-danger btn-sm"
                            CommandName="QuitarFavorito"
                            CommandArgument='<%# Eval("IdOferta") %>'
                            Visible="false">
                            Quitar de favoritos
                        </asp:LinkButton>
                    </div>
                </div>
            </div>
        </ItemTemplate>
    </asp:Repeater>
</asp:Content>