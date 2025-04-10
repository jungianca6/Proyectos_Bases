package com.example.tecbank

import android.os.Bundle
import android.widget.*
import androidx.activity.ComponentActivity
import org.json.JSONArray
import org.json.JSONObject
import java.io.InputStream

class TransferenciaActivity : ComponentActivity() {

    private var cuentas: MutableList<Cuenta> = mutableListOf()

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_transferencia)

        val spinnerCuentaOrigen: Spinner = findViewById(R.id.spinnerCuentaOrigen)
        val spinnerCuentaDestino: Spinner = findViewById(R.id.spinnerCuentaDestino)
        val editTextCantidad: EditText = findViewById(R.id.editTextCantidad)
        val btnTransferir: Button = findViewById(R.id.btnTransferir)

        // Cargar cuentas desde JSON
        cuentas = cargarCuentas()

        // Adaptador para los Spinners
        val adapter = ArrayAdapter(this, android.R.layout.simple_spinner_item, cuentas.map { it.numeroCuenta })
        adapter.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item)
        spinnerCuentaOrigen.adapter = adapter
        spinnerCuentaDestino.adapter = adapter

        btnTransferir.setOnClickListener {
            val cuentaOrigen = spinnerCuentaOrigen.selectedItem as String
            val cuentaDestino = spinnerCuentaDestino.selectedItem as String
            val cantidadStr = editTextCantidad.text.toString()

            if (cantidadStr.isNotEmpty()) {
                val cantidad = cantidadStr.toDoubleOrNull()
                if (cantidad != null && cantidad > 0) {
                    realizarTransferencia(cuentaOrigen, cuentaDestino, cantidad)
                } else {
                    Toast.makeText(this, "Cantidad inválida", Toast.LENGTH_SHORT).show()
                }
            } else {
                Toast.makeText(this, "Por favor ingresa una cantidad válida", Toast.LENGTH_SHORT).show()
            }
        }
    }

    private fun cargarCuentas(): MutableList<Cuenta> {
        val cuentasList = mutableListOf<Cuenta>()
        val inputStream: InputStream = assets.open("cuentas.json")
        val jsonText = inputStream.bufferedReader().use { it.readText() }

        val jsonArray = JSONArray(jsonText)
        for (i in 0 until jsonArray.length()) {
            val obj: JSONObject = jsonArray.getJSONObject(i)
            val numeroCuenta = obj.getString("numero_cuenta")
            val tipo = obj.getString("tipo")
            val moneda = obj.getString("moneda")
            val saldo = obj.getDouble("saldo")

            cuentasList.add(Cuenta(numeroCuenta, tipo, moneda, saldo))
        }

        return cuentasList
    }

    private fun realizarTransferencia(cuentaOrigen: String, cuentaDestino: String, cantidad: Double) {
        // Verificar que no sea la misma cuenta
        if (cuentaOrigen == cuentaDestino) {
            Toast.makeText(this, "No puedes transferir a la misma cuenta", Toast.LENGTH_SHORT).show()
            return
        }

        val cuentaOrigenObj = cuentas.find { it.numeroCuenta == cuentaOrigen }
        val cuentaDestinoObj = cuentas.find { it.numeroCuenta == cuentaDestino }

        if (cuentaOrigenObj == null || cuentaDestinoObj == null) {
            Toast.makeText(this, "Una o ambas cuentas no existen", Toast.LENGTH_SHORT).show()
            return
        }

        // Verificar saldo suficiente
        if (cuentaOrigenObj.saldo < cantidad) {
            Toast.makeText(this, "Saldo insuficiente en la cuenta origen", Toast.LENGTH_SHORT).show()
            return
        }

        // Realizar la transferencia
        cuentaOrigenObj.saldo -= cantidad
        cuentaDestinoObj.saldo += cantidad

        Toast.makeText(this, "Transferencia realizada con éxito", Toast.LENGTH_SHORT).show()

        // Aquí podrías guardar los cambios si fuera necesario
    }
}