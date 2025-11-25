<%@ Page Title="Inicio" Language="C#" MasterPageFile="~/MainPage.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="Presentation.Inicio.Home" ResponseEncoding="utf-8" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.11.3/font/bootstrap-icons.css" rel="stylesheet">
    <link href="css/Main.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="home-row row g-4">
        <!-- LEFT: Profile -->
        <aside class="col-lg-3 order-2 order-lg-1">
            <div class="home-card shadow-sm home-profile-card">
                <div class="home-banner"></div>
                <div class="card-body">
                    <div class="position-relative home-avatar-wrap w-100 text-center">
                        <asp:Image ID="imgAvatar" runat="server" CssClass="home-avatar border border-3 border-white shadow-sm" Visible="false" />
                        <span id="avatarPlaceholder" runat="server" class="home-avatar border border-3 border-white shadow-sm"></span>
                        <a href="../User/Profile.aspx" class="home-add-btn" title="Ver perfil">
                            <i class="bi bi-eye"></i>
                        </a>
                    </div>
                    <div class="text-center mt-2">
                        <div class="fw-semibold">
                            <asp:Label ID="lblNombreUsuario" runat="server" Text="Nombre Completo"></asp:Label>
                        </div>
                        <div class="small text-muted">
                            <asp:Label ID="lblDescripcion" runat="server" Text="Institución Universitaria Pascual Bravo"></asp:Label>
                        </div>
                        <div class="small text-muted">
                            <asp:Label ID="lblCiudad" runat="server" Text="Medellín, Antioquia"></asp:Label>
                        </div>
                    </div>
                    <hr>
                    <div class="d-flex align-items-center small text-muted">
                        <i class="bi bi-shield-check me-2"></i>
                        <span>Institución Universitaria Pascual Bravo</span>
                    </div>
                </div>
            </div>
        </aside>

        <!-- CENTER: Events feed -->
        <section class="col-lg-6 order-1 order-lg-2">
            <div class="text-muted small mb-2">Eventos</div>
            
            <asp:Repeater ID="rptEventos" runat="server">
                <ItemTemplate>
                    <div class="home-card shadow-sm mb-3">
                        <div class="card-body">
                            <div class="d-flex justify-content-between align-items-center mb-2">
                                <div>
                                    <div class="fw-semibold"><%# Eval("Organizador") %></div>
                                    <small class="text-muted"><%# Eval("Tipo") %></small>
                                </div>
                                <small class="text-muted"><%# Eval("FechaCreacion", "{0:dd/MM/yyyy}") %></small>
                            </div>
                            <div class="row g-4 align-items-start">
                                <div class="col-md-5">
                                    <%# ObtenerImagenEvento(Eval("RutaImagen"), Eval("Titulo")) %>
                                </div>
                                <div class="col-md-7">
                                    <span class="badge bg-<%# ObtenerColorEstado(Eval("FechaInicio"), Eval("FechaFin")) %> mb-2">
                                        <%# ObtenerEstadoEvento(Eval("FechaInicio"), Eval("FechaFin")) %>
                                    </span>
                                    <h5 class="mt-1"><%# Eval("Titulo") %></h5>
                                    <div class="small text-muted">
                                        <i class="bi bi-geo-alt-fill"></i> <%# Eval("Lugar") %><br>
                                        <i class="bi bi-calendar3"></i> <%# Eval("FechaInicio", "{0:dddd dd 'de' MMMM}") %><br>
                                        <i class="bi bi-clock-fill"></i> <%# Eval("FechaInicio", "{0:hh:mm tt}") %> 
                                        <%# Eval("FechaFin") != null ? " – " + ((DateTime)Eval("FechaFin")).ToString("hh:mm tt") : "" %>
                                    </div>
                                    <p class="mt-2 mb-2"><%# Eval("Descripcion") %></p>
                                    <button type="button" 
                                        class="btn btn-primary btn-sm mt-2"
                                        data-bs-toggle="modal"
                                        data-bs-target="#homeDetalleEventoModal"
                                        onclick="cargarDetallesEvento(this)"
                                        data-titulo='<%# Eval("Titulo") %>'
                                        data-descripcion='<%# Eval("Descripcion") %>'
                                        data-lugar='<%# Eval("Lugar") %>'
                                        data-fechainicio='<%# Eval("FechaInicio", "{0:dd/MM/yyyy HH:mm}") %>'
                                        data-fechafin='<%# Eval("FechaFin") != null ? ((DateTime)Eval("FechaFin")).ToString("dd/MM/yyyy HH:mm") : "" %>'
                                        data-organizador='<%# Eval("Organizador") %>'
                                        data-tipo='<%# Eval("Tipo") %>'
                                        data-imagen='<%# Eval("RutaImagen") %>'>
                                        <i class="bi bi-info-circle me-1"></i>Ver detalles
                                    </button>
                                </div>
                            </div>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
            
            <asp:Label ID="lblSinEventos" runat="server" CssClass="text-muted" Text="No hay eventos disponibles" Visible="false"></asp:Label>
        </section>

        <!-- RIGHT: Suggestions -->
        <aside class="col-lg-3 order-3">
            <div class="home-card shadow-sm home-suggestions-card">
                <div class="card-body">
                    <h6 class="mb-3">Sugerencias</h6>
                    <div class="vstack gap-3">
                        <asp:Repeater ID="rptSugerencias" runat="server">
                            <ItemTemplate>
                                <div class="d-flex align-items-start justify-content-between">
                                    <div class="d-flex align-items-center gap-2">
                                        <%# ObtenerAvatarUsuario(Eval("FotoPerfil")) %>
                                        <div>
                                            <div class="fw-semibold"><%# Eval("Nombre") %></div>
                                            <div class="small text-muted">
                                                <span class="home-dot"></span>
                                                <%# Eval("Rol").ToString() == "Empresa" ? 
                                                    (Eval("SectorIndustria") ?? "Empresa") : 
                                                    (Eval("ProgramaAcademico") ?? Eval("Rol")) %>
                                            </div>
                                        </div>
                                    </div>
                                    <a href="../User/Profile.aspx?id=<%# Eval("IdUsuario") %>" class="small text-decoration-none">Ver perfil</a>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </div>
            </div>
        </aside>
    </div>
    
    <!-- Modal Detalles del Evento -->
    <div class="modal fade" id="homeDetalleEventoModal" tabindex="-1" aria-labelledby="detalleEventoLabel" aria-hidden="true">
        <div class="modal-dialog modal-lg modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header text-white" style="background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);">
                    <h5 class="modal-title fw-bold" id="detalleEventoLabel">
                        <i class="bi bi-calendar-event me-2"></i>Detalles del Evento
                    </h5>
                    <button type="button" class="btn-close btn-close-white" data-bs-dismiss="modal" aria-label="Cerrar"></button>
                </div>
                <div class="modal-body">
                    <div class="text-center mb-3">
                        <img id="homeImgEventoModal" src="" alt="Imagen evento" class="img-fluid rounded shadow-sm" style="max-height: 300px; object-fit: cover;">
                    </div>
                    <h4 id="homeTituloEventoModal" class="fw-bold text-primary mb-3"></h4>
                    <p id="homeDescripcionEventoModal" class="text-secondary"></p>
                    
                    <hr>
                    
                    <div class="row g-3">
                        <div class="col-md-6">
                            <p class="mb-2">
                                <i class="bi bi-geo-alt-fill text-danger me-2"></i>
                                <strong>Lugar:</strong> <span id="homeLugarEventoModal"></span>
                            </p>
                        </div>
                        <div class="col-md-6">
                            <p class="mb-2">
                                <i class="bi bi-tag-fill text-info me-2"></i>
                                <strong>Tipo:</strong> <span id="homeTipoEventoModal"></span>
                            </p>
                        </div>
                        <div class="col-12">
                            <p class="mb-2">
                                <i class="bi bi-calendar3 text-success me-2"></i>
                                <strong>Fecha:</strong> <span id="homeFechaEventoModal"></span>
                            </p>
                        </div>
                        <div class="col-12">
                            <p class="mb-0">
                                <i class="bi bi-person-fill text-warning me-2"></i>
                                <strong>Organizador:</strong> <span id="homeOrganizadorEventoModal"></span>
                            </p>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">
                        <i class="bi bi-x-circle me-1"></i>Cerrar
                    </button>
                </div>
            </div>
        </div>
    </div>

    <script>
        document.addEventListener('DOMContentLoaded', function () {
            let lastScrollTop = 0;
            const navbar = document.querySelector('.home-navbar');
            const delta = 5;

            window.addEventListener('scroll', function () {
                let scrollTop = window.pageYOffset || document.documentElement.scrollTop;

                if (Math.abs(lastScrollTop - scrollTop) <= delta) return;

                if (scrollTop > lastScrollTop && scrollTop > 100) {
                    navbar.classList.add('home-navbar-hidden');
                } else {
                    navbar.classList.remove('home-navbar-hidden');
                }

                lastScrollTop = scrollTop;
            });
        });

        function cargarDetallesEvento(btn) {
            document.getElementById('homeTituloEventoModal').innerText = btn.getAttribute('data-titulo');
            document.getElementById('homeDescripcionEventoModal').innerText = btn.getAttribute('data-descripcion');
            document.getElementById('homeLugarEventoModal').innerText = btn.getAttribute('data-lugar');

            let fechaInicio = btn.getAttribute('data-fechainicio');
            let fechaFin = btn.getAttribute('data-fechafin');
            let fechaTexto = fechaInicio;
            if (fechaFin) fechaTexto += ' - ' + fechaFin;
            document.getElementById('homeFechaEventoModal').innerText = fechaTexto;

            document.getElementById('homeOrganizadorEventoModal').innerText = btn.getAttribute('data-organizador');
            document.getElementById('homeTipoEventoModal').innerText = btn.getAttribute('data-tipo');

            let imagen = btn.getAttribute('data-imagen');
            document.getElementById('homeImgEventoModal').src = (imagen && imagen.trim() !== '') ? imagen : '../Resources/events/default.png';
        }
    </script>
</asp:Content>
