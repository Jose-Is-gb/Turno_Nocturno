# 🌙 Turno Nocturno: El Museo del Silencio

**Un turno de vigilancia, un museo en silencio y una noche por explorar.**

Turno Nocturno es un videojuego 2D de exploración y terror desarrollado en **Unity y C#**. El jugador recorre distintos escenarios, interactúa con objetos y descubre fragmentos de la historia mediante notas y monólogos, en una ambientación nocturna acompañada de efectos de iluminación y sonido.

<!-- Añade aquí una imagen de portada:
![Portada de Turno Nocturno](docs/images/portada.png)
-->

## Sobre el juego

La experiencia gira en torno al inicio de un turno de vigilancia en un museo. Explorar el entorno, prestar atención a los mensajes y encontrar una llave permite avanzar entre sus espacios.

El proyecto combina movimiento en dos dimensiones, narrativa ambiental e interacciones sencillas para construir una atmósfera de misterio.

## Características

- **Exploración 2D:** desplazamiento en cuatro direcciones con animaciones del personaje.
- **Interacción con objetos:** recogida de llaves y apertura de puertas bloqueadas.
- **Notas interactivas:** lectura de textos al acercarse y pulsar la tecla de interacción.
- **Monólogos del personaje:** mensajes que aparecen al entrar en determinadas zonas.
- **Transiciones entre escenarios:** cambios de escena con puntos de aparición configurables.
- **Ambientación nocturna:** recursos de audio y efectos de parpadeo y oscurecimiento del escenario.
- **Menú principal:** opciones para comenzar, continuar, consultar créditos y salir.
- **Menú de pausa:** reanudar, guardar la partida y regresar al menú principal.
- **Guardado local básico:** registro de la escena y la posición del jugador mediante `PlayerPrefs`.

> El guardado actual conserva la escena y las coordenadas del personaje. No guarda el estado completo de los objetos ni la posesión de la llave entre sesiones.

## Tecnologías

| Tecnología | Uso |
| --- | --- |
| Unity `6000.4.8f1` | Motor y editor del proyecto |
| C# | Programación de las mecánicas |
| Universal Render Pipeline (URP) | Configuración de renderizado |
| Rigidbody2D y Collider2D | Movimiento, colisiones y zonas de interacción |
| Animator | Animaciones del personaje |
| Unity UI y TextMesh Pro | Menús, notas y mensajes |
| PlayerPrefs | Guardado local de escena y posición |
| Git y Git LFS | Control de versiones y gestión de recursos binarios |

## Controles

| Tecla o acción | Función |
| --- | --- |
| `W`, `A`, `S`, `D` o flechas | Mover al personaje |
| `E` | Recoger una llave, intentar abrir una puerta o abrir/cerrar una nota al estar cerca |
| `Esc` | Pausar o reanudar el juego |
| Clic izquierdo | Seleccionar opciones de los menús |

## 🚀 Cómo ejecutar el proyecto

### Requisitos

- **Unity Hub** y **Unity Editor `6000.4.8f1`**, la versión registrada en el proyecto.
- **Git** y **Git LFS** para descargar el repositorio y sus recursos.
- Conexión a internet para la descarga inicial de recursos y paquetes.

### Instalación

1. Instala Git LFS e inicialízalo:

   ```bash
   git lfs install
   ```

2. Clona el repositorio y descarga los archivos administrados por LFS:

   ```bash
   git clone https://github.com/Jose-Is-gb/Turno_Nocturno.git
   cd Turno_Nocturno
   git lfs pull
   ```

3. Abre **Unity Hub** y añade la carpeta `Turno_Nocturno` como proyecto existente. Selecciona la carpeta que contiene `Assets`, `Packages` y `ProjectSettings`.
4. Abre el proyecto con la versión de Unity indicada y espera a que finalice la importación de recursos y la resolución de paquetes.
5. Abre la escena **`Assets/Scenes/Main_Menu.unity`**.
6. Pulsa **Play** en el editor y selecciona **Comenzar**.

### Guardar y continuar

Durante la partida, pulsa `Esc` y selecciona **Guardar Partida**. Después, utiliza **Continuar** en el menú principal para cargar la escena y la posición guardadas. Si no existe un guardado, esta opción inicia la escena `INTRO 00`.

## Organización del proyecto

| Ruta | Contenido |
| --- | --- |
| `Assets/Scenes/` | Menú principal y escenarios del juego |
| `Assets/Scripts/` | Lógica de movimiento, interacción, narrativa y menús |
| `Assets/Animations/` | Animaciones y controladores |
| `Assets/Sprites/` | Recursos gráficos 2D |
| `Assets/Audio/` | Recursos de sonido |
| `Assets/Paletts/` | Recursos de paletas para los escenarios |
| `Assets/Settings/` | Configuración gráfica y recursos de URP |
| `Assets/Editor/` | Herramientas auxiliares del editor |
| `Packages/` | Dependencias de Unity |
| `ProjectSettings/` | Configuración del proyecto |

### Scripts principales

| Script | Responsabilidad |
| --- | --- |
| `PlayerMovement.cs` | Movimiento, animaciones y restauración de la posición |
| `MenuManager.cs` | Menú principal, carga de partida y créditos |
| `PauseMenu.cs` | Pausa, guardado y regreso al menú |
| `Llave.cs` | Recogida de la llave |
| `Puerta.cs` | Comprobación de la llave y acceso a otra escena |
| `NotaInteractiva.cs` | Apertura y cierre de notas |
| `MonologoTrigger.cs` | Activación de mensajes narrativos |
| `ChangeSceneOnTrigger.cs` | Transiciones y selección del punto de aparición |
| `EfectoLuces.cs` | Parpadeo y oscurecimiento de los sprites del escenario |
| `TaxiMovement.cs` | Desplazamiento automático del taxi |

## 📸 Capturas de pantalla

<!-- Guarda tus imágenes en docs/images/ y descomenta las líneas correspondientes.
También puedes reemplazarlas por enlaces a imágenes subidas directamente a GitHub.
-->

### Menú principal

![Menú principal](MDT1.png)

### Exploración del museo

![Exploración del museo](MDT2.png)
![Lectura de una nota](MDT3.png)
![Exploración del museo](MDT4.png)
![Exploración del museo](MDT5.png)
![Exploración del museo](MDT6.png)

## Créditos

Desarrollado por:

- **José Geronimo** — [Jose-Is-gb](https://github.com/Jose-Is-gb)
- **Bryan Mercado**

