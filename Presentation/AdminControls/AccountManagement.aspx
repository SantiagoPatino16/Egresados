<%@ Page Title="" Language="C#" MasterPageFile="~/MainPage.Master" AutoEventWireup="true" CodeBehind="AccountManagement.aspx.cs" Inherits="Presentation.AdminControls.AccountManagement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-4">

        <!-- Título -->
        <h3 class="mb-4">Gestión de Cuentas</h3>

        <!-- Filtros -->
        <div class="card mb-4">
            <div class="card-body">

                <div class="row g-3">

                    <!-- Tipo de cuenta -->
                    <div class="col-md-4">
                        <label class="form-label">Tipo de cuenta</label>
                        <asp:DropDownList ID="ddlRol" runat="server" CssClass="form-select">
                            <asp:ListItem Text="Todos" Value="" />
                            <asp:ListItem Text="Egresados" Value="Egresado" />
                            <asp:ListItem Text="Empresas" Value="Empresa" />
                        </asp:DropDownList>
                    </div>

                    <!-- Búsqueda general -->
                    <div class="col-md-6">
                        <label class="form-label">Buscar</label>
                        <asp:TextBox ID="txtBusqueda" runat="server" CssClass="form-control" placeholder="Nombre, correo o identificación" />
                    </div>

                    <!-- Botón buscar -->
                    <div class="col-md-2 d-flex align-items-end">
                        <asp:Button ID="btnFiltrar" runat="server" Text="Filtrar" CssClass="btn btn-primary w-100" OnClick="btnFiltrar_Click" />
                    </div>

                </div>

            </div>
        </div>

        <!-- Listado -->
        <div class="card">
            <div class="card-body">

                <asp:GridView ID="gvCuentas" runat="server"
                    CssClass="table table-striped table-bordered"
                    AutoGenerateColumns="false"
                    AllowPaging="true" PageSize="10"
                    OnPageIndexChanging="gvCuentas_PageIndexChanging">

                    <Columns>
                        <asp:BoundField DataField="Rol" HeaderText="Tipo" />
                        <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                        <asp:BoundField DataField="Correo" HeaderText="Correo" />

                        <asp:TemplateField HeaderText="Acciones">
                            <ItemTemplate>
                                <asp:LinkButton ID="btnVer" runat="server" CssClass="btn btn-sm btn-secondary me-2"
                                    CommandName="Ver" CommandArgument='<%# Eval("IdUsuario") %>'>Ver</asp:LinkButton>

                                <asp:LinkButton ID="btnEditar" runat="server" CssClass="btn btn-sm btn-warning me-2"
                                    CommandName="Editar" CommandArgument='<%# Eval("IdUsuario") %>'>Editar</asp:LinkButton>

                                <asp:LinkButton ID="btnDeshabilitar" runat="server" CssClass="btn btn-sm btn-danger"
                                    CommandName="Deshabilitar" CommandArgument='<%# Eval("IdUsuario") %>'>Deshabilitar</asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>

                    </Columns>

                </asp:GridView>

            </div>
        </div>

    </div>

</asp:Content>
