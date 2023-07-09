import { TouchableOpacity, StyleSheet, View } from 'react-native'
import AsyncStorage from '@react-native-async-storage/async-storage';
import { Text } from 'react-native-paper'
import { theme } from '../core/theme'
import { passwordValidator } from '../helpers/passwordValidator'
import React, { useState, useEffect } from 'react'
import Background from '../components/Background'
import Logo from '../components/Logo'
import Header from '../components/Header'
import Button from '../components/Button'
import TextInput from '../components/TextInput'
const globals = require('../models/Globals');

export default function LoginScreen({ navigation }) {
  const [codUsuario, setCodUsuario] = useState({ value: '', error: '' })
  const [senha, setSenha] = useState({ value: '', error: '' })

  const onScreenLoad = async () => {
    const usuario = JSON.parse(await AsyncStorage.getItem('usuario'));

    if (usuario) {
      navigation.reset({
        index: 0,
        routes: [{ name: 'MeusChamados' }],
      });
    }
  }

  useEffect(() => {
    onScreenLoad();
  }, [])

  const onLoginPressed = async () => {
    const passwordError = passwordValidator(senha.value)

    if (passwordError) {
      setSenha({ ...senha, error: passwordError })
      return
    }

    const response = await fetch(`${globals.BASE_URL}/Usuario/Login`, {
      method: 'POST',
      headers: {
        Accept: 'application/json',
        'Content-Type': 'application/json',
      },
      body: JSON.stringify({
        codUsuario: codUsuario.value,
        senha: senha.value,
      }),
    });

    const json = await response.json();

    if (json?.usuario?.codUsuario) {
      navigation.reset({
        index: 0,
        routes: [{ name: 'MeusChamados' }],
      });

      const usuario = JSON.stringify(json.usuario);
      await AsyncStorage.setItem('usuario', usuario);
      await AsyncStorage.setItem('token', json.token);

      return json.usuario;
    }

    alert('Usuário ou senha inválidos');
  }

  return (
    <Background>
      <Logo />

      <Header>Bem-vindo de volta ao SAT</Header>

      <TextInput
        label="Usuário"
        returnKeyType="next"
        value={codUsuario.value}
        onChangeText={(text) => setCodUsuario({ value: text, error: '' })}
        autoCapitalize="none"
      />

      <TextInput
        label="Senha"
        returnKeyType="done"
        value={senha.value}
        onChangeText={(text) => setSenha({ value: text, error: '' })}
        error={!!senha.error}
        errorText={senha.error}
        secureTextEntry
      />

      <View style={styles.forgotPassword}>
        <TouchableOpacity onPress={() => navigation.navigate('ResetPasswordScreen')}>
          <Text style={styles.forgot}>Esqueci minha senha</Text>
        </TouchableOpacity>
      </View>

      <Button mode="contained" onPress={onLoginPressed}>
        Login
      </Button>

      <View style={styles.row}>
        <Text>Ainda não tem conta? </Text>
        <TouchableOpacity onPress={() => navigation.replace('RegisterScreen')}>
          <Text style={styles.link}>Criar Conta</Text>
        </TouchableOpacity>
      </View>
    </Background>
  )
}

const styles = StyleSheet.create({
  forgotPassword: {
    width: '100%',
    alignItems: 'flex-end',
    marginBottom: 24,
  },
  row: {
    flexDirection: 'row',
    marginTop: 4,
  },
  forgot: {
    fontSize: 13,
    color: theme.colors.secondary,
  },
  link: {
    fontWeight: 'bold',
    color: theme.colors.primary,
  },
})
