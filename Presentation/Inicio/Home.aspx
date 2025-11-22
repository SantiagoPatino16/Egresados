<%@ Page Title="Inicio" Language="C#" MasterPageFile="~/MainPage.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="Presentation.Inicio.Home" ResponseEncoding="utf-8" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.11.3/font/bootstrap-icons.css" rel="stylesheet">
    <style>
        body { background: #eef2f5; }
        .app-container { max-width: 1200px; }
        
        /* SIDEBARS - Sin sticky, scroll normal */
        
        /* LEFT PROFILE */
        .profile-card .banner {
            height: 80px;
            background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
            border-top-left-radius: .5rem;
            border-top-right-radius: .5rem;
        }
        .avatar {
            width: 64px;
            height: 64px;
            border-radius: 50%;
            background: linear-gradient(135deg, #e9ecef 0%, #d4d9e0 100%);
            display: inline-block;
            border: 3px solid white;
        }
        .avatar-wrap {
            margin-top: -32px;
        }
        .avatar-wrap .add {
            position: absolute;
            right: 18px;
            bottom: -4px;
            width: 26px;
            height: 26px;
            border-radius: 50%;
            background: #667eea;
            color: white;
            display: grid;
            place-items: center;
            box-shadow: 0 2px 8px rgba(102, 126, 234, 0.4);
            cursor: pointer;
            transition: all 0.3s ease;
        }
        .avatar-wrap .add:hover {
            transform: scale(1.1);
            box-shadow: 0 4px 12px rgba(102, 126, 234, 0.6);
        }
        
        /* EVENT CARD */
        .img-placeholder {
            width: 100%;
            aspect-ratio: 16/9;
            background: linear-gradient(135deg, #e9ecef 0%, #f8f9fa 100%);
            border-radius: .35rem;
            display: flex;
            align-items: center;
            justify-content: center;
            color: #adb5bd;
            font-size: 2rem;
        }
        
        .card {
            transition: transform 0.2s ease, box-shadow 0.2s ease;
        }
        
        .card:hover {
            transform: translateY(-2px);
            box-shadow: 0 4px 12px rgba(0,0,0,0.1) !important;
        }
        
        /* RIGHT SUGGESTIONS */
        .dot {
            width: 10px;
            height: 10px;
            border-radius: 50%;
            background: #1cc5c5;
            display: inline-block;
            margin-right: .5rem;
        }
        
        .suggestions-card .avatar {
            width: 40px;
            height: 40px;
            min-width: 40px;
            min-height: 40px;
            background: linear-gradient(135deg, #f093fb 0%, #f5576c 100%);
            border-radius: 50%;
            object-fit: cover;
        }
        
        /* Asegurar que las imágenes de perfil sean redondas */
        .suggestions-card img.avatar {
            border-radius: 50%;
            object-fit: cover;
        }
        
        /* Animación suave para el scroll */
        html {
            scroll-behavior: smooth;
        }
        
        /* NAVBAR DINÁMICO */
        .navbar {
            transition: transform 0.3s ease-in-out;
        }
        
        .navbar.navbar-hidden {
            transform: translateY(-100%);
        }
        
        /* Imagen de evento */
        .event-image {
            width: 100%;
            aspect-ratio: 16/9;
            object-fit: cover;
            border-radius: .35rem;
        }
    </style>
    
    <script>
        // Script para navbar que se oculta al bajar y aparece al subir
        document.addEventListener('DOMContentLoaded', function() {
            let lastScrollTop = 0;
            const navbar = document.querySelector('.navbar');
            const delta = 5; // Sensibilidad del scroll
            
            window.addEventListener('scroll', function() {
                let scrollTop = window.pageYOffset || document.documentElement.scrollTop;
                
                // Evitar cambios menores
                if (Math.abs(lastScrollTop - scrollTop) <= delta) {
                    return;
                }
                
                if (scrollTop > lastScrollTop && scrollTop > 100) {
                    // Bajando - ocultar navbar
                    navbar.classList.add('navbar-hidden');
                } else {
                    // Subiendo - mostrar navbar
                    navbar.classList.remove('navbar-hidden');
                }
                
                lastScrollTop = scrollTop;
            });
        });
        
        // Script para cargar detalles del evento en el modal
        function cargarDetallesEvento(btn) {
            document.getElementById('tituloEventoModal').innerText = btn.getAttribute('data-titulo');
            document.getElementById('descripcionEventoModal').innerText = btn.getAttribute('data-descripcion');
            document.getElementById('lugarEventoModal').innerText = btn.getAttribute('data-lugar');
            
            var fechaInicio = btn.getAttribute('data-fechainicio');
            var fechaFin = btn.getAttribute('data-fechafin');
            var fechaTexto = fechaInicio;
            if (fechaFin) {
                fechaTexto += ' - ' + fechaFin;
            }
            document.getElementById('fechaEventoModal').innerText = fechaTexto;
            
            document.getElementById('organizadorEventoModal').innerText = btn.getAttribute('data-organizador');
            document.getElementById('tipoEventoModal').innerText = btn.getAttribute('data-tipo');
            
            var imagen = btn.getAttribute('data-imagen');
            if (imagen && imagen.trim() !== '') {
                document.getElementById('imgEventoModal').src = imagen;
            } else {
                document.getElementById('imgEventoModal').src = '../Resources/events/default.png';
            }
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row g-4">
        <!-- LEFT: Profile -->
        <aside class="col-lg-3 order-2 order-lg-1">
            <div class="card shadow-sm profile-card">
                <div class="banner"></div>
                <div class="card-body">
                    <div class="position-relative avatar-wrap w-100 text-center">
                        <asp:Image ID="imgAvatar" runat="server" CssClass="avatar border border-3 border-white shadow-sm" Visible="false" />
                        <span id="avatarPlaceholder" runat="server" class="avatar border border-3 border-white shadow-sm"></span>
                        <a href="../User/Profile.aspx" class="add" title="Ver perfil">
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
                    <div class="card shadow-sm mb-3">
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
                                        data-bs-target="#detalleEventoModal"
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
            <div class="card shadow-sm suggestions-card">
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
                                                <span class="dot"></span>
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
    <div class="modal fade" id="detalleEventoModal" tabindex="-1" aria-labelledby="detalleEventoLabel" aria-hidden="true">
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
                        <img id="imgEventoModal" src="" alt="Imagen evento" class="img-fluid rounded shadow-sm" style="max-height: 300px; object-fit: cover;">
                    </div>
                    <h4 id="tituloEventoModal" class="fw-bold text-primary mb-3"></h4>
                    <p id="descripcionEventoModal" class="text-secondary"></p>
                    
                    <hr>
                    
                    <div class="row g-3">
                        <div class="col-md-6">
                            <p class="mb-2">
                                <i class="bi bi-geo-alt-fill text-danger me-2"></i>
                                <strong>Lugar:</strong> <span id="lugarEventoModal"></span>
                            </p>
                        </div>
                        <div class="col-md-6">
                            <p class="mb-2">
                                <i class="bi bi-tag-fill text-info me-2"></i>
                                <strong>Tipo:</strong> <span id="tipoEventoModal"></span>
                            </p>
                        </div>
                        <div class="col-12">
                            <p class="mb-2">
                                <i class="bi bi-calendar3 text-success me-2"></i>
                                <strong>Fecha:</strong> <span id="fechaEventoModal"></span>
                            </p>
                        </div>
                        <div class="col-12">
                            <p class="mb-0">
                                <i class="bi bi-person-fill text-warning me-2"></i>
                                <strong>Organizador:</strong> <span id="organizadorEventoModal"></span>
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
</asp:Content>
