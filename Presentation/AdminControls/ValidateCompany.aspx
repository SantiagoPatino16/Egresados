<%@ Page Title="Verificar Empresas" Language="C#" MasterPageFile="~/MainPage.Master" AutoEventWireup="true" CodeBehind="ValidateCompany.aspx.cs" Inherits="Presentation.AdminControls.ValidateCompany" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .table-actions button {
            margin-right: 5px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-5">
        <h3 class="mb-4 text-primary fw-bold">Verificación de Empresas</h3>

        <asp:Panel ID="pnlEmpresas" runat="server" CssClass="card shadow-sm p-3">
            <div class="table-responsive">
                <asp:GridView ID="gvEmpresas" runat="server" AutoGenerateColumns="False" CssClass="table table-hover align-middle"
                    GridLines="None" DataKeyNames="IdUsuario" OnRowCommand="gvEmpresas_RowCommand">
                    <Columns>
                        <asp:BoundField DataField="IdUsuario" HeaderText="ID" ItemStyle-Width="5%" />
                        <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                        <asp:BoundField DataField="Correo" HeaderText="Correo" />
                        <asp:BoundField DataField="Ciudad" HeaderText="Ciudad" />
                        <asp:BoundField DataField="FechaRegistro" HeaderText="Fecha Registro" DataFormatString="{0:dd/MM/yyyy}" />
                        <asp:BoundField DataField="PersonaResp" HeaderText="Persona Responsable" />
                        <asp:BoundField DataField="SectorIndustria" HeaderText="SectorIndustria" />
                        <asp:BoundField DataField="Telefono" HeaderText="Teléfono" />

                        <asp:TemplateField HeaderText="Acciones">
                            <ItemTemplate>
                                <div class="table-actions">
                                    <asp:Button ID="btnAprobar" runat="server" CssClass="btn btn-success btn-sm"
                                        CommandName="Aprobar" CommandArgument='<%# Eval("IdUsuario") %>' Text="Aprobar" />
                                    <asp:Button ID="btnRechazar" runat="server" CssClass="btn btn-danger btn-sm"
                                        CommandName="Rechazar" CommandArgument='<%# Eval("IdUsuario") %>' Text="Rechazar" />
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </asp:Panel>
    </div>
</asp:Content>
