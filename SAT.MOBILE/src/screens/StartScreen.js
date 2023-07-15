import React from 'react'
import Background from '../components/Background'
import Logo from '../components/Logo'
import Button from '../components/Button'
import Paragraph from '../components/Paragraph'
import { Title } from 'react-native-paper'

export default function StartScreen({ navigation }) {
  return (
    <Background>
      <Logo />

      <Title>
        SAT Mobile
      </Title>

      <Paragraph>
        O jeito mais simples de interagir com o Departamento de Suporte e Serviços.
      </Paragraph>

      <Button
        mode="contained"
        onPress={() => navigation.navigate('LoginScreen')}
      >
        Login
      </Button>
      <Button
        mode="outlined"
        onPress={() => navigation.navigate('RegisterScreen')}
      >
        Criar Conta
      </Button>
    </Background>
  )
}
