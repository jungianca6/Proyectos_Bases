using TECBank_BackEnd.Models;
using TECBank_BackEnd.Utilities;
using TECBank_BackEnd.Pruebas; // 👈 Asegúrate de tener este namespace si guardaste las clases de prueba aquí
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Agregar servicios al contenedor
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAnyOriginPolicy", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var ClienteA = new ClienteModel
{
    Cedula = "1243",
    Nombre = "jorge1",
    Apellido1 = "e",
    Apellido2 = "s",
    Direccion = "S2e",
    Telefono = "8828-8888",
    IngresoMensual = 1210,
    TipoDeCliente = "Pr2mium",
    Usuario = "sapo",
    Contrasena = "32"
};

var app = builder.Build();

// Configurar el pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Configuración de CORS (debe ir antes de Authorization)
app.UseCors("AllowAnyOriginPolicy");

app.UseHttpsRedirection();
app.UseAuthorization(); // Aquí se autoriza el acceso
app.MapControllers(); // Mapea los controladores

// 🟩 Ejecutar pruebas de lectura y escritura de clientes
Console.WriteLine("\n=========== 📝 PRUEBA DE ESCRITURA DE CLIENTES ===========");
JasonEscritura pruebaEscrituraClientes = new JasonEscritura();
pruebaEscrituraClientes.Ejecutar(ClienteA);

Console.WriteLine("=========== 🔍 FILTRO POR NOMBRE ===========");
JasonLectura pruebaLecturaClientes = new JasonLectura();

pruebaLecturaClientes.Ejecutar("Nombre", "jorge12");

Console.WriteLine("=========== 🔍 FILTRO POR NOMBRE ===========");
var listaPorNombre = pruebaLecturaClientes.Ejecutar("Nombre", "jorge12");

if (listaPorNombre.Count > 0)
{
    Console.WriteLine("========= Detalles de los Clientes Encontrados por Nombre =========");
    foreach (var cliente in listaPorNombre)
    {
        // Imprimir todos los detalles del cliente
        Console.WriteLine($"Nombre Completo: {cliente.Nombre} {cliente.Apellido1} {cliente.Apellido2}");
        Console.WriteLine($"Cédula: {cliente.Cedula}");
        Console.WriteLine($"Teléfono: {cliente.Telefono}");
        Console.WriteLine($"Dirección: {cliente.Direccion}");
        Console.WriteLine($"Ingreso Mensual: {cliente.IngresoMensual}");
        Console.WriteLine($"Tipo de Cliente: {cliente.TipoDeCliente}");
        Console.WriteLine($"Usuario: {cliente.Usuario}");
        Console.WriteLine($"Contraseña: {cliente.Contrasena}");
        Console.WriteLine("------------------------------------------------------------");
    }
}
else
{
    Console.WriteLine("⚠️ No se encontraron clientes con ese nombre.");
}

Console.WriteLine("=========== 🔍 FILTRO POR CÉDULA ===========");
// Filtrar clientes por cédula "1233"
var listaPorCedula = pruebaLecturaClientes.Ejecutar("Cedula", "1233");

if (listaPorCedula.Count > 0)
{
    Console.WriteLine("========= Detalles de los Clientes Encontrados por Cédula =========");
    foreach (var cliente in listaPorCedula)
    {
        // Imprimir todos los detalles del cliente
        Console.WriteLine($"Nombre Completo: {cliente.Nombre} {cliente.Apellido1} {cliente.Apellido2}");
        Console.WriteLine($"Cédula: {cliente.Cedula}");
        Console.WriteLine($"Teléfono: {cliente.Telefono}");
        Console.WriteLine($"Dirección: {cliente.Direccion}");
        Console.WriteLine($"Ingreso Mensual: {cliente.IngresoMensual}");
        Console.WriteLine($"Tipo de Cliente: {cliente.TipoDeCliente}");
        Console.WriteLine($"Usuario: {cliente.Usuario}");
        Console.WriteLine($"Contraseña: {cliente.Contrasena}");
        Console.WriteLine("------------------------------------------------------------");
    }
}
else
{
    Console.WriteLine("⚠️ No se encontró un cliente con esa cédula.");
}
Console.WriteLine("===========  🔍 sin filtro ===========");

var listaTodos = pruebaLecturaClientes.Ejecutar();

if (listaTodos.Count > 0)
{
    Console.WriteLine("========= Detalles de Todos los Clientes =========");
    foreach (var cliente in listaTodos)
    {
        // Imprimir todos los detalles del cliente
        Console.WriteLine($"Nombre Completo: {cliente.Nombre} {cliente.Apellido1} {cliente.Apellido2}");
        Console.WriteLine($"Cédula: {cliente.Cedula}");
        Console.WriteLine($"Teléfono: {cliente.Telefono}");
        Console.WriteLine($"Dirección: {cliente.Direccion}");
        Console.WriteLine($"Ingreso Mensual: {cliente.IngresoMensual}");
        Console.WriteLine($"Tipo de Cliente: {cliente.TipoDeCliente}");
        Console.WriteLine($"Usuario: {cliente.Usuario}");
        Console.WriteLine($"Contraseña: {cliente.Contrasena}");
        Console.WriteLine("------------------------------------------------------------");
    }
}
else
{
    Console.WriteLine("⚠️ No se encontraron clientes.");
}
Console.WriteLine("=========== ✏️ CAMBIO PARCIAL DE CLIENTE ===========");

JasonEditare pruebaEditarClientes = new JasonEditare();

var cambiosParciales = new ClienteModel
{
    Telefono = "6000-0000",
    Direccion = "Nueva Casa en Cartago"
    // Los demás campos los dejamos vacíos
};

pruebaEditarClientes.EditarClienteParcial("1233", cambiosParciales);


Console.WriteLine("=========== 🔍 SIN FILTRO (TODOS) ===========");
pruebaLecturaClientes.Ejecutar();

Console.WriteLine("=========== ❌ PRUEBA DE ELIMINACIÓN ===========");
JasonEliminar pruebaEliminacionClientes = new JasonEliminar();
pruebaEliminacionClientes.EliminarPorCedula("12332");

Console.WriteLine("=========== 🔍 SIN FILTRO (TODOS) ===========");

pruebaLecturaClientes.Ejecutar();
var cuentaA = new CuentaModel
{
    NumeroDeCuenta = "C123",
    Nombre = "Cuenta Corriente",
    Descripcion = "Cuenta para pagos rápidos",
    Usuario = ClienteA.Usuario, 
    Moneda = "CRC",
    TipoDeCuenta = "Corriente"
};

// 🟩 Escritura de cuenta
Console.WriteLine("=========== 📝 PRUEBA DE ESCRITURA DE CUENTA ===========");
pruebaEscrituraClientes.GuardarCuenta(cuentaA);

// 🟩 Lectura de cuentas
Console.WriteLine("=========== 🔍 LECTURA DE CUENTAS (TODAS) ===========");
var cuentasA = pruebaLecturaClientes.LeerCuentas();

foreach (var cuenta in cuentasA)
{
    Console.WriteLine($"Número de Cuenta: {cuenta.NumeroDeCuenta}");
    Console.WriteLine($"Nombre: {cuenta.Nombre}");
    Console.WriteLine($"Descripción: {cuenta.Descripcion}");
    Console.WriteLine($"Moneda: {cuenta.Moneda}");
    Console.WriteLine($"Tipo de Cuenta: {cuenta.TipoDeCuenta}");
    Console.WriteLine("------------------------------------------------------------");
}

// 🟩 Edición de cuenta
Console.WriteLine("=========== ✏️ CAMBIO PARCIAL DE CUENTA ===========");

var cuentaEditada = new CuentaModel
{
    Descripcion = "Cuenta editada para pagos internacionales"
};

pruebaEditarClientes.EditarCuenta("C123", cuentaEditada);

// 🟩 Eliminación de cuenta
Console.WriteLine("=========== ❌ PRUEBA DE ELIMINACIÓN DE CUENTA ===========");
pruebaEliminacionClientes.EliminarCuenta("C1223");
Console.WriteLine("=========== 🔍 LECTURA DE CUENTAS (TODAS) ===========");
var cuentasB = pruebaLecturaClientes.LeerCuentas();

foreach (var cuenta in cuentasB)
{
    Console.WriteLine($"Número de Cuenta: {cuenta.NumeroDeCuenta}");
    Console.WriteLine($"Nombre: {cuenta.Nombre}");
    Console.WriteLine($"Descripción: {cuenta.Descripcion}");
    Console.WriteLine($"Moneda: {cuenta.Moneda}");
    Console.WriteLine($"Tipo de Cuenta: {cuenta.TipoDeCuenta}");
    Console.WriteLine("------------------------------------------------------------");
}

app.Run();
