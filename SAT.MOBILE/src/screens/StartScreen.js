import React from 'react'
import Background from '../components/Background'
import Logo from '../components/Logo'
import Button from '../components/Button'
import Paragraph from '../components/Paragraph'

export default function StartScreen({ navigation }) {
  return (
    <Background>
      <Logo />
      <Paragraph>
        O jeito mais simples de interagir com a assistência técnica.
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
