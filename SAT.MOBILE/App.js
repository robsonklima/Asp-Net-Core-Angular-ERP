import { StatusBar } from "expo-status-bar";
import React, { useState } from "react";
import globals from "./models/globals";
import {
  StyleSheet,
  Text,
  View,
  Image,
  TextInput,
  TouchableOpacity,
} from "react-native";
export default function App() {
  const [codUsuario, setCodUsuario] = useState("");
  const [senha, setSenha] = useState("");

  const loginAsync = async () => {
    try {
      const response = await fetch(`${globals.BASE_URL}/Usuario/Login`, {
        method: 'POST',
        headers: {
          Accept: 'application/json',
          'Content-Type': 'application/json',
        },
        body: JSON.stringify({
          codUsuario: 'dt',
          senha: 'Perto@2023',
        }),
      });

      const json = await response.json();

      return json.usuario;
    } catch (error) {
      throw (error);
    }
  };

  return (
    <View style={styles.container}>
      <Image style={styles.image} source={require("./assets/logo.png")} />
      <StatusBar style="auto" />
      <View style={styles.inputView}>
        <TextInput
          style={styles.TextInput}
          placeholder="Usuário"
          placeholderTextColor="#003f5c"
          onChangeText={(codUsuario) => setCodUsuario(codUsuario)}
        />
      </View>
      <View style={styles.inputView}>
        <TextInput
          style={styles.TextInput}
          placeholder="Senha"
          placeholderTextColor="#003f5c"
          secureTextEntry={true}
          onChangeText={(senha) => setSenha(senha)}
        />
      </View>

      <TouchableOpacity>
        <Text style={styles.forgot_button}>Esquecí a senha</Text>
      </TouchableOpacity>

      <TouchableOpacity style={styles.loginBtn} onPress={async () => {
        const usuario = await loginAsync();
        console.log(usuario);
      }}>
        <Text style={styles.loginText}>Entrar</Text>
      </TouchableOpacity>
    </View>
  );
}
const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: "#fff",
    alignItems: "center",
    justifyContent: "center",
  },

  image: {
    marginBottom: 40,
  },

  inputView: {
    backgroundColor: "#BBDEFB",
    borderRadius: 10,
    width: "70%",
    height: 45,
    marginBottom: 20,
    alignItems: "center",
  },

  TextInput: {
    height: 50,
    flex: 1,
    padding: 10,
    marginLeft: 20,
  },

  forgot_button: {
    height: 30,
    marginBottom: 30,
  },

  loginBtn: {
    width: "80%",
    borderRadius: 12,
    height: 50,
    alignItems: "center",
    justifyContent: "center",
    marginTop: 40,
    backgroundColor: "#2196F3",
  },
});