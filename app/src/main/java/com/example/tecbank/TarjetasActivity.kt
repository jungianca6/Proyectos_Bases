package com.example.tecbank

import android.os.Bundle
import android.view.View
import android.widget.*
import androidx.activity.ComponentActivity
import org.json.JSONArray
import java.io.IOException
import okhttp3.*
import okhttp3.MediaType.Companion.toMediaTypeOrNull
import org.json.JSONObject

class TarjetasActivity : ComponentActivity() {

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_tarjetas)


        val btnRealizarPago = findViewById<Button>(R.id.btnRealizarPago)
        val editMonto = findViewById<EditText>(R.id.editMonto)
        val editNumeroTarjeta = findViewById<EditText>(R.id.editNumeroTarjeta)

        val nombre = intent.getStringExtra("nombre") ?: ""
        val apellido1 = intent.getStringExtra("apellido1") ?: ""
        val apellido2 = intent.getStringExtra("apellido2") ?: ""
        val cuentaTarjeta = intent.getStringExtra("numeroCuenta") ?: ""



        btnRealizarPago.setOnClickListener {
            val numeroTarjeta = editNumeroTarjeta.text.toString().trim()
            val montoStr = editMonto.text.toString().trim()

            if (numeroTarjeta.isEmpty() || montoStr.isEmpty()) {
                Toast.makeText(this, "Debe ingresar número de tarjeta y monto", Toast.LENGTH_SHORT).show()
                return@setOnClickListener
            }

            val monto = montoStr.toDoubleOrNull()
            if (monto == null || monto <= 0) {
                Toast.makeText(this, "Monto inválido", Toast.LENGTH_SHORT).show()
                return@setOnClickListener
            }

            realizarPagoTarjeta(nombre, apellido1, apellido2, cuentaTarjeta, numeroTarjeta, monto)
        }
    }
    private fun realizarPagoTarjeta(
        nombre: String,
        apellido1: String,
        apellido2: String,
        numeroCuenta: String,
        numeroTarjeta: String,
        monto: Double
    ) {
        val json = JSONObject().apply {
            put("nombre", nombre)
            put("apellido1", apellido1)
            put("apellido2", apellido2)
            put("numeroDeCuenta", numeroCuenta)
            put("numero_de_Tarjeta", numeroTarjeta)
            put("monto", monto)
            put("moneda", "Colones")
        }

        val requestBody = RequestBody.create(
            "application/json; charset=utf-8".toMediaTypeOrNull(),
            json.toString()
        )

        val request = Request.Builder()
            .url("https://b4b6-201-204-89-80.ngrok-free.app/Movimiento/PagoTarjeta") // Actualiza si cambia
            .post(requestBody)
            .build()

        OkHttpClient().newCall(request).enqueue(object : Callback {
            override fun onFailure(call: Call, e: IOException) {
                runOnUiThread {
                    Toast.makeText(this@TarjetasActivity, "Error en conexión", Toast.LENGTH_SHORT).show()
                }
            }

            override fun onResponse(call: Call, response: Response) {
                val responseBody = response.body?.string()
                val jsonResponse = JSONObject(responseBody ?: "{}")
                val mensaje = jsonResponse.optString("message", "Respuesta desconocida")

                runOnUiThread {
                    Toast.makeText(this@TarjetasActivity, mensaje, Toast.LENGTH_LONG).show()
                }
            }
        })
    }
}
