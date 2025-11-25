<%@ Page Language="C#" AutoEventWireup="true"
    CodeBehind="FavoritesJobs.aspx.cs"
    Inherits="Presentation.JobBoardList.FavoritesJobs"
    MasterPageFile="~/MainPage.Master" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="head" runat="server">
    <link href="css/Main.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="MainContent" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="favjb-container">

        <h2 class="favjb-title">Mis Ofertas Favoritas</h2>

        <asp:Repeater ID="rptFavoritos" runat="server">
            <ItemTemplate>

                <div class="favjb-card mb-3">
                    <div class="favjb-card-body">

                        <h5 class="favjb-card-title"><%# Eval("Oferta.Titulo") %></h5>

                        <p class="favjb-card-text"><%# Eval("Oferta.Descripcion") %></p>

                        <p class="favjb-card-text"><strong>Salario:</strong> <%# Eval("Oferta.Salario") %></p>

                        <a href='EmploymentDetail.aspx?id=<%# Eval("IdOferta") %>' 
                           class="btn btn-primary btn-sm">
                            Ver detalle
                        </a>

                    </div>
                </div>

            </ItemTemplate>
        </asp:Repeater>

    </div>

</asp:Content>
