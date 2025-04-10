package com.example.tecbank

import android.content.Intent
import android.os.Bundle
import android.widget.Button
import android.widget.LinearLayout
import android.widget.TextView
import android.widget.Toast
import android.graphics.Color
import androidx.activity.ComponentActivity
import org.json.JSONArray
import java.io.InputStream
import java.io.InputStreamReader


class CuentasActivity : ComponentActivity() {

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)

        setContentView(R.layout.activity_cuentas)
        val layoutMovimientos: LinearLayout = findViewById(R.id.layout_movimientos)
        val layoutCuentas: LinearLayout = findViewById(R.id.layout_cuentas)
        val textoView: TextView = findViewById(R.id.texto_cuenta)
        val transferenciaButton: Button = findViewById(R.id.transferencias_button)

        // Leer el archivo JSON de cuentas
        val cuentas = cargarCuentas()
        transferenciaButton.setOnClickListener{
            val intent = Intent(this, TransferenciaActivity::class.java)
            startActivity(intent)

        }

        if (cuentas != null) {
            // Iterar sobre las cuentas (JSONArray) utilizando un bucle for clásico
            for (i in 0 until cuentas.length()) {
                val cuenta = cuentas.getJSONObject(i)

                // Crear un botón por cada cuenta en el archivo JSON
                val cuentaButton = Button(this)
                val numeroCuenta = cuenta.getString("numero_cuenta")
                val tipo = cuenta.getString("tipo")
                val moneda = cuenta.getString("moneda")
                val saldo = cuenta.getString("saldo")
                cuentaButton.text = "Cuenta $tipo: $numeroCuenta \nSaldo: $saldo $moneda"
                cuentaButton.textSize = 18f

                cuentaButton.setPadding(16, 16, 16, 16)
                val movimientos = cargarMovimientos(numeroCuenta)
                layoutMovimientos.removeAllViews() // Limpiar antes de agregar nuevos

                cuentaButton.setOnClickListener {

                    layoutMovimientos.removeAllViews() // Limpiar antes de agregar nuevos
                    for (i in 0 until movimientos.length()) {
                        val movimiento = movimientos.getJSONObject(i)
                        val movimientoView = TextView(this)
                        val texto = "ID: ${movimiento.getString("id")}\n" +
                                "Fecha: ${movimiento.getString("fecha")}\n" +
                                "Monto: ${movimiento.getDouble("monto")} ${movimiento.getString("moneda")}"
                        movimientoView.text = texto
                        movimientoView.textSize = 18f
                        movimientoView.setTextColor(Color.WHITE)
                        movimientoView.setPadding(16, 16, 16, 16)
                        layoutMovimientos.addView(movimientoView)
                    }
                    textoView.text = "Cuenta seleccionada: $numeroCuenta"
                }

                // Agregar el botón al layout
                layoutCuentas.addView(cuentaButton)
            }
        } else {
            // Si no se pudieron cargar las cuentas, mostrar un mensaje
            Toast.makeText(this, "Error al cargar las cuentas", Toast.LENGTH_SHORT).show()
        }

        // Manejar el botón de salida
        val btnSalir: Button = findViewById(R.id.btnSalir)
        btnSalir.setOnClickListener {
            finish()  // Cierra la actividad actual
        }
    }

    // Función para cargar las cuentas desde el archivo JSON
    private fun cargarCuentas(): JSONArray? {
        return try {
            // Abrir el archivo JSON desde assets
            val inputStream: InputStream = assets.open("cuentas.json")
            val inputStreamReader = InputStreamReader(inputStream)
            val jsonString = inputStreamReader.readText()

            // Convertir el JSON en un array
            JSONArray(jsonString)
        } catch (e: Exception) {
            e.printStackTrace() // Imprimir el error en el Logcat
            null
        }
    }

    private fun cargarMovimientos(numeroCuenta: String): JSONArray {
        val listaFiltrada = JSONArray()

        try {
            val inputStream: InputStream = assets.open("movimientos.json")
            val jsonString = inputStream.bufferedReader().use { it.readText() }
            val todosLosMovimientos = JSONArray(jsonString)

            for (i in 0 until todosLosMovimientos.length()) {
                val mov = todosLosMovimientos.getJSONObject(i)
                if (mov.getString("numero_cuenta") == numeroCuenta) {
                    listaFiltrada.put(mov)
                }
            }
        } catch (e: Exception) {
            e.printStackTrace()
        }

        return listaFiltrada
    }
}
