﻿using TECBank_BackEnd.Models;
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






/*
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

// 🟩 Ejecutar pruebas de lectura y escritura de clientes
Console.WriteLine("\n=========== 📝 PRUEBA DE ESCRITURA DE CLIENTES ===========");
JasonEscritura Escritura = new JasonEscritura();
Escritura.GuardarCliente(ClienteA);

Console.WriteLine("=========== 🔍 FILTRO POR NOMBRE ===========");
JasonLectura Lectura = new JasonLectura();


Console.WriteLine("=========== 🔍 FILTRO POR NOMBRE ===========");
var listaPorNombre = Lectura.Ejecutar("Nombre", "jorge1");

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
var listaPorCedula = Lectura.Ejecutar("Cedula", "1233");

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

var listaTodos = Lectura.Ejecutar();

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

JasonEditar Editare = new JasonEditar();

var cambiosParciales = new ClienteModel
{
    Telefono = "6000-0000",
    Direccion = "Nueva Casa en Cartago"
    // Los demás campos los dejamos vacíos
};

Editare.EditarClienteParcial("1233", cambiosParciales);


Console.WriteLine("=========== 🔍 SIN FILTRO (TODOS) ===========");
Lectura.Ejecutar();

Console.WriteLine("=========== ❌ PRUEBA DE ELIMINACIÓN ===========");
JasonEliminar Elimionacion = new JasonEliminar();
Elimionacion.EliminarPorCedula("12332");

Console.WriteLine("================================================================");















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
Escritura.GuardarCuenta(cuentaA);

// 🟩 Lectura de cuentas
Console.WriteLine("=========== 🔍 LECTURA DE CUENTAS (TODAS) ===========");
var cuentasA = Lectura.LeerCuentas();

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

Editare.EditarCuenta("C123", cuentaEditada);

// 🟩 Eliminación de cuenta
Console.WriteLine("=========== ❌ PRUEBA DE ELIMINACIÓN DE CUENTA ===========");
Elimionacion.EliminarCuenta("C1223");
Console.WriteLine("=========== 🔍 LECTURA DE CUENTAS (TODAS) ===========");
var cuentasB = Lectura.LeerCuentas("NumeroDeCuenta", "C123");

foreach (var cuenta in cuentasB)
{
    Console.WriteLine($"Número de Cuenta: {cuenta.NumeroDeCuenta}");
    Console.WriteLine($"Nombre: {cuenta.Nombre}");
    Console.WriteLine($"Descripción: {cuenta.Descripcion}");
    Console.WriteLine($"Moneda: {cuenta.Moneda}");
    Console.WriteLine($"Tipo de Cuenta: {cuenta.TipoDeCuenta}");
    Console.WriteLine("------------------------------------------------------------");
}
Console.WriteLine("================================================================");












// 🟦 TARJETAS - EJEMPLOS

var tarjetaA = new TarjetaModel
{
    Numero = "T001",
    NumeroDeCuenta = "C123",
    TipoDeTarjeta = "Debito",
    FechaDeExpiracion = "2027/12/31",
    CCV = "123",
    SaldoDisponible = 50000
};

// Escritura
Console.WriteLine("=========== 📝 PRUEBA DE ESCRITURA DE TARJETA ===========");
Escritura.GuardarTarjeta(tarjetaA);

// Lectura (todas)
Console.WriteLine("=========== 🔍 LECTURA DE TARJETAS (TODAS) ===========");
var tarjetasTodas = Lectura.LeerTarjetas();

foreach (var tarjeta in tarjetasTodas)
{
    Console.WriteLine($"Número: {tarjeta.Numero}");
    Console.WriteLine($"Cuenta Asociada: {tarjeta.NumeroDeCuenta}");
    Console.WriteLine($"Tipo: {tarjeta.TipoDeTarjeta}");
    Console.WriteLine($"Expira: {tarjeta.FechaDeExpiracion}");
    Console.WriteLine($"CCV: {tarjeta.CCV}");
    Console.WriteLine($"Saldo Disponible: {tarjeta.SaldoDisponible}");
    Console.WriteLine("------------------------------------------------------------");
}

// Lectura con filtro
Console.WriteLine("=========== 🔍 FILTRO TARJETA POR TIPO ===========");
var tarjetasDebito = Lectura.LeerTarjetas("Numero", "T001");

foreach (var tarjeta in tarjetasDebito)
{
    Console.WriteLine($"➡️ Tarjeta Débito: {tarjeta.Numero} con saldo {tarjeta.SaldoDisponible}");
}

// Edición parcial
Console.WriteLine("=========== ✏️ CAMBIO PARCIAL DE TARJETA ===========");

var tarjetaEditada = new TarjetaModel
{
    SaldoDisponible = 75000,
    TipoDeTarjeta = "zorra"
};

Editare.EditarTarjeta("T001", tarjetaEditada);

// Eliminación
Console.WriteLine("=========== ❌ PRUEBA DE ELIMINACIÓN DE TARJETA ===========");
Elimionacion.EliminarTarjeta("T0021");

// Confirmación de eliminación
Console.WriteLine("=========== 🔍 CONFIRMACIÓN: LECTURA DE TARJETAS (TODAS) ===========");
var tarjetasFinal = Lectura.LeerTarjetas();
foreach (var tarjeta in tarjetasFinal)
{
    Console.WriteLine($"✅ Tarjeta restante: {tarjeta.Numero}");
}
Console.WriteLine("================================================================");














var empleadoA = new EmpleadoModel
{
    Cedula = "1234",
    Nombre = "Carlos",
    Apellido1 = "Perez",
    Apellido2 = "Gomez",
    FechaDeNacimiento = "13/09/2004",
    IngresoMensual = 1210,

    AdminRol = true,
    Usuario = "carlos.g",
    Contrasena = "1234secure"
};

// 🟩 Escritura de empleado
Console.WriteLine("=========== 📝 PRUEBA DE ESCRITURA DE EMPLEADO ===========");
Escritura.GuardarEmpleado(empleadoA);

// 🟩 Lectura de empleados
Console.WriteLine("=========== 🔍 LECTURA DE EMPLEADOS (TODOS) ===========");
var empleadosA = Lectura.LeerEmpleados();

foreach (var empleado in empleadosA)
{
    Console.WriteLine($"Nombre Completo: {empleado.Nombre} {empleado.Apellido1} {empleado.Apellido2}");
    Console.WriteLine($"Cédula: {empleado.Cedula}");
    Console.WriteLine($"Rol: {empleado.AdminRol}");
    Console.WriteLine($"Usuario: {empleado.Usuario}");
    Console.WriteLine($"Fecha de Nacimiento: {empleado.FechaDeNacimiento}");
    Console.WriteLine("------------------------------------------------------------");
}

// 🟩 Edición de empleado
Console.WriteLine("=========== ✏️ CAMBIO PARCIAL DE EMPLEADO ===========");
var empleadoEditado = new EmpleadoModel
{
    AdminRol = false
};
Editare.EditarEmpleado("1234", empleadoEditado);

// 🟩 Eliminación de empleado
Console.WriteLine("=========== ❌ PRUEBA DE ELIMINACIÓN DE EMPLEADO ===========");
Elimionacion.EliminarEmpleado("12dasd34");
Console.WriteLine("================================================================");


















var pagoA = new PagoModel
{
    Cuenta_Emisora = "C123",
    Numero_de_Tarjeta = "T001",
    Nombre = "Carlos",
    Apellido1 = "Perez",
    Apellido2 = "Gomez",
    Fecha = "2025-04-11",
    Monto = 200,
    ID = "P001",
    Moneda = "CRC"
};

// 🟩 Escritura de pago
Console.WriteLine("=========== 📝 PRUEBA DE ESCRITURA DE PAGO ===========");
Escritura.GuardarPago(pagoA);

// 🟩 Lectura de pagos
Console.WriteLine("=========== 🔍 LECTURA DE PAGOS (TODOS) ===========");
var pagosA = Lectura.LeerPagos();

foreach (var pago in pagosA)
{
    Console.WriteLine($"ID: {pago.ID}");
    Console.WriteLine($"Monto: {pago.Monto} {pago.Moneda}");
    Console.WriteLine($"Cuenta Emisora: {pago.Cuenta_Emisora}");
    Console.WriteLine($"Número de Tarjeta: {pago.Numero_de_Tarjeta}");
    Console.WriteLine("------------------------------------------------------------");
}

Console.WriteLine("================================================================");
























var retiroA = new RetiroModel
{
    CuentaARetirar = "dsadad",
    Monto = 500,

    Nombre = "Carlos",
    Apellido1 = "Perez",
    Apellido2 = "Gomez",
    Fecha = "2025-04-11",
    ID = "R01",
    Moneda = "CRC"
};


// 🟩 Escritura de retiro
Console.WriteLine("=========== 📝 PRUEBA DE ESCRITURA DE RETIRO ===========");
Escritura.GuardarRetiro(retiroA);

// 🟩 Lectura de retiros
Console.WriteLine("=========== 🔍 LECTURA DE RETIROS (TODOS) ===========");
var retirosA = Lectura.LeerRetiros("CuentaARetirar", "dsadad");

foreach (var retiro in retirosA)
{
    Console.WriteLine($"ID: {retiro.ID}");
    Console.WriteLine($"Monto: {retiro.Monto} {retiro.Moneda}");
    Console.WriteLine($"Cuenta para Retirar: {retiro.CuentaARetirar}");
    Console.WriteLine("------------------------------------------------------------");
}

Console.WriteLine("================================================================");














var Transferencia = new TransferenciaModel
{
    Cuenta_Emisora = "dsadad",
    Cuenta_Receptora = "dsadad",
    Monto = 500,

    Nombre = "Carlos",
    Apellido1 = "Perez",
    Apellido2 = "Gomez",
    Fecha = "2025-04-11",
    ID = "Rsd01",
    Moneda = "CRC"
};


// 🟩 Escritura de retiro
Console.WriteLine("=========== 📝 PRUEBA DE ESCRITURA DE tranfe ===========");
Escritura.GuardarTransferencia(Transferencia);

// 🟩 Lectura de retiros
Console.WriteLine("=========== 🔍 LECTURA DE tranfe (TODOS) ===========");
var TransferenciaA = Lectura.LeerTransferencias("Cuenta_Receptora", "dsadad");

foreach (var transferencia in TransferenciaA)
{
    Console.WriteLine($"ID: {transferencia.ID}");
    Console.WriteLine($"Monto: {transferencia.Monto} {transferencia.Moneda}");
    Console.WriteLine($"Cuenta para tranfe: {transferencia.Cuenta_Emisora}");
    Console.WriteLine("------------------------------------------------------------");
}

Console.WriteLine("================================================================");


var asesor = new AsesorCreditoModel
{
    Cedula = "51236781243",

    Meta_Colones = 1500000,
    Meta_Creditos = new List<int> { 3, 5, 7 }
};

// 🟩 Escritura de asesor
Console.WriteLine("=========== 📝 PRUEBA DE ESCRITURA DE ASESOR ===========");
Escritura.GuardarAsesorCredito(asesor);

// 🟩 Lectura de asesores
Console.WriteLine("=========== 🔍 LECTURA DE ASESORES DE CRÉDITO (TODOS) ===========");
var asesores = Lectura.LeerAsesoresCredito("Cedula", "51236781243");

foreach (var a in asesores)
{
    Console.WriteLine($"Cédula: {a.Cedula}");

    Console.WriteLine($"Meta Colones: {a.Meta_Colones}");
    Console.WriteLine($"Meta Créditos: {string.Join(", ", a.Meta_Creditos)}");
    Console.WriteLine("------------------------------------------------------------");
}

// 🟩 Edición parcial de asesor
Console.WriteLine("=========== ✏️ CAMBIO PARCIAL DE ASESOR ===========");
var asesorEditado = new AsesorCreditoModel
{
    Meta_Colones = 2000000
};
Editare.EditarAsesorCredito("51236781243", asesorEditado);

// 🟩 Eliminación de asesor
Console.WriteLine("=========== ❌ PRUEBA DE ELIMINACIÓN DE ASESOR ===========");
Elimionacion.EliminarAsesorCredito("567we8");
Console.WriteLine("================================================================");








var prestamo = new PrestamoModel
{
    Monto_Original = 500000,
    Saldo_Pendiente = 250000,
    Cedula_Cliete = "12345678",
    Tasa_De_Interes = 0.065m,
    ID_Prestamos = "PREST123",
    FechaVencimiento = "30/12/2025"
};

// 🟩 Escritura de préstamo
Console.WriteLine("=========== 📝 PRUEBA DE ESCRITURA DE PRÉSTAMO ===========");
Escritura.GuardarPrestamo(prestamo);

// 🟩 Lectura de préstamos
Console.WriteLine("=========== 🔍 LECTURA DE PRÉSTAMOS POR CÉDULA ===========");
var prestamos = Lectura.LeerPrestamos("Cedula_Cliete", "12345678");

foreach (var p in prestamos)
{
    Console.WriteLine($"ID: {p.ID_Prestamos}");
    Console.WriteLine($"Cédula: {p.Cedula_Cliete}");
    Console.WriteLine($"Monto Original: {p.Monto_Original}");
    Console.WriteLine($"Saldo Pendiente: {p.Saldo_Pendiente}");
    Console.WriteLine($"Tasa de Interés: {p.Tasa_De_Interes}");
    Console.WriteLine($"Fecha Vencimiento: {p.FechaVencimiento}");
    Console.WriteLine("------------------------------------------------------------");
}

// 🟩 Edición parcial de préstamo
Console.WriteLine("=========== ✏️ CAMBIO PARCIAL DE PRÉSTAMO ===========");
var prestamoEditado = new PrestamoModel
{
    Saldo_Pendiente = 200000,
};
Editare.EditarPrestamo("PREST123", prestamoEditado);



















var pagoPrestamoA = new PagoPrestamoModel
{
    CuentaEmisora = "C123",
    IdPrestamo = "T001",
    Nombre = "Carlos",
    Apellido1 = "Perez",
    Apellido2 = "Gomez",
    Fecha = "2025-04-11",
    Monto = 200,
    ID = "P001",
    Moneda = "CRC"
};

// 🟩 Escritura de pago
Console.WriteLine("=========== 📝 PRUEBA DE ESCRITURA DE PAGOPrestamo ===========");
Escritura.GuardarPagoPrestamo(pagoPrestamoA);

// 🟩 Lectura de pagos
Console.WriteLine("=========== 🔍 LECTURA DE PAGOSPrestamo (TODOS) ===========");
var pagosPrestamoA = Lectura.LeerPagosPrestamo();

foreach (var pagoPrestamo in pagosPrestamoA)
{
    Console.WriteLine($"ID: {pagoPrestamo.ID}");
    Console.WriteLine($"Monto: {pagoPrestamo.Monto} {pagoPrestamo.Moneda}");
    Console.WriteLine($"Cuenta Emisora: {pagoPrestamo.CuentaEmisora}");
    Console.WriteLine($"Número de Tarjeta: {pagoPrestamo.IdPrestamo}");
    Console.WriteLine("------------------------------------------------------------");
}

Console.WriteLine("================================================================");




*/




app.Run();
