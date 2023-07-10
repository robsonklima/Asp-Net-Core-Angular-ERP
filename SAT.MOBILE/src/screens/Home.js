import React, { useState, useEffect } from 'react';
import AsyncStorage from '@react-native-async-storage/async-storage';
import Background from '../components/Background'
import Header from '../components/Header'
import Paragraph from '../components/Paragraph'

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
      <Header>Olá</Header>

      <Paragraph>
        Bem-vindo de volta {usuario?.nomeUsuario}.
      </Paragraph>
    </Background>
  )
}
