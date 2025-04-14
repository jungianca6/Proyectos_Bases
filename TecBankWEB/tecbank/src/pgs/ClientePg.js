import React, { useEffect, useState } from 'react';
import styles from './ClientePg.module.css';
import axios from "axios";
import jsPDF from 'jspdf';
import autoTable from 'jspdf-autotable';

function ClientePG() {
  const [cuenta, setCuenta] = useState(null);
  const [usuario, setUsuario] = useState(null);

  const [montoT, setmontoT] = useState('');
  const [cuentaT, setnombreT] = useState('');

  const [montoPago, setmontoPago] = useState('');
  const [tarjetaPago, settarjetaPago] = useState('');

  const [fechaIn, setfechaIn] = useState('');
  const [fechaFin, setfechaFin] = useState('');

  const [movimientosTexto, setMovimientosTexto] = useState("");

  const [seccionActual, setSeccionActual] = useState(0);

  const [idPP, setidPP] = useState('');
  const [montoPP, setmontoPP] = useState('');

  useEffect(() => {
    const cuentaGuardada = localStorage.getItem("cuenta_actual");
    if (cuentaGuardada) setCuenta(JSON.parse(cuentaGuardada));

    const usuarioGuardado = localStorage.getItem("usuario_actual");
    if (usuarioGuardado) setUsuario(JSON.parse(usuarioGuardado));
  }, []);

  if (!cuenta || !usuario) {
    return <div>Cargando información...</div>;
  }

  const handleSubmitCompras = async (e) => {
    e.preventDefault();
  
    const comData = {
      fechaInicio: `${fechaIn} 00:00`,
      fechaFinal: `${fechaFin} 00:00`,
      numeroDeCuenta: cuenta.numeroDeCuenta,
    };
  
    try {
      const response = await axios.post('https://localhost:7190/Movimiento/ListadoDeCompras', comData);
      const data = response.data;
  
      const doc = new jsPDF();
  
      doc.setFontSize(22);
      doc.text('TecBank', 14, 20);
  
      const today = new Date();
      const formattedDate = today.toLocaleDateString('es-CR', {
        day: '2-digit',
        month: '2-digit',
        year: 'numeric'
      });
      doc.setFontSize(12);
      doc.text(`Fecha de generación: ${formattedDate}`, 14, 28);
  
      let currentY = 36;
  
      if (data.retiros.length > 0) {
        doc.setFontSize(14);
        doc.text('Retiros', 14, currentY);
        currentY += 5;
  
        autoTable(doc, {
          startY: currentY,
          head: [['ID', 'Nombre', 'Apellidos', 'Cuenta', 'Monto', 'Fecha', 'Moneda']],
          body: data.retiros.map(r => [
            r.id,
            r.nombre,
            `${r.apellido1} ${r.apellido2}`,
            r.cuentaARetirar,
            r.monto,
            r.fecha,
            r.moneda
          ]),
        });
  
        currentY = doc.lastAutoTable.finalY + 10;
      }
  
      if (data.pagos_tarjetas.length > 0) {
        doc.setFontSize(14);
        doc.text('Pagos con Tarjeta', 14, currentY);
        currentY += 5;
  
        autoTable(doc, {
          startY: currentY,
          head: [['ID', 'Nombre', 'Apellidos', 'Cuenta Emisora', 'Tarjeta', 'Monto', 'Fecha', 'Moneda']],
          body: data.pagos_tarjetas.map(p => [
            p.id,
            p.nombre,
            `${p.apellido1} ${p.apellido2}`,
            p.cuenta_Emisora,
            p.numero_de_Tarjeta,
            p.monto,
            p.fecha,
            p.moneda
          ]),
        });
  
        currentY = doc.lastAutoTable.finalY + 10;
      }
  
      if (data.pagos_prestamos.length > 0) {
        doc.setFontSize(14);
        doc.text('Pagos de Préstamos', 14, currentY);
        currentY += 5;
  
        autoTable(doc, {
          startY: currentY,
          head: [['ID', 'Destinatario', 'Cuenta Emisora', 'ID Préstamo', 'Monto', 'Fecha', 'Moneda']],
          body: data.pagos_prestamos.map(p => [
            p.id,
            `${p.nombre} ${p.apellido1} ${p.apellido2}`,
            p.cuentaEmisora,
            p.idPrestamo,
            p.monto,
            p.fecha,
            p.moneda
          ]),
        });
  
        currentY = doc.lastAutoTable.finalY + 10;
      }
  
      if (data.transferencias.length > 0) {
        doc.setFontSize(14);
        doc.text('Transferencias', 14, currentY);
        currentY += 5;
  
        autoTable(doc, {
          startY: currentY,
          head: [['ID', 'Nombre', 'Apellidos', 'Cuenta Emisora', 'Cuenta Receptora', 'Monto', 'Fecha', 'Moneda']],
          body: data.transferencias.map(t => [
            t.id,
            t.nombre,
            `${t.apellido1} ${t.apellido2}`,
            t.cuenta_Emisora,
            t.cuenta_Receptora,
            t.monto,
            t.fecha,
            t.moneda
          ]),
        });
      }
  
      doc.save('ListadoDeCompras.pdf');
      alert("Lista de compras generada con éxito");
    } catch (error) {
      alert("No se pudo realizar la lista de compras");
    }
  };

  const handleMovimientos = async (e) => {
    e.preventDefault();
    const movData = {
      numeroDeCuenta: cuenta.numeroDeCuenta,
    };
    try {
      const response = await axios.post('https://localhost:7190/Movimiento/ListadoDeMovimientos', movData);
      const data = response.data;
      
      console.log("JSON recibido:", response.data);

      let textoFinal = "";
  
      if (data.retiros && data.retiros.length > 0) {
        textoFinal += "📌 Retiros:\n";
        data.retiros.forEach(r => {
          textoFinal += `• ${r.nombre} ${r.apellido1} ${r.apellido2} retiró ${r.monto} ${r.moneda} de la cuenta ${r.cuentaARetirar} el ${r.fecha}\n`;
        });
        textoFinal += "\n";
      }
  
      if (data.pagos_tarjetas && data.pagos_tarjetas.length > 0) {
        textoFinal += "📌 Pagos con Tarjeta:\n";
        data.pagos_tarjetas.forEach(p => {
          textoFinal += `• Se pagaron ${p.monto} ${p.moneda} desde la cuenta ${p.cuenta_Emisora} con la tarjeta ${p.numero_de_Tarjeta} a ${p.nombre} ${p.apellido1} ${p.apellido2} el ${p.fecha}\n`;
        });
        textoFinal += "\n";
      }
  
      if (data.pagos_prestamos && data.pagos_prestamos.length > 0) {
        textoFinal += "📌 Pagos de Préstamos:\n";
        data.pagos_prestamos.forEach(p => {
          textoFinal += `• Se pagaron ${p.monto} ${p.moneda} desde la cuenta ${p.cuentaEmisora} al préstamo ${p.idPrestamo}, beneficiario: ${p.nombre} ${p.apellido1} ${p.apellido2}, el ${p.fecha}\n`;
        });
        textoFinal += "\n";
      }

      if (data.transferencias && data.transferencias.length > 0) {
        textoFinal += "📌 Transferencias:\n";
        data.transferencias.forEach(t => {
          textoFinal += `• Se transfirieron ${t.monto} ${t.moneda} desde la cuenta ${t.cuenta_Emisora} a ${t.nombre} ${t.apellido1} ${t.apellido2} (cuenta ${t.cuenta_Receptora}) el ${t.fecha}\n`;
        });
      }
  
      setMovimientosTexto(textoFinal || "No se encontraron movimientos.");
      alert("Listado de movimientos generado con éxito");
    } catch (error) {
      alert("No se encuentran movimientos");
      setMovimientosTexto("No se encontraron movimientos.");
    }
  };

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
      const response = await axios.post('https://localhost:7190/Movimiento/PagoTarjeta', pagoData);
      alert("Pago realizado con éxito");
    } catch (error) {
      alert("No se pudo pagar la tarjeta");
    }
  };

  const handleSubmitPP = async (e) => {
    e.preventDefault();
  
    // Prepare the data to send to the backend 
    const ppData = {
      Nombre: usuario.nombre,
      Apellido1: usuario.apellido1,
      Apellido2: usuario.apellido2,
      IdPrestamo: idPP,
      Moneda: "Colones",
      Monto: montoPP,
      NumeroDeCuenta: cuenta.numeroDeCuenta
    };
  
    try {
      console.log('Datos enviados (ppData):', ppData);
      const response = await axios.post('https://localhost:7190/Movimiento/PagoPrestamo', ppData);
      console.log('Pago de préstamo realizado con éxito:', response.data);
      alert("El préstamo elegido ha sido pagado con éxito");
    } catch (error) {
      alert("No se pudo realizar el pago", error);
    }
  };

  const secciones = [
    {
      titulo: "Cuentas",
      contenido: (
        <div className="p-3">
        <div className="d-flex align-items-start mb-4" style={{ gap: "15px" }}>
        <label className={styles.labelwhite}>Mis movimientos:</label>
        <textarea
          className="form-control"
          value={movimientosTexto}
          readOnly
          rows={6}
          style={{
            resize: "none",
            marginTop: "10px",
            border: "1px solid #ccc",
            borderRadius: "5px",
            padding: "8px"
          }}
        />
        </div>
          <form onSubmit={handleMovimientos} className="mb-5">
          <button type="submit" className="btn btn-primary mb-4">Ver mis movimientos</button>
          </form>
          <h3 className="mb-3">Transferencias</h3>
          <form onSubmit={handleSubmitTransferencia} className="mb-5">
            <label className={styles.labelwhite}>Número de cuenta a transferir:</label>
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
          <form onSubmit={handleSubmitCompras} className="row g-3 mb-5">
            <div className="col-md-4">
              <label className={styles.labelwhite}>Fecha de inicio:</label>
              <input
                type="date"
                className="form-control"
                onChange={(e) => {
                  const [año, mes, dia] = e.target.value.split("-");
                  setfechaIn(`${dia}/${mes}/${año}`);
                }}
              />
            </div>
            <div className="col-md-4">
              <label className={styles.labelwhite}>Fecha de fin:</label>
              <input
                type="date"
                className="form-control"
                onChange={(e) => {
                  const [año, mes, dia] = e.target.value.split("-");
                  setfechaFin(`${dia}/${mes}/${año}`);
                }}
              />
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
          <h3 className="mb-4">Pago de préstamo</h3>
          <form onSubmit={handleSubmitPP}>
          <div className="mb-3">
            <label className={styles.labelwhite}> ID del préstamo a pagar: </label>
            <input 
              type="number" 
              name="idPP" 
              className="form-control" 
              value={idPP} 
              onChange={(e) => setidPP(e.target.value)} 
            />
          </div>

          <div className="mb-3">
            <label className={styles.labelwhite}> Monto a pagar del préstamo: </label>
            <input 
              type="number" 
              name="montoPP" 
              className="form-control" 
              value={montoPP} 
              onChange={(e) => setmontoPP(e.target.value)} 
            />
          </div>

          <div className="col-md-4 d-flex align-items-end">
            <button 
              type="button" 
              className="btn btn-primary" 
              onClick={(e) => {
                e.preventDefault();
                handleSubmitPP(e);
              }}
            >
              Pagar
            </button>
          </div>
        </form>
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
