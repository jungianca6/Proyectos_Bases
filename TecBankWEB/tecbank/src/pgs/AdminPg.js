import React from "react";
import styles from './AdminPg.module.css';

function AdminPG() {
  return (
    <div className="admin">
    <div className={styles.body}>
        <div className={styles.container}>
      <h1>TecBank</h1>
      <br />
      <hr />
      <br />
      
      <h2>Roles</h2>
      <br />

      <h3>Ingreso y modificación de roles</h3>
      <br />
      <form>
        <label> Nombre: </label>
        <input type="text" name="nombre" className="form-control" />
      </form>
      <br />
      <form>
        <label> Rol: </label>
        <input type="text" name="rol" className="form-control" />
      </form>
      <br />
      <form>
        <label> Descripción: </label>
        <input type="text" name="rol" className="form-control" />
      </form>
      <br />
        <div className="col-md-4 d-flex align-items-end">
          <button type="submit" className="btn btn-primary">Ingresar</button>
        </div>
      <br />
        <div className="col-md-4 d-flex align-items-end">
          <button type="submit" className="btn btn-primary">Modificar</button>
        </div>
        <br />

        <h3>Eliminación de roles</h3>
        <br />
        <form>
        <label> Nombre: </label>
        <input type="text" name="nombre" className="form-control" />
      </form>
      <br />
        <div className="col-md-4 d-flex align-items-end">
          <button type="submit" className="btn btn-primary">Eliminar</button>
        </div>
      <br />
      <hr />
      <br />

      <h2>Clientes</h2>
      <br />

      <h3>Ingreso y modificación de clientes</h3>
      <br />
      <form>
        <label> Nombre: </label>
        <input type="text" name="nombre" className="form-control" />
      </form>
      <br />
      <form>
        <label> Apellido 1: </label>
        <input type="text" name="nombre" className="form-control" />
      </form>
      <br />
      <form>
        <label> Apellido 2: </label>
        <input type="text" name="nombre" className="form-control" />
      </form>
      <br />
      <form>
        <label> Cédula: </label>
        <input type="number" name="rol" className="form-control" />
      </form>
      <br />
      <form>
        <label> Dirección: </label>
        <input type="text" name="rol" className="form-control" />
      </form>
      <br />
      <form>
    <label> Teléfono: </label>
    <input 
        type="tel" 
        name="telefono" 
        className="form-control" 
        pattern="[0-9]{8}" 
        title="Ingrese un número de 10 dígitos" 
        required 
        />
        </form>
      <br />
      <form>
        <label> Ingreso mensual (colones): </label>
        <input type="number" name="rol" className="form-control" />
      </form>
      <br />
      <form>
        <label> Tipo de cliente: </label>
            <select name="rol" className="form-control">
            <option value="">Seleccione...</option>
            <option value="Físico">Físico</option>
            <option value="Jurídico">Jurídico</option>
        </select>
        </form>
        <br />
      <form>
        <label> Usuario: </label>
        <input type="text" name="rol" className="form-control" />
      </form>
      <br />
      <form>
        <label> Contraseña: </label>
        <input type="text" name="rol" className="form-control" />
      </form>
      <br />
        <div className="col-md-4 d-flex align-items-end">
          <button type="submit" className="btn btn-primary">Ingresar</button>
        </div>
        <br />
        <div className="col-md-4 d-flex align-items-end">
          <button type="submit" className="btn btn-primary">Modificar</button>
        </div>
        <br />

        <h3>Eliminación de clientes</h3>
        <br />
        <form>
        <label> Cédula: </label>
        <input type="number" name="nombre" className="form-control" />
      </form>
      <br />
        <div className="col-md-4 d-flex align-items-end">
          <button type="submit" className="btn btn-primary">Eliminar</button>
        </div>
      <br />
      <hr />
      <br />

      <h2>Cuentas</h2>
      <br />

      <h3>Ingreso y modificación de cuentas</h3>
      <br />
      <form>
        <label> Número de cuenta: </label>
        <input type="number" name="nombre" className="form-control" />
      </form>
      <br />
      <form>
        <label> Descripción: </label>
        <input type="text" name="nombre" className="form-control" />
      </form>
      <br />
      <form>
        <label> Moneda: </label>
            <select name="moneda" className="form-control">
            <option value="">Seleccione...</option>
            <option value="Colones">Colones</option>
            <option value="Dólares">Dólares</option>
            <option value="Euros">Euros</option>
        </select>
        </form>
        <br />
        <form>
        <label> Tipo de cuenta: </label>
            <select name="tipo_cuenta" className="form-control">
            <option value="">Seleccione...</option>
            <option value="Ahorros">Ahorros</option>
            <option value="Corriente">Corriente</option>
        </select>
        </form>
      <br />
      <form>
        <label> Cliente: </label>
        <input type="text" name="cliente" className="form-control" />
      </form>
      <br />
        <div className="col-md-4 d-flex align-items-end">
          <button type="submit" className="btn btn-primary">Ingresar</button>
        </div>
        <br />
        <div className="col-md-4 d-flex align-items-end">
          <button type="submit" className="btn btn-primary">Modificar</button>
        </div>
        <br />

        <h3>Eliminación de cuentas</h3>
        <br />
        <form>
        <label> Número de cuenta: </label>
        <input type="number" name="nombre" className="form-control" />
      </form>
      <br />
        <div className="col-md-4 d-flex align-items-end">
          <button type="submit" className="btn btn-primary">Eliminar</button>
        </div>
      <br />

      <h3>Depósito/Retiro de efectivo a cuenta</h3>
        <br />
        <form>
        <label> Número de cuenta: </label>
        <input type="number" name="nombre" className="form-control" />
      </form>
      <br />
        <form>
        <label> Monto: </label>
        <input type="number" name="nombre" className="form-control" />
      </form>
      <br />
        <div className="col-md-4 d-flex align-items-end">
          <button type="submit" className="btn btn-primary">Depositar</button>
        </div>
        <br />
        <div className="col-md-4 d-flex align-items-end">
          <button type="submit" className="btn btn-primary">Retirar</button>
        </div>
      <br />
      <hr />
      <br />

      <h2>Tarjetas</h2>
      <br />

      <h3>Ingreso y modificación de tarjetas</h3>
      <br />
      <form>
        <label> Número de tarjeta: </label>
        <input type="number" name="nombre" className="form-control" />
      </form>
      <br />
        <form>
        <label> Tipo de tarjeta: </label>
            <select name="tipo_tarjeta" className="form-control">
            <option value="">Seleccione...</option>
            <option value="Débito">Débito</option>
            <option value="Crédito">Crédito</option>
        </select>
        </form>
        <br />
        <div className="col-md-4">
          <label htmlFor="fechaExp" className="form-label">Fecha de expiración:</label>
          <input type="date" id="fechaExp" className="form-control" />
        </div>
        <br />
        <form>
        <label> Código de seguridad: </label>
        <input type="number" min="0" max="999" name="codigo_seg" className="form-control" />
        </form>
    <br />
        <form>
        <label> Saldo disponible/monto de crédito disponible: </label>
        <input type="number" name="nombre" className="form-control" />
      </form>
      <br />
        <div className="col-md-4 d-flex align-items-end">
          <button type="submit" className="btn btn-primary">Ingresar</button>
        </div>
        <br />
        <div className="col-md-4 d-flex align-items-end">
          <button type="submit" className="btn btn-primary">Modificar</button>
        </div>
        <br />

        <h3>Eliminación de tarjetas</h3>
        <br />
        <form>
        <label> Número de tarjeta: </label>
        <input type="number" name="nombre" className="form-control" />
      </form>
      <br />
        <div className="col-md-4 d-flex align-items-end">
          <button type="submit" className="btn btn-primary">Eliminar</button>
        </div>
      <br />
      <hr />
      <br />

      <h2>Asesores de crédito</h2>
      <br />

      <h3>Ingreso y modificación de asesores</h3>
      <br />
      <form>
        <label> Nombre: </label>
        <input type="text" name="nombre" className="form-control" />
      </form>
      <br />
      <form>
        <label> Apellido 1: </label>
        <input type="text" name="nombre" className="form-control" />
      </form>
      <br />
      <form>
        <label> Apellido 2: </label>
        <input type="text" name="nombre" className="form-control" />
      </form>
        <br />
        <form>
        <label> Cédula: </label>
        <input type="number" name="rol" className="form-control" />
        </form>
    <br />
    <div className="col-md-4">
          <label htmlFor="fechaNac" className="form-label">Fecha de nacimiento:</label>
          <input type="date" id="fechaNac" className="form-control" />
        </div>
      <br />
        <div className="col-md-4 d-flex align-items-end">
          <button type="submit" className="btn btn-primary">Ingresar</button>
        </div>
        <br />
        <div className="col-md-4 d-flex align-items-end">
          <button type="submit" className="btn btn-primary">Modificar</button>
        </div>
        <br />

        <h3>Eliminación de asesores</h3>
        <br />
        <form>
        <label> Cédula: </label>
        <input type="number" name="nombre" className="form-control" />
      </form>
      <br />
        <div className="col-md-4 d-flex align-items-end">
          <button type="submit" className="btn btn-primary">Eliminar</button>
        </div>
        <br />

        <h3>Reporte sobre asesor</h3>
      <br />
      <form>
        <label> Cédula: </label>
        <input type="number" name="nombre" className="form-control" />
      </form>
      <br />
        <div className="col-md-4 d-flex align-items-end">
          <button type="submit" className="btn btn-primary">Generar reporte</button>
        </div>
      <br />
      <hr />
      <br />

      <h2>Préstamos</h2>
      <br />

      <h3>Ingreso de préstamos</h3>
      <br />
      <form>
        <label> Monto original: </label>
        <input type="number" name="nombre" className="form-control" />
      </form>
      <br />
      <form>
        <label> Saldo: </label>
        <input type="number" name="nombre" className="form-control" />
      </form>
      <br />
      <form>
        <label> Número de cliente que solicitó el préstamo: </label>
        <input type="number" name="nombre" className="form-control" />
      </form>
        <br />
        <form>
        <label> Tasa de interés (%): </label>
        <input 
            type="number" 
            name="tasa" 
            className="form-control" 
            min="0" 
            max="100" 
            step="0.01" 
            required 
        />
        </form>
      <br />
        <div className="col-md-4 d-flex align-items-end">
          <button type="submit" className="btn btn-primary">Ingresar</button>
        </div>
        <br />

        <h3>Cálculo de pagos</h3>
        <br />

        <h3>Pagos</h3>
        <br />

        <h3>Pagos extraordinarios</h3>
        <br />
      <hr />
      <br />

      <h2>Gestión de Mora</h2>
      <br />
      
      <h3>Reporte sobre mora</h3>
      <br />
      <form>
        <label> Cédula: </label>
        <input type="number" name="nombre" className="form-control" />
      </form>
      <br />
      <div className="col-md-4 d-flex align-items-end">
          <button type="submit" className="btn btn-primary">Generar reporte</button>
        </div>
      <br />


    </div>
    </div>
    </div>
  );
}

export default AdminPG;