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

  const [seccionActual, setSeccionActual] = useState(0);

  useEffect(() => {
    const cuentaGuardada = localStorage.getItem("cuenta_actual");
    if (cuentaGuardada) setCuenta(JSON.parse(cuentaGuardada));

    const usuarioGuardado = localStorage.getItem("usuario_actual");
    if (usuarioGuardado) setUsuario(JSON.parse(usuarioGuardado));
  }, []);

  if (!cuenta || !usuario) {
    return <div>Cargando información...</div>;
  }

  const handleSubmitTransferencia = async (e) => {
    e.preventDefault();
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
      const response = await axios.post('https://localhost:7190/Movimiento/Transferencia', transData);
      alert("Transferencia realizada con éxito");
    } catch (error) {
      alert("No se pudo realizar la transferencia");
    }
  };

  const handleSubmitPago = async (e) => {
    e.preventDefault();
    const pagoData = {
      Nombre: usuario.nombre,
      NumeroDeCuenta: cuenta.numeroDeCuenta,
      Apellido1: usuario.apellido1,
      Apellido2: usuario.apellido2,
      Monto: parseFloat(montoPago),
      Moneda: "Colones",
      Numero_de_Tarjeta: tarjetaPago,
    };
    try {
      const response = await axios.post('https://localhost:7190/Movimiento/Pago', pagoData);
      alert("Pago realizado con éxito");
    } catch (error) {
      alert("No se pudo pagar la tarjeta");
    }
  };

  const secciones = [
    {
      titulo: "Cuentas",
      contenido: (
        <div className="p-3">
          <h3 className="mb-3">Mis movimientos</h3>
          <button className="btn btn-primary mb-4">Ver mis movimientos</button>
  
          <h3 className="mb-3">Transferencias</h3>
          <form onSubmit={handleSubmitTransferencia} className="mb-5">
            <label className={styles.labelwhite}>Número de cuenta:</label>
            <input
              type="number"
              value={cuentaT}
              onChange={(e) => setnombreT(e.target.value)}
              className="form-control mb-3"
            />
  
            <label className={styles.labelwhite}>Monto a transferir (colones):</label>
            <input
              type="number"
              value={montoT}
              onChange={(e) => setmontoT(e.target.value)}
              className="form-control mb-3"
            />
  
            <button type="submit" className="btn btn-primary">Transferir</button>
          </form>
        </div>
      )
    },
    {
      titulo: "Tarjetas",
      contenido: (
        <div className="p-3">
          <h3 className="mb-3">Pago de tarjetas de crédito</h3>
          <form onSubmit={handleSubmitPago} className="mb-5">
            <label className={styles.labelwhite}>Número de tarjeta a pagar:</label>
            <input
              type="number"
              value={tarjetaPago}
              onChange={(e) => settarjetaPago(e.target.value)}
              className="form-control mb-3"
            />
  
            <label className={styles.labelwhite}>Monto a pagar (colones):</label>
            <input
              type="number"
              value={montoPago}
              onChange={(e) => setmontoPago(e.target.value)}
              className="form-control mb-3"
            />
  
            <button type="submit" className="btn btn-primary">Pagar</button>
          </form>
  
          <h3 className="mb-3">Listado de compras</h3>
          <form className="row g-3 mb-5">
            <div className="col-md-4">
              <label className={styles.labelwhite}>Fecha de inicio:</label>
              <input type="date" className="form-control" />
            </div>
            <div className="col-md-4">
              <label className={styles.labelwhite}>Fecha de fin:</label>
              <input type="date" className="form-control" />
            </div>
            <div className="col-md-4 d-flex align-items-end">
              <button type="submit" className="btn btn-primary">Buscar</button>
            </div>
          </form>
        </div>
      )
    },
    {
      titulo: "Préstamos",
      contenido: (
        <div className="p-3">
          <h3 className="mb-4">Pagos normales</h3>
          <h3 className="mb-5">Pagos extraordinarios</h3>
        </div>
      )
    }
  ];
  
  return (
  
    <div className="cliente">
      <div className={styles.body}>
        <div className={styles.container + " mt-5"}>
          <h1 className="mb-4">TecBank</h1> <br />
          <p className={`${styles.labelwhite}`}><strong>Usuario:</strong> {cuenta.usuario}</p>
          <p className={`mb-4 ${styles.labelwhite}`}><strong>Número de Cuenta:</strong> {cuenta.numeroDeCuenta}</p> <br />
  
          {/* Índice de secciones */}
          <div className="indice mb-4">
            {secciones.map((sec, i) => (
              <button
                key={i}
                onClick={() => setSeccionActual(i)}
                className="btn btn-primary mx-2"
              >
                {sec.titulo}
              </button> 
            ))}
          </div>
  
          {/* Navegación por flechas con título */}
          <div className="d-flex justify-content-between align-items-center my-4">
          <button
            className="btn btn-primary"
            onClick={() => setSeccionActual(prev => (prev > 0 ? prev - 1 : secciones.length - 1))}
            style={{ flex: '0 0 auto' }}
          >
            ⬅
          </button>
          <button
            className="btn btn-primary"
            onClick={() => setSeccionActual(prev => (prev + 1) % secciones.length)}
            style={{ flex: '0 0 auto' }}
          >
            ➡
          </button>
        </div>

        <h2 className="text-center mt-3 mb-0">{secciones[seccionActual].titulo}</h2>
  
          {/* Contenido de la sección actual */}
          <div className="seccion-contenido my-4">
            {secciones[seccionActual].contenido}
          </div>
        </div>
      </div>
    </div>
  );
}

export default ClientePG;
