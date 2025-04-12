import React, { useEffect, useState } from 'react';
import styles from './ClientePg.module.css';
import axios from "axios";

function ClientePG() {
  
  const [cuenta, setCuenta] = useState(null);
  const [usuario, setUsuario] = useState(null);

    const [montoT, setmontoT] = useState('');
    const [cuentaT, setnombreT] = useState('');

    const [montoPago, setmontoPago] = useState('');
    const [tarjetaPago, settarjetaPago] = useState('');

  useEffect(() => {
      // Cargar cuenta actual desde localStorage
      const cuentaGuardada = localStorage.getItem("cuenta_actual");
      if (cuentaGuardada) {
          setCuenta(JSON.parse(cuentaGuardada));
      }

      const usuarioGuardado = localStorage.getItem("usuario_actual");
      if (usuarioGuardado) {
        setUsuario(JSON.parse(usuarioGuardado));
    }

  }, []);

  if (!cuenta) {
    return <div>Cargando información...</div>; // Mostrar mensaje de carga
  }

  if (!usuario) {
    return <div>Cargando información...</div>; // Mostrar mensaje de carga
  }

  const handleSubmitTransferencia = async (e) => {
    e.preventDefault();
  
    // Prepare the data to send to the backend 
    const transData = {

      Nombre: usuario.nombre,
      Apellido1: usuario.apellido1,
      Apellido2: usuario.apellido2,
      Monto: parseFloat(montoT),
      Moneda: "Colones",
      Cuenta_Emisora: cuenta.numeroDeCuenta,
      Cuenta_Receptora: cuentaT

    };
  
    try {
      // Send data to backend using Axios 
      const response = await axios.post('https://localhost:7190/Movimiento/Transferencia', transData);
        console.log('Transferencia realizada con éxito:', response.data);
        alert("Transferencia realizada con éxito");
    } catch (error) {
      console.error('Error al realizar la transferencia:', error);
      alert("No se pudo realizar la transferencia:");
    }
  };

  const handleSubmitPago = async (e) => {
    e.preventDefault();
  
    // Prepare the data to send to the backend 
    const pagoData = {

      Nombre: usuario.nombre,
      NumeroDeCuenta: cuenta.numeroDeCuenta,
      Apellido1: usuario.apellido1,
      Apellido2: usuario.apellido2,
      Monto: parseFloat(montoPago),
      Moneda: "Colones",
      Numero_de_Tarjeta: tarjetaPago,

    };
  
    console.log('Pago realizado con éxito:', pagoData);

    try {
      // Send data to backend using Axios 
      const response = await axios.post('https://localhost:7190/Movimiento/Pago', pagoData);
        console.log('Pago realizado con éxito:', response.data);
        alert("Pago realizado con éxito");
    } catch (error) {
      console.error('Error al pagar la tarjeta:', error);
      alert("No se pudo pagar la tarjeta");
    }
  };

  return (
    <div className="cliente">
    <div className={styles.body}>
      <div className={styles.container}>
      <h1>TecBank</h1>
      <br />

      <div className="datos-en-linea">
      <p><strong>Usuario:</strong> {cuenta.usuario}</p>
      <p><strong>Número de Cuenta:</strong> {cuenta.numeroDeCuenta}</p>
      </div>

      <br />
      <hr />
      <br />

      <h2>Cuentas</h2>
      <br />
      <h3>Mis movimientos</h3>
      <br />
      <div className="col-md-4 d-flex align-items-end">
          <button type="submit" className="btn btn-primary">Ver mis movimientos</button>
        </div>
        <br />

      <h3>Transferencias</h3>
      <br />
      <form onSubmit={handleSubmitTransferencia}>
      <label> Número de cuenta: </label>
      <input 
      type="number" 
      name="cuentaT" 
      className="form-control" 
      value={cuentaT} 
      onChange={(e) => setnombreT(e.target.value)} 
    />
      <br />
      
      <label> Monto a transferir (colones): </label>
      <input 
      type="number" 
      name="montoT" 
      className="form-control" 
      value={montoT} 
      onChange={(e) => setmontoT(e.target.value)} 
    />
      <br />
      
      <div className="col-md-4 d-flex align-items-end">
      <button 
          type="button" 
          className="btn btn-primary" 
          onClick={(e) => {
            e.preventDefault();  // Asegúrate de que el evento sea pasado correctamente
            handleSubmitTransferencia(e);  // Pasa el evento correctamente
          }}
        >
          Transferir
          </button>
      </div>
    </form>
      <br />
      <hr />
      <br />

      <h2>Tarjetas</h2>
      <br />

      <h3>Pago de tarjetas de crédito</h3>

      <br />
      <form onSubmit={handleSubmitPago}>
        
      <label> Número de tarjeta a pagar: </label>
      <input 
      type="number" 
      name="numeroCuentaPago" 
      className="form-control" 
      value={tarjetaPago} 
      onChange={(e) => settarjetaPago(e.target.value)} 
    />

      <br />
      <label> Monto a pagar (colones): </label>
      <input 
      type="number" 
      name="montoPago" 
      className="form-control" 
      value={montoPago} 
      onChange={(e) => setmontoPago(e.target.value)} 
    />

      <br />
      <div className="col-md-4 d-flex align-items-end">
      <button 
          type="button" 
          className="btn btn-primary" 
          onClick={(e) => {
            e.preventDefault();  // Asegúrate de que el evento sea pasado correctamente
            handleSubmitPago(e);  // Pasa el evento correctamente
          }}
        >
          Pagar
          </button>
      </div>
    </form>
      <br />

      <h3>Listado de compras</h3>
      <br />
      <form className="row g-3">
        <div className="col-md-4">
          <label htmlFor="fechaInicio" className="form-label">Fecha de inicio:</label>
          <input type="date" id="fechaInicio" className="form-control" />
        </div>
        <br />
        <div className="col-md-4">
          <label htmlFor="fechaFin" className="form-label">Fecha de fin:</label>
          <input type="date" id="fechaFin" className="form-control" />
        </div>
        <br />
        <div className="col-md-4 d-flex align-items-end">
          <button type="submit" className="btn btn-primary">Buscar</button>
        </div>
      </form>
      <br />
      <hr />
      <br />

      <h2>Préstamos</h2>
      <br />

      <h3>Pagos normales</h3>
      <br />

      <h3>Pagos extraordinarios</h3>
    </div>
    </div>
    </div>
  );
}

export default ClientePG;
