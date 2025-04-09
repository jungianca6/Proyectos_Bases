package com.example.tecbank

import android.os.Bundle
import android.widget.Button
import android.widget.LinearLayout
import android.widget.TextView
import android.widget.Toast
import androidx.activity.ComponentActivity
import org.json.JSONArray
import java.io.InputStream
import java.io.InputStreamReader

class CuentasActivity : ComponentActivity() {

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_cuentas)

        val layoutCuentas: LinearLayout = findViewById(R.id.layout_cuentas)
        val textoView: TextView = findViewById(R.id.texto_cuenta)

        // Leer el archivo JSON de cuentas
        val cuentas = cargarCuentas()

        if (cuentas != null) {
            // Iterar sobre las cuentas (JSONArray) utilizando un bucle for clásico
            for (i in 0 until cuentas.length()) {
                val cuenta = cuentas.getJSONObject(i)

                // Crear un botón por cada cuenta en el archivo JSON
                val cuentaButton = Button(this) // Asegúrate de pasar 'this' como contexto
                cuentaButton.text = cuenta.getString("nombre")
                cuentaButton.setPadding(16, 16, 16, 16)
                cuentaButton.setOnClickListener {
                    // Mostrar el número de cuenta al hacer clic
                    val textoCuenta = "Has seleccionado la cuenta: ${cuenta.getString("numero_cuenta")}"
                    textoView.text = textoCuenta
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
}