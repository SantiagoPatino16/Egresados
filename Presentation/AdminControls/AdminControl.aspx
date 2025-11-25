<%@ Page Title="Panel de Administración" Language="C#" MasterPageFile="~/MainPage.Master" AutoEventWireup="true" CodeBehind="AdminControl.aspx.cs" Inherits="Presentation.User.AdminControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<div class="container mt-5 mb-5">
    <div class="card shadow border-0 rounded-4">
        <div class="card-header bg-dark text-white text-center rounded-top-4">
            <h3 class="mb-0">Panel de Administración</h3>
        </div>

        <div class="card-body p-4">
            <p class="text-secondary text-center mb-4">
                Panel de administrador
            </p>

            <div class="row text-center gy-4">

                <!-- Opción: Añadir Evento -->
                <div class="col-md-4">
                    <div class="card h-100 border-0 shadow-sm rounded-4">
                        <div class="card-body d-flex flex-column justify-content-center align-items-center">
                            <i class="bi bi-calendar-event text-primary" style="font-size: 3rem;"></i>
                            <h5 class="mt-3 mb-3">Gestión de Eventos</h5>
                            <asp:Button ID="btnIrEventos" runat="server" Text="Añadir Evento" CssClass="btn btn-outline-primary rounded-pill px-4" OnClick="btnIrEventos_Click" />
                        </div>
                    </div>
                </div>

                <!-- Opción: Aprobar Empresas -->
                <div class="col-md-4">
                    <div class="card h-100 border-0 shadow-sm rounded-4">
                        <div class="card-body d-flex flex-column justify-content-center align-items-center">
                            <i class="bi bi-clipboard2-check text-primary" style="font-size: 3rem;"></i>
                            <h5 class="mt-3 mb-3">Aprobar registro empresas </h5>
                            <asp:Button ID="btnEmpresas" runat="server" Text="Ver empresas" CssClass="btn btn-outline-success rounded-pill px-4" OnClick="btnVerEmpresas_Click" />
                        </div>
                    </div>
                </div>

                <!-- Opción: Ver usuarios y empresas registradas -->
                <div class="col-md-4">
                    <div class="card h-100 border-0 shadow-sm rounded-4">
                        <div class="card-body d-flex flex-column justify-content-center align-items-center">
                            <i class="bi bi-gear text-secondary" style="font-size: 3rem;"></i>
                            <h5 class="mt-3 mb-3">Gestión</h5>
                            <asp:Button ID="btnVerUsuarios" runat="server" Text="Gestión de cuentas" CssClass="btn btn-outline-secondary rounded-pill px-4" OnClick="btnVerUsuarios_Click" />
                        </div>
                    </div>
                </div>

                <!-- Opción: Validar Ofertas -->
                <div class="col-md-4">
                    <div class="card h-100 border-0 shadow-sm rounded-4">
                        <div class="card-body d-flex flex-column justify-content-center align-items-center">
                            <i class="bi bi-tags text-warning" style="font-size: 3rem;"></i>
                            <h5 class="mt-3 mb-3">Validación de Ofertas</h5>
                            <asp:Button ID="btnValidarOfertas" runat="server" Text="Validar Ofertas" CssClass="btn btn-outline-warning rounded-pill px-4" OnClick="btnValidarOfertas_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>

</asp:Content>
