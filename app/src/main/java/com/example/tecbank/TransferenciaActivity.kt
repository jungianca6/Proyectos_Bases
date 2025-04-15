package com.example.tecbank

import android.os.Bundle
import android.widget.*
import androidx.activity.ComponentActivity
import org.json.JSONArray
import org.json.JSONObject
import java.io.InputStream
import java.io.IOException
import okhttp3.*
import okhttp3.MediaType.Companion.toMediaTypeOrNull



class TransferenciaActivity : ComponentActivity() {

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_transferencia)

        val nombre = intent.getStringExtra("nombre") ?: ""
        val apellido1 = intent.getStringExtra("apellido1") ?: ""
        val apellido2 = intent.getStringExtra("apellido2") ?: ""
        val cuentaEmisora = intent.getStringExtra("numeroCuenta") ?: ""

        val editTextCuentaDestino: EditText = findViewById(R.id.editTextCuentaDestino)
        val editTextCantidad: EditText = findViewById(R.id.editTextCantidad)
        val btnTransferir: Button = findViewById(R.id.btnTransferir)

        btnTransferir.setOnClickListener {
            val cuentaDestino = editTextCuentaDestino.text.toString().trim()
            val cantidadStr = editTextCantidad.text.toString().trim()

            if (cuentaDestino.isEmpty() || cantidadStr.isEmpty()) {
                Toast.makeText(this, "Debes llenar todos los campos", Toast.LENGTH_SHORT).show()
                return@setOnClickListener
            }

            val cantidad = cantidadStr.toDoubleOrNull()
            if (cantidad == null || cantidad <= 0) {
                Toast.makeText(this, "Cantidad inválida", Toast.LENGTH_SHORT).show()
                return@setOnClickListener
            }

            realizarTransferencia(cuentaEmisora, cuentaDestino, cantidad,nombre,apellido1,apellido2)
        }
    }

    private fun realizarTransferencia(cuentaEmisora: String, cuentaDestino: String, monto: Double,
                                      nombre: String,apellido1: String,apellido2: String) {
        val json = JSONObject().apply {
            put("nombre", nombre)         // Puedes adaptar para que se envíe el nombre real del usuario
            put("apellido1", apellido1)
            put("apellido2", apellido2)
            put("monto", monto)
            put("moneda", "Colones")           // Puedes hacerlo dinámico si lo necesitas
            put("cuenta_Emisora", cuentaEmisora)
            put("cuenta_Receptora", cuentaDestino)
        }

        val requestBody = RequestBody.create(
            "application/json; charset=utf-8".toMediaTypeOrNull(),
            json.toString()
        )

        val request = Request.Builder()
            .url("https://b4b6-201-204-89-80.ngrok-free.app/Movimiento/Transferencia") // Asegúrate que este sea el endpoint correcto
            .post(requestBody)
            .build()

        OkHttpClient().newCall(request).enqueue(object : Callback {
            override fun onFailure(call: Call, e: IOException) {
                runOnUiThread {
                    Toast.makeText(
                        this@TransferenciaActivity,
                        "Error al realizar la transferencia",
                        Toast.LENGTH_SHORT
                    ).show()
                }
            }

            override fun onResponse(call: Call, response: Response) {
                response.use {
                    val responseBody = response.body?.string()
                    val jsonResponse = JSONObject(responseBody ?: "{}")
                    val mensaje = jsonResponse.optString("message", "Respuesta desconocida")

                    runOnUiThread {
                        Toast.makeText(this@TransferenciaActivity, mensaje, Toast.LENGTH_LONG).show()
                    }
                }
            }
        })
    }
}