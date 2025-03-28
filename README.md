📋 Sistema de Gestión de Incidencias
Aplicación web para reportar y gestionar incidencias técnicas, con roles de usuario y técnico.

🛠 Tecnologías Utilizadas
Backend: ASP.NET MVC 5 (.NET Framework 4.8), Entity Framework 6 (Code First)

Base de Datos: SQL Server LocalDB

Frontend: Bootstrap 5, jQuery, HTML5/CSS3

⚙ Requisitos Previos
Visual Studio 2019 o superior

.NET Framework 4.8

SQL Server LocalDB

🚀 Instalación y Configuración
1. Clonar el Repositorio
git clone [URL_DEL_REPOSITORIO]

2. Restaurar Paquetes NuGet
Abrir la solución en Visual Studio.

Hacer clic derecho en la solución > Restaurar paquetes NuGet.

3. Configurar la Base de Datos
Ejecutar las migraciones desde la Consola del Administrador de Paquetes:
Update-Database

📝 Datos Iniciales
Crear Usuarios y Técnicos
Ejecutar este script SQL después de crear la base de datos:
INSERT INTO Usuarios (Nombre, Email) VALUES  
('Juan Pérez', 'juan@example.com'),  
('María García', 'maria@example.com');  

INSERT INTO Tecnicos (Nombre, Especialidad) VALUES  
('Pedro López', 'Hardware'),  
('Ana Martínez', 'Software');  

y si no lo desea asi, inicie desde la vista index de incidencia,
registre un usuario y un tecnico desde la barra de menu.


🖥 Ejecución
En Visual Studio, presiona F5 o Iniciar Depuración.

La aplicación se abrirá en http://localhost:[puerto]
