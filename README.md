# API

Proyecto desarrollado como prueba técnica

## Antes de ejecutar

- "Compilar la Solución" para que se genere la carpeta package, el cual contiene las librerías necesarias para el funcionamiento
- Verificar que la base de datos se encuentre creada
- En el archivo Web.config modificar el `connectionString` **solo** en caso de necesitar un usuario y contraseña 
- Cada Controlador cuenta con el `[EnabelCors]` por lo que será necesario modificar el puerto del origins por el que se genera con el proyecto de Angular, por defecto es el puerto 4200. Esto es para los 3 controllers. Si el puerto es el por defecto, tan solo ignorarlo.
- Cuando se ejecuta este proyecto se mostrará una vista con error 403, esto debido a que no contiene vistas disponibles. Sin embargo, esto no será impedimiento para que funcione la API. Es obligatorio tenerlo abierto para que funcione correctamente la conexión con el Frontend en Angular.