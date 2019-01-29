# Proyecto final <div><img src="https://github.com/PanchoMen/ProyectoII/blob/master/Recursos/Logo_ULL_2018.png" width="200" align="right"/></div>
## Interfaces Inteligentes - Universidad de La Laguna


* Francisco Javier Mendoza Álvarez
* Imar Abreu Díaz
* Alejandro Hernández Padrón


### El proyecto

El proyecto consiste en el desarrollo de una aplicación VR para smartphones en el entorno Unity.  

Nuestro proyecto es lo que se conoce como un juego de escape room, el personaje, despierta en una casa y debe salir de ella investigando y enontrando objetos que le ayuden en dicha tarea.
El jugador en este caso se ve amenazado por la existencia de una bomba, por lo que debe desactivar la bomba en el tiempo estipulado o habrá perdido.  

### Forma de juego
Al tratarse de un juego VR para smartphones es necesario la utilización de una gafas de realidad virtual tipo *cardboard*.
A su vez, se ha orientado el desarrollo para su uso en conjunto con un *GamePad* para el control de los movimientos.
Existen dos configuraciones posibles para el GamePad:  
+ **GamePad "VRBOX":**  
Se ha considerado el uso del GamePad disponible por la profesora, sin embargo se ha alterado su configuración para poder emplear el mando con una sola mano, de manera vertical. Para ello se han mapeado los botones tipo "gatillo" que existen en el lateral del mando, y se han alterado los ejes del joystick para adaptarlos a su nueva posición de uso.

<div><img align="center" src="https://github.com/PanchoMen/ProyectoII/blob/master/Recursos/GamePad.png" width="600"/></div>

+ **Otros:**  
Para el resto de GamePads se mantiene la orientación original de los ejes y se ha mapeado el botón con referencia _0_ para ejecutar los clicks en pantalla.

A lo largo del mapa de juego, existen multiples elementos que el jugador puede recoger, para usar posteriormente. Para ello se debe apuntar con la retícula al objeto, si se puede interactuar con éste, la retícula cambiará de forma y al pulsar el botón, el jugador recogerá el objeto.

<div><img src="https://github.com/PanchoMen/ProyectoII/blob/master/Recursos/Captura%20de%20pantalla%202019-01-29%20a%20las%2020.30.20.png" width="600"/></div>

Para que el jugador pueda acceder a su inventario, debe realizar un movimiento especifico con la cabeza. Este movimiento es como si el jugador mirar a su abdomen. Al regresar la cabeza a la posición anterior, se podrá observar frente al jugador, la UI del inventario.
<div><img src="https://github.com/PanchoMen/ProyectoII/blob/master/Recursos/Jan-29-2019%2020-42-36.gif" width="600"/></div>
*Debido al gesto seleccionado para desplegar el inventario, es posible que en ocasiones éste, interfiera en la interacción con otros objetos, por ello, si los objetos se encuentran en determinada trayectoria, no se efectuará la interacción, sino que se desplegará el inventario. Para solucionarlo, el jugador debe alejarse ligeramente del objeto para cambiar el angulo entre éste y su cabeza.*

### Herramientas
#### Unity3D
Como es obvio, hemos empleado el entorno de desarrollo Unity, además, hemos hecho uso de su herramienta colaborativa, *Collab*, para poder realizar el trabajo de forma descentralizada.
#### GOOGLE VR for Unity
Se ha empleado el paquete de realidad virtual de Google expresamente diseñado para Unity, logrando así una facil implementación de la tecnología VR en nuestro proyecto.
#### Sweet Home 3D
Se trata de un software de diseño de casas en 3D, hemos empleado este sofware para crear el modelo 3D de nuestra casa, para posteriormente exportar y adecuar a Unity.
#### SketchUp
Se ha empleado este software de modelado 3D, para diseñar o modificar cierto modelos empleados en la escena. 
#### Dialog Flow
Es la herramienta empleada para la implementación de un chatbot, que resuleve dudas al jugador en un momento dado.
#### Watson
En combinación con Dialog Flow, se emplea para convertir el texto que responde el chatbot en audio.

### Hitos de programación logrados
* __GameController:__
El correcto desarrollo de la ejecución del juego, viene dado por el script *GameController*, ya que es el encargado de gestionarla.  
De manera simplificada, el contenido del *GameController* es el siguiente:  
  * Posee una referencia a la base de datos que contiene los posibles objetos inventariables del juego.
  * Una referencia al inventario.
  * Una referencia a la configuración del mando.
  * Eventos delegados para el control de las luces de la casa.
  * Contiene varios timers, que entre otras, sirven para controlar el tiempo restante para la explosión de la bomba, o para el nivel de batería de la linterna.
  * Funciones para gestionar las diferentes fases del juego, como ganar, explosión de la bomba por el corte de un cable incorrecto o por finalización del tiempo.
  * Funciones para la integración del chatbot.
  * Una función para mostrar mensajes al usuario.
  
* __GamepadController:__
Se encarga de elegir al comienzo del juego, la configuración adecuada para cada tipo de mando. Por ello, es necesario que el mando esté conectado antes de iniciar el juego, para que pueda detectarlo y seleccionar su configuración, si no, se apricará una por defecto.

* __Inventario:__
Existen un conjunto de scripts para la creación y gestión del inventario.

* __ChatBot:__
Se ha hecho uso de las API's de Dialog Flow y Watson. A su vez se han implementado ciertas funciones para combinar ambas API's

### Aspectos destacables
Como aspectos destacables mencionaríamos:
* El sistema de inventario, que dota al juego de unas características necesarias en este tipo de juegos. Para su implementación se han seguido una [serie de tutoriales](https://www.youtube.com/watch?v=7If0gKu4H9c) para la base y se han hecho modificaciones o añadido nuevas funcionalidades para adaptarlo a nuestro juego.

* El ChatBot, que es capaz de interpretar el lenguaje natural haciendo uso del microfono del smartphone y devolver una respuesta en el mismo formato.

### Reparto de Tareas
* Francisco Javier Mendoza Álvarez:
  - Diseño del modelo 3D de la casa
  - Implementación del sistema de inventario
  - Creación de los objetos inventariables y sus funcionalidades
  - Implementación de los scripts, animaciones, etc referentes a la bomba. Como la animación al cortar el cable adecuado o la explosión al cortar uno incorrecto
  - Adaptación de la configuración para cada tipo de GamePad
  - Escenas inicial y final

* Imar Abreu Díaz: 
  - Cambio de aspecto estético de la casa (texturas y materiales).
  - Realización de la animación de los interruptores de la luz y la caja de fusibles, con los respectivos scripts para encender y apagar las luces.
  - Añadir sonidos a las acciones que lo requerían.
  - Creación de las llaves y scripts para poder desbloquear las puertas.
  - Animación final de particulas en forma de fuegos artificiales.
  
* Alejandro Hernández Padrón:
  - Agrupación de los GameObject en las respectivas habitaciones presentes en la casa y adición de interruptores.
  - Animación de inicio del juego.
  - Temporizador de fin del juego.
  - Elementos de la UI de la camara.
  - Integración con DialogFlow mediante reconocimiento de voz en dispositivos Android.
  - Integración de la API Watson de IBM para reproducir la respuesta de DialogFlow mediante voz.
