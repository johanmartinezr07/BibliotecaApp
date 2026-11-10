# BibliotecaApp
Sistema de Gestión de Biblioteca en C# Windows Forms y SQL Server con arquitectura en 4 capas.

EVIDENCIAS
1. Menú principal 
Es el punto de entrada de la aplicación. Presenta la interfaz principal y permite navegar hacia los formularios CRUD de cada entidad. Se eliminó el formulario huérfano Form1.cs, de modo que la navegación parte únicamente de este menú.

<img width="770" height="314" alt="image" src="https://github.com/user-attachments/assets/4c1f9e25-6bdc-44fe-b43d-f665b90f9ace" />

2. Módulo de autores

Permite registrar, consultar, modificar y eliminar autores. La capa de negocio verifica que nombre y apellido no estén vacíos. Un autor con libros asociados no puede eliminarse, porque la clave foránea Libros.IdAutor lo impide.

<img width="770" height="317" alt="image" src="https://github.com/user-attachments/assets/a5d09574-3ec4-48b1-bc7f-9b0bc8176866" />

3.	Módulo de editoriales

Gestiona el nombre y el país de cada editorial. El nombre es obligatorio y el país es opcional. Respeta la relación con los libros: una editorial con libros asociados no se puede eliminar.

<img width="768" height="304" alt="image" src="https://github.com/user-attachments/assets/94ea166a-8463-42b8-ba54-3508205e0e98" />

4.	Módulo de usuarios

Gestiona los usuarios de la biblioteca (nombre, apellido, documento, teléfono y correo). El documento es obligatorio y único, por lo que no se aceptan documentos repetidos. Un usuario con préstamos registrados no se puede eliminar.

<img width="768" height="290" alt="image" src="https://github.com/user-attachments/assets/dcfdd1de-f711-4cb6-9869-54a08f9a3ab1" />

5.	Módulo de libros

Gestiona la información bibliográfica: título, año de publicación y stock. El autor y la editorial se seleccionan de listas desplegables cargadas desde la base de datos, lo que garantiza que el libro se relacione con registros existentes.

<img width="769" height="274" alt="image" src="https://github.com/user-attachments/assets/e678b0e0-5cb0-4879-96e2-849ffb7f4780" 

6.Módulo de préstamos y devoluciones

Permite registrar un préstamo seleccionando un usuario y un libro, con su fecha de présta-mo. Cada préstamo conserva su estado («Prestado» o «Devuelto») y su fecha de devolu-ción, que permanece vacía hasta que se devuelve el libro. La tabla del formulario permite consultar y diferenciar los préstamos activos de los devueltos, y editar o eliminar registros según las reglas implementadas.

<img width="770" height="276" alt="image" src="https://github.com/user-attachments/assets/f83e0694-d539-407b-bbea-b2db83d9226d" />





