# Ruleta de Roles

Una aplicación de consola en C# que permite realizar selecciones aleatorias de estudiantes para diferentes roles, implementando una arquitectura MVC limpia y escalable.

## 🏗️ Arquitectura

### Patrón MVC (Model-View-Controller)

El proyecto sigue una arquitectura MVC bien definida que separa las responsabilidades:

```
Roulette/
├── Controllers/           # Lógica de control de flujo
│   ├── MainController.cs
│   ├── EstudianteController.cs
│   ├── RolController.cs
│   └── RouletteController.cs
├── Models/               # Entidades del dominio
│   ├── Estudiante.cs
│   ├── Rol.cs
│   ├── Seleccion.cs
│   └── EstadoSeleccion.cs
├── Views/                # Interfaz de usuario
│   ├── MenuView.cs
│   ├── EstudianteView.cs
│   ├── RolView.cs
│   └── RouletteView.cs
├── Services/             # Lógica de negocio
│   ├── EstudianteService.cs
│   ├── RolService.cs
│   └── RouletteService.cs
├── Infrastructure/       # Acceso a datos y utilidades
│   ├── Files/
│   └── Storage/
│       └── TextFileManager.cs
└── Shared/              # Utilidades compartidas
    ├── Constants.cs
    └── Utilities/
        ├── AnimarRuleta.cs
        ├── SaltoDeLinea.cs
        └── Spinners.cs
```

### Componentes Principales

#### 🎮 Controllers
- **MainController**: Controlador principal que gestiona el flujo de la aplicación
- **EstudianteController**: Maneja todas las operaciones relacionadas con estudiantes
- **RolController**: Gestiona las operaciones de roles
- **RouletteController**: Controla la lógica de selección aleatoria

#### 📊 Models
- **Estudiante**: Representa un estudiante con nombre y fecha de registro
- **Rol**: Representa un rol que puede ser asignado
- **Seleccion**: Representa una selección realizada (estudiante + rol + fecha)
- **EstadoSeleccion**: Mantiene el estado de selecciones para balanceo

#### 👁️ Views
- **MenuView**: Maneja la interfaz del menú principal
- **EstudianteView**: UI para gestión de estudiantes
- **RolView**: UI para gestión de roles
- **RouletteView**: UI para el proceso de selección y historial

#### ⚙️ Services
- **EstudianteService**: Lógica de negocio para estudiantes
- **RolService**: Lógica de negocio para roles
- **RouletteService**: Lógica de selección aleatoria balanceada

## ✨ Características

### 🎯 Selección Inteligente
- **Algoritmo de Balanceo**: Asegura que todos los estudiantes tengan oportunidades equitativas
- **Persistencia de Estado**: Mantiene un registro de selecciones previas
- **Selección Aleatoria Optimizada**: Prioriza estudiantes y roles menos seleccionados

### 📋 Gestión de Datos
- **CRUD Completo**: Crear, leer, actualizar y eliminar estudiantes y roles
- **Validación de Datos**: Previene duplicados y datos inválidos
- **Persistencia en Archivos**: Almacenamiento simple y confiable

### 🎨 Interfaz de Usuario
- **UI Atractiva**: Utiliza Spectre.Console para una experiencia visual rica
- **Navegación Intuitiva**: Menús claros y fáciles de usar
- **Animaciones**: Efectos visuales durante la selección

### 📊 Historial y Reportes
- **Historial Completo**: Registro de todas las selecciones realizadas
- **Formato Legible**: Visualización clara de datos históricos
- **Persistencia**: Los datos se mantienen entre sesiones

## 🚀 Uso

### Ejecutar la Aplicación
```bash
dotnet run
```

### Funcionalidades Principales

1. **Iniciar Ruleta de Selección**
   - Realiza una selección aleatoria balanceada
   - Muestra animación y resultado
   - Guarda en historial automáticamente

2. **Ver Historial de Selecciones**
   - Muestra las últimas 20 selecciones
   - Ordenado por fecha (más reciente primero)
   - Formato tabla legible

3. **Gestionar Estudiantes**
   - Agregar nuevos estudiantes
   - Eliminar estudiantes existentes
   - Listar todos los estudiantes

4. **Gestionar Roles**
   - Agregar nuevos roles
   - Eliminar roles existentes
   - Listar todos los roles

## 🛠️ Tecnologías

- **C# .NET 9.0**: Lenguaje y framework principal
- **Spectre.Console**: UI de consola rica y atractiva
- **System.Text.Json**: Serialización de datos
- **File System**: Persistencia simple basada en archivos

## 📁 Archivos de Datos

Los datos se almacenan en la carpeta `Infrastructure/Files/`:
- `estudiantes.txt`: Lista de estudiantes
- `roles.txt`: Lista de roles
- `historial.txt`: Historial de selecciones
- `estado_seleccion.txt`: Estado de balanceo (JSON)

## 🎯 Ventajas de la Nueva Arquitectura

### ✅ Separación de Responsabilidades
- Cada capa tiene una responsabilidad específica
- Fácil mantenimiento y extensión
- Código más limpio y organizado

### ✅ Escalabilidad
- Fácil agregar nuevas funcionalidades
- Cambio de persistencia sin afectar lógica
- Reutilización de componentes

### ✅ Testabilidad
- Servicios desacoplados fáciles de testear
- Inyección de dependencias clara
- Mocking sencillo para pruebas

### ✅ Mantenibilidad
- Código más legible y autodocumentado
- Cambios aislados por capa
- Debugging más eficiente

## 🔄 Mejoras Implementadas

1. **Eliminación de Archivos Innecesarios**
   - Removed `bin/` y `obj/` folders
   - Removed solution file unnecessary for single project
   - Cleaned up old Application and UI folders

2. **Reestructuración Completa**
   - Implementación del patrón MVC
   - Separación clara de responsabilidades
   - Mejor organización del código

3. **Mejoras en la Lógica**
   - Algoritmo de selección más inteligente
   - Mejor manejo de errores
   - Validaciones mejoradas

4. **UI Mejorada**
   - Interfaces más consistentes
   - Mejor experiencia de usuario
   - Feedback visual mejorado
