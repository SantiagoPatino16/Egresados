<%@ Page Language="C#" AutoEventWireup="true"
    CodeBehind="FavoritesJobs.aspx.cs"
    Inherits="Presentation.JobBoardList.FavoritesJobs"
    MasterPageFile="~/MainPage.master" %>

<asp:Content ID="MainContent" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <h2 class="mb-4">Mis Ofertas Favoritas</h2>

    <asp:Repeater ID="rptFavoritos" runat="server">
        <ItemTemplate>
            <div class="card mb-3">
                <div class="card-body">

                    <h5><%# Eval("Oferta.Titulo") %></h5>
                    <p><%# Eval("Oferta.Descripcion") %></p>
                    <p><strong>Salario:</strong> <%# Eval("Oferta.Salario") %></p>

                    <a href='EmploymentDetail.aspx?id=<%# Eval("IdOferta") %>' class="btn btn-primary btn-sm">
                        Ver detalle
                    </a>

                </div>
            </div>
        </ItemTemplate>
    </asp:Repeater>

</asp:Content>
