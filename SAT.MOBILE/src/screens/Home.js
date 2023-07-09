import React, { useState, useEffect } from 'react';
import AsyncStorage from '@react-native-async-storage/async-storage';
import Background from '../components/Background'
import Logo from '../components/Logo'
import Header from '../components/Header'
import Paragraph from '../components/Paragraph'
import Button from '../components/Button'

export default function Home({ navigation }) {
  const [usuario, setUsuario] = useState("");

  const onScreenLoad = async () => {
    const usuario = JSON.parse(await AsyncStorage.getItem('usuario'));

    setUsuario(usuario);
  }

  useEffect(() => {
    onScreenLoad();
  }, [])

  return (
    <Background>
      <Logo />

      <Header>Olá</Header>

      <Paragraph>
        Bem-vindo de volta {usuario?.nomeUsuario}.
      </Paragraph>

      <Button
        mode="outlined"
        onPress={() =>
          navigation.reset({
            index: 0,
            routes: [{ name: 'LoginScreen' }],
          })
        }
      >
        Sair
      </Button>
    </Background>
  )
}
