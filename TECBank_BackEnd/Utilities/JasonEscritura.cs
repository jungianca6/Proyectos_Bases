﻿using TECBank_BackEnd.Models;
using TECBank_BackEnd.Utilities;

namespace TECBank_BackEnd.Pruebas
{
    public class JasonEscritura
    {
        public void GuardarCliente(ClienteModel cliente)
        {
            Jason json = new Jason();
            var clientes = json.LeerClientes();

            if (clientes.Any(c => c.Cedula == cliente.Cedula))
            {
                Console.WriteLine($"⚠️ Ya existe un cliente con la cédula {cliente.Cedula}. No se puede agregar.");
                return;
            }

            clientes.Add(cliente);
            json.GuardarClientes(clientes);
            Console.WriteLine($"✅ Cliente {cliente.Nombre} agregado correctamente.");
        }

        public void GuardarCuenta(CuentaModel cuenta)
        {
            Jason json = new Jason();
            var cuentas = json.LeerCuentas();

            if (cuentas.Any(c => c.NumeroDeCuenta == cuenta.NumeroDeCuenta))
            {
                Console.WriteLine($"⚠️ Ya existe una cuenta con el número {cuenta.NumeroDeCuenta}. No se puede agregar.");
                return;
            }

            cuentas.Add(cuenta);
            json.GuardarCuentas(cuentas);
            Console.WriteLine($"✅ Cuenta {cuenta.Nombre} agregada correctamente.");
        }

        public void GuardarTarjeta(TarjetaModel tarjeta)
        {
            Jason json = new Jason();
            var tarjetas = json.LeerTarjetas();

            if (tarjetas.Any(t => t.Numero == tarjeta.Numero))
            {
                Console.WriteLine($"⚠️ Ya existe una tarjeta con el número {tarjeta.Numero}. No se puede agregar.");
                return;
            }

            tarjetas.Add(tarjeta);
            json.GuardarTarjetas(tarjetas);
            Console.WriteLine($"✅ Tarjeta {tarjeta.Numero} agregada correctamente.");
        }


        public void GuardarEmpleado(EmpleadoModel empleado)
        {
            Jason json = new Jason();
            var empleados = json.LeerEmpleados();

            if (empleados.Any(e => e.Cedula == empleado.Cedula))
            {
                Console.WriteLine($"⚠️ Ya existe un empleado con la cédula {empleado.Cedula}. No se puede agregar.");
                return;
            }

            empleados.Add(empleado);
            json.GuardarEmpleados(empleados);
            Console.WriteLine($"✅ Empleado con cédula {empleado.Cedula} agregado correctamente.");
        }

        public void GuardarPago(PagoModel pago)
        {
            Jason json = new Jason();
            var pagos = json.LeerPagos();

            if (pagos.Any(p => p.ID == pago.ID))
            {
                Console.WriteLine($"⚠️ Ya existe un pago con el ID {pago.ID}. No se puede agregar.");
                return;
            }

            pagos.Add(pago);
            json.GuardarPagos(pagos);
            Console.WriteLine($"✅ Pago con ID {pago.ID} agregado correctamente.");
        }

        public void GuardarRetiro(RetiroModel retiro)
        {
            Jason json = new Jason();
            var retiros = json.LeerRetiros();

            if (retiros.Any(r => r.ID == retiro.ID))
            {
                Console.WriteLine($"⚠️ Ya existe un retiro con el ID {retiro.ID}. No se puede agregar.");
                return;
            }

            retiros.Add(retiro);
            json.GuardarRetiros(retiros);
            Console.WriteLine($"✅ Retiro con ID {retiro.ID} agregado correctamente.");
        }

        public void GuardarTransferencia(TransferenciaModel transferencia)
        {
            Jason json = new Jason();
            var transferencias = json.LeerTransferencias();

            if (transferencias.Any(t => t.ID == transferencia.ID))
            {
                Console.WriteLine($"⚠️ Ya existe una transferencia con el ID {transferencia.ID}. No se puede agregar.");
                return;
            }

            transferencias.Add(transferencia);
            json.GuardarTransferencias(transferencias);
            Console.WriteLine($"✅ Transferencia con ID {transferencia.ID} agregada correctamente.");
        }


        public void GuardarAsesorCredito(AsesorCreditoModel asesor)
        {
            Jason json = new Jason();
            var asesores = json.LeerAsesoresCredito();

            if (asesores.Any(a => a.Cedula == asesor.Cedula))
            {
                Console.WriteLine($"⚠️ Ya existe un asesor de crédito con la cédula {asesor.Cedula}. No se puede agregar.");
                return;
            }

            asesores.Add(asesor);
            json.GuardarAsesoresCredito(asesores);
            Console.WriteLine($"✅ Asesor de crédito {asesor.Cedula} agregado correctamente.");
        }
        public void GuardarPrestamo(PrestamoModel prestamo)
        {
            Jason json = new Jason();
            var prestamos = json.LeerPrestamos();

            if (prestamos.Any(p => p.ID_Prestamos == prestamo.ID_Prestamos))
            {
                Console.WriteLine($"⚠️ Ya existe un préstamo con el ID {prestamo.ID_Prestamos}. No se puede agregar.");
                return;
            }

            prestamos.Add(prestamo);
            json.GuardarPrestamos(prestamos);
            Console.WriteLine($"✅ Préstamo con ID {prestamo.ID_Prestamos} agregado correctamente.");
        }

        public void GuardarPagoPrestamo(PagoPrestamoModel pagoPrestamo)
        {
            Jason json = new Jason();
            var pagosPrestamo = json.LeerPagosPrestamo();

            if (pagosPrestamo.Any(p => p.ID == pagoPrestamo.ID))
            {
                Console.WriteLine($"⚠️ Ya existe un pagoPrestamo con el ID {pagoPrestamo.ID}. No se puede agregar.");
                return;
            }

            pagosPrestamo.Add(pagoPrestamo);
            json.GuardarPagosPrestamo(pagosPrestamo);
            Console.WriteLine($"✅ Pago con ID {pagoPrestamo.ID} agregado correctamente.");
        }




        public void GuardarCalendarioPago(CalendarioPagoModel calendario)
        {
            Jason json = new Jason();
            var calendarios = json.LeerCalendarioPagos();

            if (calendarios.Any(c => c.ID_Prestamo == calendario.ID_Prestamo && c.ID_Prestamo == calendario.ID_Prestamo))
            {
                Console.WriteLine($"⚠️ Ya existe una cuota #{calendario.ID_Prestamo} para el préstamo con ID {calendario.ID_Prestamo}. No se puede agregar.");
                return;
            }

            calendarios.Add(calendario);
            json.GuardarCalendarioPagos(calendarios);
            Console.WriteLine($"✅ Calendario de pago (cuota #{calendario.ID_Prestamo}) agregado correctamente al préstamo con ID {calendario.ID_Prestamo}.");
        }

    }
}
