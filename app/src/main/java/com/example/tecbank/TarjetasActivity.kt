package com.example.tecbank

import android.os.Bundle
import android.view.View
import android.widget.*
import androidx.activity.ComponentActivity

class TarjetasActivity : ComponentActivity() {

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_tarjetas)

        val spinnerTarjetas = findViewById<Spinner>(R.id.spinnerTarjetas)
        val btnRealizarPago = findViewById<Button>(R.id.btnRealizarPago)
        val formularioPago = findViewById<LinearLayout>(R.id.formularioPago)
        val btnConfirmarPago = findViewById<Button>(R.id.btnConfirmarPago)
        val editMonto = findViewById<EditText>(R.id.editMonto)

        // Tarjetas de ejemplo
        val tarjetas = listOf("**** 1234", "**** 5678", "**** 9876")

        val adapter = ArrayAdapter(this, android.R.layout.simple_spinner_item, tarjetas)
        adapter.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item)
        spinnerTarjetas.adapter = adapter

        btnRealizarPago.setOnClickListener {
            // Mostrar u ocultar el formulario
            formularioPago.visibility = if (formularioPago.visibility == View.GONE) View.VISIBLE else View.GONE
        }

        btnConfirmarPago.setOnClickListener {
            val tarjetaSeleccionada = spinnerTarjetas.selectedItem.toString()
            val monto = editMonto.text.toString()

            if (monto.isNotEmpty()) {
                Toast.makeText(this, "Pago de $monto a $tarjetaSeleccionada", Toast.LENGTH_SHORT).show()
                formularioPago.visibility = View.GONE
                editMonto.text.clear()
            } else {
                Toast.makeText(this, "Ingrese un monto válido", Toast.LENGTH_SHORT).show()
            }
        }
    }
}